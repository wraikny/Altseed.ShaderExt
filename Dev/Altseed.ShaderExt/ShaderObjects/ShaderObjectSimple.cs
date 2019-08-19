using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public sealed class ShaderObjectSimple : ShaderObjectBase
    {
        float second = 0;
        private asd.Vector2DF size;
        readonly asd.Shader2D shader;
        readonly asd.Material2D material2d;

        public ShaderObjectSimple(string pathdx, string pathgl)
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
                throw new NotSupportedException();
            }

            if(shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            material2d = asd.Engine.Graphics.CreateMaterial2D(shader);

            material2d.SetVector2DF("g_resolution", Size / Math.Max(Size.X, Size.Y));
        }


        public asd.Vector2DF Size
        {
            get => size;
            set
            {
                size = value;
                if(material2d != null)
                {
                    material2d.SetVector2DF("g_resolution", Size / Math.Max(Size.X, Size.Y));
                }
            }
        }

        protected override void Draw()
        {
            material2d.SetFloat("g_second", second);
            DrawAdditionally(Size, material2d);

            second += 1.0f / asd.Engine.CurrentFPS;
        }
    }
}
