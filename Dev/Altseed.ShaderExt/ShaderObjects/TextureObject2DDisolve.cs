using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class TextureObject2DDisolve : TextureObject2DWithMaterial
    {
        private asd.Texture2D disolveTexture;
        private float threshold = 0.0f;

        public TextureObject2DDisolve(string pathdx, string pathgl)
            : base(pathdx, pathgl)
        {

        }

        public asd.Texture2D DisolveTexture
        {
            get => disolveTexture;
            set
            {
                disolveTexture = value;
                Material2d?.SetTexture2D("g_disolveTexture", disolveTexture);
            }
        }

        public float Threshold
        {
            get => threshold;
            set
            {
                threshold = value;
                Material2d?.SetFloat("g_threshold", threshold);
            }
        }
    }
}
