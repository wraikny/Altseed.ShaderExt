using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace Altseed.ShaderExt
{
    public sealed class PostEffectDisolve : PostEffectBase, IDisolveProperty
    {
        private readonly DisolveProperty property;

        public PostEffectDisolve()
            : base(Utils.Path.DisolveDx, Utils.Path.DisolveGl)
        {
            property = new DisolveProperty(Material2d);
        }

        #region IDisolveProperty
        /// <summary>
        /// Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        public Background BackGround
        {
            get => property.BackGround;
            set => property.BackGround = value;
        }

        /// <summary>
        /// Disolveの計算方法を取得・設定する。
        /// </summary>
        public NoiseSource NoiseSource
        {
            get => property.NoiseSource;
            set => property.NoiseSource = value;
        }

        /// <summary>
        /// Disolve計算時のUVのScaleを取得・設定する。
        /// </summary>
        public asd.Vector2DF DisolveScale
        {
            get => property.DisolveScale;
            set => property.DisolveScale = value;
        }

        /// <summary>
        /// Disolve計算時のUVのOffsetを取得・設定する。 
        /// </summary>
        public asd.Vector2DF DisolveOffset
        {
            get => property.DisolveOffset;
            set => property.DisolveOffset = value;
        }

        /// <summary>
        /// 0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
        /// </summary>
        public float Threshold
        {
            get => property.Threshold;
            set => property.Threshold = value;
        }
        #endregion
    }
}
