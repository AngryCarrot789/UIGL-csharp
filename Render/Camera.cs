using OpenTK.Mathematics;
using UIGL.Utils;

namespace UIGL.Render {
    public class Camera {
        public Vector3 position = new Vector3(0f, 0f, 1f);

        public Matrix4 GetProjection(float fovDeg, float width, float height, float near, float far) {
            float fovRads = 1.0f / FMath.Tanh(fovDeg * Maths.PI / 360.0f);
            float aspect = height / width;
            float distance = near - far;

            Matrix4 proj = Matrix4.Identity;
            proj[0, 0] = fovRads * aspect;
            proj[0, 1] = 0.0f;
            proj[0, 2] = 0.0f;
            proj[0, 3] = 0.0f;

            proj[1, 0] = 0.0f;
            proj[1, 1] = fovRads;
            proj[1, 2] = 0.0f;
            proj[1, 3] = 0.0f;

            proj[2, 0] = 0.0f;
            proj[2, 1] = 0.0f;
            proj[2, 2] = (near + far) / distance;
            proj[2, 3] = 2f * near * far / distance;

            proj[3, 0] = 0.0f;
            proj[3, 1] = 0.0f;
            proj[3, 2] = -1.0f;
            proj[3, 3] = 0.0f;
            return proj;
        }

        public Matrix4 GetView() {
            return Matrix4.CreateTranslation(-this.position);
        }
    }
}