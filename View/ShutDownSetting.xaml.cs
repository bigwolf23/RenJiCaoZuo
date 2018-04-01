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
using System.Timers;
using System.Configuration; 
using System.Text.RegularExpressions;

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for ShutDownSetting.xaml
    /// </summary>
    public partial class ShutDownSetting : Window
    {
        bool bReset;
        private mainThread _parentWin;
        public mainThread ParentWindow
        {
            get { return _parentWin; }
            set { _parentWin = value; }
        }

        public Window m_pUpperWindow;
        public ShutDownSetting()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;

            string strShutDownTime = ConfigurationManager.AppSettings["ShutDownTime"];
            ShuDownTime_Edit.Text = strShutDownTime;
/*            bReset = true;*/
        }

        private void ResetTime_Button_Click(object sender, RoutedEventArgs e)
        {
            string strShutDownTime = ShuDownTime_Edit.Text;
//             if (bReset == true)
//             {
//                 bReset = false;
//                 string strShutDownTimeDefault = ConfigurationManager.AppSettings["DefaultShutDownTime"];
//                 ShuDownTime_Edit.Text = strShutDownTimeDefault;
//                 strShutDownTime = strShutDownTimeDefault;
//                 ResetTime_Button.Content = @"确认";
//             }

            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["ShutDownTime"].Value = strShutDownTime;
            cfa.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            CommonFuntion common = new CommonFuntion();
            common.setWindowsShutDown();
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            m_pUpperWindow.Show();
            this.Close();
        }

        private void ShuDownTime_Edit_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text; //1234567

            if (input.Length == 5 && !Regex.IsMatch(input, @"\d{1,2}:\d{1,2}"))
            {
                MessageBox.Show("Error!, check and try again");
            }
        }
    }
}
