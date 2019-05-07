using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaytracerCSharp.Primitives;
using RaytracerCSharp.Rays;

namespace RaytracerCSharp.AllStuff
{
    public class Scene
    {
        protected List<IHittable> _hittables;

        public virtual void OnLoad()
        {
            _hittables = new List<IHittable>();
        }

        public List<MaterialRayHitTuple> RayHit(Ray ray)
        {
            var hits = new List<MaterialRayHitTuple>();
            foreach (var hittable in _hittables)
            {
                if (hittable.Hit(ray, out RayHit rayHit, out Material material))
                {
                    hits.Add(new MaterialRayHitTuple() {Hit = rayHit, Material = material});
                }
            }

            return hits;
        }
    }
}
