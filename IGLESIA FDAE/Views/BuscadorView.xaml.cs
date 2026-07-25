using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using IGLESIA_FDAE.Services;

namespace IGLESIA_FDAE.Views
{
    public partial class BuscadorView : Page
    {
        public BuscadorView()
        {
            InitializeComponent();
        }

        private void Buscar_Click(
            object sender, RoutedEventArgs e)
        {
            EjecutarBusqueda();
        }

        private void TxtBusqueda_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EjecutarBusqueda();
            }
        }

        private void EjecutarBusqueda()
        {
            string texto = TxtBusqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                TxtAyuda.Text =
                    "Escribe algo para buscar. " +
                    "Ejemplo: 2 corintios 1:1  |  " +
                    "juan 3:16  |  genesis 1";
                PanelResultados
                    .Children.Clear();
                return;
            }

            var porRef =
                BibliaService
                    .BuscarReferencia(texto);

            if (porRef.Count > 0)
            {
                MostrarResultados(porRef);
                return;
            }

            if (texto.Length < 3)
            {
                TxtAyuda.Text =
                    "Escribe al menos 3 letras " +
                    "para buscar por contenido.";
                PanelResultados
                    .Children.Clear();
                return;
            }

            var porTexto =
                BibliaService
                    .BuscarTexto(texto);

            MostrarResultados(porTexto);
        }

        private void MostrarResultados(
            List<(string referencia,
                string texto)> resultados)
        {
            PanelResultados.Children.Clear();

            if (resultados.Count == 0)
            {
                var txtNoResult = new TextBlock
                {
                    Text = "No se encontraron " +
                           "resultados.",
                    Foreground =
                        new SolidColorBrush(
                            Colors.Gray),
                    FontSize = 16,
                    Margin =
                        new Thickness(0, 20, 0, 0)
                };
                PanelResultados
                    .Children.Add(txtNoResult);
                return;
            }

            TxtAyuda.Text =
                $"{resultados.Count} resultado(s) " +
                $"encontrado(s)";

            int max = Math.Min(
                resultados.Count, 50);

            for (int i = 0; i < max; i++)
            {
                var (referencia, texto) =
                    resultados[i];

                var border = new Border
                {
                    Background =
                        new SolidColorBrush(
                            Color.FromRgb(
                                0x22, 0x22,
                                0x22)),
                    CornerRadius =
                        new CornerRadius(8),
                    Padding =
                        new Thickness(15, 12,
                            15, 12),
                    Margin =
                        new Thickness(0, 0, 0, 8)
                };

                var stack = new StackPanel();

                var txtRef = new TextBlock
                {
                    Text = "📖 " + referencia,
                    Foreground =
                        new SolidColorBrush(
                            Color.FromRgb(
                                0x5B, 0xA8,
                                0xFF)),
                    FontSize = 16,
                    FontWeight =
                        FontWeights.Bold,
                    Margin =
                        new Thickness(0, 0, 0, 6)
                };

                var txtTexto = new TextBlock
                {
                    Text = texto,
                    Foreground =
                        new SolidColorBrush(
                            Colors.White),
                    FontSize = 15,
                    TextWrapping =
                        TextWrapping.Wrap,
                    LineHeight = 24
                };

                stack.Children.Add(txtRef);
                stack.Children.Add(txtTexto);
                border.Child = stack;

                PanelResultados
                    .Children.Add(border);
            }

            if (resultados.Count > 50)
            {
                var txtMas = new TextBlock
                {
                    Text =
                        $"... y {resultados.Count - 50}" +
                        " resultados más",
                    Foreground =
                        new SolidColorBrush(
                            Colors.Gray),
                    FontSize = 14,
                    Margin =
                        new Thickness(0, 5, 0, 0)
                };
                PanelResultados
                    .Children.Add(txtMas);
            }
        }
    }
}