using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using RaytracerCSharp.Rays;

namespace RaytracerCSharp.Primitives
{
    public class EndlessPlane : IHittable
    {
        public vec3 Normal { get; set; }

        public float Distance { get; set; }

        public EndlessPlane(vec3 normal, float distance)
        {
            Normal = normal;
            Distance = distance;
        }

        public EndlessPlane(vec3[] points)
        {
            if(points.Length<=2)
                throw new ArgumentException();

            var edge1 = points[1] - points[0];
            var edge2 = points.Last() - points[0];

            Normal = vec3.Cross(edge1, edge2);
            Distance = vec3.Dot(Normal.Normalized, points[0]);
        }

        public bool Hit(Ray ray, out RayHit hit, out Material material)
        {
            var dotNormalRayDir = vec3.Dot(ray.Direction, Normal.Normalized);
            material = Material.None;
            if (dotNormalRayDir==0)
            {
                hit = null;
                return false;
            }

            var t = (Distance - vec3.Dot(ray.Origin, Normal.Normalized)) / dotNormalRayDir;

            if (t >= 0)
            {
                hit = new RayHit();;
                hit.HitPoint = ray.Origin + t * ray.Direction;
                hit.Normal = Normal.Normalized;
                return true;
            }

            hit = null;
            return false;
        }
    }
}
