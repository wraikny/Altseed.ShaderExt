using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class ShaderObject2DRectangle : ShaderObjectBase
    {
        private asd.Vector2DF size;

        public ShaderObject2DRectangle(string pathdx, string pathgl)
            : base(pathdx, pathgl)
        {
            Size = new asd.Vector2DF(0.0f, 0.0f);

            OnDrawAdditionallyEvent += () =>
            {
                DrawSpriteRectangle(Material2d, Size, Color);
            };
        }

        public ShaderObject2DRectangle(string path)
            : this(path + ".hlsl", path + ".glsl") { }

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
