// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/AlphaMask"
{
    Properties
    {
        _MainTex ("Background Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture (Shape)", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;

            float4 _MainTex_ST;
            float4 _MaskTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uvMain : TEXCOORD0;
                float2 uvMask : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;

                // thay thế UnityObjectToClipPos
                o.pos = UnityObjectToClipPos(v.vertex);

                // thay TRANSFORM_TEX
                o.uvMain = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uvMask = v.uv * _MaskTex_ST.xy + _MaskTex_ST.zw;

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uvMain);
                float mask = tex2D(_MaskTex, i.uvMask).a;

                col.a *= mask;

                return col;
            }

            ENDCG
        }
    }
}
