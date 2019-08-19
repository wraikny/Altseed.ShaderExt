using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    abstract class ShaderObjectBase : EmptyDrawnObject2D
    {
        public ShaderObjectBase()
        {
            OnDrawAdditionallyEvent += () =>
            {
                Draw();
            };
        }

        internal abstract void Draw();

        internal void DrawAdditionally(asd.Vector2DF size)
        {
            
        }
    }
}
