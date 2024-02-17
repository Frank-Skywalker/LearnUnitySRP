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
            #pragma multi_compile_instancing
            #pragma vertex JRPUnlitPassVertex
            #pragma fragment JRPUnlitPassFragment
            #include "JRPUnlitPass.hlsl"
            ENDHLSL
        }
    }
}