Shader "Custom/DrunkWaveShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _WaveX ("Wave Scale X", Float) = 10.0
        _WaveY ("Wave Scale Y", Float) = 10.0
        _Speed ("Wave Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _WaveX;
            float _WaveY;
            float _Speed;

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

            float4 frag (v2f i) : SV_Target
            {
                // Apply sine wave distortion to the UV coordinates
                float2 uv = i.uv;
                uv.x += sin(uv.y * _WaveY + _Time.y * _Speed) * 0.05;
                uv.y += cos(uv.x * _WaveX + _Time.y * _Speed) * 0.05;

                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
