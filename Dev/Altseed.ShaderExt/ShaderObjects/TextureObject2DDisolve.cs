using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class TextureObject2DDisolve : TextureObject2DWithMaterial, IDisolveProperty
    {

        public TextureObject2DDisolve()
            : base(Utils.Path.Disolve + ".hlsl", Utils.Path.Disolve + ".glsl")
        {
            DisolveProperty = new DisolveProperty(Material2d);
        }


        #region IDisolveProperty
        public DisolveProperty DisolveProperty { get; }

        /// <summary>
        /// Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        public Background BackGround
        {
            get => DisolveProperty.BackGround;
            set => DisolveProperty.BackGround = value;
        }

        /// <summary>
        /// Disolveの計算方法を取得・設定する。
        /// </summary>
        public NoiseSource NoiseSource
        {
            get => DisolveProperty.NoiseSource;
            set => DisolveProperty.NoiseSource = value;
        }

        /// <summary>
        /// Disolve計算時のUVのOffsetを取得・設定する。 
        /// </summary>
        public asd.RectF DisolveSrc
        {
            get => DisolveProperty.DisolveSrc;
            set => DisolveProperty.DisolveSrc = value;
        }

        /// <summary>
        /// 0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
        /// </summary>
        public float Threshold
        {
            get => DisolveProperty.Threshold;
            set => DisolveProperty.Threshold = value;
        }
        #endregion

    }
}
