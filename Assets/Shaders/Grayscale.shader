Shader "Shaders/Grayscale" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_intensity("Black & White intensity", Range(0, 1)) = 0
	}
	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _intensity;

			float4 frag(v2f_img i) : COLOR{
				float4 c = tex2D(_MainTex, i.uv);

				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float i2 = 1 - clamp(_intensity, 0, 0.7);
				float3 bw = float3(lum, lum, lum) * i2;

				float4 result = c;
				result.rgb = lerp(c.rgb, bw, _intensity);
				return result;
			}
			ENDCG
		}
	}
}
