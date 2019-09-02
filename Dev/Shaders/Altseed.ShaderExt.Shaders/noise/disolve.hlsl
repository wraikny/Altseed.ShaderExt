#include "../Utils/template.hlsl"
#include "../Utils/noise_inc.hlsl"
#include "noise_core.hlsl"

Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );
float g_threshold;
float g_backgroundSource;
float4 g_backgroundColor;

float4 main( const PS_Input Input ) : SV_Target
{
    float g = calcNoise(Input.UV);
    if( g <= g_threshold ){
        int source = g_backgroundSource;
        if(source == 0) discard;
        else if(source == 1) return g_backgroundColor;
    } 

    float4 texCol = g_texture.Sample(g_sampler, Input.UV);
    return texCol * Input.Color;
}