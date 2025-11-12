Shader "Unlit/UI_Gradient_Radius_RawImage"
{
    Properties
    {
        _MainTex ("Texture (Alpha Mask)", 2D) = "white" {}
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Color", Color) = (0.5,0,0,1)
        _MaskPercent ("Mask Percent (0-1)", Range(0,1)) = 1
        _MaskFromTop ("Mask From Top", Float) = 1 // 1 = mask từ trên, 0 = từ dưới
    }

    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata_t
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
            fixed4 _TopColor;
            fixed4 _BottomColor;
            float _MaskPercent;
            float _MaskFromTop;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // --- Lấy alpha từ texture
                fixed4 tex = tex2D(_MainTex, i.uv);

                // --- Gradient
                fixed4 grad = lerp(_BottomColor, _TopColor, i.uv.y);

                // --- Áp mask texture
                grad.a *= tex.a;

                // --- Áp mask percent
                float maskStart, maskEnd;
                if (_MaskFromTop > 0.5)
                {
                    // mask từ trên
                    maskStart = 1.0 - _MaskPercent;
                    maskEnd = 1.0;
                }
                else
                {
                    // mask từ dưới
                    maskStart = 0.0;
                    maskEnd = _MaskPercent;
                }

                if (i.uv.y < maskStart || i.uv.y > maskEnd)
                {
                    grad.a = 0; // loại bỏ pixel ngoài vùng
                }

                return grad;
            }
            ENDCG
        }
    }
}