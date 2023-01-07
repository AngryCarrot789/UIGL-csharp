using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace UIGL.UI {
    public class Square : UIElement {
        protected override void Render() {
            GL.Color3(0.1f, 0.1f, 0.7f);
            GL.Vertex2( 0.5f, -0.5f);
            GL.Vertex2( 0.0f,  0.5f);
            GL.Vertex2(-0.5f, -0.5f);
        }
    }
}