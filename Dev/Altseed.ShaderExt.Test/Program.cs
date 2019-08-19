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
            asd.Engine.OpenTool();


            Altseed.ShaderExt.Utils.AddPackage();

            //var ws = asd.Engine.WindowSize.To2DF();
            //var obj = new ShaderObject2DSimple("Altseed.ShaderExt.Shaders/noise/perlinnoise.hlsl", null)
            //{
            //    Size = ws * 0.5f,
            //    Position = ws * 0.5f,
            //    CenterPosition = ws * 0.25f,
            //    Color = new asd.Color(255, 0, 0)
            //};

            var obj = new TextureObject2DDisolve
            {
                DisolveSource = DisolveSource.Value,
                DisolveScale = new asd.Vector2DF(10.0f, 10.0f),
                Texture = asd.Engine.Graphics.CreateTexture2D("AmCrDownloadCard.png")
            };

            asd.Engine.AddObject2D(obj);

            float count = 0.0f;
            while(asd.Engine.DoEvents())
            {
                if(asd.Engine.Tool.Begin("x"))
                {
                    asd.Engine.Tool.Text("Threshold: " + obj.Threshold.ToString());
                    asd.Engine.Tool.End();
                }

                obj.Threshold = ((float)Math.Sin(count) + 1.0f) / 2.0f;
                count += 0.01f;

                asd.Engine.Update();
            }

            asd.Engine.CloseTool();
            asd.Engine.Terminate();
        }
    }
}
