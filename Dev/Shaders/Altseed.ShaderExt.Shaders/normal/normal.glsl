#include "../Utils/template.glsl"

uniform sampler2D g_texture;
uniform sampler2D g_normalMap;

uniform vec4 g_lightPos0;
uniform vec3 g_lightColor0;
uniform float g_zPos;

const vec3 eyeDir = vec3(0.0, 0.0, -1.0);

vec3 calcLight(vec3 pos, vec3 normal, vec3 color, vec4 lightPos, vec3 lightColor)
{
    vec3 lightDir = (int(lightPos.w) == 0)
        ? normalize(pos - lightPos.xyz) // Point Light
        : lightPos.xyz; // Directional Light

    float diffuse = clamp(dot(-normal, lightDir), 0.1, 1.0);
    vec3 halfLE = normalize(lightDir - eyeDir);
    float specular = pow(clamp(dot(normal, halfLE), 0.0, 1.0), 100.0);

    return color * lightColor * diffuse + specular * lightColor;
}

vec4 main_()
{
    vec2 uv = inUV;

    vec4 texCol = texture(g_texture, uv) * inColor;

    vec3 normal;
    {
        normal = texture(g_normalMap, uv).xyz * 2.0 - 0.5;
        normal.xy = normalize(dirToObject(normal.xy)) * length(normal.xy);
        normal = normalize(normal);
    }
    // return vec4(normal, 1.0);

    vec3 pos = vec3(uvToObjectPosition(uv), g_zPos);

    vec3 color = calcLight(
        pos, normal, texCol.rgb,
        g_lightPos0, g_lightColor0
    );

    return vec4(color, 1.0);
}

void main()
{
    outOutput = main_();
}