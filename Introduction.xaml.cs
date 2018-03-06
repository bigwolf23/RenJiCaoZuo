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
using System.Windows.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.IO;

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for Introduction.xaml
    /// </summary>
    public partial class Introduction : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int m_TimeCount ;
        public Introduction()
        {
            InitializeComponent();
            m_TimeCount = 60;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            string sButtonText = @"收起(" + m_TimeCount.ToString() + @")s";
            Return_Button.Content = sButtonText;
        }
        /// <summary>
        /// 定时器回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            m_TimeCount--;
            string sButtonText = @"收起(" + m_TimeCount.ToString() + @")s";
            Return_Button.Content = sButtonText;
            if (m_TimeCount == 0)
            {
                dispatcherTimer.Stop();
                this.Close();
            }           
        }
        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.Close();
        }
    }
}
