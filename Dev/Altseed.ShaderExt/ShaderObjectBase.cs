using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public abstract class ShaderObjectBase : EmptyDrawnObject2D
    {
        public ShaderObjectBase() { }

        public asd.AlphaBlendMode AlphaBlend
        {
            get => coreObject.AlphaBlend;
            set => coreObject.AlphaBlend = value;
        }

        /// <summary>
        /// この2Dオブジェクトを描画する際の描画原点を取得または設定する。描画原点は拡大・回転・描画の中心となる、画像データ内での座標。
        /// </summary>
        public asd.Vector2DF CenterPosition
        {
            get => coreObject.CenterPosition;
            set => coreObject.CenterPosition = value;
        }

        internal void DrawSpriteRectangle(asd.Material2D material2D, asd.Vector2DF size, asd.Color? color = null)
        {
            var area = new asd.RectF(
                Position - CenterPosition * Scale,
                size * Scale
            );

            var color_ = color ?? new asd.Color(255, 255, 255, 255);

            coreObject.DrawSpriteWithMaterialAdditionally(
                area.Position,
                area.Position + new asd.Vector2DF(area.Size.X, 0.0f),
                area.Position + area.Size,
                area.Position + new asd.Vector2DF(0.0f, area.Size.Y),
                color_, color_, color_, color_,
                new asd.Vector2DF(0.0f, 0.0f),
                new asd.Vector2DF(1.0f, 0.0f),
                new asd.Vector2DF(1.0f, 1.0f),
                new asd.Vector2DF(0.0f, 1.0f),
                material2D,
                asd.AlphaBlendMode.Blend,
                AbsoluteDrawingPriority
            );
        }
    }
}
