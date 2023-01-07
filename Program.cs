using UIGL.Application;
using UIGL.UI;

namespace UIGL {
    class Program {
        static void Main(string[] args) {
            App app = new App();
            app.Setup();
            app.MainWindow = UIWindow.Create("MainWindow", 1024, 576);
            app.Start();
        }
    }
}
