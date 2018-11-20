Shader "Custom/ObstacleScrollingShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll speed", Range (-5.0,5.0)) = 0.00
        _MaskTex ("Mask", 2D) = "white" {}
        _Alpha ("Alpha Value", Range (0.00, 100.00)) = 1.00
        _MaxDistance ("Max distance", Range (1.0, 1000.0)) = 1.00
        _DistanceColor ("Distance Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags {"Queue" = "Transparent"}
        LOD 100
        //Blend SrcAlpha One
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off
        Cull Off
        ZWrite Off
        //ZTest Always
        //ZTest Greater
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex,_MaskTex;
            float4 _MainTex_ST, _MaskTex_ST;
            float4 _Color, _DistanceColor;
            float _Alpha, _MaxDistance, _ScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 scrolled_uv = i.uv;
                scrolled_uv.y = (scrolled_uv.y + _ScrollSpeed * _Time[1]);
                fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(scrolled_uv, _MainTex));
                float a = col.a;
                float minimum = min(col.r, min(col.g, col.b));
                col.r = col.g = col.b = 1;

                col = lerp(_Color, col, minimum * a + _SinTime[3] * _Alpha);
                col.a = a;
                //col.rgb = col.rgb;

                float camDist = distance(i.worldPos, _WorldSpaceCameraPos);

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);

                //masking

                fixed4 mask = tex2D(_MaskTex, TRANSFORM_TEX(i.uv, _MaskTex));
                return col * lerp(fixed4(1,1,1,1), _DistanceColor, camDist/_MaxDistance) * mask;
            }
            ENDCG
        }
    }
}
