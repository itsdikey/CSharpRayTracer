using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaytracerCSharp.Utils
{
    public class Mathf
    {

        public static float PI;

        static Mathf()
        {
            PI = (float) Math.PI;
        }

        public static float pow(float x, int y)
        {
            float result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= x;
            }
            return result;
        }

        public static float square(float x)
        {
            return pow(x, 2);
        }

        public static float sqrt(float x)
        {
            return (float) Math.Sqrt(x);
        }

        public static float min(float x, float y)
        {
            return x < y ? x : y;
        }

        public static float max(float x, float y)
        {
            return x > y ? x : y;
        }

        public static bool solveQuad(float a, float b, float c, ref float x0, ref float x1)
        {
            float discr = b * b - 4 * a * c;
            if (discr < 0) return false;
            else if (discr == 0) x0 = x1 = -0.5f * b / a;
            else
            {
                float q = (b > 0) ?
                    -0.5f * (b + sqrt(discr)) :
                    -0.5f * (b - sqrt(discr));
                x0 = q / a;
                x1 = c / q;
            }

            if (x0 > x1)
            {
                float temp = x0;
                x0 = x1;
                x1 = temp;
            }

            return true;
        }

        public static float angleDegToRad(float angle)
        {
            return angle / 180f * Mathf.PI;
        }

        public static float angleRadToDeg(float angle)
        {
            return angle / Mathf.PI * 180f;
        }
    }
}
