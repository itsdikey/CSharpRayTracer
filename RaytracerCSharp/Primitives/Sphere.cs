using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using RaytracerCSharp.Rays;
using RaytracerCSharp.Utils;

namespace RaytracerCSharp.Primitives
{
    public class Sphere : IHittable
    {
        public vec3 Origin { get; set; }
        public float Radius { get; set; }

        public Material Material { get; set; }
        public Sphere(vec3 origin, float r, Material mat)
        {
            Origin = origin;
            Radius = r;
            Material = mat;
        }

        public void TransformOrigin(mat4 transform)
        {
            Origin = (transform * new vec4(Origin)).xyz;
        }

        public bool Hit(Ray ray, out RayHit hit, out Material material)
        {



            var l = ray.Origin - Origin;


            var a = 1;
            var b = 2 * vec3.Dot(ray.Direction,l);
            var c = l.LengthSqr - Mathf.square(Radius);

          
            //var d = Mathf.square(b) 
            //        - 4*c;

            //if (d < 0)
            //{
            //    hit = null;
            //    color = Color.Black;
            //    return false;
            //}

            //d = Mathf.sqrt(d);

            //var t1 = (-b - d) / 2;
            //var t2 = (-b + d) / 2;

            float t1=default(float), t2=default(float);

            if (!Mathf.solveQuad(a, b, c, ref t1, ref t2))
            {
                hit = null;
                material = Material.None;
                return false;
            }
            


            if (Math.Abs(t1 - t2) < 0.0002f)
            {
                hit = null;
                material = Material.None;
                return false;
            }

            if (t1 > 0 && t2 > 0)
            {
                var target = Mathf.min(t1, t2);
                var hitPoint = ray.PointAt(target);
                hit = new RayHit(){HitPoint = hitPoint, Normal = (hitPoint-Origin).Normalized};
                material = Material;
                return true;
            }

            if (Math.Sign(t1)!=Math.Sign(t2))
            {
                hit = null;
                material = Material.None;
                return false;
            }

            hit = null;
            material = Material.None;
            return false;
        }


    }
}
