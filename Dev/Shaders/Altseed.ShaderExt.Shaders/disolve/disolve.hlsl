#include "../Utils/noise_inc.hlsl"

Texture2D g_texture : register( t0 );
Texture2D g_noiseTexture : register( t1 );
SamplerState g_sampler : register( s0 );

float2 g_resolution;
// float g_second;

float g_threshold;
float g_noiseSource;
float2 g_disolveScale;
float2 g_disolveOffset;

float g_backgroundSource;
float4 g_backgroundColor;

float getDisolveValue(float2 uv)
{
    int source = g_noiseSource;

    uv = (source == 0)
        ? (uv * g_disolveScale + g_disolveOffset)
        : (uv * g_resolution * g_disolveScale + g_disolveOffset)
    ;
    
    float result = 0.0;
    switch(source)
    {
        case -1: {
            float4 m = g_noiseTexture.Sample(g_sampler, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
        case 0: {
            result = saturate( random(uv) );
            break;
        }
        case 1: {
            result = blockNoise(uv);
            break;
        }
        case 2: {
            result = valueNoise(uv);
            break;
        }
        case 3: {
            result = perlinNoise(uv);
            break;
        }
        case 4: {
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
    float g = getDisolveValue(Input.UV);
    if( g < g_threshold ){
        switch( int(g_backgroundSource) )
        {
            case 0: discard;
            case 1: return g_backgroundColor;
        }
    } 

    float4 texCol = g_texture.Sample(g_sampler, Input.UV);
    return texCol * Input.Color;
}