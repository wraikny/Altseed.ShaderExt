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

            var scene = new asd.Scene() { HDRMode = true };
            var layer = new asd.Layer2D();
            scene.AddLayer(layer);

            var testTex = asd.Engine.Graphics.CreateTexture2D("AmCrDownloadCard.png");

            //var obj = new TextureObject2DDisolve
            //{
            //    Threshold = 0.5f,
            //    NoiseSource = NoiseSource.PerlinNoise,
            //    NoiseSrc = new asd.RectF(0.0f, 0.0f, 10.0f, 10.0f),
            //    Texture = testTex
            //};

            //var noise = new ShaderObject2DNoise
            //{
            //    Color = new asd.Color(255, 0, 255),
            //    Size = new asd.Vector2DF(300.0f, 300.0f),
            //    NoiseType = NoiseType.Fbm,
            //    NoiseSrc = new asd.RectF(0.0f, 0.0f, 5.0f, 5.0f),
            //    IsDoubled = true,
            //    Threshold = 0.5f,
            //    BackGround = Background.Color(0, 0, 0)
            //    //AlphaBlend = asd.AlphaBlendMode.Add
            //};

            var ws = asd.Engine.WindowSize.To2DF();

            var normalObj = new TextureObject2DNormalMap()
            {
                Texture = testTex,
                ZPos = 0.0f,
                NormalMap = asd.Engine.Graphics.CreateTexture2D("wgld_normalmap.png"),
                CenterPosition = testTex.Size.To2DF() * 0.5f,
                Position = ws * 0.5f,
                //Angle = 45.0f
            };
            
            //layer.AddObject(obj);
            //layer.AddObject(noise);
            layer.AddObject(normalObj);

            //var pe = new PostEffectChromaticAberrationSimple();
            //layer.AddPostEffect(pe);

            asd.Engine.ChangeScene(scene);

            //normalObj.Light0 = LightType.Directional(1.0f, 1.0f, -1.0f);
            
            
            float count = 0.0f;
            while(asd.Engine.DoEvents())
            {
                //if(asd.Engine.Tool.Begin("x"))
                //{
                //    asd.Engine.Tool.Text("Threshold: " + obj.Threshold.ToString());
                //    asd.Engine.Tool.End();
                //}
                
                var mousePos = asd.Engine.Mouse.Position;
                
                //normalObj.Light0 = LightType.Directional(mousePos.X - ws.X/2.0f, mousePos.Y - ws.Y / 2.0f, -100.0f);
                normalObj.Light0 = LightType.Point(mousePos, 200.0f);
                normalObj.Angle += 0.5f;
                
                //normalObj.Position = new asd.Vector2DF(100.0f, 0.0f) { Radian = count * 3.0f };
                //obj.ZOffset = count;
                //noise.ZOffset = count;
                //noise.NoiseSrc = new asd.RectF(count, count, 5.0f, 5.0f);

                //obj.Threshold = ((float)Math.Sin(count) + 1.0f) / 2.0f;
                //pe.OffsetRed = new asd.Vector2DF(0.1f, 0.0f) { Radian = count };
                //pe.OffsetGreen = new asd.Vector2DF(0.1f, 0.0f) { Radian = 2.0f * count };
                //pe.OffsetBlue = new asd.Vector2DF(0.1f, 0.0f) { Radian = -count };
                //pe.SetZoom((float)Math.Sin(count) * 0.25f + 1.25f);
                count += 0.01f;

                asd.Engine.Update();
            }

            asd.Engine.CloseTool();
            asd.Engine.Terminate();
        }
    }
}
