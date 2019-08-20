using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class PostEffectReactive : asd.PostEffect
    {
        public event Action<asd.RenderTexture2D, asd.RenderTexture2D> OnDrawEvent = delegate { };
        protected override void OnDraw(asd.RenderTexture2D dst, asd.RenderTexture2D src)
        {
            base.OnDraw(dst, src);
            OnDrawEvent(dst, src);
        }

        /// <summary>
        /// マテリアルを用いてテクスチャに画像を描画する。
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="material2d"></param>
        public new void DrawOnTexture2DWithMaterial(asd.RenderTexture2D dst, asd.Material2D material2d)
        {
            base.DrawOnTexture2DWithMaterial(dst, material2d);
        }
    }
}
