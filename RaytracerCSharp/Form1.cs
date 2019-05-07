using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastBitmapLib;
using GlmSharp;
using RaytracerCSharp.AllStuff;
using RaytracerCSharp.Primitives;
using RaytracerCSharp.Rays;
using RaytracerCSharp.Utils;

namespace RaytracerCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private Scene _scene;
        private Renderer _renderer;

        protected override void OnLoad(EventArgs e)
        {
            this.DoubleBuffered = true;
            _scene = new MyScene();
            _renderer = new Renderer(Width, Height);
            _scene.OnLoad();
            Invalidate();

            
            _renderer.Light = new Light()
            {
                LightPos = new vec3(0, 8, -2),
                LightStrength = 0.4f,
                LightColor = Color.Blue.ToVec4()
            };

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar == 's')
            {
                _renderer.EnableShadows = !_renderer.EnableShadows;
            }

            if (e.KeyChar == 'g')
            {
                _renderer.EnableGI = !_renderer.EnableGI;
            }


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _renderer.RayTraceScene(_scene);
            _renderer.RenderCached(e.Graphics);
            e.Graphics.DrawString("Press s to toggle shadows, g to toggle GI",Font,Brushes.White,0,0);
            Invalidate();
        }
    }
}