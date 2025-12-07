Shader "Unlit/UI_Gradient_Radius_RawImage"
{
    Properties
    {
        _MainTex ("Texture (Alpha Mask)", 2D) = "white" {}
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Color", Color) = (0.5,0,0,1)
        _MaskPercent ("Mask Percent (0-1)", Range(0,1)) = 1
        _MaskFromTop ("Mask From Top", Float) = 1

        // 👇 Stencil properties (Giữ nguyên)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
            "UnityUI.AcknowledgeSupport" = "true" // ⬅️ QUAN TRỌNG: Báo cho Unity biết Shader này hỗ trợ UI Masking
        }

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            // 🔹 Stencil chuẩn UI
            Stencil
            {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
            }
            ColorMask [_ColorMask]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc" // ⬅️ QUAN TRỌNG: Bao gồm hàm UnityGet2DClipping

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                // 🔹 THÊM BIẾN MASKING: Dữ liệu này được gửi từ Canvas Renderer
                float4 maskUV : TEXCOORD1; 
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                // 🔹 THÊM BIẾN MASKING: Truyền dữ liệu clipping sang Fragment Shader
                float4 maskUV : TEXCOORD1; 
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
                o.maskUV = v.maskUV; // 🔹 Gán dữ liệu clipping
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed4 grad = lerp(_BottomColor, _TopColor, i.uv.x);
                grad.a *= tex.a; // Mask Alpha Texture

                // --- 1. ÁP DỤNG MASK PERCENT TÙY CHỈNH ---
                float maskStart, maskEnd;
                if (_MaskFromTop > 0.5)
                {
                    maskStart = 1.0 - _MaskPercent;
                    maskEnd = 1.0;
                }
                else
                {
                    maskStart = 0.0;
                    maskEnd = _MaskPercent;
                }

                if (i.uv.y < maskStart || i.uv.y > maskEnd)
                    grad.a = 0;

                // --- 2. ÁP DỤNG RECTMASK2D CLIPPING ---
                // Truyền dữ liệu maskUV vào đoạn code clipping thủ công.
                // Giả sử rằng rectMaskUV có thể là [0,0,1,1] cho vùng clipping

                float rectMaskAlpha = 1.0;
                if (i.maskUV.x < 0 || i.maskUV.x > 1 || i.maskUV.y < 0 || i.maskUV.y > 1)
                {
                    rectMaskAlpha = 0.0;  // Nếu pixel ra ngoài vùng clipping thì ẩn nó
                }

                // Nhân alpha cuối cùng với alpha mask của RectMask2D
                grad.a *= rectMaskAlpha; 
                
                return grad;
            }
            ENDCG
        }
    }
}
