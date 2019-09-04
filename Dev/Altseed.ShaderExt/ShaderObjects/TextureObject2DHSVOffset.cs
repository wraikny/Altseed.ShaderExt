using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class TextureObject2DHSVOffset : TextureObject2DWithMaterial
    {
        public TextureObject2DHSVOffset()
            : base(Utils.Path.HSVOffset)
        {
            HueOffset = 0.0f;
            SatuationOffset = 0.0f;
            ValueOffset = 0.0f;
        }

        private float hueOffset;
        private float satuationOffset;
        private float valueOffset;

        /// <summary>
        /// 色相のオフセットを指定する。1.0を超えると0.0からループする。
        /// </summary>
        public float HueOffset
        {
            get => hueOffset;
            set
            {
                hueOffset = value;

                Material2d.SetFloat("g_hueOffset", hueOffset);
            }
        }

        /// <summary>
        /// 彩度のオフセットを指定する。
        /// </summary>
        public float SatuationOffset
        {
            get => satuationOffset;
            set
            {
                satuationOffset = value;

                Material2d.SetFloat("g_saturationOffset", satuationOffset);
            }
        }

        /// <summary>
        /// 輝度のオフセットを指定する。
        /// </summary>
        public float ValueOffset
        {
            get => valueOffset;
            set
            {
                valueOffset = value;

                Material2d.SetFloat("g_valueOffset", valueOffset);
            }
        }
    }
}
