using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Text.RegularExpressions;
using RenJiCaoZuo.Common;

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.StartupUri = new Uri(@"View\mainThread.xaml", UriKind.Relative);
        }
    }

}
