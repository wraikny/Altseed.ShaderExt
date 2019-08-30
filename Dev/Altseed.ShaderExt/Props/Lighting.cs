using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public sealed class LightType
    {
        private readonly bool isDirectional;
        private asd.Vector3DF p;

        private LightType(bool isDirectional, asd.Vector3DF p)
        {
            this.isDirectional = isDirectional;
            this.p = p;
        }

        public static LightType Directional(asd.Vector3DF direction)
        {
            direction.Normalize();
            return new LightType(true, direction);
        }

        public static LightType Directional(float x, float y, float z) => Directional(new asd.Vector3DF(x, y, z));
        public static LightType Point(asd.Vector3DF pos) => new LightType(false, pos);
        public static LightType Point(float x, float y, float z) => Point(new asd.Vector3DF(x, y, z));

        public void Mathch(Action<asd.Vector3DF> fDirectional, Action<asd.Vector3DF> fPoint)
        {
            if(isDirectional)
            {
                fDirectional(p);
            }
            else
            {
                fPoint(p);
            }
        }

        public asd.Vector4DF ToVector4()
        {
            var w = isDirectional ? 1.0f : 0.0f;
            return new asd.Vector4DF(p.X, p.Y, p.Z, w);
        }
    }
}
