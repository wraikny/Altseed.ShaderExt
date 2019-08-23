#include "../Utils/template.glsl"
#include "../Utils/noise_inc.glsl"

uniform sampler2D g_texture;
uniform sampler2D g_noiseTexture;

uniform float g_zOffset;
uniform float g_threshold;
uniform float g_noiseType;
uniform vec2 g_disolveScale;
uniform vec2 g_disolveOffset;

uniform float g_backgroundSource;
uniform vec4 g_backgroundColor;

#include "../Utils/inout.glsl"

float calcNoise(vec2 uv)
{
    int source = g_noiseType;

    uv = (source == -1)
        ? (uv * g_disolveScale + g_disolveOffset)
        : (uv * g_resolution * g_disolveScale + g_disolveOffset)
    ;

    vec2 pos = vec2(uv, 0.0, g_zOffset);
    
    float result = 0.0;
    switch(source)
    {
        case -1: {
            vec4 m = texture(g_noiseTexture, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
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
    
    return fmod(result, 1.0);
}

vec4 main_()
{
    float g = calcNoise(inUV);
    if( g <= g_threshold ){
        int source = g_backgroundSource;
        if(source == 0) discard;
        else if(source == 1) return g_backgroundColor;
    } 

    vec4 texCol = texture(g_texture, inUV);
    return texCol * Input.Color;
}

void main()
{
    outOutput = main_();
}