Shader "Custom/URPLineRendererEmission"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (0,1,1,1)
        _EmissionStrength ("Emission Strength", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _BaseColor;
            float4 _EmissionColor;
            float _EmissionStrength;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            float4 frag(Varyings i) : SV_Target
            {
                float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                float4 baseCol = tex * _BaseColor;

                float4 emission = _EmissionColor * _EmissionStrength;
                baseCol.rgb += emission.rgb;

                return baseCol;
            }
            ENDHLSL
        }
    }
}
