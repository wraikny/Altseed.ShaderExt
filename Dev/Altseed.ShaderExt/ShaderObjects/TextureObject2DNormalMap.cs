using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class TextureObject2DNormalMap : TextureObject2DWithMaterial
    {
        public TextureObject2DNormalMap()
            : base(Utils.Path.NormalMap)
        {
            ZPos = 0.0f;
            Light0 = LightType.Directional(new asd.Vector3DF(0.0f, 0.0f, -1.0f));
            LightColor0 = new asd.Color(255, 255, 255, 255);
        }

        private asd.Texture2D normalMap;
        private float zPos;

        private LightType light0;
        private asd.Color lightColor0;
        
        public asd.Texture2D NormalMap
        {
            get => normalMap;
            set
            {
                normalMap = value;
                Material2d.SetTexture2D("g_normalMap", normalMap);
                //Material2d.SetTextureFilterType("g_normalMap", asd.TextureFilterType.Linear);
            }
        }

        public float ZPos
        {
            get => zPos;
            set
            {
                zPos = value;
                Material2d.SetFloat("g_zPos", zPos);
            }
        }

        public LightType Light0
        {
            get => light0;
            set
            {
                light0 = value;
                Material2d.SetVector4DF("g_lightPos0", light0.ToVector4());
            }
        }
        
        public asd.Color LightColor0
        {
            get => lightColor0;
            set
            {
                lightColor0 = value;
                var v = new asd.Vector3DF(lightColor0.R / 255.0f, lightColor0.G / 255.0f, lightColor0.B / 255.0f);
                Material2d.SetVector3DF("g_lightColor0", v);
            }
        }
    }
}
