Shader "Hidden/SaturationAlterShader" {
    Properties {
        _MainTex("Camera Input", 2D) = "white" {}
        _Brightness("Brightness", Float) = 1
        _Saturation("Saturation", Float) = 1
        _Contrast("Contrast", Float) = 1
    }

    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            half _Brightness;
            half _Saturation;
            half _Contrast;

            struct v2f {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            v2f vert(appdata_img v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET {
                fixed4 src = tex2D(_MainTex, i.uv);
                // Brightness
                fixed3 colour = src.rgb * _Brightness;
                // Saturation
                fixed luminance = Luminance(src.rgb);
                fixed3 luminanceColour = fixed3(luminance, luminance, luminance);
                colour = lerp(luminanceColour, colour, _Saturation);
                // Contrast
                fixed3 avgColour = fixed3(.5, .5, .5);
                colour = lerp(avgColour, colour, _Contrast);
                return fixed4(colour, src.a);
            }

            ENDCG
        }
    }

    Fallback Off
}
