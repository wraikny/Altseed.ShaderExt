#include "../Utils/template.hlsl"
#include "../Utils/noise_inc.hlsl"

Texture2D g_texture : register( t0 );
Texture2D g_normalMap : register( t1 );
SamplerState g_sampler : register( s0 );

float4 g_lightPos0;
float3 g_lightColor0;
float g_zPos;
float g_HDR; // bool

static const float3 eyeDir = float3(0.0, 0.0, -1.0);

float3 calcLight(float3 pos, float3 normal, float3 color, float4 lightPos, float3 lightColor)
{
    float3 lightDir = (int(lightPos.w) == 0)
        ? normalize(pos - lightPos.xyz) // Point Light
        : normalize(lightPos.xyz); // Directional Light
    
    float3 reflectedLightDir = reflect(lightDir, normal);

    // return reflectedLightDir;
    
    float d = -dot(reflectedLightDir, eyeDir);

    return (color * lightColor * d);
    if(int(g_HDR) == 0)
    {
        return saturate(color * lightColor * d);
    }
    else
    {
        return (color * lightColor * d);
    }
}

float4 main( const PS_Input Input ) : SV_Target
{
    float4 texCol = g_texture.Sample(g_sampler, Input.UV) * Input.Color;
    float3 normal = normalize(g_normalMap.Sample(g_sampler, Input.UV).xyz * 2.0 - 1.0);
    float3 pos = float3(g_position + g_size * Input.UV, g_zPos);

    float3 color = calcLight(
        pos, normal, texCol.rgb,
        g_lightPos0, g_lightColor0
    );

    return float4(color, 1.0);
}