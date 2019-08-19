Texture2D g_texture : register( t0 );
Texture2D g_disolveTexture : register( t0 );
SamplerState g_sampler : register( s0 );


float g_threshold;


struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

float4 main( const PS_Input Input ) : SV_Target
{
    float4 m = g_disolveTexture.Sample(g_sampler, Input.UV);

    float g = m.r * 0.2 + m.g * 0.7 + m.b * 0.1;
    if( g < g_threshold ){ discard; } 

    float4 texCol = g_texture.Sample(g_sampler, Input.UV);
    return texCol * Input.Color;
}