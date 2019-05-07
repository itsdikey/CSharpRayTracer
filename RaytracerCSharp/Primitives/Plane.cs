using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using RaytracerCSharp.Rays;

namespace RaytracerCSharp.Primitives
{
    public class Plane : IHittable
    {
        public Plane(params vec3[] points)
        {
            Points = points;
            FirstTriangle = new Triangle(points[0], points[1], points[2]);
            SecondTriangle = new Triangle(points[0], points[2], points[3]);
        }

        public Material Material { get; set; }

        public vec3[] Points { get; set; }

        public Triangle FirstTriangle { get; set; }
        public Triangle SecondTriangle { get; set; }

        public vec3 Position
        {
            get
            {
                var pos = vec3.Zero;
                foreach (var point in Points)
                {
                    pos += point;
                }

                return pos / Points.Length;
            }
        }

        public bool Hit(Ray ray, out RayHit hit, out Material material)
        {
            var hit1 = FirstTriangle.Hit(ray, out hit, out material);
            if (hit1)
            {
                material = Material;
                return true;
            }
            var hit2 = SecondTriangle.Hit(ray, out hit, out material);
            if (hit2)
            {
                material = Material;
                return true;
            }


            return false;
        }

        public void Transform(mat4 transform)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                var vec4 = new vec4(Points[i], 1);
                Points[i] = (transform * vec4).xyz;
            }
            FirstTriangle = new Triangle(Points[0], Points[1], Points[2]);
            SecondTriangle = new Triangle(Points[0], Points[2], Points[3]);
        }
    }
}
