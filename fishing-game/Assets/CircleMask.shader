Shader "Custom/CircleMask"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _PlayerPos ("Player Position", Vector) = (0,0,0,0)
        _Radius ("Radius", Range(0.1, 10)) = 2
        _Feather ("Feather", Range(0.01, 1)) = 0.2
        
        // Pixelization properties
        _PixelSize ("Pixel Size", Range(1, 50)) = 10
    }
    
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
            "RenderPipeline"="UniversalPipeline"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            half4 _Color;
            float2 _PlayerPos;
            float _Radius;
            float _Feather;
            float _PixelSize;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);
                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                // Sample texture normally (no pixelation)
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * _Color;
                
                // Calculate pixelated distance for circle mask
                float2 pixelWorldPos = floor(i.worldPos.xy * _PixelSize) / _PixelSize;
                float dist = distance(pixelWorldPos, _PlayerPos);
                
                // Create pixelated alpha mask
                float alpha = 1 - smoothstep(_Radius + _Feather, _Radius - _Feather, dist);
                
                col.a *= max(0.25, alpha);
                
                return col;
            }
            ENDHLSL
        }
    }
}