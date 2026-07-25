using System.Windows;
using System.Windows.Controls;

namespace IGLESIA_FDAE.Views
{
    public partial class InicioView : Page
    {
        public InicioView()
        {
            InitializeComponent();
        }

        private void Biblia_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new BibliaView()
            );
        }

        private void Himnario_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new HimnarioView()
            );
        }

        private void Buscador_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new BuscadorView()
            );
        }
    }
}