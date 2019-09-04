using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace Altseed.ShaderExt
{
    public sealed class PostEffectDisolve : PostEffectBase
    {

        public PostEffectDisolve()
            : base(Utils.Path.Disolve + ".hlsl", Utils.Path.Disolve + ".glsl")
        {
            Property = new DisolveProperty(Material2d);
        }
        
        #region
        private DisolveProperty Property { get; }

        /// <summary>
        /// Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        public Background BackGround
        {
            get => Property.Background;
            set => Property.Background = value;
        }

        /// <summary>
        /// Disolveの計算方法を取得・設定する。
        /// </summary>
        public NoiseSource NoiseSource
        {
            get => Property.NoiseSource;
            set => Property.NoiseSource = value;
        }

        /// <summary>
        /// ノイズを計算するUVの位置とサイズの比率を取得・設定する。 
        /// </summary>
        public asd.RectF NoiseSrc
        {
            get => Property.Src;
            set => Property.Src = value;
        }

        /// <summary>
        /// 0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
        /// </summary>
        /// /// <remarks>
        /// 0.0fのとき、完全に表示される。
        /// 1.0fのとき、完全に消える。
        /// </remarks>
        public float Threshold
        {
            get => Property.Threshold;
            set => Property.Threshold = value;
        }

        /// <summary>
        /// ノイズのZ軸方向のオフセットを指定する。
        /// </summary>
        public float ZOffset
        {
            get => Property.ZOffset;
            set => Property.ZOffset = value;
        }
        #endregion
    }
}
