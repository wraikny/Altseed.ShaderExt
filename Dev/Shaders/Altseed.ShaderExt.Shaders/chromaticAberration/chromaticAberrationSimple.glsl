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

    vec2 red = texture(g_texture, uv + g_offset_red).ra;
    vec2 green = texture(g_texture, uv + g_offset_green).ga;
    vec2 blue = texture(g_texture, uv + g_offset_blue).ba;

    vec3 texCol = texture(g_texture, inUV).rgb;
    return vec4(lerp(texCol, vec3(red.x, green.x, blue.x), g_alpha), 1.0);
}

void main()
{
    outOutput = main_();
}