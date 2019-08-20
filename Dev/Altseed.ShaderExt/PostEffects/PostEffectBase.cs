using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public abstract class PostEffectBase
    {
        internal readonly asd.PostEffect coreObject;

        protected asd.Material2D Material2d { get; private set; }
        private float second = 0.0f;

        public PostEffectBase(string pathdx, string pathgl)
        {
            var obj = new PostEffectReactive();

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

            obj.OnDrawEvent += (dst, src) => {
                OnDraw(dst, src);
                OnDrawEvent(dst, src);
            };

            coreObject = obj;
        }

        public static implicit operator asd.PostEffect(PostEffectBase x)
        {
            return x.coreObject;
        }

        public event Action<asd.RenderTexture2D, asd.RenderTexture2D> OnDrawEvent = delegate { };

        protected abstract void OnDraw(asd.RenderTexture2D dst, asd.RenderTexture2D src);
    }
}
