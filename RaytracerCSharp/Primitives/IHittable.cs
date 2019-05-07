using System.Drawing;
using RaytracerCSharp.Rays;

namespace RaytracerCSharp.Primitives
{
    public interface IHittable
    {
        bool Hit(Ray ray, out RayHit hit, out Material material);
    }
}