Shader "MyCustoms/Ring" {
	Properties{
		OffsetX("OffsetX",Range(0,1)) = 1.0
	}


		SubShader{
			Tags {
				"RenderType" = "Transparent" 
				"Queue" = "Transparent"
			}
			Pass {
 			Cull Off 
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc"
			fixed OffsetX;

			#define TAU 6.28318530718
			struct MeshData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
				half3 normal : NORMAL;
			};

			struct Interpolator {
				float4 position : SV_POSITION; 
				float2 uv : TEXCOORD0;
				half3 normal : TEXCOORD1;

			};
			
			Interpolator vert(MeshData v) {
				Interpolator outp;
				outp.position = UnityObjectToClipPos(v.position);
				outp.uv = v.uv;
				outp.normal = mul(unity_ObjectToWorld ,v.normal);
				return outp; 
			}

			float InverseLerp(float A,float B, float C) {
				return (C - A) / (B - A);
			}

			float4 frag(Interpolator i) : SV_Target{
				float4 OutColor = float4(1,1,0,0) ;

				float Offset  = cos(i.uv.x * TAU * 3) * 0.01   ; 
				OutColor = cos(i.uv.y       * TAU);
				OutColor =  frac(cos((i.uv.y + _Time.y * 0.1) * 4 * TAU)) ;
				OutColor *= 1 - i.uv.y;

 				return  OutColor * (abs(i.normal.y) < 0.999)  ;
			}

			ENDCG 
		}
	}



}
