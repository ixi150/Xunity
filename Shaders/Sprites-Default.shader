// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Tiled"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        
        _Tiling ("Tiling", Vector) = (1,1,0,0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        //Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            
            float4 _Tiling;
            
            float2 GetTilledCoord(float2 coord)
            {
                coord.x = coord.x + _Tiling.x;
                coord.y = coord.y + _Tiling.y;
                coord.x = (coord.x * _Tiling.z)%1;
                coord.y = (coord.y * _Tiling.w)%1;
                return coord;
            }
            
            v2f Vert(appdata_t IN)
            {
                v2f OUT;
            
                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
            
                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif
                
                OUT.texcoord = IN.texcoord;
                //OUT.texcoord = GetTilledCoord(IN.texcoord);
                OUT.color = IN.color * _Color * _RendererColor;
            
                return OUT;
            }
            
            fixed4 Frag(v2f IN) : SV_Target
            {
                //float2 uv = IN.texcoord;
                float2 uv = GetTilledCoord(IN.texcoord);
                fixed4 c = SampleSpriteTexture (uv) * IN.color;
                //c.rgb *= c.a;
                return c;
            }
            
        ENDCG
        }
    }
}
