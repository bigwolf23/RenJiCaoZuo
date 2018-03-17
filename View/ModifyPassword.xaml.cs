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
    /// Interaction logic for ModifyPassword.xaml
    /// </summary>
    public partial class ModifyPassword : Window
    {
        public Window m_pUpperWindow;
        public ModifyPassword()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
            //System.Diagnostics.Process.Start("osk.exe");
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            m_pUpperWindow.Show();
            this.Close();
        }

        private void SavePassword_Button_Click(object sender, RoutedEventArgs e)
        {
            string strPassword = ConfigurationManager.AppSettings["Password"];
            if (strPassword == OldPassword_Edit.Password)
            {
                if (NewPassword_Edit.Password.Length == 0)
                {
                    MessageBox.Show(@"新密码不能为空，请输入！");
                    return;
                }
                if (NewPassword_Edit.Password == ConfirmPassword_Edit.Password )
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings["Password"].Value = NewPassword_Edit.Password;
                    cfa.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");

                    m_pUpperWindow.Show();
                    this.Close();
                }
                else
                {
                    NewPassword_Edit.Clear();
                    ConfirmPassword_Edit.Clear();
                    MessageBox.Show(@"新旧密码不一致，请重新输入！");
                }
            }
            else
            {
                OldPassword_Edit.Clear();
                MessageBox.Show(@"原密码输入错误，请重新输入！");
            }

        }
    }
}
