Shader "Unlit/ToonSimpleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // しきい値を設けて陰影を計算
                half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));
                if (nl <= 0.01f) nl = 0.3f;
                else if (nl <= 0.3f) nl = 0.5f;
                else nl = 1.0f;
                // テクスチャカラーに乗算
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb *= nl;
                return col;
            }
            ENDCG
        }
    }
}