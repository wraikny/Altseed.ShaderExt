#include "../Utils/noise_inc.hlsl"

Texture2D g_texture : register( t0 );
Texture2D g_disolveTexture : register( t1 );
SamplerState g_sampler : register( s0 );

float g_threshold;

float g_disolveSource;

float2 g_disolveScale;
float2 g_disolveOffset;

float getDisolveValue(float2 uv)
{
    int source = g_disolveSource;
    
    float result = 0.0f;
    switch(source)
    {
        case 0: {
            float4 m = g_disolveTexture.Sample(g_sampler, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
        case 1: {
            result = saturate( random(uv) );
            break;
        }
        case 2: {
            result = blockNoise(uv);
            break;
        }
        case 3: {
            result = valueNoise(uv);
            break;
        }
        case 4: {
            result = perlinNoise(uv);
            break;
        }
        case 5: {
            result = fBm(uv);
            break;
        }
    }
    return saturate(result);
}

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

float4 main( const PS_Input Input ) : SV_Target
{
    float g = getDisolveValue(Input.UV * g_disolveScale + g_disolveOffset);
    if( g < g_threshold ){ discard; } 

    float4 texCol = g_texture.Sample(g_sampler, Input.UV);
    return texCol * Input.Color;
}