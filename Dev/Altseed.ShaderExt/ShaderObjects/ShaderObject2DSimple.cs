using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public sealed class ShaderObject2DSimple : ShaderObjectBase
    {
        private float second = 0.0f;
        private asd.Vector2DF size;
        //private asd.Texture2D texture;
        readonly asd.Shader2D shader;
        readonly asd.Material2D material2d;

        public ShaderObject2DSimple(string pathdx, string pathgl)
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
                throw new NotSupportedException();
            }

            if(shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            material2d = asd.Engine.Graphics.CreateMaterial2D(shader);

            material2d.SetVector2DF("g_resolution", Size / Math.Max(Size.X, Size.Y));
            //material2d.SetTexture2D("g_texture", texture);

            OnDrawAdditionallyEvent += () =>
            {
                material2d.SetFloat("g_second", second);
                DrawSpriteRectangle(Size, material2d);

                second += 1.0f / asd.Engine.CurrentFPS;
            };
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

        //public asd.Texture2D Texture
        //{
        //    get => texture;
        //    set
        //    {
        //        texture = value;
        //        if (material2d != null)
        //        {
        //            material2d.SetTexture2D("g_texture", texture);
        //        }
        //    }
        //}
    }
}
