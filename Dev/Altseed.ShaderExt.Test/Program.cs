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

            var scene = new asd.Scene();
            var layer = new asd.Layer2D();
            scene.AddLayer(layer);

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
                Threshold = 0.5f,
                NoiseSource = NoiseSource.PerlinNoise,
                NoiseSrc = new asd.RectF(0.0f, 0.0f, 10.0f, 10.0f),
                Texture = asd.Engine.Graphics.CreateTexture2D("AmCrDownloadCard.png")
            };

            var noise = new ShaderObject2DNoise
            {
                Size = new asd.Vector2DF(300.0f, 300.0f),
                NoiseType = NoiseType.Fbm,
                NoiseSrc = new asd.RectF(0.0f, 0.0f, 5.0f, 5.0f),
                IsDoubled = true,
                Threshold = 0.5f,
                BackGround = Background.Color(0, 0, 0)
                //AlphaBlend = asd.AlphaBlendMode.Add
            };

            layer.AddObject(obj);
            layer.AddObject(noise);

            var pe = new PostEffectChromaticAberrationSimple();
            layer.AddPostEffect(pe);

            asd.Engine.ChangeScene(scene);

            float count = 0.0f;
            while(asd.Engine.DoEvents())
            {
                if(asd.Engine.Tool.Begin("x"))
                {
                    asd.Engine.Tool.Text("Threshold: " + obj.Threshold.ToString());
                    asd.Engine.Tool.End();
                }

                //obj.ZOffset = count;
                noise.ZOffset = count;
                noise.NoiseSrc = new asd.RectF(count, count, 5.0f, 5.0f);

                obj.Threshold = ((float)Math.Sin(count) + 1.0f) / 2.0f;
                pe.OffsetRed = new asd.Vector2DF(0.1f, 0.0f) { Radian = count };
                pe.OffsetGreen = new asd.Vector2DF(0.1f, 0.0f) { Radian = 2.0f * count };
                pe.OffsetBlue = new asd.Vector2DF(0.1f, 0.0f) { Radian = -count };
                //pe.SetZoom((float)Math.Sin(count) * 0.25f + 1.25f);
                count += 0.01f;

                asd.Engine.Update();
            }

            asd.Engine.CloseTool();
            asd.Engine.Terminate();
        }
    }
}
