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
                throw new NotSupportedException("Unsupported tform");
            }
            
            if (shader == null)
            {
                throw new ArgumentException("Compiled Error");
            }

            Material2d = asd.Engine.Graphics.CreateMaterial2D(shader);
            
            OnUpdateEvent += () => {
                Material2d.SetFloat("g_second", second);
                second += 1.0f / asd.Engine.CurrentFPS;
            };

        }

        /// <summary>
        /// 描画時のブレンドモードを取得・設定する。
        /// </summary>
        public asd.AlphaBlendMode AlphaBlend
        {
            get => coreObject.AlphaBlend;
            set => coreObject.AlphaBlend = value;
        }

        internal asd.Vector2DF GetGlobalLeftUpPosition()
        {
            var diff = -CenterPosition * Scale;
            diff.Degree += Angle;
            return GetGlobalPosition() + diff;
        }

        internal void DrawSpriteRectangle(asd.Material2D material2D, asd.Vector2DF size, asd.Color? color = null)
        {
            var size_ = size * Scale;
            var pos = GetGlobalLeftUpPosition();
            var sizeX = new asd.Vector2DF(size_.X, 0.0f);
            var sizeY = new asd.Vector2DF(0.0f, size_.Y);
            sizeX.Degree += Angle;
            sizeY.Degree += Angle;
            
            var color_ = color ?? AbsoluteColor;
            
            Material2d.SetVector2DF("g_position", pos);
            //Material2d.SetFloat("g_angle", Angle);
            Material2d.SetVector2DF("g_sizeX", sizeX);
            Material2d.SetVector2DF("g_sizeY", sizeY);

            coreObject.DrawSpriteWithMaterialAdditionally(
                pos,
                pos + sizeX,
                pos + sizeX + sizeY,
                pos + sizeY,
                color_, color_, color_, color_,
                new asd.Vector2DF(0.0f, 0.0f),
                new asd.Vector2DF(1.0f, 0.0f),
                new asd.Vector2DF(1.0f, 1.0f),
                new asd.Vector2DF(0.0f, 1.0f),
                material2D,
                AlphaBlend,
                AbsoluteDrawingPriority
            );
        }
    }
}
