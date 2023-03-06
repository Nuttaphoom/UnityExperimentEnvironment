Shader "MyCustoms/Outline"
{
	Properties{
		_MainTex("Texture", 2D) = "white"  {} 
		_Color("Outline Color", Color) = (1,1,1,1)
	}

		SubShader
		{
			Cull Off
 			Pass {
				CGPROGRAM
				#pragma vertex vert 
				#pragma fragment frag 
				#include "UnityCG.cginc"

				sampler2D _MainTex; 
				            struct MeshData	
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };
				struct Interpolator {
					fixed4 position : SV_POSITION; 
					fixed2 uv : TEXCOORD0 ;
				};

				Interpolator vert(MeshData v) {
					Interpolator outp;
					outp.position = UnityObjectToClipPos(v.vertex); 
					outp.uv = v.uv;
					return outp;
				}

				fixed4 _Color; 
				float4 _MainTex_TexelSize;

				float4 frag(Interpolator i) : SV_Target {
					half4 c = tex2D(_MainTex, i.uv);
					c.rgb *= c.a;
					half4 outlineC = _Color; 

					fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0,_MainTex_TexelSize.y)).a ;
					fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
					fixed rightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
					fixed leftAlpha = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;
					
					outlineC.a *= ceil(c.a);
					return lerp(outlineC, c , ceil(upAlpha * downAlpha * rightAlpha * leftAlpha));
				}




			ENDCG 
		}
	}
}
