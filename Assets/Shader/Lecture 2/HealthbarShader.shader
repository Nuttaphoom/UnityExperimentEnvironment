Shader "Unlit/HealthbarShader"
{
    Properties
    {
		_MainTexture("Filled Texture",2D) = "white" {}
		_HPSlider("Health Point", Range(0,1)) = 1
		_BorderThick("Thickness", Range(0,1)) = 1
	}

		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
			Pass
			{			
				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha 

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				#define GREEN float4(0, 1, 0, 1)
				#define RED float4(1,0,0,1) 
				#define PURPLE float4(1,0,1,1) 

				struct Meshdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct Interpolators
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				float _HPSlider; 
				sampler _MainTexture;
				float4 _MainTexture_ST;
				float _BorderThick;
				float InverseLerp(float a, float b, float c) {
					return (c - a) / (b - a); 
				}

				Interpolators vert (Meshdata v)
				{
					Interpolators o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTexture);

					return o;
				}

				fixed4 frag (Interpolators i) : SV_Target
				{
					float2 uv = i.uv; 
					
					uv.x *= 8;
 					float2 pointOfInterest = float2(clamp( uv.x    ,0.5,7.5),0.5 );
					float dist = distance(uv, pointOfInterest) * 2 - 1  ; 

					

					float borderDist = dist + _BorderThick ;
					float borderMask;
					borderMask =  step(0,-borderDist) ;
					
					borderMask = saturate(-borderDist / fwidth(borderDist));

					fixed3  color = tex2D(_MainTexture, float2(_HPSlider,i.uv.y));
					float uvMask = saturate(i.uv / fwidth(i.uv));
					return float4(color * uvMask,1); 
					color *= (_HPSlider - 0.2 <= 0) ? cos(_Time.y * 4) + 2 : 1;

					float healthBarMask = saturate((i.uv.x < _HPSlider) / fwidth((i.uv.x < _HPSlider)));
					clip(-dist);
					return float4(color  * healthBarMask, 1 )  ;
				}
				ENDCG
        }
    }
}
