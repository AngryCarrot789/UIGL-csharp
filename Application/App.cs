using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenTK.Windowing.GraphicsLibraryFramework;
using UIGL.Application.Dispatch;
using UIGL.UI;
using ErrorCode = OpenTK.Windowing.GraphicsLibraryFramework.ErrorCode;

namespace UIGL.Application {
    /// <summary>
    /// A class used for managing the application environment, e.g. windows and inputs
    /// </summary>
    public class App {
        public static App Instance { get; }

        private bool isRunning;
        private bool isMarkedForShutdown;

        private Action startCallback;

        /// <summary>
        /// A list of active windows
        /// </summary>
        public List<UIWindow> Windows { get; }
        public ShutdownMode ShutdownMode { get; set; }
        public Dispatcher Dispatcher { get; }

        public Thread Thread { get; private set; }

        /// <summary>
        /// The current application's tick, which increments as time goes on.
        /// <para>
        /// This can be used to check if multiple dispatcher calls occurred on the same tick frame
        /// </para>
        /// </summary>
        public uint Tick { get; private set; }

        private App() {
            this.Windows = new List<UIWindow>();
            this.ShutdownMode = ShutdownMode.OnAllWindowsClosed;
            this.Dispatcher = Dispatcher.Current;
        }

        static App() {
            Instance = new App();
            Instance.Setup();
        }

        public static UIWindow CreateWindow(string title, int width, int height) {
            Instance.ValidateThread();
            unsafe {
                Window* ptr = GLFW.CreateWindow(width, height, title, null, null);
                if (ptr == null) {
                    throw new Exception("Failed to create window");
                }

                UIWindow window = new UIWindow(ptr) {Title = title, Width = width, Height = height};
                Instance.Windows.Add(window);
                return window;
            }
        }

        private void ValidateThread() {
            if (Thread.CurrentThread != this.Thread) {
                throw new Exception("Illegal thread access");
            }
        }

        public static void Wake() {
            GLFW.PostEmptyEvent();
        }

        private void Setup() {
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

            foreach (UIWindow window in this.Windows) {
                window.Close();
                window.Dispose();
            }

            GLFW.Terminate();
        }

        private void Run() {
            // unsafe {
            //     GLFW.SetWindowRefreshCallback(this.mainWindow.Handle, hWnd => this.Render());
            // }

            // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            // GL.Enable(EnableCap.LineSmooth);

            try {
                this.Tick = 1;
                this.startCallback?.Invoke();
                this.isRunning = true;

                this.TickApplication();
                do {
                    GLFW.PollEvents();
                    if (this.isMarkedForShutdown) {
                        break;
                    }

                    this.TickApplication();
                    foreach (UIWindow window in this.Windows.Where(window => window.IsOpen).ToList()) {
                        if (window.ShouldClose()) {
                            window.Dispose();
                            continue;
                        }

                        window.MakeContextCurrent();
                        window.RenderFrameFull();
                    }

                    Thread.Sleep(1);
                } while (true);
            }
            catch (Exception e) {
                Console.WriteLine("Exception during main app tick: " + e);
            }

            try {
                this.Shutdown();
            }
            catch (Exception e) {
                Console.WriteLine("Failed to shutdown application: " + e);
            }
        }

        private void TickApplication() {
            this.Tick++;
            this.Dispatcher.Process(Priority.ASAP);
            this.Dispatcher.Process(Priority.AppTickPre);
            if (this.ShutdownMode == ShutdownMode.OnAllWindowsClosed && this.Windows.Count == 0) {
                this.MarkForShutdown();
            }
            else {
                // do other shite
            }
            this.Dispatcher.Process(Priority.AppTickPost);
        }

        private static void OnGLFWError(ErrorCode error, string description) {
            Console.WriteLine($"GLFW Error: {error}");
            Console.WriteLine(description);
        }

        /// <summary>
        /// Starts the application, and calls the given callback function when it has started
        /// <para>
        /// This function returns only after the given callback is called
        /// </para>
        /// </summary>
        public static void RunApplication(Action onStarted) {
            App app = Instance;
            app.startCallback = onStarted;

            Thread thread = new Thread(app.Run);
            app.Thread = thread;
            thread.Name = "REghZy UIGL - Main Thread";
            app.Thread.Start();

            do { } while (!app.isRunning);
        }
    }
}