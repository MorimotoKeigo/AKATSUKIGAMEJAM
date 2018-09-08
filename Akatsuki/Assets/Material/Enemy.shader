Shader "1"
{
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

            sampler2D _CameraDepthTexture;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float box(float2 st, float size)
            {
                size = 0.5 + size * 0.5;
                st = step(st, size) * step(1.0 - st, size);
                return st.x * st.y;
            }


            float wave(float2 st, float n)
            {
                st = (floor(st * n) + 0.5) / n;
                float d = distance(0.5, st);
                return (1 + sin(d * 3 - _Time.y * 3.0)) * 0.5;
            }

            float box_wave(float2 uv, float n)
            {
                float2 st = frac(uv * n);
                float size = wave(uv, n);
                return box(st, size);
            }

            float heart(float2 st)
            {
            // 位置とか形の調整
            st = (st - float2(0.5, 0.38)) * float2(2.1, 2.8);

                return pow(st.x, 2) +
              pow(st.y - sqrt(abs(st.x)), 2);
}


            fixed4 frag (v2f i) : SV_Target
            {
                float n = 10;
                return wave(i.uv, n);

            //float d = heart(i.uv);
            //return step(d, abs(sin(d * 8 - _Time.w * 2)));

            }
            ENDCG
        }
    }
}