using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class ShaderObject2DSimple : ShaderObjectBase
    {
        private float second = 0.0f;
        private asd.Vector2DF size;
        readonly asd.Shader2D shader;
        protected asd.Material2D Material2d { get; private set; }

        public ShaderObject2DSimple(string pathdx, string pathgl)
        {
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

            if(shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            Material2d = asd.Engine.Graphics.CreateMaterial2D(shader);

            Material2d.SetVector2DF("g_resolution", Size / Math.Max(Size.X, Size.Y));

            OnDrawAdditionallyEvent += () =>
            {
                DrawSpriteRectangle(Material2d, Size, Color);
            };

            OnUpdateEvent += () => {
                Material2d.SetFloat("g_second", second);
                second += 1.0f / asd.Engine.CurrentFPS;
            };
        }


        public asd.Vector2DF Size
        {
            get => size;
            set
            {
                size = value;
                if(Material2d != null)
                {
                    Material2d.SetVector2DF("g_resolution", Size / Math.Max(Size.X, Size.Y));
                }
            }
        }
    }
}
