#include "../Utils/noise_inc.glsl"
#include "../Utils/template.glsl"

uniform sampler2D g_texture;
uniform sampler2D g_noiseTexture;

uniform float g_threshold;
uniform float g_noiseSource;
uniform vec2 g_disolveScale;
uniform vec2 g_disolveOffset;

uniform float g_backgroundSource;
uniform vec4 g_backgroundColor;

#include "../Utils/inout.glsl"

float getDisolveValue(vec2 uv)
{
    int source = g_noiseSource;

    uv = (source == -1)
        ? (uv * g_disolveScale + g_disolveOffset)
        : (uv * g_resolution * g_disolveScale + g_disolveOffset)
    ;
    
    float result = 0.0;
    switch(source)
    {
        case -1: {
            vec4 m = texture(g_noiseTexture, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
        case 0: {
            result = saturate( random(uv) );
            break;
        }
        case 1: {
            result = blockNoise(uv);
            break;
        }
        case 2: {
            result = valueNoise(uv);
            break;
        }
        case 3: {
            result = perlinNoise(uv);
            break;
        }
        case 4: {
            result = fBm(uv);
            break;
        }
    }
    
    return fmod(result, 1.0);
}

void main()
{
    float g = getDisolveValue(inUV);
    if( g <= g_threshold ){
        int source = g_backgroundSource;
        if(source == 0) discard;
        else if(source == 1) return g_backgroundColor;
    } 

    vec4 texCol = texture(g_texture, inUV);
    outOutput = texCol * Input.Color;
}