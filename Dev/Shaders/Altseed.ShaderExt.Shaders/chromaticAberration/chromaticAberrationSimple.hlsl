Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );

float2 g_offset_red;
float2 g_offset_green;
float2 g_offset_blue;

float2 g_offset;
float2 g_scale;

float g_alpha;

#include "../Utils/template.hlsl"

float4 main( const PS_Input Input ) : SV_Target
{
    float2 uv = Input.UV * g_scale + g_offset;

    float2 red = g_texture.Sample(g_sampler, uv + g_offset_red).ra;
    float2 green = g_texture.Sample(g_sampler, uv + g_offset_green).ga;
    float2 blue = g_texture.Sample(g_sampler, uv + g_offset_blue).ba;

    float3 texCol = g_texture.Sample(g_sampler, Input.UV).rgb;
    return float4(lerp(texCol, float3(red.x, green.x, blue.x), g_alpha), 1.0);
}
