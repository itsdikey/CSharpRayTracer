using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace RaytracerCSharp.Rays
{
    public class Ray
    {
        public vec3 Origin { get; set; }
        public vec3 Direction { get; set; }

        public Ray()
        {

        }

        public static Ray ConstructRay(vec3 origin, vec3 dir)
        {
            return new Ray() {Origin = origin, Direction = dir.Normalized};
        }

        public static Ray ConstructRayFromTo(vec3 from, vec3 to)
        {
            var dir = to - from;
            return new Ray() { Origin = from, Direction = dir.Normalized };
        }

        public vec3 PointAt(float t)
        {
            return Origin + t * Direction;
        }
    }
}
