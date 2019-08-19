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

            while(asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
