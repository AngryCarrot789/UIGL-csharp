using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using UIGL.Render;

namespace UIGL.UI {
    public class UIWindow {
        private readonly unsafe Window* handle;
        private bool hasLoadedBindings;

        private string title;
        private int width;
        private int height;

        public unsafe Window* Handle {
            get => this.handle;
        }

        public string Title {
            get => this.title;
            set {
                unsafe {
                    GLFW.SetWindowTitle(this.handle, this.title = value);
                }
            }
        }

        public int Width {
            get => this.width;
            set {
                unsafe {
                    GLFW.SetWindowSize(this.handle, this.width = value, this.height);
                }
            }
        }

        public int Height {
            get => this.height;
            set {
                unsafe {
                    GLFW.SetWindowSize(this.handle, this.width, this.height = value);
                }
            }
        }

        public Matrix4 Perspective { get; private set; }

        private unsafe UIWindow(Window* ptr) {
            this.handle = ptr;
            GLFW.SetWindowSizeCallback(this.handle, this.OnSizeChanged);
        }

        public void UpdatePerspective() {
            Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(-this.Width, this.Width, this.Height, -this.Height, 0.001f, 1f) * Matrix4.CreateScale(2f);
            ortho.Column3 = new Vector4(-1f, 1f, 0f, 1f);
            this.Perspective = ortho;
        }

        private unsafe void OnSizeChanged(Window* window, int w, int h) {
            GL.Viewport(0, 0, this.width = w, this.height = h);
            this.UpdatePerspective();
        }

        public static UIWindow Create(string title, int w, int h) {
            unsafe {
                return Create(title, w, h, null, null);
            }
        }

        public static unsafe UIWindow Create(string title, int w, int h, Monitor* monitor, Window* share) {
            Window* ptr = GLFW.CreateWindow(w, h, title, monitor, share);
            if (ptr == null) {
                throw new Exception("Failed to create window");
            }

            UIWindow window = new UIWindow(ptr);
            window.width = w;
            window.height = h;
            window.title = title;
            window.UpdatePerspective();
            return window;
        }

        public void MakeContextCurrent() {
            unsafe {
                GLFW.MakeContextCurrent(this.handle);
                this.EnsureLoadedBindings();
            }
        }

        public void Show() {
            unsafe {
                this.EnsureLoadedBindings();
                GLFW.ShowWindow(this.handle);
            }
        }

        private void EnsureLoadedBindings() {
            if (this.hasLoadedBindings) {
                return;
            }

            GL.LoadBindings(new GLFWBindingsContext());
            this.hasLoadedBindings = true;
        }

        public bool ShouldClose() {
            unsafe {
                return GLFW.WindowShouldClose(this.handle);
            }
        }
    }
}
