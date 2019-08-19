using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            asd.Engine.Initialize("Altseed.ShaderExt.Test", 800, 450, new asd.EngineOption());

            var obj = new asd.TextureObject2D();

            string shaderString = @"
Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

float4 main( const PS_Input Input ) : SV_Target
{
    float4 color = g_texture.Sample(g_sampler, Input.UV);
    return float4( 1.0 - color.x, 1.0 - color.y, 1.0 - color.z, color.w);
}
";

            var shader = asd.Engine.Graphics.CreateShader2D(shaderString);
            var mat = asd.Engine.Graphics.CreateMaterial2D(shader);
            var texture = Altseed.ShaderExt.Utils.CreateTexture2D("wraikny_icon.jpg");
            mat.SetTexture2D("g_texture", texture);
            mat.SetTextureFilterType("g_texture", asd.TextureFilterType.Linear);
            mat.SetTextureWrapType("g_texture", asd.TextureWrapType.Repeat);
            //obj.Material = mat;
            obj.Texture = texture;

            asd.Engine.AddObject2D(obj);

            while(asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
