using PdfCreator.ViewModels;
using System.Windows;

namespace PdfCreator
{
    public partial class App : Application
    {
        //public App()
        //{
        //    Startup += (s, e) => { System.Diagnostics.Debug.WriteLine("Demarrage de l'app ok !"); };
        //    PdfCreator.Helpers.LogWriter.LogToFile("Start app");
        //}

        private static ConvertisseurViewModel convertisseurVM;

        public static ConvertisseurViewModel ConvertisseurVM
        {
            get {
                if (convertisseurVM == null)
                    {
                        convertisseurVM = new ConvertisseurViewModel();
                    }
                return convertisseurVM;
            }
        }
    }
}