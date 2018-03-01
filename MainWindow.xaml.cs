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

namespace RenJiCaoZuo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
//             ImageBrush b = new ImageBrush();
//             //b.ImageSource = new BitmapImage(@("Frozen.jpg"));
//             b.Stretch = Stretch.Fill;
//             this.Background = b;
        }

//         BitMatrix bitMatrix;
// 
//         private void Button_Click_1(object sender, RoutedEventArgs e)
//         {
//             string content = @"12345679890";
// 
// 
//             Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
//             hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
// 
//             bitMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, 200, 200);
//             this.img.Stretch = Stretch.Fill;
//             this.img.Source = toImage(bitMatrix);
//         }

//         private BitmapImage toImage(BitMatrix matrix)
//         {
//             try
//             {
//                 int width = matrix.Width;
//                 int height = matrix.Height;
// 
//                 Bitmap bmp = new Bitmap(width, height);
// 
//                 // byte[] pixel = new byte[width * height];
// 
//                 for (int x = 0; x < height; x++)
//                 {
//                     for (int y = 0; y < width; y++)
//                     {
//                         if (bitMatrix[x, y])
//                         {
//                             bmp.SetPixel(x, y, System.Drawing.Color.Black);
//                         }
//                         else
//                         {
//                             bmp.SetPixel(x, y, System.Drawing.Color.White);
//                         }
//                     }
//                 }
// 
//                 return ConvertBitmapToBitmapImage(bmp);
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
// 
//         }
//         private static BitmapImage ConvertBitmapToBitmapImage(Bitmap wbm)
//         {
//             BitmapImage bimg = new BitmapImage();
// 
//             using (MemoryStream stream = new MemoryStream())
//             {
//                 wbm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
// 
//                 bimg.BeginInit();
//                 stream.Seek(0, SeekOrigin.Begin);
//                 bimg.StreamSource = stream;
//                 bimg.CacheOption = BitmapCacheOption.OnLoad;
//                 bimg.EndInit();
//             }
//             return bimg;
// 
//         }

    }
}
