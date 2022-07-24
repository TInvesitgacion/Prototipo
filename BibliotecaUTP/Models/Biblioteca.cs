using System.Collections.Generic;

namespace BibliotecaUTP.Models {
    public class Biblioteca {
        public string Titulo { get; set; }
        public string Enlace { get; set; }
        public string Descripcion { get; set; }
        public int TotalResultado { get; set; }
        public int InicioIndice { get; set; }
        public int CantidadItemsPaginado { get; set; }

        public List<Documentos> Documentos { get; set; }
    }
}
