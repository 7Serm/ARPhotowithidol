Shader "Unlit/BoltmeshSheder"
{
    Properties
    {
        _ShadowIntensity ("shadow", Range (0, 1)) = 0.6
        _StarIntensity ("starpower", Range (0, 1)) = 0.8
        _StarSize ("starsize", Range (0.01, 0.1)) = 0.05
    }

    SubShader
    {
        Tags {"Queue"="Transparent" }
        Pass
        {
            Tags {"LightMode" = "ForwardBase" }
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            uniform float _ShadowIntensity;
            uniform float _StarIntensity;
            uniform float _StarSize;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                LIGHTING_COORDS(0,1)
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            float starShape(float2 uv, float size)
            {
                uv = (uv - 0.5) * 2.0;
                float d = length(uv);
                float angle = atan2(uv.y, uv.x);
                float spokes = 5.0;
                float r = cos(angle * spokes) * 0.5 + 0.5;
                return smoothstep(size, size + 0.01, r - d);
            }

            fixed4 frag(v2f i) : COLOR
            {
                float attenuation = LIGHT_ATTENUATION(i);
                float star = starShape(frac(i.uv / _StarSize), 0.5);
                float starMask = step(0.5, star); // 星の形を定義するマスク
                return fixed4(1, 1, 0, (1 - attenuation) * _ShadowIntensity + starMask * _StarIntensity);
            }
            ENDCG
        }
    }
    Fallback "VertexLit"
}
