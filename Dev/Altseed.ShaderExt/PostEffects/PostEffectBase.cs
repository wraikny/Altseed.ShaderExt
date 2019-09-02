using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public abstract class PostEffectBase : asd.PostEffect
    {
        protected asd.Material2D Material2d { get; private set; }
        private float second = 0.0f;

        private asd.Vector2DI _lastWindowSize = new asd.Vector2DI();

        public PostEffectBase(string pathdx, string pathgl)
        {
            asd.Shader2D shader;

            if (asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.DirectX11)
            {
                if (pathdx == null) throw new NotSupportedException();
                shader = Utils.LoadShader2D(pathdx);
            }
            else if (asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.OpenGL)
            {
                if (pathgl == null) throw new NotSupportedException();
                shader = Utils.LoadShader2D(pathgl);
            }
            else
            {
                throw new NotSupportedException("Unsupported platform");
            }

            if (shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            Material2d = asd.Engine.Graphics.CreateMaterial2D(shader);
        }

        protected override void OnDraw(asd.RenderTexture2D dst, asd.RenderTexture2D src)
        {
            Material2d.SetFloat("g_second", second);
            second += asd.Engine.DeltaTime;

            var wsi = asd.Engine.WindowSize;
            if (_lastWindowSize != wsi)
            {
                _lastWindowSize = wsi;
                var ws = wsi.To2DF();
                Material2d.SetVector2DF("g_resolution", ws / Math.Min(ws.X, ws.Y));
                Material2d.SetVector2DF("g_size", ws);
            }

            Material2d.SetTexture2D("g_texture", src);

            DrawOnTexture2DWithMaterial(dst, Material2d);

            OnDrawEvent();
        }

        /// <summary>
        /// 毎フレーム描画される処理を登録できる。
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        public event Action OnDrawEvent = delegate { };
    }
}
