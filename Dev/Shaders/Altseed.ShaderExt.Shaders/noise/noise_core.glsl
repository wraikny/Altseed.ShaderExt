uniform sampler2D g_noiseTexture;
uniform float g_zOffset;
uniform float g_noiseType;
uniform vec2 g_noiseScale;
uniform vec2 g_noiseOffset;

float calcNoise(vec2 uv)
{
    int source = int(g_noiseType);

    if(source != -1) {
        uv = vec2(uv.x, 1.0 - uv.y);
        uv *= g_resolution;
    }
    uv = uv * g_noiseScale + g_noiseOffset;

    vec3 pos = vec3(uv, g_zOffset);
    
    float result = 0.0;
    switch(source)
    {
        case -1: {
            vec4 m = texture(g_noiseTexture, uv);
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
    return clamp(result, 0.0, 1.0);
}