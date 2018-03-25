using System.Windows.Controls;

namespace PdfCreator.Views
{
    public partial class ConvertisseurPage : Page
    {
        public ConvertisseurPage()
        {
            InitializeComponent();

            this.DataContext = App.ConvertisseurVM;
        }
    }
}