using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;

namespace PdfCreator.Views
{
    public partial class SplashPage : Page
    {
        public SplashPage()
        {
            InitializeComponent();

            // Pause dans un autre thread puis passe a la page.
            this.Loaded += (s, e) => {
                Task T;
                T = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    this.Dispatcher.Invoke(()=> this.NavigationService.Navigate(new ConvertisseurPage()));
                });
            };
        }
    }
}