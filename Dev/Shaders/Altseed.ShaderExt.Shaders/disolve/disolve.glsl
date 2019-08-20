#include "../Utils/noise_inc.glsl"

uniform sampler2D g_texture;
uniform sampler2D g_disolveTexture;

uniform float g_threshold;
uniform float g_disolveSource;
uniform vec2 g_disolveScale;
uniform vec2 g_disolveOffset;

float getDisolveValue(vec2 uv)
{
    int source = g_disolveSource;
    
    float result = 0.0;
    switch(source)
    {
        case 0: {
            vec4 m = texture(g_disolveTexture, uv);
            result = (m.r * 0.2 + m.g * 0.7 + m.b * 0.1);
            break;
        }
        case 1: {
            result = saturate( random(uv) );
            break;
        }
        case 2: {
            result = blockNoise(uv);
            break;
        }
        case 3: {
            result = valueNoise(uv);
            break;
        }
        case 4: {
            result = perlinNoise(uv);
            break;
        }
        case 5: {
            result = fBm(uv);
            break;
        }
    }
    return saturate(result);
}

in vec4 inPosition;
in vec2 inUV;
in vec4 inColor;

out vec4 outOutput;

void main()
{
    float g = getDisolveValue(inUV * g_disolveScale + g_disolveOffset);
    if( g < g_threshold ){ discard; } 

    vec4 texCol = texture(g_texture, inUV);
    outOutput = texCol * Input.Color;
}