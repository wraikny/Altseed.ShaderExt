using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public abstract class ShaderObjectBase : EmptyDrawnObject2D
    {
        protected asd.Material2D Material2d { get; private set; }
        private float second = 0.0f;

        public ShaderObjectBase(string pathdx, string pathgl)
        {
            asd.Shader2D shader;

            if (asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.DirectX11)
            {
                if (pathdx == null) throw new NotSupportedException();
                shader = Utils.LoadShader2D(pathdx);
            }
            else if (asd.Engine.Graphics.GraphicsDeviceType == asd.GraphicsDeviceType.OpenGL)
            {
                if (pathgl == null) throw new NotSupportedException();
                shader = Utils.LoadShader2D(pathgl);
            }
            else
            {
                throw new NotSupportedException("Unsupported platform");
            }

            if (shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            Material2d = asd.Engine.Graphics.CreateMaterial2D(shader);

            OnUpdateEvent += () => {
                Material2d.SetFloat("g_second", second);
                second += asd.Engine.DeltaTime;
            };

        }

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
