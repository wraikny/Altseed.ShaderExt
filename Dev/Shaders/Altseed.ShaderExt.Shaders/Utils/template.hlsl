float g_second;
float2 g_resolution;

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

bool isOutOfUV(float2 uv)
{
    return(
        (uv.x < 0.0 || 1.0 < uv.x)
        || (uv.y < 0.0 || 1.0 < uv.y)
    );
}

float4 getTexInside(Texture2D tex, SamplerState spl, float2 uv)
{
    return lerp(
        tex.Sample(spl, uv),
        (0.0).xxxx,
        isOutOfUV(uv)
    );
}
