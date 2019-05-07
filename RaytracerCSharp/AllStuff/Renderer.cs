using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastBitmapLib;
using GlmSharp;
using RaytracerCSharp.Primitives;
using RaytracerCSharp.Rays;
using RaytracerCSharp.Utils;

namespace RaytracerCSharp.AllStuff
{
    public class Renderer
    {
        public Light Light { get; set; }

        private Bitmap _screenBitmap;
        private PixelNormalizer _pixelNormalizer;

        public bool EnableShadows { get; set; }

        public bool EnableGI { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Renderer(int screenWidth, int screenHeight)
        {
            Width = screenWidth;
            Height = screenHeight;

            _screenBitmap = new Bitmap(this.Width, this.Height);
            _pixelNormalizer = new PixelNormalizer(this.Width, this.Height, (float)Math.PI / 2f);
            _pixelNormalizer.Initialize();

            using (var fastBitmap = _screenBitmap.FastLock())
            {
                fastBitmap.Clear(Color.CornflowerBlue);
            }
        }

        public void RayTraceScene(Scene scene)
        {
            using (var fastBitmap = _screenBitmap.FastLock())
            {
                fastBitmap.Clear(Color.CornflowerBlue);
                for (int x = 0; x < fastBitmap.Width; x++)
                {
                    for (int y = 0; y < fastBitmap.Height; y++)
                    {
                        var targetRay = Ray.ConstructRayFromTo(vec3.Zero, _pixelNormalizer.GetAt(x, y));
                        MaterialRayHitTuple closest = GetClosestHit(scene, targetRay);

                        if (closest != null)
                        {

                            vec4 colVec = CalculateColor(closest.Hit, closest.Material, scene, targetRay);

                            fastBitmap.SetPixel(x, y, colVec.ClampTop().ToColor());
                        }
                    }
                }
            }
        }

        private static MaterialRayHitTuple GetClosestHit(Scene scene, Ray targetRay)
        {
            var hits = scene.RayHit(targetRay);
            var closest = hits.FirstOrDefault();

            foreach (var hit in hits)
            {
                if (Math.Abs(hit.Hit.HitPoint.z) < Math.Abs(closest.Hit.HitPoint.z))
                {
                    closest = hit;
                }
            }

            return closest;
        }

        public void RenderCached(Graphics g)
        {
            g.DrawImage(_screenBitmap, new Point(0,0));
        }

        private vec4 CalculateColor(RayHit rayHit, Material material, Scene scene, Ray fromRay, int numberOfReflect=0)
        {
            if (numberOfReflect > 5)
                return vec4.Zero;

            vec4 ambient = material.AmbientColor * material.AmbientCoeff;
            ambient.x = 1;
            float diffuse = vec3.Dot(Light.LightPos - rayHit.HitPoint, rayHit.Normal);
            diffuse = Mathf.max(0, diffuse);

            if (EnableShadows)
            {
                var lightRay = Ray.ConstructRayFromTo(rayHit.HitPoint + rayHit.Normal * 0.01f, Light.LightPos);
                var lightHits = scene.RayHit(lightRay);
                if (lightHits.Count > 0)
                    diffuse = 0;
            }
           
            vec4 diffuseColor = diffuse > 0 ? material.LambertColor * diffuse : vec4.Zero;
            var colVec = ambient + Light.LightStrength * ColorToVec4.Blend(Light.LightColor, diffuseColor, material.Reflectiveness);
            if (colVec.Length <= 0)
            {
                colVec.x = 1;
            }


            if (EnableGI)
            {
                vec3 direction = vec3.Reflect(-fromRay.Direction, rayHit.Normal).Normalized;
                Ray newRay = Ray.ConstructRay(rayHit.HitPoint + rayHit.Normal * 0.01f, direction);
                var hit = GetClosestHit(scene, newRay);
                if (hit != null)
                    return colVec + material.Reflectiveness * CalculateColor(hit.Hit, material, scene, newRay, numberOfReflect + 1);
            }
          
            return colVec;
        }
    }
}
