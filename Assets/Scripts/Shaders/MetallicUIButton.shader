Shader "UI/MetallicButton"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)  // Bắt buộc để Unity dùng Image.color
        _Shininess ("Shininess", Range(0.1, 10)) = 2
        _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Pass
        {
            Name "Default"
            Tags { "LightMode" = "Always" }

            Cull Off
            ZWrite Off
            ZTest [unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _HighlightColor;
            float _Shininess;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color; // color đến từ Image component
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texCol = tex2D(_MainTex, i.uv) * i.color;

                // Highlight theo chiều dọc
                float h = pow(1.0 - abs(i.uv.y - 0.5) * 2.0, _Shininess);
                float3 finalRGB = texCol.rgb + _HighlightColor.rgb * h * texCol.a;

                return fixed4(finalRGB, texCol.a);
            }
            ENDCG
        }
    }
}