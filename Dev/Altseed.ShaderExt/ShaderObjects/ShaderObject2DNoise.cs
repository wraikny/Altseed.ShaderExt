using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class ShaderObject2DNoise : ShaderObject2DRectangle
    {
        public ShaderObject2DNoise()
            :base(Utils.Path.Noise + ".hlsl", Utils.Path.Noise + ".glsl")
        {
            Property = new NoiseProperty(Material2d);
        }

        #region
        private NoiseProperty Property { get; }

        /// <summary>
        /// ノイズの計算方法を取得・設定する。
        /// </summary>
        public NoiseType NoiseType
        {
            get => Property.NoiseType;
            set => Property.NoiseType = value;
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
        /// Disolveと二値化を有効にするかどうかを取得・設定する。
        /// </summary>
        public bool IsDoubled
        {
            get => Property.IsDoubled;
            set => Property.IsDoubled = value;
        }

        /// <summary>
        /// 二値化が有効なとき、0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
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
        /// 二値化が有効なとき、Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        public Background BackGround
        {
            get => Property.Background;
            set => Property.Background = value;
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
