Shader "Custom/Toon"
{
   Properties
    {

        _Color ("Color", Color) = (0.3, 0.3, 0.3, 1)//GameObjectの色
        [NoScaleOffset] _MainTex("Main Texture", 2D) = "white" {}
        _Width ("width",float) = 0.1//枠の大きさ
        _FrameColor("FrameColor",Color) = (0,0,0,1)//枠の色
    }
   SubShader
    {
        Tags {
            "RenderPipeline"="UniversalPipeline"//UniversalPipelineを使用
        }
 
        LOD 100//閾値

        Pass//枠
        {
            Tags{ "LightMode" = "UniversalForward"}//URPにおける描画順番の指定に必要
            Cull Front//描画順番　後
            HLSLPROGRAM//HLSLを使用
            #pragma vertex vert//頂点の操作
            #pragma fragment frag//画素の色塗
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"//Core.hlslをimport
            #include  "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"//Lighting.hlslをimport
            
            struct appdata
            {
                float4 vertex : POSITION;// セマンティクス(頂点の位置)
                float3 normal : NORMAL;//セマンティクス(頂点の法線)
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;//他のレンダリング対象に対する色
                float3 normalWS : TEXCOORD1;//セマンティクス(2番目のUV座標)
            };
            float _Width;
            v2f vert (appdata v)
            {
                v2f o;
                half4x4 scaleMatrix = half4x4(1 +_Width, 0, 0, 0,
                                             0, 1 + _Width, 0, 0,
                                             0, 0, 1 + _Width, 0,
                                             0, 0, 0, 1);
                v.vertex = mul(scaleMatrix, v.vertex);//行列計算してGameObjectの拡大
                o.vertex = TransformObjectToHClip(v.vertex);
                VertexNormalInputs normal = GetVertexNormalInputs(v.normal);
                o.normalWS = normal.normalWS;
                return o;
            }
            float4 _FrameColor;
            float4 frag (v2f i) : SV_Target
            {                
                return _FrameColor;//枠の色
            }
            ENDHLSL
        }

        Pass
        {
            Cull Back//描画順番　先

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"//Core.hlslをimport
            #include  "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"//Lighting.hlslをimport
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normalWS : TEXCOORD1;//セマンティクス(2番目のUV座標)
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                VertexNormalInputs normal = GetVertexNormalInputs(v.normal);
                o.normalWS = normal.normalWS;
                return o;
            }
            float4 _Color;
            float4 _MainTex;
            float4 frag (v2f i) : SV_Target
            {                
                Light lt = GetMainLight();//光を取得
                float4 col = _Color;
                float strength = dot(lt.direction, i.normalWS);//光の強さを計算
                if(strength <= 0.2)//強さによって場合分け
                {
                    strength = 0.1;
                }
                else if(strength <= 0.66)
                {
                    strength = 0.5;
                }
                else
                {
                    strength = 1;
                }
                float4 lightColor = float4(lt.color, 1);//光の色
                return col* lightColor*strength;//最終的な色の計算
            }
            ENDHLSL
        }
}
}
