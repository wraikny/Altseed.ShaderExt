// http://nn-hokuson.hatenablog.com/entry/2017/01/27/195659

float random (vec3 p) { 
    return fract(sin(dot(p, vec3(12.9898, 78.233, 323.3849))) * 43758.5453);
}

float blockNoise(vec3 st)
{
    return random(floor(st));
}

float valueNoise(vec3 st)
{
    vec3 p = floor(st);

    float v000 = random(p + vec3(0, 0, 0));
    float v100 = random(p + vec3(1, 0, 0));

    float v010 = random(p + vec3(0, 1, 0));
    float v110 = random(p + vec3(1, 1, 0));

    float v001 = random(p + vec3(0, 0, 1));
    float v101 = random(p + vec3(1, 0, 1));

    float v011 = random(p + vec3(0, 1, 1));
    float v111 = random(p + vec3(1, 1, 1));
    
    vec3 f = fract(st);
    vec3 u = f * f * (3.0 - 2.0 * f);

    float v000_100 = mix(v000, v100, u.x);
    float v010_110 = mix(v010, v110, u.x);
    float v001_101 = mix(v001, v101, u.x);
    float v011_111 = mix(v011, v111, u.x);
    return mix(
        mix(v000_100, v010_110, u.y),
        mix(v001_101, v011_111, u.y),
        u.z
    );
}

vec2 random2(vec3 st){
    vec2 xy = vec2( dot(st, vec3(127.1, 311.7, 54.2948)), dot(st, vec3(269.5, 183.3, 92.2043)) );
    return -1.0 + 2.0 * fract(sin(xy) * (43758.5453123) );
}

float perlinNoise(vec3 st)
{
    vec3 p = floor(st);

    float v000 = random(p + vec3(0, 0, 0));
    float v100 = random(p + vec3(1, 0, 0));

    float v010 = random(p + vec3(0, 1, 0));
    float v110 = random(p + vec3(1, 1, 0));

    float v001 = random(p + vec3(0, 0, 1));
    float v101 = random(p + vec3(1, 0, 1));

    float v011 = random(p + vec3(0, 1, 1));
    float v111 = random(p + vec3(1, 1, 1));

    vec3 f = fract(st);
    vec3 u = f * f * (3.0 - 2.0 * f);

    float a = mix( dot( v000, f - vec3(0,0,0) ), dot( v100, f - vec3(1,0,0) ), u.x );
    float b = mix( dot( v010, f - vec3(0,1,0) ), dot( v110, f - vec3(1,1,0) ), u.x );
    float c = mix( dot( v001, f - vec3(0,0,1) ), dot( v101, f - vec3(1,0,1) ), u.x );
    float d = mix( dot( v011, f - vec3(0,1,1) ), dot( v111, f - vec3(1,1,1) ), u.x );

    return mix(
        mix(a, b, u.y),
        mix(c, d, u.y),
    u.z) + 0.5;
}

float fBm(vec3 st) 
{
    float f = 0;
    vec3 q = st;

    f += 0.5000 * perlinNoise( q ); q = q * 2.01;
    f += 0.2500 * perlinNoise( q ); q = q * 2.02;
    f += 0.1250 * perlinNoise( q ); q = q * 2.03;
    f += 0.0625 * perlinNoise( q ); q = q * 2.01;

    return f;
}
