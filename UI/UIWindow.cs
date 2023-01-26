using System;
using System.Linq;
using System.Net.Mime;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using UIGL.Application;
using UIGL.Application.Dispatch;
using UIGL.Application.Inputs;
using UIGL.Render;
using UIGL.UI.Core;

namespace UIGL.UI {
    public class UIWindow : IDisposable {
        public bool isDirtyRender = true;
        public bool isDirtyFullRender = true;

        private readonly bool notInCtor;
        private bool hasLoadedBindings;
        private string title;
        private int width;
        private int height;

        public Keyboard Keyboard { get; } = new Keyboard();
        public unsafe Window* Handle { get; }

        public Control Content { get; set; }

        public string Title {
            get => this.title;
            set {
                this.title = value;
                if (this.notInCtor) {
                    unsafe {
                        GLFW.SetWindowTitle(this.Handle, value);
                    }
                }
            }
        }

        public int Width {
            get => this.width;
            set {
                this.width = value;
                if (this.notInCtor) {
                    unsafe {
                        GLFW.SetWindowSize(this.Handle, value, this.height);
                    }
                }
            }
        }

        public int Height {
            get => this.height;
            set {
                this.height = value;
                if (this.notInCtor) {
                    unsafe {
                        GLFW.SetWindowSize(this.Handle, this.width, value);
                    }
                }
            }
        }

        public bool IsOpen { get; private set; }

        public unsafe UIWindow(Window* ptr) {
            this.Handle = ptr;
            this.MakeContextCurrent();
            GLFW.SetWindowSizeCallback(ptr, this.OnSizeChanged);
            GLFW.SetKeyCallback(ptr, this.OnKeyInput);

            this.notInCtor = true;
        }

        public void RenderFrameFull() {
            Drawing.LoadOrthoMatrix2D(this.width, this.Height);

            if (this.isDirtyRender) {
                this.isDirtyRender = false;
                if (this.isDirtyFullRender) {
                    GL.Clear(ClearBufferMask.ColorBufferBit);
                    this.isDirtyFullRender = false;
                }

                if (this.Content != null) {
                    Control.Render(this.Content);
                }

                unsafe {
                    GLFW.SwapBuffers(this.Handle);
                }
            }
        }

        private unsafe void OnSizeChanged(Window* window, int w, int h) {
            GL.Viewport(0, 0, this.width = w, this.height = h);
            this.isDirtyRender = true;
            this.isDirtyFullRender = true;
        }

        private unsafe void OnKeyInput(Window* window, Keys key, int scancode, InputAction action, KeyModifiers mods) {
            App.Instance.Dispatcher.Parameter1 = this;
            try {
                this.Keyboard.OnWindowInput(this, key, scancode, action, mods);
            }
            finally {
                App.Instance.Dispatcher.Parameter1 = null;
            }
        }

        public void MakeContextCurrent() {
            unsafe {
                GLFW.MakeContextCurrent(this.Handle);
                this.EnsureLoadedBindings();
            }
        }

        public void Show() {
            App.Instance.Dispatcher.Invoke(() => {
                unsafe {
                    this.MakeContextCurrent();
                    GLFW.ShowWindow(this.Handle);
                    this.IsOpen = true;
                }
            });
        }

        public void Close() {
            unsafe {
                GLFW.SetWindowShouldClose(this.Handle, true);
                this.IsOpen = false;
            }
        }

        private void EnsureLoadedBindings() {
            if (this.hasLoadedBindings) {
                return;
            }

            this.hasLoadedBindings = true;
            unsafe {
                Window* previous = GLFW.GetCurrentContext();
                if (previous != this.Handle)
                    GLFW.MakeContextCurrent(this.Handle);

                GL.LoadBindings(new GLFWBindingsContext());

                if (previous != this.Handle)
                    GLFW.MakeContextCurrent(previous);
            }
        }

        public bool ShouldClose() {
            unsafe {
                return GLFW.WindowShouldClose(this.Handle);
            }
        }

        public void Dispose() {
            unsafe {
                this.Close();
                GLFW.SetWindowSizeCallback(this.Handle, null);
                GLFW.SetKeyCallback(this.Handle, null);
                GLFW.DestroyWindow(this.Handle);

                App.Instance.Windows.Remove(this);
            }
        }
    }
}
