#include "../Utils/template.hlsl"

Texture2D g_texture : register( t0 );
Texture2D g_normalMap : register( t1 );
SamplerState g_sampler : register( s0 );
SamplerState g_samplerNormal : register( s1 );

float4 g_lightPos0;
float3 g_lightColor0;
float g_zPos;

static const float3 eyeDir = float3(0.0, 0.0, -1.0);

float3 calcLight(float3 pos, float3 normal, float3 color, float4 lightPos, float3 lightColor)
{
    float3 lightDir = (int(lightPos.w) == 0)
        ? normalize(pos - lightPos.xyz) // Point Light
        : lightPos.xyz; // Directional Light

    float diffuse = clamp(dot(-normal, lightDir), 0.1, 1.0);
    float3 halfLE = normalize(lightDir - eyeDir);
    float specular = pow(clamp(dot(normal, halfLE), 0.0, 1.0), 100.0);
    // return specular.xxx;

    return color * lightColor * diffuse + specular * lightColor;
}

float4 main( const PS_Input Input ) : SV_Target
{
    float2 uv = Input.UV;

    float4 texCol = g_texture.Sample(g_sampler, uv) * Input.Color;

    float3 normal;
    {
        normal = g_normalMap.Sample(g_samplerNormal, uv).xyz * 2.0 - 0.5;
        normal.xy = normalize(dirToObject(normal.xy)) * length(normal.xy);
        normal = normalize(normal);
    }

    float3 pos = float3(uvToObjectPosition(uv), g_zPos);

    float3 color = calcLight(
        pos, normal, texCol.rgb,
        g_lightPos0, g_lightColor0
    );

    return float4(color, 1.0);
}
