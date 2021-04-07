Shader "Unlit/Smoke"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Maskandpower("R:Grandient G:Mask B:第二个5层渐变",2D)="white"{}
        _Allcolor("整体烟雾颜色",color)=(0,0,0,0)
        _Noise1Params("R:Tilling缩放 G:Speed B:扰动强度",vector)=(0,0,0,0)
        _Noise2Params("R:Tilling缩放 G:Speed B:扰动强度",vector)=(0,0,0,0)
        _NoiseTex("Noise贴图",2D)="White"{}

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
                float2 uv1:TEXCOORD1;
                float2 uv2:TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Maskandpower;
            sampler2D _NoiseTex;
            half4  _Allcolor;
            half4 _Noise1Params;
            half4 _Noise2Params;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
                o.uv1=v.uv*_Noise1Params.r+frac(_Time.y*_Noise1Params.g);
                o.uv2=v.uv*_Noise2Params.r+frac(_Time.y*_Noise2Params.g);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                half4 mask=tex2D(_Maskandpower,i.uv);

                //采样第一个noise
                half noise=tex2D(_NoiseTex,i.uv1).g;

                //采样第二个noise
                half noise2=tex2D(_NoiseTex,i.uv2).g;

                // apply fog
                half3 finalCol=_Allcolor;
                half Alpha=mask.g;

                half NoiseAll=noise*_Noise1Params.z+noise2*_Noise2Params.z;
                //uv总扰动
                half2 uvparrallx=i.uv+NoiseAll*mask.b;
                half finalAlpha=tex2D(_Maskandpower,uvparrallx).g;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return float4(finalCol,finalAlpha*mask.r);
                //float4(finalCol,Alpha);
            }
            ENDCG
        }
    }
}
