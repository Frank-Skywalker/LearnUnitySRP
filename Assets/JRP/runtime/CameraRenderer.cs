using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    Camera camera;
    ScriptableRenderContext context;
    CullingResults cullingResults;

    const string bufferName = "xxx command buffer";

    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
        
    private CommandBuffer commandBuffer = new CommandBuffer()
    {
        name = bufferName
    };
    
    public void Render(ScriptableRenderContext contex, Camera camera)
    {
        this.context = contex;
        this.camera = camera;
        
        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull())
            return;
            
        Setup();
        DrawVisibleGeometry();
        DrawUnsupportedShaders();
        DrawGizmos();
        Submit();
    }

    bool Cull()
    {
        if (camera.TryGetCullingParameters(out var parameters))
        {
            cullingResults = context.Cull(ref parameters);
            return true;
        }

        return false;
    }

    void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
    }

    void Setup()
    {
        context.SetupCameraProperties(camera);
        var clearFlags = camera.clearFlags;
        commandBuffer.ClearRenderTarget(
            clearFlags <= CameraClearFlags.Depth,
            clearFlags <= CameraClearFlags.Color,
            clearFlags <= CameraClearFlags.Color ?
                camera.backgroundColor.linear : Color.clear
        );
        commandBuffer.BeginSample(sampleName);
        ExecuteBuffer();
    }
    void DrawVisibleGeometry()
    {   
        // Draw opaque objects
        var sortingSettings = new SortingSettings(camera)
        {
            criteria = SortingCriteria.CommonOpaque
        };
        var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
        
        // Draw skybox
        context.DrawSkybox(camera);
        
        // Draw transparent objects
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
    }
    
    void Submit()
    {
        commandBuffer.EndSample(sampleName);
        ExecuteBuffer();
        context.Submit();
    }
}
