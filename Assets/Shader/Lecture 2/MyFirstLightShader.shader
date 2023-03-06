Shader "Unlit/MyFirstLightShader"
{
	Properties
	{
		_LightColor("Light Color",Color) = (1,1,1,1)
		_Gloss("Gloss", Range(0,1)) = 1 
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Pass
		{
			Name "Light"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			fixed4 _LightColor; 
			fixed _Gloss; 
            struct MeshData	
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION; 
				float3 normal : TEXCOORD1;
				float3 wVertex : TEXCOORD2; 
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv; 
				o.normal = mul(unity_ObjectToWorld, v.normal);
				o.wVertex = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            float4 frag (Interpolators i) : SV_Target
			{
				//Diffuse Lighting 
				float3 L = _WorldSpaceLightPos0.xyz ; 
				float3 diffuse = dot(L, i.normal) * _LightColor;
				float3 V = normalize(_WorldSpaceCameraPos - i.wVertex);
				//Specular Light (Phong) 
				float3 R = reflect(-L,i.normal)  ; 
				float3 specular = dot(R,V);
				//Specularr Light (bling-phong)
				float3 H = normalize(L + V);  
				specular = saturate(dot(H, i.normal)) * (saturate(dot(i.normal,L)) > 0);
				float specularExpo = exp2(_Gloss * 11) + 2;
				specular = pow(specular, specularExpo);
				return float4(diffuse + specular, 1);

			}
            ENDCG
        }

		Pass
		{
			Blend One One
			Name "Paint COlor"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			fixed4 _LightColor;
			fixed _Gloss;
			struct MeshData
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct Interpolators
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
				float3 wVertex : TEXCOORD2;
			};

			Interpolators vert(MeshData v)
			{
				Interpolators o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.normal = mul(unity_ObjectToWorld, v.normal);
				o.wVertex = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			float4 frag(Interpolators i) : SV_Target
			{
				return float4(1,1,0,0);
			}
			ENDCG
		}

			 
    }
}
