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
            Application.Current.StartupUri = new Uri(getFirstPageName(), UriKind.Relative);
            CommonFuntion pCommon = new CommonFuntion();
            pCommon.setWindowsShutDown();
        }

        private string getFirstPageName()
        {
            
            string strPageType = ConfigurationManager.AppSettings["FirstPageName"]; 

            if(strPageType == "1")
            {
                return "MainWindow.xaml";
            }
            else if(strPageType == "2")
            {
                return "MainWindow1.xaml";
            }
            else if (strPageType == "3")
            {
                return "MainWindow2.xaml";
            }
            else
            {
                return "MainWindow.xaml";
            }
        }


    }

}
