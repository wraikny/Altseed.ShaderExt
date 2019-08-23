uniform float g_second;
uniform vec2 g_resolution;

in vec4 inPosition;
in vec2 inUV;
in vec4 inColor;

out vec4 outOutput;

float lerp1(float a, float b, float k)
{
    return (a * k + b * (1.0 - k));
}

vec2 lerp1(vec2 a, vec2 b, float k)
{
    return (a * k + b * (1.0 - k));
}

vec3 lerp1(vec3 a, vec3 b, float k)
{
    return (a * k + b * (1.0 - k));
}

vec4 lerp1(vec4 a, vec4 b, float k)
{
    return (a * k + b * (1.0 - k));
}


bool isOutofUV(vec2 uv)
{
    return(
        (uv.x < 0.0 || 1.0 < uv.x)
        || (uv.y < 0.0 || 1.0 < uv.y)
    );
}

vec4 getTexInside(sampler2D tex, vec2 uv)
{
    return lerp4(
        texture(tex, uv),
        vec4(0.0),
        isOutOfUV(uv)
    );
}