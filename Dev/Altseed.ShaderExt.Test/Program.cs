using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            asd.Engine.Initialize("Altseed.ShaderExt.Test", 800, 450, new asd.EngineOption());

            Altseed.ShaderExt.Utils.AddPackage();

            var ws = asd.Engine.WindowSize.To2DF();

            var obj = new ShaderObject2DSimple("noise/perlinnoise.hlsl", null)
            {
                Size = ws * 0.5f,
                Position = ws * 0.5f,
                CenterPosition = ws * 0.25f
            };

            asd.Engine.AddObject2D(obj);

            while(asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
