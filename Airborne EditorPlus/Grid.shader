Shader ""Unlit/Grid""
{
    Properties
    {
        _GridColor (""Grid Color"", Color) = (1,1,1,1)
        _BackgroundColor (""Background Color"", Color) = (0,0,0,0)
        _CellSize (""Cell Size"", Float) = 1
        _LineThickness (""Line Thickness"", Range(0.001, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { ""Queue""=""Transparent"" ""RenderType""=""Transparent"" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include ""UnityCG.cginc""

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

            float4 _GridColor;
            float4 _BackgroundColor;
            float _CellSize;
            float _LineThickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * (1.0 / _CellSize);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 gridUV = frac(i.uv);
                float line = step(gridUV.x, _LineThickness) + step(gridUV.y, _LineThickness);
                float isLine = saturate(line);
                return lerp(_BackgroundColor, _GridColor, isLine);
            }
            ENDCG
        }
    }
}