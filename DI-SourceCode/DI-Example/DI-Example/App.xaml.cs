using System.Windows;
using StudentsManager.CSV;
using StudentsManager.DataAccess;
using StudentsManager.ViewModels;
using StudentsManager.Views;

namespace StudentsManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ComposeObjects()
        {            
#if CSV
            var dataProvider = new StudentsCsvProvider(@"..\..\StudentsRepo.csv");
#else
            var dataProvider = new StudentsXmlProvider(@"..\..\StudentsRepo.xml");
#endif
            var vm = new MainViewModel(dataProvider);
            var view = new MainView(vm);

            Application.Current.MainWindow = view;            
        }
    }
}
