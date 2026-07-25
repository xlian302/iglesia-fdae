using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace IGLESIA_FDAE.Services
{
    public static class BibliaService
    {
        private static Dictionary<string,
            Dictionary<string,
            Dictionary<string, string>>> _biblia;

        private static Dictionary<string, string> _nombreAKey;

        private static readonly Dictionary<string, string>
            _nombresBonitos = new()
        {
            ["genesis"] = "Génesis",
            ["exodo"] = "Éxodo",
            ["levitico"] = "Levítico",
            ["numeros"] = "Números",
            ["deuteronomio"] = "Deuteronomio",
            ["josue"] = "Josué",
            ["jueces"] = "Jueces",
            ["rut"] = "Rut",
            ["1_samuel"] = "1 Samuel",
            ["2_samuel"] = "2 Samuel",
            ["1_reyes"] = "1 Reyes",
            ["2_reyes"] = "2 Reyes",
            ["1_cronicas"] = "1 Crónicas",
            ["2_cronicas"] = "2 Crónicas",
            ["esdras"] = "Esdras",
            ["nehemias"] = "Nehemías",
            ["ester"] = "Ester",
            ["job"] = "Job",
            ["salmos"] = "Salmos",
            ["proverbios"] = "Proverbios",
            ["eclesiastes"] = "Eclesiastés",
            ["cantares"] = "Cantares",
            ["isaias"] = "Isaías",
            ["jeremias"] = "Jeremías",
            ["lamentaciones"] = "Lamentaciones",
            ["ezequiel"] = "Ezequiel",
            ["daniel"] = "Daniel",
            ["oseas"] = "Oseas",
            ["joel"] = "Joel",
            ["amos"] = "Amós",
            ["abdias"] = "Abdías",
            ["jonas"] = "Jonás",
            ["miqueas"] = "Miqueas",
            ["nahum"] = "Nahúm",
            ["habacuc"] = "Habacuc",
            ["sofonias"] = "Sofonías",
            ["hageo"] = "Hageo",
            ["zacarias"] = "Zacarías",
            ["malaquias"] = "Malaquías",
            ["mateo"] = "Mateo",
            ["marcos"] = "Marcos",
            ["lucas"] = "Lucas",
            ["juan"] = "Juan",
            ["hechos"] = "Hechos",
            ["romanos"] = "Romanos",
            ["1_corintios"] = "1 Corintios",
            ["2_corintios"] = "2 Corintios",
            ["galatas"] = "Gálatas",
            ["efesios"] = "Efesios",
            ["filipenses"] = "Filipenses",
            ["colosenses"] = "Colosenses",
            ["1_tesalonicenses"] = "1 Tesalonicenses",
            ["2_tesalonicenses"] = "2 Tesalonicenses",
            ["1_timoteo"] = "1 Timoteo",
            ["2_timoteo"] = "2 Timoteo",
            ["tito"] = "Tito",
            ["filemon"] = "Filemón",
            ["hebreos"] = "Hebreos",
            ["santiago"] = "Santiago",
            ["1_pedro"] = "1 Pedro",
            ["2_pedro"] = "2 Pedro",
            ["1_juan"] = "1 Juan",
            ["2_juan"] = "2 Juan",
            ["3_juan"] = "3 Juan",
            ["judas"] = "Judas",
            ["apocalipsis"] = "Apocalipsis"
        };

        public static void Cargar()
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "biblia_himnario",
                "biblia.json");

            string json = File.ReadAllText(ruta);

            _biblia = JsonSerializer.Deserialize<
                Dictionary<string,
                Dictionary<string,
                Dictionary<string, string>>>>(json);

            _nombreAKey = new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var kvp in _nombresBonitos)
            {
                _nombreAKey[kvp.Value.ToLower()
                    .Normalize(
                        System.Text.NormalizationForm
                            .FormD)
                    .Replace(" ", "")] = kvp.Key;

                _nombreAKey[kvp.Key] = kvp.Key;
            }
        }

        private static string Normalizar(string texto)
        {
            string s = texto.ToLower()
                .Normalize(
                    System.Text.NormalizationForm.FormD);

            var sb = new System.Text.StringBuilder();

            foreach (char c in s)
            {
                var uc = System.Globalization
                    .CharUnicodeInfo.GetUnicodeCategory(c);

                if (uc !=
                    System.Globalization
                        .UnicodeCategory
                        .NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString()
                .Normalize(
                    System.Text.NormalizationForm.FormC)
                .Replace(" ", "");
        }

        private static (string libro, string capitulo,
            string versiculo) ParsearReferencia(
                string entrada)
        {
            string limpia = entrada.Trim();

            var match = Regex.Match(
                limpia,
                @"^(\d?\s*[A-Za-záéíóúñÁÉÍÓÚÑ]+)\s+" +
                @"(\d+)\s*:\s*(\d+)$",
                RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return (
                    match.Groups[1].Value.Trim(),
                    match.Groups[2].Value,
                    match.Groups[3].Value);
            }

            var matchSinVersiculo = Regex.Match(
                limpia,
                @"^(\d?\s*[A-Za-záéíóúñÁÉÍÓÚÑ]+)\s+" +
                @"(\d+)$",
                RegexOptions.IgnoreCase);

            if (matchSinVersiculo.Success)
            {
                return (
                    matchSinVersiculo.Groups[1]
                        .Value.Trim(),
                    matchSinVersiculo.Groups[2].Value,
                    null);
            }

            return (limpia, null, null);
        }

        private static string BuscarKeyLibro(
            string nombre)
        {
            string norm = Normalizar(nombre);

            foreach (var kvp in _nombreAKey)
            {
                if (Normalizar(kvp.Key) == norm)
                    return kvp.Value;
            }

            string conEspacios = nombre
                .Replace("_", " ");

            foreach (var kvp in _nombresBonitos)
            {
                if (string.Equals(
                    kvp.Value, conEspacios,
                    StringComparison
                        .OrdinalIgnoreCase))
                {
                    return kvp.Key;
                }
            }

            foreach (var kvp in _nombresBonitos)
            {
                if (kvp.Key.Contains(norm) ||
                    norm.Contains(kvp.Key))
                {
                    return kvp.Key;
                }
            }

            return null;
        }

        public static List<string> ObtenerLibros()
        {
            return _biblia.Keys.ToList();
        }

        public static List<string> ObtenerCapitulos(
            string libro)
        {
            return _biblia[libro].Keys.ToList();
        }

        public static Dictionary<string, string>
            ObtenerVersiculos(
                string libro, string capitulo)
        {
            return _biblia[libro][capitulo];
        }

        public static List<(string referencia,
            string texto)> BuscarReferencia(
                string entrada)
        {
            var resultados =
                new List<(string, string)>();

            var (nombreLibro, cap, ver) =
                ParsearReferencia(entrada);

            string key = BuscarKeyLibro(nombreLibro);

            if (key == null ||
                !_biblia.ContainsKey(key))
            {
                return resultados;
            }

            string nombreBonito =
                _nombresBonitos.ContainsKey(key)
                    ? _nombresBonitos[key]
                    : key;

            if (cap != null &&
                _biblia[key].ContainsKey(cap))
            {
                if (ver != null &&
                    _biblia[key][cap]
                        .ContainsKey(ver))
                {
                    resultados.Add((
                        $"{nombreBonito} {cap}:{ver}",
                        _biblia[key][cap][ver]));
                }
                else if (ver == null)
                {
                    foreach (var v in _biblia[key][cap])
                    {
                        resultados.Add((
                            $"{nombreBonito} " +
                            $"{cap}:{v.Key}",
                            v.Value));
                    }
                }
            }

            return resultados;
        }

        public static List<(string referencia,
            string texto)> BuscarTexto(
                string texto)
        {
            var resultados =
                new List<(string, string)>();

            string busqueda = texto.ToLower()
                .Normalize(
                    System.Text.NormalizationForm.FormD);

            var sb = new System.Text.StringBuilder();

            foreach (char c in busqueda)
            {
                var uc = System.Globalization
                    .CharUnicodeInfo
                    .GetUnicodeCategory(c);

                if (uc !=
                    System.Globalization
                        .UnicodeCategory
                        .NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            string norm = sb.ToString()
                .Normalize(
                    System.Text.NormalizationForm.FormC);

            foreach (var libro in _biblia)
            {
                string nombreBonito =
                    _nombresBonitos.ContainsKey(
                        libro.Key)
                        ? _nombresBonitos[libro.Key]
                        : libro.Key;

                foreach (var cap in libro.Value)
                {
                    foreach (var ver in cap.Value)
                    {
                        string versoNorm = ver.Value
                            .ToLower()
                            .Normalize(
                                System.Text
                                    .NormalizationForm
                                    .FormD);

                        var sb2 =
                            new System.Text
                                .StringBuilder();

                        foreach (char c in versoNorm)
                        {
                            var uc = System.Globalization
                                .CharUnicodeInfo
                                .GetUnicodeCategory(c);

                            if (uc !=
                                System.Globalization
                                    .UnicodeCategory
                                    .NonSpacingMark)
                            {
                                sb2.Append(c);
                            }
                        }

                        string versoNormalizado =
                            sb2.ToString()
                            .Normalize(
                                System.Text
                                    .NormalizationForm
                                    .FormC);

                        if (versoNormalizado
                            .Contains(norm))
                        {
                            resultados.Add((
                                $"{nombreBonito} " +
                                $"{cap.Key}:{ver.Key}",
                                ver.Value));
                        }
                    }
                }
            }

            return resultados;
        }

        public static string ObtenerNombreBonito(
            string key)
        {
            return _nombresBonitos.ContainsKey(key)
                ? _nombresBonitos[key]
                : key;
        }
    }
}