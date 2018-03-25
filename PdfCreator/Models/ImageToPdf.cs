using System;

namespace PdfCreator.Models
{
    public class ImageToPdf : iTextSharp.text.Image
    {

        public ImageToPdf(Uri uri):base(uri)
        {

        }

        public ImageToPdf(iTextSharp.text.Image img):base(img)
        {

        }

        public override float ScaledWidth => base.ScaledWidth;

        public override float ScaledHeight => base.ScaledHeight;

        public override bool IsJpeg()
        {
            return base.IsJpeg();
        }

        public override void SetAbsolutePosition(float absoluteX, float absoluteY)
        {
            base.SetAbsolutePosition(absoluteX, absoluteY);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}