// http://nn-hokuson.hatenablog.com/entry/2017/01/27/195659

float __lerp1(float a, float b, float x)
{
    return (a * x + b * (1.0 - x));
}

float random (vec2 p) { 
    return fract(sin(dot(p, vec2(12.9898,78.233))) * 43758.5453);
}

float blockNoise(vec2 st)
{
    vec2 p = floor(st);
    return random(p);
}

float valueNoise(vec2 st)
{
    vec2 p = floor(st);
    vec2 f = fract(st);

    float v00 = random(p + vec2(0, 0));
    float v10 = random(p + vec2(1, 0));
    float v01 = random(p + vec2(0, 1));
    float v11 = random(p + vec2(1, 1));
    
    vec2 u = f * f * (3.0 - 2.0 * f);            

    float v0010 = __lerp1(v00, v10, u.x);
    float v0111 = __lerp1(v01, v11, u.x);
    return __lerp1(v0010, v0111, u.y);
}

vec2 random2(vec2 st){
    st = vec2( dot(st, vec2(127.1,311.7)), dot(st, vec2(269.5,183.3)) );
    return -1.0 + 2.0 * frac(sin(st) * 43758.5453123);
}

float perlinNoise(vec2 st) 
{
    vec2 p = floor(st);
    vec2 f = frac(st);
    vec2 u = f * f * (3.0 - 2.0 * f);

    vec2 v00 = random2(p + vec2(0,0));
    vec2 v10 = random2(p + vec2(1,0));
    vec2 v01 = random2(p + vec2(0,1));
    vec2 v11 = random2(p + vec2(1,1));

    return __lerp1(
        __lerp1( dot( v00, f - vec2(0,0) ), dot( v10, f - vec2(1,0) ), u.x ),
        __lerp1( dot( v01, f - vec2(0,1) ), dot( v11, f - vec2(1,1) ), u.x ), 
    u.y) + 0.5f;
}

float fBm(vec2 st) 
{
    float f = 0;
    vec2 q = st;

    f += 0.5000 * perlinNoise( q ); q = q * 2.01;
    f += 0.2500 * perlinNoise( q ); q = q * 2.02;
    f += 0.1250 * perlinNoise( q ); q = q * 2.03;
    f += 0.0625 * perlinNoise( q ); q = q * 2.01;

    return f;
}
