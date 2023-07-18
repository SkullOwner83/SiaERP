using System;
using System.Windows.Media.Imaging;

namespace SiaERP.ViewModels
{
    internal class Bitmap
    {
        private Uri uri;

        public Bitmap(Uri uri)
        {
            this.uri = uri;
        }

        public static implicit operator BitmapImage(Bitmap v)
        {
            throw new NotImplementedException();
        }
    }
}