Shader "Unlit/MetaBall"
{
    Properties
    {
        _Color("Color",Color) = (1,1,1,1)
        _LD("Light Direction", Vector) = (0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"           
              #define STEP_LIMIT 1000  
            #define SUR_DIST 0.001 
            #define MAX_DIST 1000 
            float3 _LD ; 
            float4 _Color ; 

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0; 
                float3 wVertex : TEXCOORD1 ; 
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD2 ; 
            };

            
            // root smooth min (k=0.01)
            float smin( float a, float b, float k = 0.01 )
            {
                float h = a-b;
                return 0.5*( (a+b) - sqrt(h*h+k) );
            }

            float unionSDF(float distA, float distB) {
                return smin(distA, distB);
            }

            float circleSDF(float3 p,float r){
                return length(p) - r;
            } 

            float sceneSDF(float3 p) {
                return circleSDF(p,0.25);            
            }

            float3 GetNormal(float3 p) {
                float2 tem = float2(0.001,0) ; 

                float3 n =  sceneSDF(p) - float3(sceneSDF(float3(p - tem.xyy)),
                                            sceneSDF(float3(p - tem.yxy)),
                                           sceneSDF(float3(p-tem.yyx) )) ; 

                return normalize(n) ;
            }


            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.wVertex =  v.vertex  ; 
                o.normal = GetNormal(o.wVertex) ; 
                o.uv = v.uv ; 
                return o;
            }



            float raymarch(float3 p, float3 dir) {
                float dist = 0 ; 
                for (int i = 0; i < STEP_LIMIT; i++) {
                    float3 pix = p + dir * dist ;
                    float sdf = sceneSDF(pix) ; 
                    dist += sdf ;
                    //Found
                    if (sdf < SUR_DIST) {
                        return dist ; 
                    }

                    //Not Found 
                    if (sdf >= MAX_DIST) {
                        return MAX_DIST ;  
                    }
                }

                return MAX_DIST ;            
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                float3 color = 0 ;
                float3 cam = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1));
                float3 dir = normalize(i.wVertex - cam) ; 
                float r = raymarch(cam,dir) ;
 

                if (r < MAX_DIST) {
                    color =  _Color;
                }else {
                    discard;
                }

                //Diffuse Lighting
                float diffuse = 1 ; 
                float3 lightDir = normalize(_LD) ;
                diffuse = saturate(dot(i.normal,lightDir) );

                 

                //Specular Lighting 


                return float4 (color * diffuse, 1);

            }
            ENDCG
        }
    }
}
