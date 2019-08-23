Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );

float2 g_offset_red;
float2 g_offset_green;
float2 g_offset_blue;

float2 g_caOffset;
float2 g_caScale;

float g_caAlpha;

#include "../Utils/template.hlsl"

float4 main( const PS_Input Input ) : SV_Target
{
    float2 uv = Input.UV * g_caScale + g_caOffset;

    float2 red = g_texture.Sample(g_sampler, uv + g_offset_red).ra;
    float2 green = g_texture.Sample(g_sampler, uv + g_offset_green).ga;
    float2 blue = g_texture.Sample(g_sampler, uv + g_offset_blue).ba;

    return float3(red.x, green.x, blue.x, g_caAlpha);
}
