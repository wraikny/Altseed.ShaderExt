Texture2D g_noiseTexture : register( t1 );
SamplerState g_samplerNoise : register( s1 );

float g_zOffset;
float g_noiseType;
float2 g_noiseScale;
float2 g_noiseOffset;

float calcNoise(float2 uv)
{
    int source = g_noiseType;

    if(source != -1) {
        uv *= g_resolution;
    }
    uv = uv * g_noiseScale + g_noiseOffset;

    float3 pos = float3(uv, g_zOffset);
    
    float result = 0.0;
    switch(source)
    {
        case -1: {
            float4 m = g_noiseTexture.Sample(g_samplerNoise, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
        case 0: {
            result = random(pos);
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