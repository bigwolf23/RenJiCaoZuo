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
        public string Activity_content;

        public Introduction(string AllTempInfo)
        {
            InitializeComponent();
            setButtonAndTimer();
            setAllTempleInfoText(AllTempInfo);            
        }

        /// <summary>
        /// 定时器回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setAllTempleInfoText(string AllTempInfo)
        {
            if (AllTempInfo.Length < 0)
            {
                return;
            }

            string Wenzi = AllTempInfo;

            Wenzi = Wenzi.TrimStart((char[])"\n\r".ToCharArray());



            string strtempa = "<img src=\"data:image/jpeg;base64,";
            string strtempb = ">";
            //string strtempb = "\" ";
            int IndexofA = Wenzi.IndexOf(strtempa);
            int IndexofB = Wenzi.IndexOf(strtempb);
            string ImgString = null;
            if (IndexofA != -1 && IndexofB != -1)
            {
                int nLength = strtempa.Length;
                ImgString = Wenzi.Substring(IndexofA , IndexofB - IndexofA - nLength);
                Wenzi = Wenzi.Substring(IndexofB + 1, Wenzi.Length - IndexofB - 1);
            }

            if (ImgString != null && ImgString.Length > 0)
            {
                strtempa = "<img src=\"data:image/jpeg;base64,";
                strtempb = "\" ";
                //string strtempb = "\" ";
                IndexofA = ImgString.IndexOf(strtempa);
                IndexofB = ImgString.IndexOf(strtempb);
                int nLength = strtempa.Length;
                ImgString = ImgString.Substring(IndexofA + nLength, IndexofB - IndexofA - nLength);
                BitmapImage Pic_img = byteArrayToImage(Convert.FromBase64String(ImgString));
                this.TempInfo_Img.Source = Pic_img;


                this.TempInfo_Img.Height = Pic_img.PixelHeight > 600 ? 600 : Pic_img.PixelHeight;
                if (Pic_img.PixelWidth > 787)
                {
                    this.TempInfo_Img.Width = 680;
                }
                else
                {
                    this.TempInfo_Img.Width = Pic_img.PixelWidth;
                }
            }

            All_TemplInfo_TextBlock.Text = Wenzi;
            

        }


        /// <summary>
        /// 定时器回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setButtonAndTimer()
        {
            m_TimeCount = 60;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            string sButtonText = @"收起(" + m_TimeCount.ToString() + @")s";
            Return_Button.Content = sButtonText;
        }
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
        
        private BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            
            try
            {
                MemoryStream stream = new MemoryStream();
                stream.Write(byteArrayIn, 0, byteArrayIn.Length);
                stream.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage returnImage = new BitmapImage();
                returnImage.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                returnImage.StreamSource = ms;
                returnImage.EndInit();

                return returnImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

    }
}
