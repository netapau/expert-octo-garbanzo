namespace PdfCreator.Helpers
{
    static class ImagesFilter
    {
        /// <summary>
        /// Get the Filter string for all supported image types.
        /// This can be used directly to the FileDialog class Filter Property.
        /// </summary>
        /// <returns>string</returns>
        public static string GetImageFilter()
        {
            return "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        }
    }
}
