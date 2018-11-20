Shader "Custom/RoadShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Alpha ("Alpha Value", Range (0.00, 100.00)) = 1.00
        _MaxDistance ("Max distance", Range (1.0, 1000.0)) = 1.00
        _DistanceColor ("Distance Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha One
        //Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off
        Cull Off
        ZWrite Off

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD2;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color, _DistanceColor;
            float _Alpha, _MaxDistance;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float camDist = distance(i.worldPos, _WorldSpaceCameraPos);
                camDist=0;

                fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
                float a = col.a;
                float minimum = min(col.r, min(col.g, col.b));
                col.rgb = 1;

                col = lerp(_Color, col, minimum * a + _SinTime[3] * _Alpha);
                col.a = a * _Color.a;

                return i.color * col * lerp((1,1,1,1), _DistanceColor, camDist/_MaxDistance);
            }
            ENDCG
        }
    }
}
