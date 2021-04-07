Shader "Unlit/Boom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskAndGradient("蒙版",2D)="white"{}
        _MaskAndGradient2("R:vertex蒙版 G:3渐变Mask  B:烟雾Shape",2D)="white"{}
        _AllCol("整体颜色",color)=(0,0,0,0)
        _Opacity("整体不透明度",range(0,5))=1
        _NoiseParams("Noise参数 R:tilling G:Speed B:强度",vector)=(0,0,0,0)
        _Noise2Params("R:Tilling缩放 G:Speed B:扰动强度",vector)=(0,0,0,0)
        _NoiseMap("Noise扰动贴图", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" 
               "RenderType"="Transparent"
               "IgnoreProjector"="True"
        
        }
        LOD 100

        Pass
        {
            Blend  SrcAlpha OneMinusSrcAlpha
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
                float2 uv1 :TEXCOORD1;
                float2 uv2:TEXCOORD2;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 mask:TEXCOORD3;
            };

            sampler2D _MainTex;
            sampler2D _MaskAndGradient;
            sampler2D _MaskAndGradient2;
            sampler2D _NoiseMap;
            float4 _MainTex_ST;
            half _Opacity;
            half4 _AllCol;
            half4 _NoiseParams;
            half4 _Noise2Params;
            

            v2f vert (appdata v)
            {
                v2f o;
                
                
                o.uv = v.uv;
                o.uv1= v.uv*_NoiseParams.x+float2(0.0,frac(_Time.y*_NoiseParams.y));
                o.uv2= v.uv*_Noise2Params.r+float2(0.0,frac(_Time.y*_Noise2Params.g));
                o.mask= tex2Dlod(_MaskAndGradient,o.uv.xyxy);
                half vertexoffset=(step(0.5,v.vertex.x)-0.5)*o.mask.x*1.2;
                //v.vertex.x=
                o.vertex = UnityObjectToClipPos(v.vertex+float4(vertexoffset,0.0,0.0,0.0));
                //o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                half  noise=tex2D(_NoiseMap,i.uv1).g;
                half  noise2=tex2D(_NoiseMap,i.uv2).g;
                half4  GradientStatic=tex2D(_MaskAndGradient2,i.uv);
                half2  uvparallx=i.uv+(noise*_NoiseParams.z-noise2*_Noise2Params.z)*GradientStatic.r;
                half4  Gradient=tex2D(_MaskAndGradient2,uvparallx);


                half   shape=1-Gradient.b;
                //计算Alpha通道
                half   alpha=shape*_Opacity*GradientStatic.r*GradientStatic.b*GradientStatic.g*noise*noise*(1-GradientStatic.r);


                return float4(_AllCol.xxx,alpha);
            }
            ENDCG
        }
    }
}
