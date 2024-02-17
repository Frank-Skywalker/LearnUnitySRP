#ifndef INCLUDE_GUARD_JRPUnlitPass
#define INCLUDE_GUARD_JRPUnlitPass

#include "../ShaderLibrary/Common.hlsl"

UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
    UNITY_DEFINE_INSTANCED_PROP(float4, _BaseColor)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)


struct VertexAttribute
{
    float3 positionOS: POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Interpolants
{
    float4 positionCS : SV_POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

Interpolants JRPUnlitPassVertex(VertexAttribute input)
{
    Interpolants output;
    UNITY_SETUP_INSTANCE_ID(input);
    // Set instance id for interpolants
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    float3 positionWS = TransformObjectToWorld(input.positionOS);
    output.positionCS = TransformWorldToHClip(positionWS);
    
    return output;
}


float4 JRPUnlitPassFragment(Interpolants input) : SV_TARGET
{
    UNITY_SETUP_INSTANCE_ID(input);
    float4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);
    return baseColor;   
}

#endif