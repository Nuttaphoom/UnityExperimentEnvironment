
Shader "MyCustoms/Overlay"
{
	Properties{
		_MainTex("Main Texture", 2D) = "white"  {}
		_OverlayTex("Overlay Texture" , 2D) = "black"  {}
		_Direction("Converage Direction" , Vector) = (0,1,0)
		_Intensity("Intensity " , Range(0,1)) = 1 
	}

	SubShader{
		Pass {
			CGPROGRAM 
			#pragma vertex vertexFunc 
			#pragma fragment fragmentFunc 

			#include "UnityCG.cginc"

			struct MeshData {
				float4 vertex : POSITION; 
				float4 texcoord : TEXCOORD0;
				float3 normal : NORMAL; 
			};
			struct Interpolator {
				float4 pos : SV_POSITION; 
				float3 normal : NORMAL; 
				float2 uv_m : TEXCOORD0; 
				float2 uv_o : TEXCOORD0; 
			};

			sampler2D _MainTex;  
			float4 _MainTex_ST;  
			sampler2D _OverlayTex; 
			float4 _OverlayTex_ST;  

			Interpolator vertexFunc(MeshData v) {
				Interpolator outp;  
				outp.pos = UnityObjectToClipPos(v.vertex); 
				outp.uv_m = TRANSFORM_TEX(v.texcoord, _MainTex);
				outp.uv_o = TRANSFORM_TEX(v.texcoord, _OverlayTex);
				outp.normal = mul(unity_ObjectToWorld, v.normal);
				return outp;
			}

			float3 _Direction; 
			fixed _Intensity;
 			
			fixed4 fragmentFunc(Interpolator i) : COLOR 
			{
				fixed dir = dot(normalize(i.normal),_Direction); 
				if (dir < 1 - _Intensity) {
					dir = 0;
				}

				fixed4 t = tex2D(_MainTex, i.uv_m);
				fixed4 t2 = tex2D(_OverlayTex, i.uv_o);
				return lerp(t,t2,dir);
			}


			ENDCG 
		}
	}

}