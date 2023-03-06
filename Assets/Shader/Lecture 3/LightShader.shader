Shader "Unlit/LighttShader"
{
    Properties
    {
        _LightColor("Light Color", Color) = (1,1,1,1)
        _Gloss("_Gloss",Range(0,1)) = 0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct Interpolators
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 wVertex : TEXCOORD2;
            };

            fixed4 _LightColor;
            fixed _Gloss;
            Interpolators vert(MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.wVertex = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                o.normal = mul(unity_ObjectToWorld, v.normal);
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                //Diffuse Lighting 
                float3 L = _WorldSpaceLightPos0;
                float3 N = normalize(i.normal);
                float3 diffuse = saturate(dot(L, N)) + 0.1;

                //Specular Lighting
                float3 H = normalize(_WorldSpaceCameraPos - i.wVertex);
                float3 R = normalize(H + L);
                float3 specular = saturate(dot(R,N)) * (dot(N,L) > 0);


                specular = pow(specular, _Gloss * 64);
                return float4(diffuse * _LightColor + specular  ,1);
            }
            ENDCG
        }
    }
}
