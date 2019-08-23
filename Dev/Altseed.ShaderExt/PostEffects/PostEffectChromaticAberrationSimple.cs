using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt.PostEffects
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
            Src = new asd.RectF(0.0f, 0.0f, 1.0f, 1.0f);
            Alpha = 1.0f;
        }

        private asd.Vector2DF offsetRed;
        private asd.Vector2DF offsetGreen;
        private asd.Vector2DF offsetBlue;
        private asd.RectF src;
        private float alpha;

        private asd.Vector2DF WindowSize => asd.Engine.WindowSize.To2DF();

        public asd.Vector2DF OffsetRed
        {
            get => offsetRed;
            set
            {
                offsetRed = value;
                Material2d?.SetVector2DF("g_offset_red", offsetRed / WindowSize);
            }
        }

        public asd.Vector2DF OffsetGreen
        {
            get => offsetGreen;
            set
            {
                offsetGreen = value;
                Material2d?.SetVector2DF("g_offset_green", offsetGreen / WindowSize);
            }
        }

        public asd.Vector2DF OffsetBlue
        {
            get => offsetBlue;
            set
            {
                offsetBlue = value;
                Material2d?.SetVector2DF("g_offset_blue", offsetBlue / WindowSize);
            }
        }

        /// <summary>
        /// 割合で描画範囲を指定する。 
        /// </summary>
        public asd.RectF Src
        {
            get => src;
            set
            {
                src = value;
                Material2d?.SetVector2DF("g_caOffset", src.Position / WindowSize);
                Material2d?.SetVector2DF("g_caScale", src.Size / WindowSize);
            }
        }

        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
                Material2d?.SetFloat("g_caAlpha", alpha);
            }
        }
    }
}
