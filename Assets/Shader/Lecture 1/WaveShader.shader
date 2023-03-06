Shader "MyCustoms/VertexOffset" {
	Properties{
		_Amp("Wave Amp",Range(0.1,0.3)) = 0.1 
		_WaveColor("Wave Color", Color) = (1,1,1,1) 
	}


		SubShader{
			Tags {
				"RenderType" = "Transparent"
				"Queue" = "Transparent"
			}
			
			Pass {
 			Cull Off 
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc"

			#define TAU 6.28318530718
			float _Amp;
			float4 _WaveColor;
			struct MeshData {
				float4 vertex : POSITION;
				float2 uv0 : TEXCOORD0;
				half3 normal : NORMAL;
			};

			struct Interpolator {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				half3 normal : TEXCOORD1;

			};

			float4 GetWaveFromCenter(float2 uv) {
				float dist = length(uv * 2 - 1);
				float4 wave = cos((dist + _Time.y * 0.05) * TAU * 5) * 0.5 + 0.5;
				wave *= 1 - dist;
				return wave;
			}

			Interpolator vert(MeshData v) {
				Interpolator outp;
				v.vertex.y = GetWaveFromCenter(v.uv0) * _Amp ;

				outp.vertex = UnityObjectToClipPos(v.vertex);
				outp.normal = mul(unity_ObjectToWorld, v.normal);
				outp.uv = v.uv0;
				return outp;
			}

			float InverseLerp(float A,float B, float C) {
				return (C - A) / (B - A);
			}

			float4 frag(Interpolator i) : SV_Target{
				float4 t =   cos((i.uv.y + _Time.y * 0.05) * TAU * 5) * 0.5 + 0.5;
				/*float distanceFromCenter = length(i.uv * 2 - 1);
				float4 wave = cos((distanceFromCenter - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5;
				wave *= 1 - distanceFromCenter;
				return wave * (distanceFromCenter < 0.9);*/
				return GetWaveFromCenter(i.uv) * _WaveColor;
				 
			}

			ENDCG
		}
	}



}
