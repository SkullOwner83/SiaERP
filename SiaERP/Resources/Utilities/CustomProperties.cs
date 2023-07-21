using System.Windows.Media;
using System.Windows;

namespace SiaERP.Resources.Utilities
{
    public static class CustomProperties
    {
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.RegisterAttached("Image", typeof(ImageSource), typeof(CustomProperties), new PropertyMetadata(null));

        public static void SetImage(UIElement element, ImageSource value)
        {
            element.SetValue(ImageProperty, value);
        }

        public static ImageSource GetImage(UIElement element)
        {
            return (ImageSource)element.GetValue(ImageProperty);
        }
    }
}
