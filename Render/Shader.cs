using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace UIGL.Render {
    public class Shader {
        public int ProgramId { get; }

        private readonly Dictionary<string, int> uniforms;

        internal Shader(int progId) {
            this.ProgramId = progId;
            this.uniforms = new Dictionary<string, int>();
        }

        public static ShaderBuilder Builder() {
            return new ShaderBuilder();
        }

        public void Use() {
            GL.UseProgram(this.ProgramId);
        }

        public int GetUniformLocation(string name) {
            if (this.uniforms.TryGetValue(name, out int location)) {
                return location;
            }
            else {
                return this.uniforms[name] = GL.GetUniformLocation(this.ProgramId, name);
            }
        }

        public void SetUniformBool(string name, bool value) {
            GL.Uniform1(this.GetUniformLocation(name), value ? 1 : 0);
        }

        public void SetUniformInt(string name, int value) {
            GL.Uniform1(this.GetUniformLocation(name), value);
        }

        public void SetUniformFloat(string name, float value) {
            GL.Uniform1(this.GetUniformLocation(name), value);
        }

        public void SetUniformVec2(string name, in Vector2 value) {
            GL.Uniform2(this.GetUniformLocation(name), value.X, value.Y);
        }

        public void SetUniformVec2(int location, in Vector2 value) {
            GL.Uniform2(location, value.X, value.Y);
        }

        public static void SetUniformVec2(int location, float x, float z) {
            GL.Uniform2(location, x, z);
        }

        public void SetUniformVec3(string name, in Vector3 value) {
            GL.Uniform3(this.GetUniformLocation(name), value.X, value.Y, value.Z);
        }

        public void SetUniformVec4(string name, in Vector4 value) {
            GL.Uniform4(this.GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
        }

        public void SetUniformMatrix4(string name, Matrix4 value) {
            unsafe {
                GL.UniformMatrix4(this.GetUniformLocation(name), 1, true, (float*) &value);
            }
        }

        public static void SetUniformMatrix4(int location, Matrix4 value) {
            unsafe {
                GL.UniformMatrix4(location, 1, true, (float*) &value);
            }
        }
    }
}