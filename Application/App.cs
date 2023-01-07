using System;
using System.Threading;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using UIGL.Application.Dispatch;
using UIGL.Render;
using UIGL.UI;
using UIGL.Utils;
using ErrorCode = OpenTK.Windowing.GraphicsLibraryFramework.ErrorCode;

namespace UIGL.Application {
    public class App {
        private bool isSetup;

        private UIWindow mainWindow;
        private bool isRunning;
        private bool isMarkedForShutdown;
        private int tick;

        public UIWindow MainWindow {
            get => this.mainWindow;
            set => this.mainWindow = value;
        }

        public App() {

        }

        public void Setup() {
            if (this.isSetup)
                return;
            this.isSetup = true;

            GLFW.SetErrorCallback(OnGLFWError);
            if (!GLFW.Init()) {
                throw new Exception("Unable to initialise GLFW");
            }

            GLFW.DefaultWindowHints();
            GLFW.WindowHint(WindowHintBool.Visible, true);
            GLFW.WindowHint(WindowHintBool.Resizable, true);
        }

        public void MarkForShutdown() {
            this.isMarkedForShutdown = true;
        }

        private void Shutdown() {
            this.isRunning = false;
            GLFW.Terminate();
        }

        public static void DrawSquare(float x, float y, float w, float h, ref Matrix4 cam) {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(0.1f, 0.3f, 0.8f);

            //
            // C ___ B
            //  |   |
            //  |___|
            // D     A

            GL.Vertex4(cam * new Vector4(x + w, y - h, 0, 1));
            GL.Vertex4(cam * new Vector4(x + w, y,     0, 1));
            GL.Vertex4(cam * new Vector4(x,     y,     0, 1));
            GL.Vertex4(cam * new Vector4(x,     y - h, 0, 1));

            GL.End();
        }

        public static void DrawSquare(float x, float y, float w, float h) {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.1f, 0.3f, 0.8f);
            GL.Vertex4(new Vector4(x + w, y - h, 0, 1));
            GL.Vertex4(new Vector4(x + w, y,     0, 1));
            GL.Vertex4(new Vector4(x,     y,     0, 1));
            GL.Vertex4(new Vector4(x,     y - h, 0, 1));
            GL.End();
        }

        public const float DEG_TO_RAD = 3.14159265f / 180f; // 0.01745329F

        public Matrix4 GetScreenMatrix() {
            return new Matrix4();
        }

        public void Start() {
            if (!this.isSetup) {
                this.Setup();
            }

            if (this.mainWindow == null) {
                throw new Exception("App started with no main window");
            }

            if (this.mainWindow != null) {
                this.mainWindow.MakeContextCurrent();
                this.mainWindow.Show();
            }

            GL.Viewport(0, 0, this.mainWindow.Width, this.mainWindow.Height);

            float w = this.mainWindow.Width, h = this.mainWindow.Height;
            // Matrix4 cameraMatrix = Matrix4.CreatePerspectiveFieldOfView(90f * DEG_TO_RAD, w / h, 0.01f, 10f);
            Matrix4 cameraMatrix = Matrix4.CreateOrthographic(w, h, 0f, 1f);

            long nextRun = 0L;
            long ticks = 0;

            float offsetX = 0f;
            float offsetY = 0f;
            Matrix4 view = new Matrix4();

            Shader shader = Shader.Builder().
                                   LoadSource("F:\\VSProjsV2\\UIGL\\Assets\\Shaders", "main_shader.vert", "colour_shader.frag").
                                   BindAttribLocation(0, "in_pos").
                                   Build();

            unsafe {
                GLFW.SetWindowRefreshCallback(this.mainWindow.Handle, hWnd => RenderApp());
                GLFW.SetKeyCallback(this.mainWindow.Handle, (window, key, code, action, mods) => {
                    switch (key) {
                        case Keys.W: offsetY -= 0.005f; break;
                        case Keys.S: offsetY += 0.005f; break;
                        case Keys.A: offsetX -= 0.005f; break;
                        case Keys.D: offsetX += 0.005f; break;
                    }
                });
            }

            try {
                GLFW.PollEvents();
                while (true) {
                    GLFW.WaitEvents();
                    // GLFW.PollEvents();
                    if (this.isMarkedForShutdown) {
                        break;
                    }

                    this.Tick();

                    view = Matrix4.LookAt(new Vector3(0, 0, 5f), new Vector3(0, 0, -1), Vector3.UnitY);

                    RenderApp();

                    Thread.Yield();
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception during main app tick: " + e);
            }

            void RenderApp() {
                ticks++;
                if (DateTime.Now.Ticks > nextRun) {
                    Console.WriteLine("Total ticks: " + ticks);
                    nextRun = DateTime.Now.Ticks + TimeSpan.FromSeconds(1).Ticks;
                }

                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.PushMatrix();
                Matrix4 projection = Matrix4.CreateOrthographic(w, h, 0f, 1f);
                Matrix4 mat = Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, -1), new Vector3(0, 1, 0));

                shader.SetUniformMatrix4("projection", projection);
                shader.SetUniformMatrix4("view", mat);
                shader.SetUniformVec4("in_colour", new Vector4(0.1f, 0.3f, 0.8f, 1f));
                shader.Use();

                GL.Begin(PrimitiveType.Quads);

                // GL.Color3(0.1f, 0.3f, 0.8f);
                GL.Vertex4(new Vector4(0 + 5f + offsetX, 0 - 5f  - offsetY, 0, 1));
                GL.Vertex4(new Vector4(0 + 5f + offsetX, 0       - offsetY, 0, 1));
                GL.Vertex4(new Vector4(0      + offsetX, 0       - offsetY, 0, 1));
                GL.Vertex4(new Vector4(0      + offsetX, 0 - 5f  - offsetY, 0, 1));
                // GL.Vertex4(new Vector4( 0.5f, -0.5f, 0, 1));
                // GL.Vertex4(new Vector4( 0.0f,  0.5f, 0, 1));
                // GL.Vertex4(new Vector4(-0.5f, -0.5f, 0, 1));
                GL.End();

                // DrawSquare(offsetX, offsetY, 50f, 50f);
                GL.PopMatrix();

                unsafe {
                    GLFW.SwapBuffers(this.mainWindow.Handle);
                }
            }

            try {
                this.Shutdown();
            }
            catch (Exception e) {
                Console.WriteLine("Failed to shutdown application: " + e);
            }
        }

        private void Tick() {
            if (++this.tick < 0) {
                this.tick = 0;
            }

            Dispatcher.Current.Process(Priority.AppPre);

            if (this.mainWindow != null) {
                if (this.mainWindow.ShouldClose()) {
                    this.MarkForShutdown();
                    return;
                }
            }

            Dispatcher.Current.Process(Priority.InputPre);
            Dispatcher.Current.Process(Priority.InputPost);

            Dispatcher.Current.Process(Priority.RenderPre);
            Dispatcher.Current.Process(Priority.RenderPost);

            Dispatcher.Current.Process(Priority.AppIdle);
            Dispatcher.Current.Process(Priority.ContextIdle);
            Dispatcher.Current.Process(Priority.AppPost);
        }

        public static void Wake() {
            GLFW.PostEmptyEvent();
        }

        #region OpenGL

        private static void OnGLFWError(ErrorCode error, string description) {
            Console.WriteLine($"GLFW Error: {error}");
            Console.WriteLine(description);
        }

        #endregion
    }
}