using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class Background
    {
        private asd.Color? color = null;
        private Background() { }
        private Background(asd.Color color) { this.color = color; }

        public static Background Discard => new Background();
        public static Background ConsoleColor(asd.Color color) => new Background(color);

        public void Match(Action fDiscard, Action<asd.Color> fColor)
        {
            if (color == null)
            {
                fDiscard();
            }
            else
            {
                fColor(color.Value);
            }
        }
    }
}
