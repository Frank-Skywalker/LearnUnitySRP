#ifndef INCLUDE_GUARD_JRPUnlitPass
#define INCLUDE_GUARD_JRPUnlitPass

#include "../ShaderLibrary/Common.hlsl"

float4 _BaseColor;

float4 JRPUnlitPassVertex(float3 positionOS : POSITION) : SV_POSITION
{
    float3 positionWS = TransformObjectToWorld(positionOS);
    float4 positionCS = TransformWorldToHClip(positionWS);
    return positionCS;
}


float4 JRPUnlitPassFragment() : SV_TARGET
{
    return _BaseColor;   
}

#endif