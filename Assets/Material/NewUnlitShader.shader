Shader "Custom/ClearCircle"
{
Properties
{
    _MainTex ("Texture", 2D) = "white" {}
    _Color ("Color", Color) = (1,1,1,1)
    _SpotCenter ("Spot Center", Vector) = (0.5,0.5,0,0)
    _CursorSizeX ("Cursor Size X", Float) = 10.0
    _CursorSizeY ("Cursor Size Y", Float) = 10.0
    _transparentRadius ("Complete Transparent Radius", Float) = 0.6
    _minAlpha ("Minimal Alpha Value", Float) = 0.9
}

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

sampler2D _MainTex;
float4 _MainTex_TexelSize;
float4 _Color;
float2 _SpotCenter;
float _CursorSizeX;
float _CursorSizeY;
float _transparentRadius;
float _minAlpha;

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

fixed4 frag (v2f i) : SV_Target
{
    fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;

    float normalizedRadiusX = (_CursorSizeX) * _MainTex_TexelSize.x;
    float normalizedRadiusY = (_CursorSizeY) * _MainTex_TexelSize.y;
    float aspectRatio = normalizedRadiusX / normalizedRadiusY;
    float2 distVector = (i.uv - _SpotCenter) * float2(1.0, aspectRatio);
    float dist = length(distVector);

    float alphaGradient = smoothstep(normalizedRadiusX * _transparentRadius, normalizedRadiusX, dist) * _minAlpha;
    texColor.a *= alphaGradient;

    return texColor;
}
ENDCG
        }
    }
    FallBack "Diffuse"
}