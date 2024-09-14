Shader "Custom/DrunkWobbleVR"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionAmount ("Distortion Amount", Range(0.0, 1.0)) = 0.3
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
            float _DistortionAmount;

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
                float2 uv = i.uv;

                // Add distortion to simulate drunk wobble
                uv.x += sin(uv.y * 10.0 + _Time.y * 2.0) * _DistortionAmount;
                uv.y += cos(uv.x * 10.0 + _Time.y * 2.0) * _DistortionAmount;

                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
