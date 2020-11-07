// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/GravitationalLensingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex; //sampler2D is a texture
            uniform float2 _Position;   //float2 is a vector 2 (2 floats)
            uniform float _Radius;       //issa float (REALY?!?!?! NO WAY!!!)
            uniform float _Ratio;       //them to
            uniform float _Distance;    //and this guy

            struct v2f {
                float4 pos : POSITION;  //special keyword for getting position of something
                float2 uv : TEXCOORD0;  //texture coordinates of uv map
            };

            //pulling world position and screen position
            //gives a vertex and converts to a fragment
            //e.g if there are a bunch of pixels on screen all
            //doing the same thing they become a fragment of the screen
            //in a sense, same pixels are grouped together so renderer doesn't 
            //have to do so much work
            //e.g "gonna do the same thing for all of these rather than look,
            //at every pixel individually"  
            v2f vert (appdata_img v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i) : COLOR{
                float2 offset = i.uv - _Position;   //look at current pixel's position - blackhole position
                float2 ratio = {_Ratio, 1}; //ratio we are going to work with
                float radius = length(offset/ratio);
                //equation responsible for gravitational lensing
                float deformation = 1 / pow(radius * pow(_Distance, 0.5), 2) * _Radius * 0.1;

                //how far does light need to look like it is if it is being bent 
                //so it looks like its further or closer away or "super booky"
                offset = offset * (1 - deformation);
                offset += _Position;

                //resolved pixel (what its going to be)
                //taking original image and offset pixels 
                half4 res = tex2D(_MainTex, offset); 

                if (radius * _Distance < _Radius) {
                    res = half4( 0, 0, 0, 1 );
                }

                //return resolved pixel
                return res;
            }

            ENDCG
        }
    }
}
