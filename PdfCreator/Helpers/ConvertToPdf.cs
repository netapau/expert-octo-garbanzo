using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text;

namespace PdfCreator.Helpers
{
    public delegate void Converted(object sender, ConvertedEventArgs arg);

    /// <summary>
    /// Prends une liste d'images et crée un pdf avec une image par page.
    /// Les images doivent etre numerotées dans l'ordre, ou être dans l'ordre de l'index de la List<Image>.
    /// </summary>
    public class ConvertToPdf
    {
        public event Converted FileConverted;

        private bool working = false;
        
        // Crée une liste d'images a partir d'une liste de strings.
        private async Task<List<iTextSharp.text.Image>> GenereImages(List<string> listeDeImages)
        {
            var listImgs = new List<iTextSharp.text.Image>();
            List<iTextSharp.text.Image> T = await Task.Run(() => { 
                for (int i = 0; i < listeDeImages.Count; i++)
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(listeDeImages[i]);
                    listImgs.Add(image);
                }
                return listImgs;
            });
            return T;
        }

        // Generation pdf.
        public async void ConvertFromStringList(List<string> listeDeImages)
        {
            // TODO : Verifier le lock correctement.
            if (working == false)
            { 
                working = true;

                // Generation de liste d'images iTextSharp.
                var listImgs = await GenereImages(listeDeImages);

                await Task.Run(()=>
                { 
                    if (listImgs.Count >= 1)
                    {
                        string fichier = "MON_PDF.pdf";
                        var doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER);
                        // TODO : afficher le chemin de fichier au bon endroit (un label ?).
                        var os = new System.IO.FileStream(fichier, System.IO.FileMode.OpenOrCreate);
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

                        working = false;
                        doc.Close();

                        FileConverted?.Invoke(this, new ConvertedEventArgs { FileConverted = fichier });
                    }
                });
            }
        }
    }

    public class ConvertedEventArgs : EventArgs
    {
        public string FileConverted { get; set; }
    }
}