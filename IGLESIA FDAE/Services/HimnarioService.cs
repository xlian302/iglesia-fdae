using System;
using System.IO;
using System.Text.Json;
using IGLESIA_FDAE.Models;

namespace IGLESIA_FDAE.Services
{
    public static class HimnarioService
    {
        private static Dictionary<string, Himno> _himnos
            = new Dictionary<string, Himno>();

        public static void Cargar()
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "biblia_himnario",
                "himnos.json");

            string json = File.ReadAllText(ruta);

            _himnos = JsonSerializer.Deserialize<
                Dictionary<string, Himno>>(json)
                ?? new Dictionary<string, Himno>();
        }

        public static Dictionary<string, Himno>
            ObtenerTodos()
        {
            return _himnos;
        }
    }
}