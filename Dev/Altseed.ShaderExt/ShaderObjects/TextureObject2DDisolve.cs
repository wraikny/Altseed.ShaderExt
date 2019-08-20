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
        private NoiseType disolveSource = NoiseType.Texture;
        private asd.Vector2DF disolveScale = new asd.Vector2DF(1.0f, 1.0f);
        private asd.Vector2DF disolveOffset = new asd.Vector2DF(0.0f, 0.0f);
        private float threshold = 0.0f;

        private const string Pathdx = "Altseed.ShaderExt.Shaders/disolve/disolve.hlsl";
        private const string Pathgl = "Altseed.ShaderExt.Shaders/disolve/disolve.glsl";
        public TextureObject2DDisolve()
            : base(Pathdx, Pathgl)
        {
            Material2d?.SetTextureWrapType("g_disolveTexture", asd.TextureWrapType.Repeat);
            Material2d?.SetTextureFilterType("g_disolveTexture", asd.TextureFilterType.Linear);

            Material2d?.SetFloat("g_disolveSource", (float)disolveSource);
            Material2d?.SetTexture2D("g_disolveTexture", disolveTexture);
            Material2d?.SetVector2DF("g_disolveScale", disolveScale);
            Material2d?.SetVector2DF("g_disolveOffset", disolveOffset);
            Material2d?.SetFloat("g_threshold", threshold);
        }

        /// <summary>
        /// Disolveの計算方法を取得・設定する。
        /// </summary>
        public NoiseType DisolveSource
        {
            get => disolveSource;
            set
            {
                disolveSource = value;
                Material2d?.SetFloat("g_disolveSource", (float)disolveSource);
            }
        }

        /// <summary>
        /// DisolveSourceがTextureのときに使用するTextureを取得・設定する。
        /// </summary>
        public asd.Texture2D DisolveTexture
        {
            get => disolveTexture;
            set
            {
                disolveTexture = value;
                Material2d?.SetTexture2D("g_disolveTexture", disolveTexture);
            }
        }

        /// <summary>
        /// Disolve計算時のUVのScaleを取得・設定する。
        /// </summary>
        public asd.Vector2DF DisolveScale
        {
            get => disolveScale;
            set
            {
                disolveScale = value;
                Material2d?.SetVector2DF("g_disolveScale", disolveScale);
            }
        }

        /// <summary>
        /// Disolve計算時のUVのOffsetを取得・設定する。 
        /// </summary>
        public asd.Vector2DF DisolveOffset
        {
            get => disolveOffset;
            set
            {
                disolveOffset = value;
                Material2d?.SetVector2DF("g_disolveOffset", disolveOffset);
            }
        }

        /// <summary>
        /// 0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
        /// </summary>
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
