using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public enum NoiseType
    {
        Random = 0,
        BlockNoise = 1,
        ValueNoise = 2,
        PerlinNoise = 3,
        Fbm = 4,
    }

    public sealed class NoiseSource
    {
        private readonly asd.Texture2D texture = null;
        private readonly NoiseType? noiseType;

        private NoiseSource(asd.Texture2D texture) {
            this.texture = texture;
        }
        private NoiseSource(NoiseType noiseType) {
            this.noiseType = noiseType;
        }

        public static NoiseSource Texture(asd.Texture2D texture) =>
            new NoiseSource(texture);

        public static NoiseSource Random => new NoiseSource(NoiseType.Random);
        public static NoiseSource BlockNoise => new NoiseSource(NoiseType.BlockNoise);
        public static NoiseSource ValueNoise => new NoiseSource(NoiseType.ValueNoise);
        public static NoiseSource PerlinNoise => new NoiseSource(NoiseType.PerlinNoise);
        public static NoiseSource Fbm => new NoiseSource(NoiseType.Fbm);

        public void Match(Action<asd.Texture2D> fTexture, Action<NoiseType> fNoise)
        {
            if(texture != null)
            {
                fTexture(texture);
            }
            else if (noiseType != null)
            {
                fNoise(noiseType.Value);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

    }

    public class NoisePropertyBase
    {
        protected asd.Material2D Material2d { get; }

        public NoisePropertyBase(asd.Material2D material2d)
        {
            Material2d = material2d;
        }

        private asd.RectF disolveSrc;
        private float zOffset;

        /// <summary>
        /// ノイズを計算するUVの位置とサイズの比率を取得・設定する。 
        /// </summary>
        public asd.RectF Src
        {
            get => Src;
            set
            {
                disolveSrc = value;
                Material2d?.SetVector2DF("g_noiseOffset", disolveSrc.Position);
                Material2d?.SetVector2DF("g_noiseScale", disolveSrc.Size);
            }
        }

        /// <summary>
        /// ノイズのZ軸方向のオフセットを指定する。
        /// </summary>
        public float ZOffset
        {
            get => zOffset;
            set
            {
                zOffset = value;
                Material2d?.SetFloat("g_zOffset", zOffset);
            }
        }
    }

    public class DisolvePropertyBase : NoisePropertyBase
    {
        public DisolvePropertyBase(asd.Material2D material2d)
            : base(material2d)
        {
            Background = Background.Discard;
            Threshold = 0.0f;
        }

        private Background background;
        private float threshold;

        /// <summary>
        /// Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        public Background Background
        {
            get => background;
            set
            {
                background = value;
                background.Match(
                    () => {
                        Material2d?.SetFloat("g_backgroundSource", 0);
                    },
                    color => {
                        Material2d?.SetFloat("g_backgroundSource", 1);

                        float convertColor(byte x)
                        {
                            return x / 255.0f;
                        }
                        Material2d?.SetVector4DF("g_backgroundColor",
                            new asd.Vector4DF(
                                convertColor(color.R),
                                convertColor(color.G),
                                convertColor(color.B),
                                convertColor(color.A)
                            )
                        );
                    }
                );
            }
        }

        /// <summary>
        /// 0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
        /// </summary>
        /// <remarks>
        /// 0.0fのとき、完全に表示される。
        /// 1.0fのとき、完全に消える。
        /// </remarks>
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

    public class NoiseProperty : DisolvePropertyBase
    {
        public NoiseProperty(asd.Material2D material2d)
            : base(material2d)
        {
            NoiseType = NoiseType.Random;
            IsDoubled = false;
        }

        private bool isDoubled;
        private NoiseType noiseType;

        /// <summary>
        /// ノイズの計算方法を取得・設定する。
        /// </summary>
        public NoiseType NoiseType
        {
            get => noiseType;
            set
            {
                noiseType = value;
                Material2d?.SetFloat("g_noiseType", (float)noiseType);
            }
        }

        /// <summary>
        /// Disolveと二値化を有効にするかどうかを取得・設定する。
        /// </summary>
        public bool IsDoubled
        {
            get => isDoubled;
            set
            {
                isDoubled = value;
                Material2d?.SetFloat("g_isDoubled", isDoubled ? 1 : 0);
            }
        }
    }


    public class DisolveProperty : DisolvePropertyBase
    {
        public DisolveProperty(asd.Material2D material2d)
            : base(material2d)
        {
            NoiseSource = NoiseSource.Random;
        }

        private NoiseSource noiseSource;

        /// <summary>
        /// ノイズの計算方法を取得・設定する。
        /// </summary>
        public NoiseSource NoiseSource
        {
            get => noiseSource;
            set
            {
                noiseSource = value;
                noiseSource.Match(texture => {
                    Material2d?.SetFloat("g_noiseType", -1.0f);
                    Material2d?.SetTexture2D("g_noiseTexture", texture);
                }, noiseType => {
                    Material2d?.SetFloat("g_noiseType", (float)noiseType);
                });
            }
        }
    }
}
