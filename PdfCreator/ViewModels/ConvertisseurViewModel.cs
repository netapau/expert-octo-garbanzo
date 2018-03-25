using Microsoft.Win32;
using PdfCreator.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfCreator.ViewModels
{
    public class ConvertisseurViewModel : BaseViewModel
    {
        List<string> images;

        #region [PROPS]

        private string uri;

        public string Chemin {
            get {
                    return uri;
                }
            set {
                    uri = value;
                    RaisePropertyChanged("Chemin");
                }
        }

        #endregion

        #region [COMMANDS]
        
        public RelayCommand OpenDir { get; set; }

        public RelayCommand CreatePdf { get; set; }

        public RelayCommand AppMinimize { get; set; }

        public RelayCommand AppClose { get; set; }

        #endregion

        #region [CTOR.]
        public ConvertisseurViewModel()
        {
            // Ouvre le dossier images et alimente la liste d'images.
            OpenDir = new RelayCommand(
                o =>
                {
                    // Recherche de fichiers image.
                    OpenFileDialog ofd = new OpenFileDialog
                    {
                        Filter = ImagesFilter.GetImageFilter()
                    };  
                    Nullable<bool> ret = ofd.ShowDialog();

                    images = new List<string>();

                    if (ret == true)
                    {
                        var file = ofd.FileName;
                        var pos = file.LastIndexOf('\\');
                        var repertoire = file.Substring(0, pos);

                        string[] fichiers = null;
                        fichiers = Directory.Exists(repertoire) ? fichiers = Directory.GetFiles(repertoire) : fichiers = new string[0];
                    
                        foreach (var item in fichiers)
                        {
                            if (item.Contains(".jpg") || item.Contains(".JPG"))
                                { images.Add(item); }
                        }
                    }

                    ofd = null;
                }, 
                o => true);

            // Creation de Pdf.
            CreatePdf = new RelayCommand(
                o => 
                {
                    var convert = new ConvertToPdf();
                    convert.ConvertFromStringList(images);
                },
                o => true);

            // Minimize.
            AppMinimize = new RelayCommand(
                o=>{ App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized; }, 
                o => true);

            // Close.
            AppClose = new RelayCommand(
                o => { App.Current.MainWindow.Close(); },
                o => true);
        }

        #endregion

    }
}