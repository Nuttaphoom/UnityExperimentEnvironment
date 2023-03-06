// This shader visuzlizes the normal vector values on the mesh.
Shader "Example/URPUnlitShaderNormal"
{
	Properties
	{ 
		_AnimationSpeed("Animation Speed",Range(0,3)) = 0   
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			float _AnimationSpeed;
			struct Attributes
			{
				float4 positionOS   : POSITION;
				// Declaring the variable containing the normal vector for each
				// vertex.
				half3 normal        : NORMAL;
			};

			struct Varyings
			{
				float4 positionHCS  : SV_POSITION;
				half3 normal        : TEXCOORD0;
			};

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				if (IN.positionOS.y != 0) {
					IN.positionOS.x += sin(_Time.y * _AnimationSpeed + IN.positionOS.y);
				}
				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				// Use the TransformObjectToWorldNormal function to transform the
				// normals from object to world space. This function is from the
				// SpaceTransforms.hlsl file, which is referenced in Core.hlsl.
				OUT.normal = TransformObjectToWorldNormal(IN.normal);
				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half4 color = 0;
				// IN.normal is a 3D vector. Each vector component has the range
				// -1..1. To show all vector elements as color, including the
				// negative values, compress each value into the range 0..1.
				color.rgb = IN.normal * 0.5 + 0.5;
 				return color;
			}
			ENDHLSL
		}
	}
}