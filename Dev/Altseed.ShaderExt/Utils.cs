using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class Utils
    {
        public static string LoadFile(string path)
        {
            if(!asd.Engine.File.Exists("Altseed.ShaderExt.Shaders/license.txt"))
            {
                asd.Engine.File.AddRootPackage("Altseed.ShaderExt.Shaders.pack");
            }

            var buf = asd.Engine.File.CreateStaticFile("Altseed.ShaderExt.Shaders/" + path).Buffer;
            return Encoding.UTF8.GetString(buf);
        }

        public static asd.Texture2D CreateTexture2D(string path)
        {
            if (!asd.Engine.File.Exists("Altseed.ShaderExt.Shaders/license.txt"))
            {
                asd.Engine.File.AddRootPackage("Altseed.ShaderExt.Shaders.pack");
            }

            var filename = "Altseed.ShaderExt.Shaders/" + path;

            if(!asd.Engine.File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            return asd.Engine.Graphics.CreateTexture2D(filename);
        }
    }
}
