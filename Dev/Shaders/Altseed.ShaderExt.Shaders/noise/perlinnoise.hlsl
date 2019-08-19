
#include "../Utils/noise_inc.hlsl"

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

float g_second;
float2 g_resolution;

float4 main( const PS_Input Input ) : SV_Target
{
    float x = perlinNoise(Input.UV * 10.0 * g_resolution + g_second.xx);
    return float4(x * Input.Color.rgb, Input.Color.a);
}