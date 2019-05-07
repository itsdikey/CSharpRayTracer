using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace RaytracerCSharp.AllStuff
{
    public class Light
    {
        public vec3 LightPos { get; set; }
        public vec4 LightColor { get; set; }
        public float LightStrength { get; set; }
    }
}
