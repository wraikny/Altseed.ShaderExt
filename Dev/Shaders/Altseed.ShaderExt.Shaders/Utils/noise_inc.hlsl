// http://nn-hokuson.hatenablog.com/entry/2017/01/27/195659

float random (float3 p) { 
    return frac(sin(dot(p, float3(12.9898, 78.233, 323.3849))) * 43758.5453);
}

float blockNoise(float3 st)
{
    return random(floor(st));
}

float valueNoise(float3 st)
{
    float3 p = floor(st);

    float v000 = random(p + float3(0, 0, 0));
    float v100 = random(p + float3(1, 0, 0));

    float v010 = random(p + float3(0, 1, 0));
    float v110 = random(p + float3(1, 1, 0));

    float v001 = random(p + float3(0, 0, 1));
    float v101 = random(p + float3(1, 0, 1));

    float v011 = random(p + float3(0, 1, 1));
    float v111 = random(p + float3(1, 1, 1));
    
    float3 f = frac(st);
    float3 u = f * f * (3.0 - 2.0 * f);

    float v000_100 = lerp(v000, v100, u.x);
    float v010_110 = lerp(v010, v110, u.x);
    float v001_101 = lerp(v001, v101, u.x);
    float v011_111 = lerp(v011, v111, u.x);
    return lerp(
        lerp(v000_100, v010_110, u.y),
        lerp(v001_101, v011_111, u.y),
        u.z
    );
}

float2 random2(float3 st) {
    float2 xy = float2(
        dot(st, float3(127.1, 311.7, 54.2948))
        , dot(st, float3(269.5, 183.3, 92.2043))
    );
    return -1.0 + 2.0 * frac(sin(xy) * (43758.5453123) );
}

float perlinNoise(float3 st)
{
    float3 p = floor(st);

    float v000 = random(p + float3(0, 0, 0));
    float v100 = random(p + float3(1, 0, 0));

    float v010 = random(p + float3(0, 1, 0));
    float v110 = random(p + float3(1, 1, 0));

    float v001 = random(p + float3(0, 0, 1));
    float v101 = random(p + float3(1, 0, 1));

    float v011 = random(p + float3(0, 1, 1));
    float v111 = random(p + float3(1, 1, 1));

    float3 f = frac(st);
    float3 u = f * f * (3.0 - 2.0 * f);

    float a = lerp( dot( v000, f - float3(0,0,0) ), dot( v100, f - float3(1,0,0) ), u.x );
    float b = lerp( dot( v010, f - float3(0,1,0) ), dot( v110, f - float3(1,1,0) ), u.x );
    float c = lerp( dot( v001, f - float3(0,0,1) ), dot( v101, f - float3(1,0,1) ), u.x );
    float d = lerp( dot( v011, f - float3(0,1,1) ), dot( v111, f - float3(1,1,1) ), u.x );

    return lerp(
        lerp(a, b, u.y),
        lerp(c, d, u.y),
    u.z) + 0.5;
}

float fBm(float3 st) 
{
    float f = 0;
    float3 q = st;

    f += 0.5000 * perlinNoise( q ); q = q * 2.01;
    f += 0.2500 * perlinNoise( q ); q = q * 2.02;
    f += 0.1250 * perlinNoise( q ); q = q * 2.03;
    f += 0.0625 * perlinNoise( q ); q = q * 2.01;

    return f;
}
