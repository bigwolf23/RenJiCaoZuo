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
        private DispatcherTimer dispatcherSrcollBarTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMilliseconds (40) };
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

            //pWebData = null;
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;

            setDisplayByMode();
            if (pWebData!=null)
            {
                if (pWebData.m_pTempInfoData.success == true)
                {
                    //显示寺庙介绍
                    setTempInfoData();
                    //设定寺院名字的图片
                    setTemplInfoNamePic();
                }

                if (pWebData.m_pqRCodeInfoData.success == true)
                {
                    //设定二维码
                    setQRCodePic();
                }

                if (pWebData.m_pTemplePayHistoryData.success == true)
                {
                    //获取捐赠listview的内容
                    getDonateListContent();
                    //显示捐赠listview内容
                    displayDonateList();
                }

                if (pWebData.m_pMonkInfoData.success == true)
                {
                    //显示法师ListView内容
                    displayMonkList();
                }

                if (pWebData.m_pActivityInfoData.success == true)
                {
                    //获取寺庙活动的内容
                    getActiveInfoContent();
                    //显示寺庙活动
                    DisplayActiveInfoContent();
                    //显示寺庙活动在listview中
                    DisplayActiveInfoContentInList();
                }
            }
        }

        private void setDisplayByMode()
        {
            string strMode = ConfigurationManager.AppSettings["FirstPageName"];
            setActivityAndMonk_Img(strMode);
            setMonk_PageShow(strMode);
        }

        //设定法师或者活动的图片
        private string Activity_MonkImage;
        private void setActivityAndMonk_Img(string strMode)
        {
            if(strMode == "1"){
                Activity_MonkImage = "pack://SiteOfOrigin:,,,/Res/title02.png";
            }else{
                Activity_MonkImage = "pack://SiteOfOrigin:,,,/Res/title04.png";
            }
            Uri ImageFilePathUri = new Uri(Activity_MonkImage);
            this.ActivityAndMonk_Img.Source = new BitmapImage(ImageFilePathUri); 
        }

        private void setMonk_PageShow(string strMode)
        {
            if (strMode == "1")
            {
                this.UpPage_Button.Visibility = Visibility.Visible;
                this.DownPage_Button.Visibility = Visibility.Visible;
                this.NewsBackground_Img.Visibility = Visibility.Visible;
                this.ActivityInfo_Label.Visibility = Visibility.Visible;
                this.MonkInfo_ListView.Visibility = Visibility.Visible;

                Activity_Detail.Visibility = Visibility.Hidden;
                ActivityInfo_Next_Button.Visibility = Visibility.Hidden;
                ActivityInfo_Prev_Button.Visibility = Visibility.Hidden;
                ActivityInfo_ListView.Visibility = Visibility.Hidden;
            }
            else
            {
                this.UpPage_Button.Visibility = Visibility.Hidden;
                this.DownPage_Button.Visibility = Visibility.Hidden;
                this.NewsBackground_Img.Visibility = Visibility.Hidden;
                this.ActivityInfo_Label.Visibility = Visibility.Hidden;
                this.MonkInfo_ListView.Visibility = Visibility.Hidden;

                Activity_Detail.Visibility = Visibility.Visible;
                ActivityInfo_Next_Button.Visibility = Visibility.Visible;
                ActivityInfo_Prev_Button.Visibility = Visibility.Visible;
                ActivityInfo_ListView.Visibility = Visibility.Visible;
            }
            

        }


        //获取捐赠ListView的内容
        private void getDonateListContent()
        {
            foreach (TemplePayHistoryDatabody payHistTemp in pWebData.m_pTemplePayHistoryData.body.data)
            {
                PayListHistory temp = new PayListHistory();
                temp.amount = payHistTemp.amount;
                temp.Name = payHistTemp.name;
                temp.payTypeName = payHistTemp.payTypeName;
                //temp.payTypeName = @"布施捐款";
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

//             foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
//             {
//                 monkinfoDisp temp = new monkinfoDisp();
//                 temp.MonkInfo = MonkTemp.name + "1" + "\n\r" + MonkTemp.info;
//                 temp.MonkInfoImage = MonkTemp.url;
//                 m_MonkList.Add(temp);
//             }
// 
//             foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
//             {
//                 monkinfoDisp temp = new monkinfoDisp();
//                 temp.MonkInfo = MonkTemp.name + "2" + "\n\r" + MonkTemp.info;
//                 temp.MonkInfoImage = MonkTemp.url;
//                 m_MonkList.Add(temp);
//             }


            Image img = new Image();
            img.Source = new BitmapImage(new Uri(@"pack://SiteOfOrigin:,,,/Res/btn01.png"));
            this.UpPage_Button.Content = img;

            if (m_MonkList.Count <= 2)
            {
                Image imgDown = new Image();
                imgDown.Source = new BitmapImage(new Uri(@"pack://SiteOfOrigin:,,,/Res/btn03.png"));
                this.DownPage_Button.Content = imgDown;
            }
            else
            {
                Image imgDown = new Image();
                imgDown.Source = new BitmapImage(new Uri(@"pack://SiteOfOrigin:,,,/Res/btn04.png"));
                this.DownPage_Button.Content = imgDown;
            }

            
            this.MonkInfo_ListView.ItemsSource = m_MonkList.ToList();
        }


        //获取寺庙活动的内容
        public class ActivityList
        {
//             public string ActivityMainImage { get; set; }

            public string ActivityMain { get; set; }
            public string ActivityMainDetail { get; set; }
        }
        List<ActivityList> m_pActivityListInfo = new List<ActivityList>();
        private void getActiveInfoContent()
        {
            this.ActivityInfo_ListView.ItemsSource = m_pActivityListInfo.ToList();
            foreach (ActivityInfoDatabody ActivityInfTemp in pWebData.m_pActivityInfoData.body.data)
            {
                ActivityList pTemp = new ActivityList();
                pTemp.ActivityMain = ActivityInfTemp.activity;
                pTemp.ActivityMainDetail = ActivityInfTemp.detail;
                m_pActivityListInfo.Add(pTemp);
                myActivityInfoQueue.Enqueue(ActivityInfTemp.activity);
            }

            this.ActivityInfo_ListView.ItemsSource = m_pActivityListInfo.ToList();
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

         //显示寺庙活动
       
        private void DisplayActiveInfoContentInList()
        {
            if (myActivityInfoQueue.Count > 0)
            {
                this.ActivityInfo_ListView.ItemsSource = m_pActivityListInfo.ToList();
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
            if (pWebData.m_pTempInfoData.success!=false)
            {
                TemplInfo_TextBlock.Text = pWebData.m_pTempInfoData.body.data.info.ToString();
            }
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


        private void DownPage_Button_Click(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(MonkInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            //scroll. .LineRight();

            if ((m_MonkList.Count > 2)&&(scroll.HorizontalOffset / 532) <= (m_MonkList.Count - 3))
            {
                if((scroll.HorizontalOffset / 532) == (m_MonkList.Count - 3))
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("pack://SiteOfOrigin:,,,/Res/btn03.png"));
                    this.DownPage_Button.Content = img;
                }
                
                Image img2 = new Image();
                img2.Source = new BitmapImage(new Uri("pack://SiteOfOrigin:,,,/Res/btn02.png"));
                this.UpPage_Button.Content = img2;

//                 dispatcherSrcollBarTimer.Tick += delegate
//                 {
//                     m_nScrollMove++;
//                     scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 53 * m_nScrollMove);
//                     if (m_nScrollMove == 10)
//                     {
//                         m_nScrollMove = 0;
//                         dispatcherSrcollBarTimer.Stop();
//                         return;
//                     }
//                 };
//                 dispatcherSrcollBarTimer.Start();
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 532);

               
            }
        }

        int m_nScrollUpMove = 0;
        private void UpPage_Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(MonkInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            //scroll.LineLeft();
            //m_nScrollMove = scroll.HorizontalOffset / 530;
            m_nScrollUpMove = 0;
            if ((m_MonkList.Count > 2) && (scroll.HorizontalOffset / 532) >= 0)
            {
                if ((scroll.HorizontalOffset / 532) == 1)
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(@"pack://SiteOfOrigin:,,,/Res/btn01.png"));
                    this.UpPage_Button.Content = img;
                }

                Image img2 = new Image();
                img2.Source = new BitmapImage(new Uri(@"pack://SiteOfOrigin:,,,/Res/btn04.png"));
                this.DownPage_Button.Content = img2;


//                 dispatcherSrcollBarTimer.Tick += delegate
//                 {
//                     m_nScrollUpMove++;
//                     scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 53 * m_nScrollUpMove);
//                     if (m_nScrollUpMove == 10)
//                     {
//                         m_nScrollUpMove = 0;
//                         dispatcherSrcollBarTimer.Stop();
//                         return;
//                     }
//                 };
//                 dispatcherSrcollBarTimer.Start();
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 532);


            }


        }

        private void Activity_Detail_Click(object sender, RoutedEventArgs e)
        {
            string strDetail = @"";
            if (pWebData != null)
            {
                ListViewAutomationPeer lvap = new ListViewAutomationPeer(ActivityInfo_ListView);
                var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
                var scroll = svap.Owner as ScrollViewer;
                int nSelIndex = (int)scroll.ContentHorizontalOffset / 922;

                int n = 0;
                foreach (ActivityList temp in m_pActivityListInfo)
                {
                    if (nSelIndex == n)
                    {
                        strDetail = temp.ActivityMainDetail;
                        break;
                    }
                    n++;
                }

                /*ActivityInfo_ListView.SelectedItems(nSelIndex);*/

                //strDetail = pWebData.m_pActivityInfoData.body.data.info.ToString();
            }

            Introduction IntroductionWin = new Introduction(strDetail);
            IntroductionWin.Owner = this;
            IntroductionWin.ShowDialog();

        }

        private void TemplInfo_Detail_Click(object sender, RoutedEventArgs e)
        {
            string strDetail = @"";
            if (pWebData.m_pTempInfoData.success == true)
            {
                strDetail = pWebData.m_pTempInfoData.body.data.detail;
            }

            Introduction IntroductionWin = new Introduction(strDetail);
            IntroductionWin.Owner = this;
            IntroductionWin.ShowDialog();
        }

        private void ActivityInfo_Prev_Button_Click(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(ActivityInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            //scroll. .LineRight();
            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 922);
        }

        private void ActivityInfo_Next_Button_Click(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(ActivityInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            //scroll. .LineRight();
            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 922);
        }
    }
}
