Shader "Hidden/ImageOutLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MinOffset("MinOffset",Float) = 0.01
        _OutLineCol("OutLineCol",Color) = (0,0,0,0)
        _Flag("Flag",Float) = 1
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Tags  {"Queue" = "Transparent" "RenderType"="TransparentCutout"}
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _MinOffset,_Flag;
            fixed4 _OutLineCol;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                if (_Flag > 0) return col;
                fixed right,left,top,bottom;
                
                right =tex2D(_MainTex,i.uv + (float2(1,0) * _MainTex_TexelSize.xy * _MinOffset)).a;
                left = tex2D(_MainTex,i.uv + (float2(-1,0)* _MainTex_TexelSize.xy* _MinOffset)).a;
                top = tex2D(_MainTex,i.uv + (float2(0,1) * _MainTex_TexelSize.xy* _MinOffset)).a;
                bottom = tex2D(_MainTex,i.uv + (float2(0,-1) * _MainTex_TexelSize.xy* _MinOffset)).a;

                if (right == 0 && left == 0 && top == 0 && bottom == 0)return fixed4(1, 1, 1, 0);
                float IsEdge = right * left* top * bottom;
                col.rgb = lerp(_OutLineCol.rgb,col.rgb,IsEdge);
                return col;
            }
            ENDCG
        }
    }
}
