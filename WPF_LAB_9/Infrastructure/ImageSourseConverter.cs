using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF_LAB_9.Infrastructure
{
    public class ImageSourseConverter : IValueConverter
    {
        string root = Directory.GetCurrentDirectory();
        string ImageDirectory => Path.Combine(root, "Images"); 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = new BitmapImage();            
            using (var stream = File.OpenRead(Path.Combine(ImageDirectory, (string)value)))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
