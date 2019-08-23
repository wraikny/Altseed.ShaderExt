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

    public class NoiseSource
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

    public interface IDisolveProperty
    {
        /// <summary>
        /// Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        Background BackGround { get; set; }
        /// <summary>
        /// Disolveの計算方法を取得・設定する。
        /// </summary>
        NoiseSource NoiseSource { get; set; }

        /// <summary>
        /// Disolve計算時のUVのScaleを取得・設定する。
        /// </summary>
        asd.Vector2DF DisolveScale { get; set; }

        /// <summary>
        /// Disolve計算時のUVのOffsetを取得・設定する。 
        /// </summary>
        asd.Vector2DF DisolveOffset { get; set; }

        /// <summary>
        /// 0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。
        /// </summary>
        float Threshold { get; set; }
    }

    public class DisolveProperty : IDisolveProperty
    {
        private asd.Material2D Material2d { get; }
        public DisolveProperty(asd.Material2D material2d)
        {
            Material2d = material2d;
        }

        private Background background;
        private NoiseSource noiseSource;
        private asd.Vector2DF disolveScale = new asd.Vector2DF(1.0f, 1.0f);
        private asd.Vector2DF disolveOffset = new asd.Vector2DF(0.0f, 0.0f);
        private float threshold = 0.0f;

        /// <summary>
        /// Disolveで切り抜いたときの背景を取得・設定する。
        /// </summary>
        public Background BackGround
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
                            return ((float)x) / 255.0f;
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
        /// Disolveの計算方法を取得・設定する。
        /// </summary>
        public NoiseSource NoiseSource
        {
            get => noiseSource;
            set
            {
                noiseSource = value;
                noiseSource.Match(texture => {
                    Material2d?.SetFloat("g_noiseSource", -1.0f);
                    Material2d?.SetTexture2D("g_noiseTexture", texture);

                }, noiseType => {
                    Material2d?.SetFloat("g_noiseSource", (float)noiseType);
                });
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
