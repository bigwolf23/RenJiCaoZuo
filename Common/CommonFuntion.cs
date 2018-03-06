using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Text.RegularExpressions;

namespace RenJiCaoZuo
{
    public class CommonFuntion
    {
        public void setWindowsShutDown()
        {
            DateTime dt;
            DateTime today = DateTime.Now.Date;
            string sDate = today.Year + "/" + today.Month + "/" + today.Day;
            string strShutDownTime = ConfigurationManager.AppSettings["ShutDownTime"];
            if (Regex.IsMatch(strShutDownTime, @"\d{1,2}:\d{1,2}"))
            {
                // Successful match
            }
            else
            {
                strShutDownTime = @"23:30";
            } 
            sDate = sDate + " " + strShutDownTime + @":00";
            dt = Convert.ToDateTime(sDate);
            TimeSpan span = (TimeSpan)(dt - DateTime.Now);
            int diff = (int) span.TotalSeconds;
            string strShutdownParam = @"-s -t " + diff.ToString();
            System.Diagnostics.Process.Start(@"c:/windows/system32/shutdown.exe", strShutdownParam);
        }

        public void callMaipage(Window WindowName)
        {
            string strPageType = ConfigurationManager.AppSettings["FirstPageName"];

            if (strPageType == "1")
            {
                MainWindow MainWindowWin = new MainWindow();
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

            WindowName.Close();
        }
    }
}
