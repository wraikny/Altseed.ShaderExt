#include "../Utils/template.glsl"
#include "../Utils/noise_inc.glsl"
#include "noise_core.glsl"

uniform float g_backgroundSource;
uniform vec4 g_backgroundColor;

uniform float g_threshold;
uniform float g_isDoubled;

vec4 main_()
{
    float g = calcNoise(inUV);
    if(int(g_isDoubled) == 0) {
        return g * inColor;
    } else {
        if( g <= g_threshold ){
            int source = int(g_backgroundSource);
            if(source == 0) discard;
            else if(source == 1) return g_backgroundColor;
        }
        return inColor;
    }
}
void main()
{
    outOutput = main_();
}