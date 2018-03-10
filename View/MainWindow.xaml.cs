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
using System.Windows.Automation.Peers;

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimerList = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(2) };
        private DispatcherTimer dispatcherTimerActivity = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };
        public GetWebData pWebData;
        public Uri ImageFilePathUri;

        Queue<PayListHistory> myPayQueue = new Queue<PayListHistory>();
        class PayListHistory
        {
            public string Name { get; set; }
            public string payTypeName { get; set; }
            public double amount { get; set; }
        }

        Queue<string> myActivityInfoQueue = new Queue<string>();
        
        public MainWindow(GetWebData setWebData)
        {
            pWebData = setWebData;
            

            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;

            //显示寺庙介绍
            setTempInfoData();
            //设定寺院名字的图片
            setTemplInfoNamePic();

            //设定二维码
            setQRCodePic();

            //获取捐赠listview的内容
            getDonateListContent();
            //显示捐赠listview内容
            displayDonateList();

            //显示法师ListView内容
            displayMonkList();

            //获取寺庙活动的内容
            getActiveInfoContent();
            //显示寺庙活动
            DisplayActiveInfoContent();
            
            
        }

        //获取捐赠ListView的内容
        private void getDonateListContent()
        {
            foreach (TemplePayHistoryDatabody payHistTemp in pWebData.m_pTemplePayHistoryData.body.data)
            {
                PayListHistory temp = new PayListHistory();
                temp.amount = payHistTemp.amount;
                temp.Name = payHistTemp.name;
                //temp.payTypeName = payHistTemp.payTypeName;
                temp.payTypeName = @"布施捐款";
                myPayQueue.Enqueue(temp);
            }            
        }
        
        //显示捐赠ListView内容
        private void displayDonateList()
        {
            this.DonateInfo_List.ItemsSource = myPayQueue.ToList();
            dispatcherTimerList.Tick += delegate
            {
                myPayQueue.Enqueue(myPayQueue.Dequeue());  // 把队列中派头的放到队尾
                this.DonateInfo_List.ItemsSource = myPayQueue.ToList();
            };
            dispatcherTimerList.Start();
        }

        public class monkinfoDisp
        {
            public string MonkInfoImage { get; set; }
            public string MonkInfo { get; set; }
        }

        //显示法师ListView内容
        List<monkinfoDisp> m_MonkList = new List<monkinfoDisp>();
        private void displayMonkList()
        {


            this.MonkInfo_ListView.ItemsSource = m_MonkList.ToList();
            foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
            {
                monkinfoDisp temp = new monkinfoDisp();
                temp.MonkInfo = MonkTemp.name + "\n\r" + MonkTemp.info;
                temp.MonkInfoImage = MonkTemp.url;
                m_MonkList.Add(temp);
            }

            foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
            {
                monkinfoDisp temp = new monkinfoDisp();
                temp.MonkInfo = MonkTemp.name + "1" + "\n\r" + MonkTemp.info;
                temp.MonkInfoImage = MonkTemp.url;
                m_MonkList.Add(temp);
            }

            foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
            {
                monkinfoDisp temp = new monkinfoDisp();
                temp.MonkInfo = MonkTemp.name + "2" + "\n\r" + MonkTemp.info;
                temp.MonkInfoImage = MonkTemp.url;
                m_MonkList.Add(temp);
            }


            this.MonkInfo_ListView.ItemsSource = m_MonkList.ToList();
        }


        //获取寺庙活动的内容
        private void getActiveInfoContent()
        {
            foreach (ActivityInfoDatabody ActivityInfTemp in pWebData.m_pActivityInfoData.body.data)
            {
                myActivityInfoQueue.Enqueue(ActivityInfTemp.activity);
            }
        }

        //显示寺庙活动
        private void DisplayActiveInfoContent()
        {
            if (myActivityInfoQueue.Count > 0)
            {
                string strDisp = myActivityInfoQueue.Dequeue();
                ActivityInfo_Label.Content = strDisp;
                myActivityInfoQueue.Enqueue(strDisp);

                dispatcherTimerActivity.Tick += delegate
                {   
                    strDisp = myActivityInfoQueue.Dequeue();
                    ActivityInfo_Label.Content = strDisp;
                    myActivityInfoQueue.Enqueue(strDisp);  // 把队列中派头的放到队尾
                };
                dispatcherTimerActivity.Start();
            }
            
        }

        //获取二维码
        private void setQRCodePic()
        {
            Uri ImageFilePathUri = new Uri(pWebData.m_pqRCodeInfoData.body.data.url);
            QRCode_Image.Source = new BitmapImage(ImageFilePathUri); 
        }

        //获取寺庙名字的图片
        private void setTemplInfoNamePic()
        {
            Uri ImageFilePathUri = new Uri(pWebData.m_pTempInfoData.body.data.url);
            TempInfo_Image.Source = new BitmapImage(ImageFilePathUri); 
        }
        

        //获取寺庙的基本信息
        private void setTempInfoData()
        {
            TemplInfo_TextBlock.Text = pWebData.m_pTempInfoData.body.data.info.ToString();
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
                LoginPasswordWin.m_pMainWindow = this;
                this.Hide();
            }
        }

        private void Activity_Detail_Click(object sender, RoutedEventArgs e)
        {
            Introduction IntroductionWin = new Introduction(pWebData.m_pTempInfoData.body.data.info.ToString());
            IntroductionWin.Owner = this;
            IntroductionWin.ShowDialog();
        }

        private void DownPage_Button_Click(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(MonkInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            //scroll. .LineRight();
            if ((scroll.HorizontalOffset / 530) <= (m_MonkList.Count - 3))
            {
                if((scroll.HorizontalOffset / 530) == (m_MonkList.Count - 3))
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("Res/btn03.png"));
                    this.DownPage_Button.Content = img;
                }
                
                Image img2 = new Image();
                img2.Source = new BitmapImage(new Uri(@"D:\GitClone\DongHuashi\RenJiCaoZuo\View\Res\btn02.png"));
                this.UpPage_Button.Content = img2;

                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 530);
            }
            
        }


        private void UpPage_Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(MonkInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            //scroll.LineLeft();
            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 530);
        }

    }
}
