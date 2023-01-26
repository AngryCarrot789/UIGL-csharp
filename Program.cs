using System.Runtime.InteropServices.WindowsRuntime;
using UIGL.Application;
using UIGL.UI;
using UIGL.UI.Core;

namespace UIGL {
    class Program {
        static void Main(string[] args) {
            App.RunApplication(() => {
                UIWindow window = App.CreateWindow("MainWindow", 500, 500);
                window.Content = new Rectangle() {
                    Margin = new Thickness(10, 10, 0, 0),
                    TargetWidth = 200,
                    TargetHeight = 75
                };
                window.Show();
            });
        }
    }
}
