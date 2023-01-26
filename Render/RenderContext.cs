using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using UIGL.UI;

namespace UIGL.Render {
    public class RenderContext {
        private static readonly Shader PRIMITIVE_SHADER;

        static RenderContext() {
            PRIMITIVE_SHADER = Shader.Builder().
                                      LoadSource("F:\\VSProjsV2\\UIGL\\Assets\\Shaders", "main_shader.vert", "colour_shader.frag").
                                      BindAttribLocation(0, "in_pos").
                                      BindAttribLocation(1, "in_colour").
                                      Build();
        }

        public UIWindow CurrentWindow { get; private set; }

        public RenderContext() {

        }

        public void BeginFrame(UIWindow window) {
            this.CurrentWindow = window;
            PRIMITIVE_SHADER.Use();
            PRIMITIVE_SHADER.SetUniformMatrix4("projection", window.Perspective);
            // PRIMITIVE_SHADER.SetUniformMatrix4("view", Matrix4.Identity);
        }

        public void EndFrame() {
            this.CurrentWindow = null;
        }

        public void DrawSquare(int x, int y, int width, int height, Color4 colour) {
            unsafe {
                this.DrawSquare(x, y, width, height, *(Vector4*) &colour);
            }
        }

        public void DrawSquare(int x, int y, int width, int height, Vector4 colour) {
            PRIMITIVE_SHADER.SetUniformVec4("in_colour", colour);
            GL.LineWidth(5f);
            GL.Begin(PrimitiveType.Quads);
            // 0th pixels are not rendered which is why 1 is added to x and y; e.g 0,0,500,500

            int x2 = width + x;
            int y2 = height + y;
            GL.Vertex3(x2,    y2,    0f);
            GL.Vertex3(x2,    y + 1, 0f);
            GL.Vertex3(x + 1, y + 1, 0f);
            GL.Vertex3(x + 1, y2,    0f);
            GL.End();
        }
    }
}