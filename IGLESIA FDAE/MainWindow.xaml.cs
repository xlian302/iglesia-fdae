using System.Windows;

namespace IGLESIA_FDAE
{
    public partial class MainWindow : Window
    {
        public static MainWindow? Instancia;

        public MainWindow()
        {
            InitializeComponent();

            Instancia = this;

            MainFrame.Navigate(
                new Views.InicioView()
            );
        }
    }
}