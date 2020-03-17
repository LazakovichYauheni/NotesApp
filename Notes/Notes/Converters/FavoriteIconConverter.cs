using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Notes.Converters
{
    public class FavoriteIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? new BitmapImage(new Uri("ms-appx:///Assets/FavoriteBigRed.png")) : new BitmapImage(new Uri("ms-appx:///Assets/FavoriteBig.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? new BitmapImage(new Uri("ms-appx:///Assets/FavoriteBig.png")) : new BitmapImage(new Uri("ms-appx:///Assets/FavoriteBigRed.png"));
        }
    }
}
