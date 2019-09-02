uniform float g_second;
uniform vec2 g_resolution;
uniform vec2 g_size;

// --- Only ShaderObject ---
uniform vec2 g_position;
// uniform float g_angle;
uniform vec2 g_sizeX;
uniform vec2 g_sizeY;

vec2 dirToObject(vec2 v) {
    return g_sizeX * v.x + g_sizeY * v.y;
}

vec2 uvToObjectPosition(vec2 uv) {
    return (g_position + dirToObject(vec2(uv.x, 1.0 - uv.y)));
}
// -------------------------
bool isOutOfUV(vec2 uv)
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
        isOutOfUV(uv) ? 1.0 : 0.0
    );
}

in vec4 inPosition;
in vec2 inUV;
in vec4 inColor;
out vec4 outOutput;

vec2 getUVSameHLSL() {
    return vec2(inUV.x, 1.0 - inUV.y);
}