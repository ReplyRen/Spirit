Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("速度",range(-3,3))=1
        _Noise1Params("R:tilling G:速度 B:扰动强度",vector)=(0,0,0,1.0)
        _Noise2Params("R:tilling G:速度 B:扰动强度",vector)=(0,0,0,1.0)
        _NoiseMap("Noise贴图",2D)="white"{}
        _AllCol("所有颜色",color)=(0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" 
               "Queue"="Transparent"
               "IgnoreProjector"="True"
        
        }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseMap;
            float4 _MainTex_ST;
            float _Speed;
            half4 _Noise1Params;
            half4 _Noise2Params;
            half4 _AllCol;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
                
                
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                i.uv=i.uv-0.5;
                float theta=atan2(i.uv.y,i.uv.x);//attan 要反推回角度  atan2是360度完整的
                theta=theta/3.1415926*0.5+0.5;//角度怎么换算为(0,1)采样贴图 -1,1  转到0,1完成ramp;
                //\\float r=length(i.uv+frac(_Time.y*_Speed);
                float r1=length(i.uv)+frac(_Time.y*_Speed);
                float2 newuv1=float2(theta,r1);
                fixed4 col = tex2D(_NoiseMap, newuv1);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return float4(_AllCol.rgb,col.g);
            }
            ENDCG
        }
    }
}
