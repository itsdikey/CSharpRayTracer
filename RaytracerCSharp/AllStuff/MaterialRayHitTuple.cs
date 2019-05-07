using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaytracerCSharp.Primitives;
using RaytracerCSharp.Rays;

namespace RaytracerCSharp.AllStuff
{
    public class MaterialRayHitTuple
    {
        public RayHit Hit { get; set; }
        public Material Material { get; set; }
    }
}
