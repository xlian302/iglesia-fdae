using System.Collections.Generic;

namespace IGLESIA_FDAE.Models
{
    public class Biblia
    {
        public Dictionary<string,
            Dictionary<string,
            Dictionary<string, string>>> Libros
        { get; set; }
    }
}