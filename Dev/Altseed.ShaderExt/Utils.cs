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
            var filename = "Altseed.ShaderExt.Shaders/" + path;

            if (!asd.Engine.File.Exists(filename))
            {
                asd.Engine.File.AddRootPackage("Altseed.ShaderExt.Shaders.pack");

                if (!asd.Engine.File.Exists(filename))
                {
                    throw new FileNotFoundException();
                }
            }

            var buf = asd.Engine.File.CreateStaticFile(filename).Buffer;
            return Encoding.UTF8.GetString(buf);
        }

        public static asd.Texture2D CreateTexture2D(string path)
        {

            var filename = "Altseed.ShaderExt.Shaders/" + path;

            if(!asd.Engine.File.Exists(filename))
            {
                asd.Engine.File.AddRootPackage("Altseed.ShaderExt.Shaders.pack");

                if(!asd.Engine.File.Exists(filename))
                {
                    throw new FileNotFoundException();
                }
            }

            return asd.Engine.Graphics.CreateTexture2D(filename);
        }
    }
}
