using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

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

        private unsafe UIWindow(Window* ptr) {
            this.handle = ptr;

            GLFW.SetWindowSizeCallback(this.handle, this.OnSizeChanged);
        }

        private unsafe void OnSizeChanged(Window* window, int w, int h) {
            this.width = w;
            this.height = h;
            GL.Viewport(0, 0, w, h);
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
