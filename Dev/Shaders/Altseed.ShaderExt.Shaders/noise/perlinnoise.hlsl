#include "../Utils/noise_inc.hlsl"

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

int g_second;
float2 g_resolution;

float4 main( const PS_Input Input ) : SV_Target
{
    float x = perlinNoise(Input.UV * g_resolution + float2(g_second, g_second));
    return float4(x, x, x, 1.0);
}