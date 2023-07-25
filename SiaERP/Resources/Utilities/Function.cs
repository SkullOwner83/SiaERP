using SiaERP.Models;
using SiaERP.ViewModels;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SiaERP.Resources.Utilities
{
    public class Function
    {
        //Convert bitmap image to bytes array to save in database
        public static byte[] BitmapImageToBytes(BitmapImage Image)
        {
            if (Image != null)
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

            return null;
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
