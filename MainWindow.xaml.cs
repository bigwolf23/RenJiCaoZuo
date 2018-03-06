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
            GetTempleInfobyWebService();
            displayListboxdata();
        }
        private void GetTempleInfobyWebService()
        {
            //寺庙信息
            string ssTempleInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/templeInfo?templeId=61ccf194f2b24e1a8a17d5a70251d589");
            //大师信息
            string ssMonkInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/monkInfo?id=6217fb65b54848679590b9478182f527");
            //寺庙活动信息
            string ssActivityInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/activityInfo?id=f618a2a094ca4b419e828fd7d2aeade5");
            //寺庙活动详细
            string ssGetActivityById = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/getActivityById?id=12a9800b25e14ba190d6ba6e5a649c5d");
            //寺庙活动详细
            string ssqRCodeInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/qRCodeInfo?id=20f757cbac914ec3abd9e5686038430d");
            //寺庙布施记录
            string ssTemplePayHistory = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/templePayHistory?id=73ba7b2b78e74ba0ac1d31d10270994c");
            //大殿布施记录
            string sshousePayHistory = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/templePayHistory?id=73ba7b2b78e74ba0ac1d31d10270994c");

        }

        public static string HttpGet(string url)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
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
