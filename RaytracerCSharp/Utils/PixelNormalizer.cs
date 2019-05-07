using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace RaytracerCSharp
{
    public class PixelNormalizer
    {
        private float ImageWidth { get; set; }
        private float ImageHeight { get; set; }

        private float Fov { get; set; }

        private vec3[,] _dataCache;

        public PixelNormalizer(int imageWidth, int imageHeight, float fov)
        {
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            _dataCache = new vec3[imageWidth,imageHeight];
            Fov = fov;
        }

        public vec3 GetAt(int x, int y)
        {
            return _dataCache[x, y];
        }

        public void Initialize()
        {
            float imageRatio = ImageWidth / ImageHeight;
            var tan = (float) Math.Tan(Fov/2);
            for (int x = 0; x < (int) ImageWidth; x++)
            {
                for (int y = 0; y < (int) ImageHeight; y++)
                {
                    float normalizedX = (x + 0.5f) / ImageWidth;
                    float normalizedY = (y + 0.5f) / ImageHeight;
                    float pixelScreenX = 2 * normalizedX - 1;
                    float pixelScreenY = 1 - 2 * normalizedY;
                    float pX = pixelScreenX * tan * imageRatio;
                    float pY = pixelScreenY * tan;

                    _dataCache[x,y] = new vec3(pX,pY,-1);

                    // _dataCache[x,y] = 
                }
            }

        }




    }
}
