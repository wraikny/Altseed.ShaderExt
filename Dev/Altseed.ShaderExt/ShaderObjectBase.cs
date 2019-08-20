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
