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
            int nlength = AllTempInfo.Length;
            int nLine = nlength / 40;

//             if (nLine*All_TemplInfo_TextBlock.FontSize > All_TemplInfo_TextBlock.Height )
//             {
//                 All_TemplInfo_TextBlock.Height = All_TemplInfo_TextBlock.FontSize * (nLine + 1);
//             }

            string stra = AllTempInfo;
            string strtempa = "<p>";
            string strtempb = "</p>";
            //我们要求c---g之间的字符串，也就是：defghi 
            //求得strtempa 和 strtempb 出现的位置: 
            int IndexofA = stra.IndexOf(strtempa);
            int IndexofB = stra.IndexOf(strtempb);
            string Wenzi = stra.Substring(IndexofA + 3, IndexofB - IndexofA - 3);

            strtempa = "base64,";
            strtempb = "\" ";
            IndexofA = stra.IndexOf(strtempa);
            IndexofB = stra.IndexOf(strtempb);
            string ImgString = stra.Substring(IndexofA + 7, IndexofB - IndexofA - 7);
            
            All_TemplInfo_TextBlock.Text = Wenzi;
            if (ImgString.Length > 0) 
            {
                BitmapImage Pic_img = byteArrayToImage(Convert.FromBase64String(ImgString));
                this.TempInfo_Img.Source = Pic_img;


                this.TempInfo_Img.Height = Pic_img.PixelHeight;
                this.TempInfo_Img.Width = Pic_img.PixelWidth;
            }

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

//         public string NoHTML(string Htmlstring)  //替换HTML标记
//         {
//             //删除脚本
//             Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
//             //删除HTML
//             Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
//             Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "\"", RegexOptions.IgnoreCase);//保留【 “ 】的标点符合
//             Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "\"", RegexOptions.IgnoreCase);//保留【 ” 】的标点符合
//             Htmlstring.Replace("<", "");
//             Htmlstring.Replace(">", "");
//             Htmlstring.Replace("\r\n", "");
//             //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
//             return Htmlstring;
//         }

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
