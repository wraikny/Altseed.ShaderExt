#include "../Utils/template.glsl"
#include "../Utils/noise_inc.glsl"
#include "noise_core.glsl"

uniform sampler2D g_texture;
uniform float g_threshold;
uniform float g_backgroundSource;
uniform vec4 g_backgroundColor;

uniform float g_edgeWidth;
uniform vec4 g_edgeColor;
uniform float g_edgeBrightness;

vec4 main_()
{
    float g = calcNoise(inUV);

    float edgeUp = g_threshold + g_edgeWidth * g_threshold;
    float edgeDown = edgeUp - g_edgeWidth;

    if( g <= edgeDown ){
        int source = int(g_backgroundSource);
        if(source == 0) discard;
        else if(source == 1) return g_backgroundColor;
    } else if (g <= edgeUp ) {
        return vec4(g_edgeColor.rgb * g_edgeBrightness, g_edgeColor.a);
    }

    vec4 texCol = texture(g_texture, inUV);
    return texCol * inColor;
}

void main()
{
    outOutput = main_();
}