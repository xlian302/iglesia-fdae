using System.Linq;
using System.Windows.Controls;
using IGLESIA_FDAE.Services;

namespace IGLESIA_FDAE.Views
{
    public partial class BibliaView : Page
    {
        public BibliaView()
        {
            InitializeComponent();

            cmbLibro.ItemsSource =
                BibliaService.ObtenerLibros();

            if (cmbLibro.Items.Count > 0)
                cmbLibro.SelectedIndex = 0;
        }

        private void cmbLibro_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (cmbLibro.SelectedItem == null)
                return;

            string libro =
                cmbLibro.SelectedItem.ToString();

            cmbCapitulo.ItemsSource =
                BibliaService.ObtenerCapitulos(libro);

            if (cmbCapitulo.Items.Count > 0)
                cmbCapitulo.SelectedIndex = 0;
        }

        private void cmbCapitulo_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)

        {
            if (cmbLibro.SelectedItem == null ||
                cmbCapitulo.SelectedItem == null)
                return;

            string libro =
                cmbLibro.SelectedItem.ToString();

            string capitulo =
                cmbCapitulo.SelectedItem.ToString();

            var versiculos =
                BibliaService.ObtenerVersiculos(
                    libro,
                    capitulo);

            ListaVersiculos.ItemsSource =
                versiculos.Select(v =>
                $"{v.Key}. {v.Value}");
        }
    }
}