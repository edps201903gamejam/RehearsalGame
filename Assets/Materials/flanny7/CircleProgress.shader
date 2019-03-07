Shader "Custom/CircleProgress" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Cutoff("Cutoff", Range(0, 1)) = 1.0
		_Width("Width", Range(0, 1)) = 0.6
	}
		SubShader{
			Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
			LOD 200

			CGPROGRAM

			// カスタムの「Original」ライティング関数を指定
			#pragma surface surf Original fullforwardshadows alpha:fade

			#pragma target 3.0

			static const float PI = 3.14159265f;

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			fixed4 _Color;
			float _Cutoff;
			float _Width;

			// カスタムライティングを適用する
			half4 LightingOriginal(SurfaceOutput s, half3 lightDir, half atten) {
				// ライティングの影響を受けさせないため、受け取った色情報をそのまま返す
				return half4(s.Albedo, s.Alpha);
			}

			void surf(Input IN, inout SurfaceOutput o) {
				float2 pos = (IN.uv_MainTex - float2(0.5, 0.5)) * 2.0;
				float angle = (atan2(pos.y, pos.x) - atan2(1.0, 0.0)) / (PI * 2);

				if (angle < 0) {
					angle += 1.0;
				}

				float len = length(pos);
				float edge = 0.03;
				float width = 1 - _Width;
				float inner = smoothstep(width, width + edge, len);
				float outer = smoothstep(1.0 - edge, 1.00, len);
				float opaque = inner - outer;

				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				float cutoff = angle < _Cutoff ? 0 : 1;
				o.Alpha = _Color.a * opaque * cutoff;
			}
			ENDCG
		}
			FallBack "Diffuse"
}