Shader "Custom/IceShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,0)
		_Glossiness("Smoothness", Range(0,3)) = 1.0
	}


	SubShader{
		Tags{ "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
#pragma surface surf Standard alpha:fade
#pragma target 3.0

	struct Input {
		float3 worldNormal;
		float3 viewDir;
	};

	half _Glossiness;
	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		o.Albedo = _Color;
		float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
		o.Alpha = alpha*_Glossiness;
	}
	ENDCG
	}
		FallBack "Diffuse"
}