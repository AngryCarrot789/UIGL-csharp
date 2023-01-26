using OpenTK.Graphics.OpenGL;

namespace UIGL.Render {
    public static class Drawing {
        public static void LoadOrthoMatrix2D(double width, double height, double near = -1d, double far = 1d) {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, width, height, 0, near, far);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public static void DrawRect(float x, float y, float w, float h) {
            GL.PushMatrix();
            GL.Translate(x, y, 0f);

            float x2 = x + w;
            float y2 = y + h;

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(x2, y2);
            GL.Vertex2(x2, y);
            GL.Vertex2(x, y);
            GL.Vertex2(x, y2);
            GL.End();

            GL.PopMatrix();
        }
    }
}