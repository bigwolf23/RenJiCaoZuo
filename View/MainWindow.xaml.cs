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
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using RenJiCaoZuo.WebData;


namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(2) };
        class My
        {
            public string Name { get; set; }
            public SolidColorBrush BGColor { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
            //GetTempleInfobyWebService();
            displayListboxdata();
        }

        


        private void displayListboxdata()
        {
            Queue<My> myQueue = new Queue<My>(
                typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select((pi, i) => new My() { BGColor = pi.GetValue(null) as SolidColorBrush, Name = i + "  " + pi.Name })
                .Take(20)
            );
            this.DonateInfo_List.ItemsSource = myQueue.ToList();
            dispatcherTimer.Tick += delegate
            {
                myQueue.Enqueue(myQueue.Dequeue());  // 把队列中派头的放到队尾
                this.DonateInfo_List.ItemsSource = myQueue.ToList();
            };
            dispatcherTimer.Start();
        }

        private void DownPage_Button_Click(object sender, RoutedEventArgs e)
        {
            Introduction IntroductionWin = new Introduction();
            IntroductionWin.Owner = this;
            IntroductionWin.ShowDialog();
            //IntroductionWin.Topmost = true;
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
