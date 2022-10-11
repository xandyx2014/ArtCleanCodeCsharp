using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using StudentsManager.Views;

namespace StudentsManager.Unity.LateBinding
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer _container;
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureContainerForLateBinding();
            ComposeGraph();

            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainerForLateBinding()
        {
            _container = new UnityContainer();
            _container.LoadConfiguration();
        }

        private void ComposeGraph()
        {
            Application.Current.MainWindow = _container.Resolve<MainView>();
        }
    }
}
