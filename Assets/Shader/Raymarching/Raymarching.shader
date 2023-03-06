Shader "Unlit/Raymarching"
{
    Properties
    {
        _Color("Color",Color) =  (1,1,1,1)
        _LD("Light Direction",Vector) = (0,-1,0,0)
        _Gloss("Gloss",Range(0,1)) = 0 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CULL OFF
            CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define MAX_STEP 100  
            #define MAX_DIST 100      

            float4 _Color ;
            float4 _LD ;
            float _Gloss ; 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL ; 
            };

            struct v2f
            {
                float2 uv : TEXCOORD0 ;
                float4 vertex : SV_POSITION ;
                float3 normal : TEXCOORD1 ; 
                float3 wVeretx : TEXCOORD2 ; 
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv ;
                o.wVeretx = v.vertex ; //Object space 
                o.normal = mul(unity_ObjectToWorld,v.normal) ;
                return o;
            }

            float4x4 Rotate(int type,float theta) {
                float4x4 r = 0 ; 
                if (type == 0) {
                    r = float4x4(float4(1,0,0,0),
                                 float4(0,cos(theta),-sin(theta),0),
                                 float4(0,sin(theta),cos(theta),0),
                                 float4(0,0,0,1)) ;
                }else if (type == 1){
                    r = float4x4(float4(cos(theta),0,sin(theta),0),
                                 float4(0,1,0,0),
                                 float4(-sin(theta),0,cos(theta),0),
                                 float4(0,0,0,1)) ;
                }else if (type == 2) {
                    r = float4x4(float4(cos(theta),-sin(theta),0,0),
                                 float4(sin(theta),cos(theta),0,0),
                                 float4(0,0,1,0),
                                 float4(0,0,0,1)) ;
                }

                return r; 
            }

             // root smooth min (k=0.01)
            float smin( float a, float b, float k = 0.01 )
            {
                float h = a-b;
                return 0.5*( (a+b) - sqrt(h*h+k) );
            }

            float boxSDF( float3 p, float3 b )
            {
              float3 q = abs(p) - b;
              return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
            }

            float intersectSDF(float distA, float distB) {
                return max(distA, distB);
            }

            float unionSDF(float distA, float distB) {
                return smin(distA, distB);
            }

            float differenceSDF(float distA, float distB) {
                return max(distA, -distB);
            }

            float sphereSDF(float3 p,float r) {
                
                float3 c = float3(-0.5f, 0, 0);
                return length(p % 0.2f -c ) - r ;
            }

            float torusSDF( float3 p, float2 t )
            {
              float2 q = float2(length(p.xz)-t.x,p.y);
              return length(q)-t.y;
            }

            float scenetSDF(float3 p) {
                /*
                float s1 = sphereSDF(p + float3(0, + sin(_Time.y) / 10,0) ,0.25) ; 
                float s2 = sphereSDF(p + float3(cos(_Time.y) / 2    ,0,sin(_Time.y) / 2 ),0.15 ) ; 

                float2 t = float2(0.5,0.05);
                float d = unionSDF(torusSDF(mul(Rotate(2,    _Time.y )  ,p)     ,t),
                             torusSDF(mul(Rotate(0,  _Time.y )  ,p)     ,t)) ;
 
                d = unionSDF(d, s2) ;
                d = unionSDF(d, s1) ;*/               
                
                //Infite loop sphere
                float s1 = sphereSDF(p ,  0.1);
                 return s1;
            }

            float3 GetNormal(float3 p) {
                float2 t = float2(0.001,0) ;
                float3 normal = scenetSDF(p) - float3(scenetSDF(p - t.xyy),
                                        scenetSDF(p - t.yxy) ,
                                        scenetSDF(p - t.yyx) ) ;

                return normalize(normal) ; 
            }

            float Raymarching(float3 eye, float3 viewRayDirection) {
                float depth = 0;

                for (int i = 0; i < MAX_STEP; i++) {
                    float dist = scenetSDF(eye + depth * viewRayDirection);
                     if (dist < 0.001) {
                        return depth;
                    }
                    depth += dist;
                    if (depth >= MAX_DIST) 
                        return MAX_DIST;
                }
                return MAX_DIST ;
            }
            fixed4 frag (v2f i) : SV_Target
            {   
                //X,Y,Z = 0.4 - 0.6 
                
                
                float2 uv = i.uv *2 -1    ;
                float3 cameraInObject = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1));  //to object space
                float3 rD = normalize( i.wVeretx - cameraInObject  ) ;

                 float dist = Raymarching(cameraInObject,rD) ;

                fixed3 color = 0 ; 
                if (dist < MAX_DIST) {
                    color.rgb = _Color ; 
                     
                 }else {
                     discard ;
                 }

 
                 //lighting 
                 float3 p = cameraInObject + rD * dist ;  
                 float3 n = GetNormal(p) ; 
                 float3 l = _LD ;
                 float diffuse = dot(n,l) ;

                 float specular = saturate(dot(normalize(l + (cameraInObject - p )),n))   * (saturate(dot(n,l)) > 0);
                 float specularExpo = exp2(_Gloss * 11) + 2;
				 specular = pow(specular, specularExpo) * _Gloss;

                 //Shadow 
                 float shadow = Raymarching(p + n * 0.001,l) ;
                 if (shadow < length(p - l)) {
                    shadow = 0.1 ;
                 }else 
                    shadow = 1 ;

                 return fixed4( color    * ( diffuse +   specular),1);
            }
            ENDCG
        }
    }
}
