using System.Windows;
using IGLESIA_FDAE.Services;

namespace IGLESIA_FDAE
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                BibliaService.Cargar();
                HimnarioService.Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error cargando archivos:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}