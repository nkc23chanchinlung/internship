Shader "Custom/SimpleColor"
{
    Properties
    {
        [Header(Lambert)]
        _Color("Color", Color) = (0.3, 0.3, 0.3, 1)
        _MainTex ("MainTex", 2D) = "white" {}
        _LambertThresh("LambertThresh", float) = 0.5 
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
        }

        Pass
        {
            Name "Character-Toon"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            float4 _MainTex_ST;
            float _LambertThresh;
            float4 _Color;
            
            v2f vert (appdata v)
            {
                v2f o;

                VertexPositionInputs inputs = GetVertexPositionInputs(v.vertex.xyz);
                // スクリーン座標に変換.
                o.vertex = inputs.positionCS;
                // ワールド座標系変換.
                o.normal = normalize(TransformObjectToWorldNormal(v.normal));
                
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Main light情報の取得.
                Light mainLight;
                mainLight = GetMainLight();
                
                float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                
                
                //  UNorm lambert. 0~1.
                float uNormDot = saturate(dot(mainLight.direction.xyz, i.normal) * 0.5f + 0.5f);
                // _LambertThreshを閾値とした二値化.
                // step(y,x) ... y < x ? 1 : 0
                float ramp = step(uNormDot, _LambertThresh);
                // mainLight.colorの乗算を影色とする.
                color.rgb = lerp(color, color * mainLight.color, ramp);
                return color;
            }
            ENDHLSL
        }
    }
}