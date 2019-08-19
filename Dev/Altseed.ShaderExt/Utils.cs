using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class Utils
    {
        public static void AddPackage()
        {
            asd.Engine.File.AddRootPackage("Altseed.ShaderExt.Shaders.pack");
        }

        public static string LoadFile(string path)
        {
            var filename = "Altseed.ShaderExt.Shaders/" + path;

            if (!asd.Engine.File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            var buf = asd.Engine.File.CreateStaticFile(filename).Buffer;
            return Encoding.UTF8.GetString(buf);
        }

        private static string LoadShaderText(string path)
        {
            var text = LoadFile(path);

            var dirs = path.Split('/');
            var dir = (dirs.Count() == 1)
                ? String.Empty
                : (String.Join("/", dirs.Take(dirs.Length - 1)) + "/")
            ;

            var r = new Regex("#include \\\"(?<filename>.+)\\\"");

            var matches = r.Matches(text);
            var buf = new StringBuilder();
            int lastIndex = 0;
            foreach (Match item in matches)
            {
                buf.Append(text.Substring(lastIndex, item.Index - lastIndex));

                var filename = item.Groups["filename"].Value;
                buf.Append(LoadShaderText(dir + filename));

                lastIndex = item.Index + item.Length;
            }
            return buf.ToString() + text.Substring(lastIndex);
        }

        internal static asd.Shader2D LoadShader2D(string path)
        {
            var text = LoadShaderText(path);
#if DEBUG
            Console.WriteLine("Code: " + text);
#endif
            return asd.Engine.Graphics.CreateShader2D(text);
        }

        public static asd.Texture2D CreateTexture2D(string path)
        {

            var filename = "Altseed.ShaderExt.Shaders/" + path;

            if (!asd.Engine.File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            return asd.Engine.Graphics.CreateTexture2D(filename);
        }
    }
}
