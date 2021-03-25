Shader "Hidden/FlameShader"
{
    Properties
    {
        _BrightCol("BrightColor",Color) = (0,0,0,0)
        _MidCol("MidColr",Color) = (0,0,0,0)
        _BaseCol("BaseCol",Color) = (0,0,0,0)
        _NoiseTex("NoiseTexture",2D) = "white"{}
        _MaskTex("MaskTexture",2D) = "white"{}
        [MaterialToggle(_TEX_ON)] _Toggle("Enable VisibleTex", int) = 0 
        _VisibleTex("VisibleTex",2D) = "white"{}
        _NoiseMoveDir("NoiseMoveDir",vector) = (0,0,0,0)

        _BrightRange("BrightRange",Range(0,1)) = 0.5
        _MidRange("MidRange",Range(0,1)) = 0.5
        _MaskPower("Maskpower",Range(0,5))=1
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Tags {"Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _TEX_OFF _TEX_ON

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _NoiseTex,_MaskTex;
            #if _TEX_ON
            sampler2D _VisibleTex;
            #endif
            float4 _NoiseMoveDir;
            float _BrightRange,_MidRange,_MaskPower;
            fixed4 _BaseCol,_BrightCol,_MidCol;
            fixed4 frag (v2f i) : SV_Target
            {
                float noise = tex2D(_NoiseTex, i.uv - _Time.x * _NoiseMoveDir.xy).x;
                float gradientVal = tex2D(_MaskTex,i.uv).x;

                float step1 = step(noise, gradientVal);
				float step2 = step(noise, gradientVal- _BrightRange);
				float step3 = step(noise, gradientVal- _MidRange);
                float4 c = float4(lerp(_BaseCol.rgb,_BrightCol.rgb,step1 - step2 ),step1 );
 
				c.rgb = lerp (c.rgb,_MidCol.rgb,step2 - step3 );
 #if _TEX_ON
 fixed4 mask = tex2D(_VisibleTex,i.uv);
 mask = pow(mask, _MaskPower);
 c.a =lerp(0,mask,c.a)*1.3;
 #endif
				return c;
            }
            ENDCG
        }
    }
}
