using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public sealed class PostEffectChromaticAberrationSimple : PostEffectBase
    {
        public PostEffectChromaticAberrationSimple()
            :base(Utils.Path.ChromaticAberrationSimple + ".hlsl",
                 Utils.Path.ChromaticAberrationSimple + ".glsl")
        {
            var zero2 = new asd.Vector2DF(0.0f, 0.0f);
            OffsetRed = zero2;
            OffsetGreen = zero2;
            OffsetBlue = zero2;
            var ws = WindowSize;
            Src = new asd.RectF(0.0f, 0.0f, ws.X, ws.Y);
            Alpha = 1.0f;
        }

        private asd.Vector2DF offsetRed;
        private asd.Vector2DF offsetGreen;
        private asd.Vector2DF offsetBlue;
        private asd.RectF src;
        private float alpha;

        private asd.Vector2DF WindowSize => asd.Engine.WindowSize.To2DF();

        /// <summary>
        /// 赤色のオフセット比率を取得・設定する。
        /// </summary>
        public asd.Vector2DF OffsetRed
        {
            get => offsetRed;
            set
            {
                offsetRed = value;
                Material2d?.SetVector2DF("g_offset_red", offsetRed);
            }
        }

        /// <summary>
        /// 緑色のオフセット比率を取得・設定する。
        /// </summary>
        public asd.Vector2DF OffsetGreen
        {
            get => offsetGreen;
            set
            {
                offsetGreen = value;
                Material2d?.SetVector2DF("g_offset_green", offsetGreen);
            }
        }

        /// <summary>
        /// 青色のオフセット比率を取得・設定する。
        /// </summary>
        public asd.Vector2DF OffsetBlue
        {
            get => offsetBlue;
            set
            {
                offsetBlue = value;
                Material2d?.SetVector2DF("g_offset_blue", offsetBlue);
            }
        }

        /// <summary>
        /// 描画範囲を取得・設定する。
        /// </summary>
        public asd.RectF Src
        {
            get => src;
            set
            {
                src = value;
                Material2d?.SetVector2DF("g_offset", src.Position / WindowSize);
                Material2d?.SetVector2DF("g_scale", src.Size / WindowSize);
            }
        }

        /// <summary>
        /// 透過度を取得・設定する。
        /// </summary>
        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
                Material2d?.SetFloat("g_alpha", alpha);
            }
        }
    }
}
