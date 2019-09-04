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
            int windowWidth = 1600;
            int windowHeight = windowWidth * 9 / 16;
            asd.Engine.Initialize("Altseed.ShaderExt.Test", windowWidth, windowHeight, new asd.EngineOption());
            asd.Engine.OpenTool();

            
            Altseed.ShaderExt.Utils.AddPackage();

            var scene = new asd.Scene() { HDRMode = true };
            var layer = new asd.Layer2D();
            scene.AddLayer(layer);
            
            var testTex = asd.Engine.Graphics.CreateTexture2D("AmCrDownloadCard.png");
            float count = 0.0f;
            var ws = asd.Engine.WindowSize.To2DF();
            
            var size = testTex.Size.To2DF();
            var scale = new asd.Vector2DF(1.0f, 1.0f) * 0.5f * (windowWidth / 800.0f);

            // Disolveを掛けるサンプル
            var disolveObj = new TextureObject2DDisolve
            {
                Threshold = 0.5f,
                NoiseSource = NoiseSource.PerlinNoise,
                NoiseSrc = new asd.RectF(0.0f, 0.0f, 10.0f, 10.0f),
                Texture = testTex,

                EdgeWidth = 0.1f,
                EdgeColor = new asd.Color(255, 255, 0, 255),
                EdgeBrightness = 1.0f,

                CenterPosition = size * 0.5f,
                Position = ws * (new asd.Vector2DF(0.75f, 0.25f)),
                Scale = scale,
            };
            
            disolveObj.OnUpdateEvent += () => {
                //obj.ZOffset = count;
                disolveObj.Threshold = (float)Math.Sin(count) * 0.5f + 0.5f;
            };

            // ノイズを表示するサンプル。
            var noise = new RectangleObject2DNoise
            {
                Color = new asd.Color(255, 255, 0),
                Size = new asd.Vector2DF(300.0f, 300.0f),
                NoiseType = NoiseType.PerlinNoise,
                NoiseSrc = new asd.RectF(0.0f, 0.0f, 5.0f, 5.0f),
                IsDoubled = true,
                Threshold = 0.5f,
                BackGround = Background.Color(0, 0, 0),
                //AlphaBlend = asd.AlphaBlendMode.Add

                CenterPosition = size * 0.5f,
                Position = ws * (new asd.Vector2DF(0.25f, 0.75f)),
                Scale = scale,
            };
            noise.OnUpdateEvent += () =>
            {
                noise.ZOffset = count;
                noise.NoiseSrc = new asd.RectF(count, count, 5.0f, 5.0f);
            };
            
            // Normal Mapを用いて画像を表示するサンプル。
            var normalObj = new TextureObject2DNormalMap()
            {
                Texture = testTex,
                NormalMap = asd.Engine.Graphics.CreateTexture2D("AmCrDownloadCard_normalmap.png"),
                ZPos = 0.0f,

                CenterPosition = size * 0.5f,
                Position = ws * 0.25f,
                Scale = scale,
            };
            normalObj.OnUpdateEvent += () => {
                var mousePos = asd.Engine.Mouse.Position;
                normalObj.Light0 = LightType.Point(mousePos, 100.0f);
                //normalObj.Light0 = LightType.Directional(-1.0f, -1.0f, -1.0f);
                //normalObj.Angle += 0.5f;
            };

            // HSV Offset
            var hsvObj = new TextureObject2DHSVOffset()
            {
                Texture = testTex,

                CenterPosition = size * 0.5f,
                Position = ws * (new asd.Vector2DF(0.75f, 0.75f)),
                Scale = scale,
            };
            hsvObj.OnUpdateEvent += () => {
                hsvObj.HueOffset += 0.001f;
                hsvObj.ValueOffset = (float)Math.Sin(count * 0.1f) * 0.5f;
            };

            layer.AddObject(disolveObj);
            layer.AddObject(noise);
            layer.AddObject(normalObj);
            layer.AddObject(hsvObj);

            // 色収差をかける
            var pe = new PostEffectChromaticAberrationSimple();
            pe.OnDrawEvent += () => {
                pe.OffsetRed = new asd.Vector2DF(0.025f, 0.0f) { Radian = count };
                pe.OffsetGreen = new asd.Vector2DF(0.025f, 0.0f) { Radian = 2.0f * count };
                pe.OffsetBlue = new asd.Vector2DF(0.025f, 0.0f) { Radian = -count };
                pe.SetZoom((float)Math.Sin(count) * 0.1f + 1.0f);
            };
            //layer.AddPostEffect(pe);
            
            var peDisolve = new PostEffectDisolve() {
                NoiseSource = NoiseSource.BlockNoise,
                NoiseSrc = new asd.RectF(0.0f, 0.0f, 10.0f, 10.0f),
                BackGround = Background.Color(100, 100, 200),
            };
            peDisolve.OnDrawEvent += () => {
                peDisolve.Threshold = (float)Math.Sin(count) * 0.5f + 0.5f;
            };
            //layer.AddPostEffect(peDisolve);
            asd.Engine.ChangeScene(scene);
            
            while(asd.Engine.DoEvents())
            {
                count += 0.01f;

                asd.Engine.Update();
            }

            asd.Engine.CloseTool();
            asd.Engine.Terminate();
        }
    }
}
