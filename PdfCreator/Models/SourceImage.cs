using System.Drawing;

namespace PdfCreator.Models
{
    public class SourceImage
    {
        public Image BitmapImg { get; private set; }

        public SourceImage(string uri)
        {
            BitmapImg = Bitmap.FromFile(uri);
        }
    }
}
