using System;
using Panels.DiContainer;
using Panels.View;
using Panels.ViewModel;
using SimpleInjector;

namespace Panels
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = BootStrap();
            RunApplication(container);
        }

        private static Container BootStrap()
        {
            // コンテナ作成
            var c = new Container();
            c.Options.ConstructorResolutionBehavior = new GreediestConstructorBehavior();
            c.Register<MainWindow>();
            c.Register<MainWindowViewModel>();
            c.Verify();
            return c;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}