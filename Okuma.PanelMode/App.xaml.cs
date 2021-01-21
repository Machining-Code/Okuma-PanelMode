using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Okuma.PanelMode
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Serilog.Log.Information("Started application {@Name}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Serilog.Log.Information("Error in {@Name}: [{Type}] {Message", 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                e.Exception?.GetType()?.Name,
                e.Exception?.Message
                );
            MessageBox.Show($"An error has occurred: {e.Exception?.Message}");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Serilog.Log.Information("Exited application {@Name}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            base.OnExit(e);
        }
    }
}
