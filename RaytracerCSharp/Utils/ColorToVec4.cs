using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace RaytracerCSharp.Utils
{
    public static class ColorToVec4
    {
        public static vec4 ToVec4(this Color color)
        {
            return new vec4(color.A, color.R, color.G, color.B).To01();
        }

        public static Color ToColor(this vec4 colVec)
        {
            return Color.FromArgb((int) (colVec.x * 255), (int) (colVec.y * 255), (int) (colVec.z * 255),
                (int) (colVec.w * 255));
        }

        public static vec4 To01(this vec4 target)
        {
            return target / 255f;
        }

        public static vec4 ClampTop(this vec4 target)
        {
            var x = Mathf.min(target.x, 1);
            var y = Mathf.min(target.y, 1);
            var z = Mathf.min(target.z, 1);
            var w = Mathf.min(target.w, 1);
            return new vec4(x,y,z,w);
        }

        public static vec4 Blend(vec4 first, vec4 second, float amount)
        {
            return amount * first + second * (1f - amount);
        }
    }
}
