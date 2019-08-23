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

        public static string LoadFile(string filename)
        {
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

        public static asd.Shader2D LoadShader2D(string path)
        {
            var text = LoadShaderText(path);
#if DEBUG
            {
                var dirPath = "Altseed.ShaderExt.Cache";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var textWithLineNums = new StringBuilder();
                var textLineIndex = 1;
                foreach(var s in text.Split('\n'))
                {
                    textWithLineNums.Append(String.Format("{0}: {1}\n", textLineIndex, s));
                    textLineIndex++;
                }
                Console.WriteLine("Code:\n" + textWithLineNums.ToString());
            }
#endif
            return asd.Engine.Graphics.CreateShader2D(text);
        }

        public static asd.Texture2D CreateTexture2D(string filename)
        {
            if (!asd.Engine.File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            return asd.Engine.Graphics.CreateTexture2D(filename);
        }

        internal static asd.Shader2D LoadShader2DInternal(string path)
        {
            return LoadShader2D("Altseed.ShaderExt.Shaders/" + path);
        }

        internal static asd.Texture2D CreateTexture2DInternal(string path)
        {
            return CreateTexture2D("Altseed.ShaderExt.Shaders/" + path);
        }

        internal static class Path
        {
            private const string directory = "Altseed.ShaderExt.Shaders";

            internal const string Disolve = directory + "/disolve/disolve";
            internal const string ChromaticAberrationSimple = directory + "/chromaticAberration/chromaticAberrationSimple";
            internal const string Noise = directory + "/noise/noise";
        }
    }
}
