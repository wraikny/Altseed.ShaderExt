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

    float2 red = getTexInside(g_texture, g_sampler, uv - g_offset_red * g_resolution).ra;
    float2 green = getTexInside(g_texture, g_sampler, uv - g_offset_green * g_resolution).ga;
    float2 blue = getTexInside(g_texture, g_sampler, uv - g_offset_blue * g_resolution).ba;

    float3 texCol = g_texture.Sample(g_sampler, Input.UV).rgb;
    return float4(lerp(texCol, float3(red.x, green.x, blue.x), g_alpha), 1.0);
}
