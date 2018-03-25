using System.Windows;

namespace PdfCreator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            NavigationFrame.Navigate(new SplashPage());
        }
    }
}
