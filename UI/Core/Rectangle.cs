using OpenTK.Graphics.OpenGL;
using UIGL.Render;

namespace UIGL.UI.Core {
    public class Rectangle : Control {
        protected override void Render() {
            GL.Color3(0.5, 0.3, 0.7);
            Drawing.DrawRect(0, 0, this.TargetWidth, this.TargetHeight);
        }
    }
}