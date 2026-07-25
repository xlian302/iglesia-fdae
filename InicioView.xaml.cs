using System.Windows;
using System.Windows.Controls;

namespace IGLESIA_FDAE.Views
{
    public partial class InicioView : Page
    {
        public InicioView()
        {
            InitializeComponent();

            BtnBiblia.Click += BtnBiblia_Click;
            BtnHimnos.Click += BtnHimnos_Click;
            BtnBuscador.Click += BtnBuscador_Click;
        }

        private void BtnBiblia_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new BibliaView()
            );
        }

        private void BtnHimnos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new HimnarioView()
            );
        }

        private void BtnBuscador_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new BuscadorView()
            );
        }
    }
}