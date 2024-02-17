using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering;


public class JRPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();
    private bool useDynamicBatching;
    private bool useGPUInstancing;
    
    public JRPipeline(bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher)
    {
        this.useDynamicBatching = useDynamicBatching;
        this.useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
    }
    
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {}

    protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
    {
        foreach (var camera in cameras) {
            renderer.Render(context, camera, useDynamicBatching, useGPUInstancing);
        }
    }
}
