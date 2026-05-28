Shader "Unlit/BlueGradientShader"
{
    Properties
    {
        _MainTex ("Font Atlas", 2D) = "white" {}
        _FaceColor ("Face Color", Color) = (1,1,1,1)
        _GradientTex ("Gradient Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
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

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 screenUV : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _GradientTex;
            fixed4 _FaceColor;

            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.screenUV = v.vertex.xy;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample SDF font
                fixed4 sdf = tex2D(_MainTex, i.uv);
                
                // Gradient theo UV.y (tùy chỉnh nếu muốn dùng screen space)
                float gradY = i.uv.y;
                fixed4 gradColor = tex2D(_GradientTex, float2(0.5, gradY)); // giữa texture

                // Áp dụng alpha từ SDF font
                float alpha = smoothstep(0.5 - 0.1, 0.5 + 0.1, sdf.a);

                return gradColor * _FaceColor.a * alpha;
            }
            ENDCG
        }
    }
}
