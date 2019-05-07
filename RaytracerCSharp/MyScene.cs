using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using RaytracerCSharp.AllStuff;
using RaytracerCSharp.Primitives;
using RaytracerCSharp.Utils;

namespace RaytracerCSharp
{
    public class MyScene : Scene
    {
        public override void OnLoad()
        {
            base.OnLoad();
            Sphere sphere = new Sphere(new vec3(0, 0, -6), 2,
                new Material()
                {
                    AmbientCoeff = 0.2f,
                    AmbientColor = Color.Brown.ToVec4(),
                    LambertColor = Color.Brown.ToVec4(),
                    IsValidMaterial = true,
                    Reflectiveness = 0.2f
                });

            Sphere sphere2 = new Sphere(new vec3(-3, 0, -8), 2,
                new Material()
                {
                    AmbientCoeff = 0.2f,
                    AmbientColor = Color.Blue.ToVec4(),
                    LambertColor = Color.Blue.ToVec4(),
                    IsValidMaterial = true,
                    Reflectiveness = 0.5f
                });

            Sphere sphere3 = new Sphere(new vec3(3, 0, -8), 2,
                new Material()
                {
                    AmbientCoeff = 0.2f,
                    AmbientColor = Color.Green.ToVec4(),
                    LambertColor = Color.Green.ToVec4(),
                    IsValidMaterial = true,
                    Reflectiveness = 0.5f
                });

            Triangle triangle = new Triangle(new vec3(-1, 0, -2), new vec3(1, 0, -2), new vec3(1, 1, -2));
            /*var position = new vec3(_triangle.Position);

            
            _triangle.Transform(mat4.Translate(position)*mat4.RotateX(Mathf.angleDegToRad(-45))* mat4.Translate(-position));*/

            triangle.Material = new Material()
            {
                AmbientCoeff = 0.2f,
                AmbientColor = Color.Brown.ToVec4(),
                LambertColor = Color.Brown.ToVec4(),
                IsValidMaterial = true,
                Reflectiveness = 0.2f
            };

            Plane plane = new Plane(new vec3(-1, 0, -2), new vec3(1, 0, -2), new vec3(1, 1, -2), new vec3(-1, 1, -2));

            plane.Material = new Material()
            {
                AmbientCoeff = 0.2f,
                AmbientColor = Color.BlueViolet.ToVec4(),
                LambertColor = Color.BlueViolet.ToVec4(),
                IsValidMaterial = true,
                Reflectiveness = 0.2f
            };

            var position = new vec3(plane.Position);


            plane.Transform(mat4.Translate(position) * mat4.RotateX(Mathf.angleDegToRad(-45)) * mat4.Translate(-position));
            plane.Transform(mat4.Translate(position) * mat4.Scale(5) * mat4.Translate(-position));
            plane.Transform(mat4.Translate(new vec3(0,-2f,-4f)));


            _hittables.Add(sphere);
           // _hittables.Add(triangle);
            _hittables.Add(plane);
            _hittables.Add(sphere2);
            _hittables.Add(sphere3);

        }

    }
}
