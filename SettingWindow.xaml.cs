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

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
        }

        private void ShutDownWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            ShutDownSetting ShutDownSettingWin = new ShutDownSetting();
            ShutDownSettingWin.Show();
            this.Close();
        }

        private void ModiyPassword_Button_Click(object sender, RoutedEventArgs e)
        {
            ModifyPassword ModifyPasswordWin = new ModifyPassword();
            ModifyPasswordWin.Show();
            this.Close();
        }

        private void ReturnMain_Button_Click(object sender, RoutedEventArgs e)
        {
            CommonFuntion a = new CommonFuntion();
            a.callMaipage(this);
        }

        private void ReturnDesktop_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
