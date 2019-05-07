using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;


namespace RaytracerCSharp.Primitives
{
    public struct Material
    {
        public float AmbientCoeff;
        public vec4 AmbientColor;
        public vec4 LambertColor;
        public float Reflectiveness;
        public bool IsValidMaterial;
        public static Material None => new Material(){IsValidMaterial = false};
    }
}
