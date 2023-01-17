using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using UIGL.Utils;

namespace UIGL.Render {
    public class FrameBuffer {
        private int width;
        private int height;

        public void BeginRender(int w, int h) {
            this.width = w;
            this.height = h;
        }

        public void DrawTriangle() {
            GL.Begin(BeginMode.Triangles);

            // Vector3 a = new Vector3(0.5f, -0.5f, 0f);
            // Vector3 b = new Vector3(0.0f, 0.5f, 0f);
            // Vector3 c = new Vector3(-0.5f, -0.5f, 0f);
            Vector3 a = new Vector3(500f, -500f, 0f);
            Vector3 b = new Vector3(0f, 500f, 0f);
            Vector3 c = new Vector3(-500f, -500f, 0f);

            GL.Vertex3(a);
            GL.Vertex3(b);
            GL.Vertex3(c);

            GL.End();
        }

        public void DrawSquareAABB(int x, int y, int w, int h) {
            GL.Begin(PrimitiveType.Quads);

            int x2 = w + x;
            int y2 = h + y;

            // GL.Vertex3(x2, y2, 0f);
            // GL.Vertex3(x2, y,  0f);
            // GL.Vertex3(x,  y,  0f);
            // GL.Vertex3(x,  y2, 0f);

            // 0th pixels are not rendered which is why 1 is added to x and y; e.g 0,0,500,500
            GL.Vertex3(x2,    y2,    0f);
            GL.Vertex3(x2,    y + 1, 0f);
            GL.Vertex3(x + 1, y + 1, 0f);
            GL.Vertex3(x + 1, y2,    0f);

            GL.End();
        }
    }
}