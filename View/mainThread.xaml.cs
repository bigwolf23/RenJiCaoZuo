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
        public GetWebData getWebData = new GetWebData();

        public mainThread()
        {
            InitializeComponent();
            setWindowsShutDown();
            GetWebData();
            callMainPage();
        }

        private void setWindowsShutDown()
        {
            CommonFuntion pCommon = new CommonFuntion();
            pCommon.setWindowsShutDown();
        }
        
        
        private void GetWebData()
        {
            GetWebData getWebData= new GetWebData();
        }

        public void callMainPage()
        {
            string strPageType = ConfigurationManager.AppSettings["FirstPageName"];

            if (strPageType == "1")
            {
                MainWindow MainWindowWin = new MainWindow();
                MainWindowWin.Owner = this;
                MainWindowWin.Show();
            }
            else if (strPageType == "2")
            {
                //                 MainWindow MainWindowWin1 = new MainWindow1();
                //                 MainWindowWin1.Show();
            }
            else if (strPageType == "3")
            {
                //                 MainWindow MainWindowWin2 = new MainWindow2();
                //                 MainWindowWin2.Show();
            }

//             WindowName.Close();
        }
    }
}
