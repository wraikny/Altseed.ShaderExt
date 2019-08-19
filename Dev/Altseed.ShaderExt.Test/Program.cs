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

            var obj = new ShaderObject2DSimple("noise/perlinnoise.hlsl", null);

            obj.Size = asd.Engine.WindowSize.To2DF();

            asd.Engine.AddObject2D(obj);

            while(asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
