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

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for LoginPassord.xaml
    /// </summary>
    public partial class LoginPassord : Window
    {
        public Window m_pMainWindow;
        public LoginPassord()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            string strPassword = ConfigurationManager.AppSettings["Password"];
            if (strPassword == Password_Edit.Password)
            {
                SettingWindow SettingWindowWin = new SettingWindow();
                SettingWindowWin.m_pMainWindow = m_pMainWindow;
                SettingWindowWin.Show();
                this.Close();
            }
            else{
                Password_Edit.Clear();
                MessageBox.Show(@"密码输入错误，请重新输入！");
            }
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            m_pMainWindow.Show();
            this.Close(); ;
        }

        private void Password_Edit_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

    }
}
