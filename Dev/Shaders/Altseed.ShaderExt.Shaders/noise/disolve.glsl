#include "../Utils/template.glsl"
#include "../Utils/noise_inc.glsl"
#include "noise_core.glsl"

uniform sampler2D g_texture;
uniform float g_threshold;
uniform float g_backgroundSource;
uniform vec4 g_backgroundColor;

vec4 main_()
{
    float g = calcNoise(inUV);
    if( g <= g_threshold ){
        int source = int(g_backgroundSource);
        if(source == 0) discard;
        else if(source == 1) return g_backgroundColor;
    }

    vec4 texCol = texture(g_texture, inUV);
    return texCol * inColor;
}

void main()
{
    outOutput = main_();
}