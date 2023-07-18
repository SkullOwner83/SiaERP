using Models;
using SiaERP.ViewModels;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SiaERP.Resources.Utilities
{
    public class Function
    {
        public static void CloneObject(Customer Source, Customer Destination)
        {
            Type ObjectType = typeof(Customer);
            PropertyInfo[] Properties = ObjectType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                if (Property.CanWrite)
                {
                    object? Value = Property.GetValue(Source);
                    Property.SetValue(Destination, Value);
                }
            }
        }

        //Convert bitmap image to bytes array to save in database
        public static byte[] BitmapImageToBytes(BitmapImage Image)
        {
            JpegBitmapEncoder Encoder = new JpegBitmapEncoder(); // Dependiendo del formato de la imagen, puedes usar otros encoders (PNG, BMP, etc.)
            Encoder.Frames.Add(BitmapFrame.Create(Image));
            byte[] ImageData;

            using (MemoryStream Stream = new MemoryStream())
            {
                Encoder.Save(Stream);
                ImageData = Stream.ToArray();
            }

            return ImageData;
        }

        //Convert bytes array to bitmap image to show in view
        public static BitmapImage BytesToBitmapImage(byte[] ImageData)
        {
            if (ImageData == null || ImageData.Length == 0)
            {
                return null;
            }

            using (MemoryStream Stream = new MemoryStream(ImageData))
            {
                BitmapImage Image = new BitmapImage();
                Image.BeginInit();
                Image.CacheOption = BitmapCacheOption.OnLoad;
                Image.StreamSource = Stream;
                Image.EndInit();
                Image.Freeze(); // Importante con MemoryStream, para que la imagen pueda ser utilizada en el UI thread.
                return Image;
            }
        }
    }
}
