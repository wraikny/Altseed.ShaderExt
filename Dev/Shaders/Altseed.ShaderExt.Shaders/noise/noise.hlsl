#include "../Utils/template.hlsl"
#include "../Utils/noise_inc.hlsl"
#include "noise_core.hlsl"

float g_backgroundSource;
float4 g_backgroundColor;

float g_threshold;
float g_isDoubled;

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