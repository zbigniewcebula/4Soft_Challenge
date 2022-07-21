Shader "Custom/LightingShader"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

		_AnimationAmplitude("Animation Amplitude", Float) = 1.0
		_AnimationFrequency("Animation Frequency", Float) = 2.0
		_AnimationSpeed("Animation Speed", Float) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Lambert vertex:vert

			sampler2D _MainTex;
			fixed4 _Color;

			float _AnimationAmplitude;
			float _AnimationFrequency;
			float _AnimationSpeed;

			struct Input
			{
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v)
			{
				float t = _Time.x * _AnimationSpeed;
				v.vertex.y = sin(
					(v.vertex.x + t) * _AnimationFrequency
				) * cos(
					(v.vertex.z - t) * _AnimationFrequency
				) * _AnimationAmplitude;
			}

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}