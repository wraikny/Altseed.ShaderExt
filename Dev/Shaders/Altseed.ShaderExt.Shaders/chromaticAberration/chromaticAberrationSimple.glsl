uniform sampler2D g_texture;

uniform vec2 g_offset_red;
uniform vec2 g_offset_green;
uniform vec2 g_offset_blue;

uniform vec2 g_offset;
uniform vec2 g_scale;

uniform float g_alpha;

#include "../Utils/template.glsl"

vec4 main_()
{
    vec2 uv = inUV * g_scale + g_offset;

    vec2 red = getTexInside(g_texture, uv + g_offset_red * g_resolution).ra;
    vec2 green = getTexInside(g_texture, uv + g_offset_green * g_resolution).ga;
    vec2 blue = getTexInside(g_texture, uv + g_offset_blue * g_resolution).ba;

    vec3 texCol = texture(g_texture, inUV).rgb;
    return vec4(lerp3(texCol, vec3(red.x, green.x, blue.x), g_alpha), 1.0);
}

void main()
{
    outOutput = main_();
}
