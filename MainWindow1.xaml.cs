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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public MainWindow1()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;

            DateTime dt;
            DateTime today = DateTime.Now.Date;
            string sDate = today.Year + "/" + today.Month + "/" + today.Day;
            sDate = sDate + " " + @"23:30" + @":00";
            dt = Convert.ToDateTime(sDate);
            TimeSpan span = (TimeSpan)(dt - DateTime.Now);
            double diff = span.TotalSeconds;
        }

        private void DownPage_Button_Click(object sender, RoutedEventArgs e)
        {
            Introduction IntroductionWin = new Introduction();
            IntroductionWin.ShowDialog();
            IntroductionWin.Topmost = true;
        }

        int nCount = 0;
        private void SettingBorder_DoubleClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nCount += 1;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; nCount = 0; };
            timer.IsEnabled = true;

            if (nCount % 2 == 0)
            {
                timer.IsEnabled = false;
                nCount = 0;
                LoginPassord LoginPasswordWin = new LoginPassord();
                LoginPasswordWin.Show();
                this.Close();
            }
        }


    }
}
