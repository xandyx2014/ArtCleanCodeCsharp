using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using StudentsManager.CSV;
using StudentsManager.DataAccess.Interface;
using StudentsManager.Models;
using StudentsManager.Views;

namespace StudentsManager.Unity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UnityContainer _container;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureContainer();
            ComposeGraph();

            Current.MainWindow.Show();
        }

        private void ComposeGraph()
        {
            Current.MainWindow = _container.Resolve<MainView>();
        }

        private void ConfigureContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType<StudentsCsvProvider>(new InjectionConstructor(@"..\..\StudentsRepo.csv"));
            _container.RegisterType<IDataProvider<Student>, StudentsCsvProvider>(
                new ContainerControlledLifetimeManager() //singleton - one instance shared by all objects that depend on IDataProvider<Student>
            );

            /*
            _container.RegisterType<IDataProvider<Student>, StudentsCsvProvider>(
                new TransientLifetimeManager() //instance per request
                new PerThreadLifetimeManager() //single instance per thread
            );
            */
        }
    }
}
