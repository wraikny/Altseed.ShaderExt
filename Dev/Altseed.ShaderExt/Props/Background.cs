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
        public static Background Color(asd.Color color) => new Background(color);
        public static Background Color(byte r, byte g, byte b, byte a) => Color(new asd.Color(r, g, b, a));
        public static Background Color(byte r, byte g, byte b) => Color(new asd.Color(r, g, b));
        public static Background Color(int r, int g, int b, int a) => Color(new asd.Color(r, g, b, a));

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
