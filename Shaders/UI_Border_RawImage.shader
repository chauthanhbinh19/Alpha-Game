Shader "Unlit/UI_Border_RawImage"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineSize ("Outline Size", Range(0,10)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float4 _Color;
            float4 _OutlineColor;
            float _OutlineSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 mainCol = tex2D(_MainTex, i.uv) * _Color;

                float alpha = mainCol.a;

                // Nếu pixel gốc không có alpha (alpha=0) thì kiểm tra outline
                if (alpha == 0)
                {
                    float outlineAlpha = 0;
                    float2 texel = _MainTex_TexelSize.xy * _OutlineSize;

                    // Sample 8 hướng để tạo viền
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(texel.x, 0)).a;
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(-texel.x, 0)).a;
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(0, texel.y)).a;
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(0, -texel.y)).a;

                    outlineAlpha += tex2D(_MainTex, i.uv + float2(texel.x, texel.y)).a;
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(-texel.x, texel.y)).a;
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(texel.x, -texel.y)).a;
                    outlineAlpha += tex2D(_MainTex, i.uv + float2(-texel.x, -texel.y)).a;

                    outlineAlpha = saturate(outlineAlpha);

                    return float4(_OutlineColor.rgb, outlineAlpha * _OutlineColor.a);
                }

                // Pixel gốc → giữ nguyên, không blend với outline
                return mainCol;
            }
            ENDCG
        }
    }
}