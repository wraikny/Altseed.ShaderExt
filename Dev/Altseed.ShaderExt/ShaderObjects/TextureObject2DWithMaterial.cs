using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class TextureObject2DWithMaterial : ShaderObjectBase
    {
        private asd.Vector2DF size;
        private asd.Texture2D texture;
        public TextureObject2DWithMaterial(string pathdx, string pathgl)
            : base(pathdx, pathgl)
        {
            OnDrawAdditionallyEvent += () =>
            {
                DrawSpriteRectangle(Material2d, size, Color);
            };
        }

        public asd.Texture2D Texture
        {
            get => texture;
            set
            {
                texture = value;

                size = texture?.Size.To2DF() ?? new asd.Vector2DF(0.0f, 0.0f);

                Material2d?.SetTexture2D("g_texture", texture);
                Material2d?.SetVector2DF("g_resolution", size / Math.Max(size.X, size.Y));
            }
        }
    }
}
