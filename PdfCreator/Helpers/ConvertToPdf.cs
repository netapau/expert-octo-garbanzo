using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;

namespace PdfCreator.Helpers
{
    /// <summary>
    /// Prends une liste d'images et crée un pdf avec une image par page.
    /// Les images doivent etre numerotées dans l'ordre, ou être dans l'ordre de l'index de la List<Image>.
    /// </summary>
    public class ConvertToPdf
    {
        // Generation pdf.
        public void ConvertFromStringList(List<string> listeDeImages)
        {
            // Generation de liste d'images iTextSharp.
            var listImgs = new List<iTextSharp.text.Image>();
            for (int i = 0; i < listeDeImages.Count; i++)
            {
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(listeDeImages[i]);
                listImgs.Add(image);
            }

            if (listImgs.Count >= 1)
            {
                var doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER);
                var os = new System.IO.FileStream("MON_PDF.pdf", System.IO.FileMode.OpenOrCreate);
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, os);
                doc.Open();

                try { 
                    foreach (var image in listImgs)
                    {
                        iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image);

                        if (pic.Height > pic.Width)
                        {
                            //Maximum height is 800 pixels.
                            float percentage = 0.0f;
                            percentage = 700 / pic.Height;
                            pic.ScalePercent(percentage * 100);
                        }
                        else
                        {
                            //Maximum width is 600 pixels.
                            float percentage = 0.0f;
                            percentage = 540 / pic.Width;
                            pic.ScalePercent(percentage * 100);
                        }

                        pic.Border = iTextSharp.text.Rectangle.BOX;
                        pic.BorderColor = iTextSharp.text.BaseColor.BLACK;
                        pic.BorderWidth = 3f;
                        doc.Add(pic);
                        doc.NewPage();
                    }
                }
                catch (DocumentException de)
                {
                    Console.Error.WriteLine(de.Message);
                }
                catch (IOException ioe)
                {
                    Console.Error.WriteLine(ioe.Message);
                }

                doc.Close();
            }
        }
    }
}