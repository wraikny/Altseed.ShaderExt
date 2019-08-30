uniform float g_second;
uniform vec2 g_resolution;

in vec4 inPosition;
in vec2 inUV;
in vec4 inColor;

out vec4 outOutput;

bool isOutofUV(vec2 uv)
{
    return(
        (uv.x < 0.0 || 1.0 < uv.x)
        || (uv.y < 0.0 || 1.0 < uv.y)
    );
}

vec4 getTexInside(sampler2D tex, vec2 uv)
{
    return mix(
        texture(tex, uv),
        vec4(0.0),
        isOutOfUV(uv)
    );
}