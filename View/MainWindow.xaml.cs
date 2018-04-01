﻿using System;
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
        private mainThread _parentWin;
        public mainThread ParentWindow
        {
            get { return _parentWin; }
            set { _parentWin = value; }
        }

        class PayListHistory
        {
            public string Name { get; set; }
            public string payTypeName { get; set; }
            public double amount { get; set; }
        }

        public class monkinfoDisp
        {
            public string MonkInfoImage { get; set; }
            public string MonkName { get; set; }
            public string MonkInfo { get; set; }
            public int MonkInfoIndex { get; set; }
        }

        //获取寺庙活动的内容
        public class ActivityList
        {
            //             public string ActivityMainImage { get; set; }

            public string ActivityMain { get; set; }
            public string ActivityMainDetail { get; set; }
        }

        
        private DispatcherTimer dispatcherDonateTimerList = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(2) };
        //activity 的label更新的timer
        private DispatcherTimer dispatcherTimerList = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };

        private DispatcherTimer dispatcherTimerRefresh = null;
        //dispatcherSrcollBarTimer这个timer暂时不用，保留
        private DispatcherTimer dispatcherSrcollBarTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMilliseconds (40) };
        public GetWebData pWebData;
        public Uri ImageFilePathUri;
        private string Activity_MonkImage;
        Queue<ActivityList> myActivityInfoQueue = new Queue<ActivityList>();
        Queue<PayListHistory> myPayQueue = new Queue<PayListHistory>();
        //显示法师ListView内容
        List<monkinfoDisp> m_MonkList = new List<monkinfoDisp>();
        private Dictionary<int, string> m_MonkinfoDetail = new Dictionary<int, string>();
        //显示活动横向list内容
        List<ActivityList> m_pActivityListInfo = new List<ActivityList>();

        private void setWindowsShutDown()
        {
            CommonFuntion pCommon = new CommonFuntion();
            pCommon.setWindowsShutDown();

        }
        private void getPageRefreshTime()
        {
            string sRefreshTime = ConfigurationManager.AppSettings["PageRefreshTime"];
            int nRefreshTime = Convert.ToInt16(sRefreshTime);

            dispatcherTimerRefresh = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(nRefreshTime) };

        }
        public MainWindow(Window ParentWindowTemp)
        {
            
            //pWebData = Application.Current.getAllWebData();
            InitializeComponent();
            ParentWindow = (mainThread)ParentWindowTemp;
            //setWindowsShutDown();
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
            //this.Topmost = true;
            //TemplInfo_TextBlock.Text = @"汉皇重色思倾国，御宇多年求不得。杨家有女初长成，养在深闺人未识。天生丽质难自弃，一朝选在君王侧。回眸一笑百媚生，六宫粉黛无颜色。春寒赐浴华清池，温泉水滑洗凝脂。侍儿扶起娇无力，始是新承恩泽时。云鬓花颜金步摇，芙蓉帐暖度春宵。春宵苦短日高起，从此君王不早朝。侍儿扶起娇无力，始是新承恩泽时。云鬓花颜金步摇，芙蓉帐暖度春宵。春宵苦短日高起，从此君王不早朝。侍儿扶起娇无力，始是新承恩泽时。云鬓花颜金步摇，芙蓉帐暖度春宵。春宵苦短日高起，从此君王不早朝。侍儿扶起娇无力，始是新承恩泽时。云鬓花颜金步摇，芙蓉帐暖度春宵。春宵苦短日高起，从此君王不早朝。侍儿扶起娇无力，始是新承恩泽时。云鬓花颜金步摇，芙蓉帐暖度春宵。春宵苦短日高起，从此君王不早朝。侍儿扶起娇无力，始是新承恩泽时。云鬓花颜金步摇，芙蓉帐暖度春宵。春宵苦短日高起，从此君王不早朝。";
            pWebData = ParentWindow.AllWebData;
            setDisplayByMode();
            if (pWebData != null)
            {
                if (pWebData.m_pTempInfoData.success == true)
                {
                    //显示寺庙介绍
                    setTempInfoData();
                    //设定寺院名字的图片
                    setTemplInfoNamePic();
                }
                getPageRefreshTime();
                if (pWebData.m_pqRCodeInfoData.success == true)
                {
                    //设定二维码
                    setQRCodePic();
                }

                if (pWebData.m_pHousePayHistoryData.success == true)
                {
                    getDonateHouseContent();
                }
                if (pWebData.m_pTemplePayHistoryData.success == true)
                {
                    ////获取捐赠listview的内容
                    getDonateListContent();
                    //显示捐赠listview内容
                    displayDonateList();

                    //                     DisplayDonateListByTimer();

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
                    //显示寺庙活动在label上
                    DisplayActiveInfoContent();
                    //显示寺庙活动在listview中
                    DisplayActiveInfoContentInList();
                }
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void setDisplayByMode()
        {
            string strMode = ConfigurationManager.AppSettings["FirstPageName"];
            setActivityAndMonk_Img(strMode);
            setMonk_PageShow(strMode);
        }

        //设定法师或者活动的图片

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
        //
        private void DisplayDonateListByTimer()
        {

            
            //this.DonateInfo_List.ClearValue();
            dispatcherTimerRefresh.Tick += delegate
            {
                
//                 myPayQueue.Clear();
//                 dispatcherDonateTimerList.Stop();
//                 this.DonateInfo_List.ItemsSource = null;
//                 this.DonateInfo_List.Items.Clear();
//                 this.DonateInfo_List.Items.Refresh();
                //获取捐赠TextBox的内容
                getDonateHouseContent();
//                 pWebData.GetTemplePayHistorybyWebService();
//                 if (pWebData.m_pTemplePayHistoryData.success == true)
//                 {
//                     //获取捐赠listview的内容
//                     getDonateListContent();
//                     //显示捐赠listview内容
//                     displayDonateList();
//                 }
            };
            dispatcherTimerRefresh.Start();
        }

        //获取捐赠TextBox的内容
        private void getDonateHouseContent()
        {
            pWebData.GetHousePayHistorybyWebService();
            if (pWebData.m_pHousePayHistoryData.success == true && pWebData.m_pHousePayHistoryData.body.data!=null)
            {
                HouseName.Text = pWebData.m_pHousePayHistoryData.body.data.name;
                HousepayTypeName.Text = pWebData.m_pHousePayHistoryData.body.data.payTypeName;
                Houseamount.Text = pWebData.m_pHousePayHistoryData.body.data.amount.ToString();
            }
        }


        //获取捐赠ListView的内容
        private void getDonateListContent()
        {
            myPayQueue.Clear();
            foreach (TemplePayHistoryDatabody payHistTemp in pWebData.m_pTemplePayHistoryData.body.data)
            {
                PayListHistory temp = new PayListHistory();
                temp.amount = payHistTemp.amount;
                temp.Name = payHistTemp.name;
                temp.payTypeName = payHistTemp.payTypeName;
                myPayQueue.Enqueue(temp);
            }            
        }
        
        //显示捐赠ListView内容
        private void displayDonateList()
        {
            int nCount = 0;
            this.DonateInfo_List.ItemsSource = myPayQueue.ToList();
            if (myPayQueue.Count > 0)
            {
                dispatcherDonateTimerList.Tick += delegate
                {
                    nCount++;
                    string sRefreshTime = ConfigurationManager.AppSettings["PageRefreshTime"];
                    int nRefreshTime = Convert.ToInt16(sRefreshTime);
                    if (nCount % nRefreshTime == 0)
                    {
                        nCount = 0;
                        myPayQueue.Clear();
                        this.DonateInfo_List.ItemsSource = null;
                        this.DonateInfo_List.Items.Clear();
                        this.DonateInfo_List.Items.Refresh();

                        pWebData.GetTemplePayHistorybyWebService();
                        getDonateListContent();

                        this.DonateInfo_List.ItemsSource = myPayQueue.ToList();

                        //获取捐赠TextBox的内容
                        getDonateHouseContent();
                    }
                    else
                    {
                        myPayQueue.Enqueue(myPayQueue.Dequeue());  // 把队列中派头的放到队尾
                        this.DonateInfo_List.ItemsSource = myPayQueue.ToList();
                    }

                };
                dispatcherDonateTimerList.Start();
            }
        }

        private void getActiveInfoContent()
        {
            this.ActivityInfo_ListView.ItemsSource = m_pActivityListInfo.ToList();
            foreach (ActivityInfoDatabody ActivityInfTemp in pWebData.m_pActivityInfoData.body.data)
            {
                ActivityList pTemp = new ActivityList();
                pTemp.ActivityMain = ActivityInfTemp.activity;
                pTemp.ActivityMainDetail = ActivityInfTemp.detail;
                m_pActivityListInfo.Add(pTemp);
                myActivityInfoQueue.Enqueue(pTemp);
            }

//             foreach (ActivityInfoDatabody ActivityInfTemp in pWebData.m_pActivityInfoData.body.data)
//             {
//                 ActivityList pTemp = new ActivityList();
//                 pTemp.ActivityMain =  "1" + ActivityInfTemp.activity ;
//                 pTemp.ActivityMainDetail = "1" + ActivityInfTemp.detail;
//                 m_pActivityListInfo.Add(pTemp);
//                 myActivityInfoQueue.Enqueue(ActivityInfTemp.activity);
//             }
// 
//             foreach (ActivityInfoDatabody ActivityInfTemp in pWebData.m_pActivityInfoData.body.data)
//             {
//                 ActivityList pTemp = new ActivityList();
//                 pTemp.ActivityMain = "2" + ActivityInfTemp.activity;
//                 pTemp.ActivityMainDetail = "2" + ActivityInfTemp.detail;
//                 m_pActivityListInfo.Add(pTemp);
//                 myActivityInfoQueue.Enqueue(ActivityInfTemp.activity);
//             }

            this.ActivityInfo_ListView.ItemsSource = m_pActivityListInfo.ToList();
        }
        public string strActivityInfoDetail;
        //显示寺庙活动
        private void DisplayActiveInfoContent()
        {
            if (myActivityInfoQueue.Count > 0)
            {
                ActivityList pTemp  = myActivityInfoQueue.Dequeue();
                string strDisp = pTemp.ActivityMain;
                ActivityInfo_Label.Content = strDisp;
                strActivityInfoDetail = pTemp.ActivityMainDetail;
                myActivityInfoQueue.Enqueue(pTemp);

                dispatcherTimerList.Tick += delegate
                {
                    pTemp = myActivityInfoQueue.Dequeue();
                    strDisp = pTemp.ActivityMain;
                    ActivityInfo_Label.Content = strDisp;
                    strActivityInfoDetail = pTemp.ActivityMainDetail;
                    myActivityInfoQueue.Enqueue(pTemp);  // 把队列中派头的放到队尾
                };
                dispatcherTimerList.Start();
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

        private void displayMonkList()
        {
            this.MonkInfo_ListView.ItemsSource = m_MonkList.ToList();
            int nIndex = 0;
            foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
            {
                nIndex++;
                monkinfoDisp temp = new monkinfoDisp();
                temp.MonkInfo = MonkTemp.info;
                temp.MonkInfoImage = MonkTemp.url;
                temp.MonkName = MonkTemp.name;
                temp.MonkInfoIndex = nIndex;
                m_MonkList.Add(temp);

                m_MonkinfoDetail.Add(nIndex, MonkTemp.detail);

            }

//             foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
//             {
//                 nIndex++;
//                 monkinfoDisp temp = new monkinfoDisp();
//                 temp.MonkInfo = MonkTemp.info;
//                 temp.MonkInfoImage = MonkTemp.url;
//                 temp.MonkName = MonkTemp.name + "1";
//                 temp.MonkInfoIndex = nIndex;
//                 m_MonkList.Add(temp); m_MonkinfoDetail.Add(nIndex, MonkTemp.detail);
//             }
// 
//             foreach (MonkInfoDatabody MonkTemp in pWebData.m_pMonkInfoData.body.data)
//             {
//                 nIndex++;
//                 monkinfoDisp temp = new monkinfoDisp();
//                 temp.MonkInfo = MonkTemp.info;
//                 temp.MonkInfoImage = MonkTemp.url;
//                 temp.MonkName = MonkTemp.name + "2";
//                 temp.MonkInfoIndex = nIndex;
//                 m_MonkList.Add(temp); m_MonkinfoDetail.Add(nIndex, MonkTemp.detail);
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


        //获取二维码
        private void setQRCodePic()
        {
            if (pWebData.m_pqRCodeInfoData.body.data != null )
            {
                if (pWebData.m_pqRCodeInfoData.body.data.url.Length > 0)
                {
                    Uri ImageFilePathUri = new Uri(pWebData.m_pqRCodeInfoData.body.data.url);
                    QRCode_Image.Source = new BitmapImage(ImageFilePathUri); 
                }

            }

        }

        //获取寺庙名字的图片
        private void setTemplInfoNamePic()
        {
            if (pWebData.m_pTempInfoData.body.data != null && pWebData.m_pTempInfoData.body.data.url.Length > 0)
            {
                Uri ImageFilePathUri = new Uri(pWebData.m_pTempInfoData.body.data.url);
                TempInfo_Image.Source = new BitmapImage(ImageFilePathUri); 
            }

        }
        

        //获取寺庙的基本信息
        private void setTempInfoData()
        {
            if (pWebData.m_pTempInfoData.success != false && pWebData.m_pTempInfoData.body.data != null)
            {
                TemplInfo_TextBlock.Text = pWebData.m_pTempInfoData.body.data.info.ToString();
                //TemplInfo_TextBlock.Text = pWebData.m_pTempInfoData.body.data.info.ToString() + pWebData.m_pTempInfoData.body.data.info.ToString();
            }
        }

        //左上角双击银行商标
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
                LoginPasswordWin.ParentWindow = ParentWindow;
                LoginPasswordWin.Show();

                //this.Hide();
                this.Close();
            }
        }

        //法师下一页预览
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
        //法师上一页预览
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

            Introduction IntroductionWin = new Introduction(strDetail,2);
            IntroductionWin.Owner = this;
            IntroductionWin.ShowDialog();

        }

        private void TemplInfo_Detail_Click(object sender, RoutedEventArgs e)
        {
            string strDetail = @"";
            if (pWebData.m_pTempInfoData != null && pWebData.m_pTempInfoData.success == true)
            {
                strDetail = pWebData.m_pTempInfoData.body.data.detail;
            }

            Introduction IntroductionWin = new Introduction(strDetail,1);
            IntroductionWin.Owner = this;
            IntroductionWin.ShowDialog();
        }

        private void ActivityInfo_Prev_Button_Click(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(ActivityInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            if ((m_pActivityListInfo.Count > 1) && (scroll.HorizontalOffset / 926) >= 0)
            {
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 926);
            }
        }

        private void ActivityInfo_Next_Button_Click(object sender, RoutedEventArgs e)
        {
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(ActivityInfo_ListView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap.Owner as ScrollViewer;
            if ((m_pActivityListInfo.Count > 1) && (scroll.HorizontalOffset / 926) <= (m_pActivityListInfo.Count - 2))
            {
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 926);
            }
            
        }

        private void ActivityInfo_Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ActivityInfo_Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string strDetail = strActivityInfoDetail;

            if (strDetail.Length > 0)
            {
                Introduction IntroductionWin = new Introduction(strDetail, 2);
                IntroductionWin.Owner = this;
                IntroductionWin.ShowDialog();
            }
        }


        private void MonkInfo_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            monkinfoDisp emp = MonkInfo_ListView.SelectedItem as monkinfoDisp;
            if (emp != null)
            {
                if(m_MonkinfoDetail.ContainsKey(emp.MonkInfoIndex))
                {
                    string strDetail = m_MonkinfoDetail[emp.MonkInfoIndex];
                    if (strDetail.Length > 0)
                    {
                        Introduction IntroductionWin = new Introduction(strDetail, 3);
                        IntroductionWin.Owner = this;
                        IntroductionWin.ShowDialog();
                    }
                }
                MonkInfo_ListView.UnselectAll();
            }

        }


    }
}
