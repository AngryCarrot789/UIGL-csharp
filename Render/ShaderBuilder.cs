using System;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace UIGL.Render {
    public class ShaderBuilder {
        private int progId;
        private int vertId;
        private int fragId;

        public ShaderBuilder LoadSource(string directory, string vertFile, string fragFile) {
            string vertSrc = File.ReadAllText(Path.Combine(directory, vertFile));
            string fragSrc = File.ReadAllText(Path.Combine(directory, fragFile));
            return this.LoadSource(vertSrc, fragSrc);
        }

        public ShaderBuilder LoadSource(string vertSrc, string fragSrc) {
            this.vertId = CompileShader(vertSrc, ShaderType.VertexShader);
            this.fragId = CompileShader(fragSrc, ShaderType.FragmentShader);

            this.progId = GL.CreateProgram();
            GL.AttachShader(this.progId, this.vertId);
            GL.AttachShader(this.progId, this.fragId);

            return this;
        }

        public ShaderBuilder BindAttribLocation(int index, string attributeName) {
            GL.BindAttribLocation(this.progId, index, attributeName);
            return this;
        }

        public Shader Build() {
            GL.LinkProgram(this.progId);
            GL.DeleteShader(this.vertId);
            GL.DeleteShader(this.fragId);

            return new Shader(this.progId, this.vertId, this.fragId);
        }

        private static int CompileShader(string code, ShaderType type) {
            int id = GL.CreateShader(type);
            GL.ShaderSource(id, code);
            GL.CompileShader(id);

            GL.GetShader(id, ShaderParameter.CompileStatus, out int isCompiled);
            if (isCompiled < 1) {
                string err = GL.GetShaderInfoLog(id);
                GL.ShaderSource(id, "");
                GL.DeleteShader(id);

                throw new Exception("Failed to compile shader: " + err);
            }

            return id;
        }
    }
}