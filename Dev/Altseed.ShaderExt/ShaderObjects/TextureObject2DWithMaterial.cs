using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class TextureObject2DWithMaterial : ShaderObjectBase
    {
        private float second = 0.0f;
        private asd.Vector2DF size;
        private asd.Texture2D texture;

        readonly asd.Shader2D shader;
        readonly asd.Material2D material2d;

        public TextureObject2DWithMaterial(string pathdx, string pathgl)
        {
            if (asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.DirectX11)
            {
                if (pathdx == null) throw new NotSupportedException();
                shader = Utils.LoadShader2DInternal(pathdx);
            }
            else if (asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.OpenGL)
            {
                if (pathgl == null) throw new NotSupportedException();
                shader = Utils.LoadShader2DInternal(pathgl);
            }
            else
            {
                throw new NotSupportedException("Unsupported platform");
            }

            if (shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            material2d = asd.Engine.Graphics.CreateMaterial2D(shader);

            OnDrawAdditionallyEvent += () =>
            {
                DrawSpriteRectangle(size, material2d);
            };

            OnUpdateEvent += () =>
            {
                material2d.SetFloat("g_second", second);
                second += 1.0f / asd.Engine.CurrentFPS;
            };
        }

        public asd.Texture2D Texture
        {
            get => texture;
            set
            {
                texture = value;
                size = texture.Size.To2DF();
                if (material2d != null)
                {
                    material2d.SetTexture2D("g_texture", texture);
                    material2d.SetVector2DF("g_resolution", size / Math.Max(size.X, size.Y));
                }
            }
        }
    }
}
