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

        readonly asd.Shader2D shader;
        readonly asd.Material2D material2d;

        public ShaderObjectSimple(string pathdx, string pathgl) {
            if(asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.DirectX11)
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

            material2d = asd.Engine.Graphics.CreateMaterial2D(shader);
            var ws = asd.Engine.WindowSize.To2DF();
            material2d.SetVector2DF("g_resolution", ws / Math.Max(ws.X, ws.Y));
        }

        public asd.Vector2DF Size { get; set; }

        protected override void Draw()
        {
            material2d.SetFloat("g_second", second);
            DrawAdditionally(Size, material2d);

            second += 1.0f / asd.Engine.CurrentFPS;
        }
    }
}
