Shader "JRP/Unlit"
{
    Properties
    {
        _BaseColor("My Color!!!!", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex JRPUnlitPassVertex
            #pragma fragment JRPUnlitPassFragment
            #include "JRPUnlitPass.hlsl"
            ENDHLSL
        }
    }
}