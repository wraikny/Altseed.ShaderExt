#include "../Utils/template.hlsl"
#include "../Utils/noise_inc.hlsl"

float g_zOffset;
float g_noiseType;
float2 g_noiseScale;
float2 g_noiseOffset;

float g_backgroundSource;
float4 g_backgroundColor;

float g_threshold;
float g_isDoubled;

float calcNoise(float2 uv)
{
    int source = g_noiseType;

    uv = (uv * g_resolution * g_noiseScale + g_noiseOffset);
    float3 pos = float3(uv, g_zOffset);
    
    float result = 0.0;
    switch(source)
    {
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
    float g = calcNoise(Input.UV);
    if(int(g_isDoubled) == 0) {
        return g * Input.Color;
    } else {
        if( g <= g_threshold ){
            int source = g_backgroundSource;
            if(source == 0) discard;
            else if(source == 1) return g_backgroundColor;
        }
        return Input.Color;
    }
}