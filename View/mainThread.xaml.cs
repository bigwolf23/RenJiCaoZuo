using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Text.RegularExpressions;
using RenJiCaoZuo.Common;
using RenJiCaoZuo.WebData;

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for mainThread.xaml
    /// </summary>
    public partial class mainThread : Window
    {
        public mainThread()
        {
            InitializeComponent();
            setWindowsShutDown();
            GetWebData();
        }

        private void setWindowsShutDown()
        {
            CommonFuntion pCommon = new CommonFuntion();
            pCommon.setWindowsShutDown();
        }

        private void GetWebData()
        {
            CommonFuntion pCommon = new CommonFuntion();
            pCommon.setWindowsShutDown();
        }

        private string getFirstPageName()
        {

            string strPageType = ConfigurationManager.AppSettings["FirstPageName"];

            if (strPageType == "1")
            {
                return @"View\MainWindow.xaml";
            }
            else if (strPageType == "2")
            {
                return @"View\MainWindow1.xaml";
            }
            else if (strPageType == "3")
            {
                return @"View\MainWindow2.xaml";
            }
            else
            {
                return @"View\MainWindow.xaml";
            }
        }
    }
}
