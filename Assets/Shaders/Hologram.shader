Shader "GTI745/Hologram" {

	Properties{
		_RimColor("Rim Color", Color) = (0,0.5,0.5,0)
		_RimPower("Rim Power", Range(0,8.0)) = 3.0
	}
		SubShader{
			tags { "Queue" = "Transparent" }

			//Pass {
			//	ZWrite On
			//	ColorMask 0
			//}

			CGPROGRAM

			#pragma surface surf Lambert alpha:fade

			struct Input {
				float3 viewDir;
			};

			float4 _RimColor;
			float _RimPower;
			float _RimCutOff;

			void surf(Input IN, inout SurfaceOutput o) {
				half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
				rim = rim > _RimCutOff ? rim : 0;
				o.Emission = _RimColor * pow(rim, _RimPower);
				o.Alpha = pow(rim, _RimPower);
			}

			ENDCG
	}

		FallBack "Diffuse"
}