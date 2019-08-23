#include "../Utils/template.hlsl"
#include "../Utils/noise_inc.hlsl"

Texture2D g_texture : register( t0 );
Texture2D g_noiseTexture : register( t1 );
SamplerState g_sampler : register( s0 );

float g_zOffset;
float g_threshold;
float g_noiseSource;
float2 g_noiseScale;
float2 g_noiseOffset;

float g_backgroundSource;
float4 g_backgroundColor;

float getDisolveValue(float2 uv)
{
    int source = g_noiseSource;

    uv = (source == 0)
        ? (uv * g_noiseScale + g_noiseOffset)
        : (uv * g_resolution * g_noiseScale + g_noiseOffset)
    ;

    float3 pos = float3(uv, g_zOffset);
    
    float result = 0.0;
    switch(source)
    {
        case -1: {
            float4 m = g_noiseTexture.Sample(g_sampler, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
        case 0: {
            result = saturate( random(pos) );
            break;
        }
        case 1: {
            result = blockNoise(pos);
            break;
        }
        case 2: {
            result = valueNoise(pos);
            break;
        }
        case 3: {
            result = perlinNoise(pos);
            break;
        }
        case 4: {
            result = fBm(pos);
            break;
        }
    }
    return saturate(result);
}

float4 main( const PS_Input Input ) : SV_Target
{
    float g = getDisolveValue(Input.UV);
    if( g <= g_threshold ){
        int source = g_backgroundSource;
        if(source == 0) discard;
        else if(source == 1) return g_backgroundColor;
    } 

    float4 texCol = g_texture.Sample(g_sampler, Input.UV);
    return texCol * Input.Color;
}