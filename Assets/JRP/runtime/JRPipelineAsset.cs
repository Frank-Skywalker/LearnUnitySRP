using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/JRPipeline")]
public class JRPipelineAsset : RenderPipelineAsset
{
    [SerializeField]
    private bool useDyanmicBatching = false;

    [SerializeField]
    private bool useGPUInstancing = true;

    [SerializeField]
    private bool useSRPBatcher = true;

    protected override RenderPipeline CreatePipeline()
    {
        return new JRPipeline(useDyanmicBatching, useGPUInstancing, useSRPBatcher);
    }
}
