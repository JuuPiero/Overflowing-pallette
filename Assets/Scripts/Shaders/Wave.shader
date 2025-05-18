Shader "UI/WaveGridEffect"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _WaveSpeed ("Wave Speed", Float) = 3.0
        _WaveFreq ("Wave Frequency", Float) = 20.0
        _WaveStrength ("Wave Strength", Float) = 0.05
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 screenUV : TEXCOORD1;
            };

            fixed4 _Color;
            float _WaveSpeed;
            float _WaveFreq;
            float _WaveStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // Convert to screen UV (0-1)
                float2 screen = ComputeScreenPos(o.pos).xy;
                o.screenUV = screen / screen.w;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.screenUV, center);

                float wave = sin((dist * _WaveFreq - _Time.y * _WaveSpeed)) * _WaveStrength;
                float alpha = 1.0 - dist * 1.5; // Fade edges (optional)
                return _Color + wave * alpha;
            }
            ENDCG
        }
    }
}