using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using RaytracerCSharp.Rays;

namespace RaytracerCSharp.Primitives
{
    public class Triangle : IHittable
    {
        public Triangle(params vec3[] points)
        {
            if(points.Length!=3)
                throw new ArgumentException();

            Points = points;

            ContainingPlane = new EndlessPlane(points);

        }

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

        public vec3[] Points { get; set; }

        public EndlessPlane ContainingPlane { get; private set; }

        public Material Material { get; set; }

        public bool Hit(Ray ray, out RayHit hit, out Material material)
        {
            hit = null;
            material = Material.None;
            if (ContainingPlane.Hit(ray, out hit, out material))
            {
                var areaTotal = ContainingPlane.Normal.Length;

           
                vec3 edge0 = Points[1] - Points[0];
                vec3 vp0 = hit.HitPoint - Points[0];
                vec3 cross = vec3.Cross(edge0,vp0);
                if (vec3.Dot(ContainingPlane.Normal,cross) < 0) return false; // P is on the right side 

                // edge 1
                vec3 edge1 = Points[2] - Points[1];
                vec3 vp1 = hit.HitPoint - Points[1];
                cross = vec3.Cross(edge1, vp1);
                var u = cross.Length / areaTotal;
                if (u > 1) return false;
                if (vec3.Dot(ContainingPlane.Normal, cross) < 0) return false; // P is on the right side 

                // edge 2
                var edge2 = Points[0] - Points[2];
                vec3 vp2 = hit.HitPoint - Points[2];
                cross = vec3.Cross(edge2, vp2);
                var v = cross.Length / areaTotal;
                if (v > 1) return false;
                if (vec3.Dot(ContainingPlane.Normal, cross) < 0) return false; // P is on the right side; 

                if (u + v > 1 || u+v<0)
                    return false;
                material = Material;

                return true; // this ray hits the triangle 
               
            }
            return false;
        }

        public void Transform(mat4 transform)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                var vec4 = new vec4(Points[i],1);
                Points[i] = (transform * vec4).xyz;
            }
            ContainingPlane = new EndlessPlane(Points);
        }
    }
}
