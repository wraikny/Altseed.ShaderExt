using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public abstract class PostEffectBase : IDisposable
    {
        internal readonly PostEffectReactive coreObject;

        protected asd.Material2D Material2d { get; private set; }
        private float second = 0.0f;

        public PostEffectBase(string pathdx, string pathgl)
        {
            coreObject = new PostEffectReactive();

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

            coreObject.OnDrawEvent += (dst, src) => {
                Material2d?.SetFloat("g_second", second);
                second += 1.0f / asd.Engine.CurrentFPS;

                var ws = asd.Engine.WindowSize.To2DF();
                Material2d?.SetVector2DF("g_resolution", ws / Math.Min(ws.X, ws.Y));

                Material2d?.SetTexture2D("g_texture", src);

                coreObject.DrawOnTexture2DWithMaterial(dst, Material2d);

                OnDraw(dst, src);
                OnDrawEvent(dst, src);
            };
        }

        public static implicit operator asd.PostEffect(PostEffectBase x)
        {
            return x.coreObject;
        }

        /// <summary>
        /// 毎フレーム描画される処理を登録できる。
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        public event Action<asd.RenderTexture2D, asd.RenderTexture2D> OnDrawEvent = delegate { };

        /// <summary>
        /// オーバーライドして、毎フレーム描画される処理を記述できる。
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        protected virtual void OnDraw(asd.RenderTexture2D dst, asd.RenderTexture2D src) { }

        /// <summary>
        /// このポストエフェクトが有効かどうか、取得、設定する。
        /// </summary>
        public bool IsEnabled
        {
            get => coreObject.IsEnabled;
            set => coreObject.IsEnabled = value;
        }

        //protected void DrawOnTexture2DWithMaterial(asd.RenderTexture2D dst, asd.Material2D material2d)
        //{
        //    coreObject.DrawOnTexture2DWithMaterial(dst, material2d);
        //}

        public void Dispose()
        {
            coreObject.Dispose();
        }
    }
}
