using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using UIGL.Render;

namespace UIGL.UI.Core {
    public class Control {
        public bool isDirty = true;

        public Thickness Margin { get; set; }

        public float TargetWidth { get; set; }
        public float TargetHeight { get; set; }

        public float RenderWidth { get; private set; }

        public float RenderHeight { get; private set; }

        public Control() {

        }

        public void Measure() {

        }

        protected virtual void Render() {

        }

        public static void Render(Control element) {
            GL.PushMatrix();
            GL.Translate(element.Margin.Left, element.Margin.Top, 0d);
            element.Render();
            GL.PopMatrix();
        }
    }
}