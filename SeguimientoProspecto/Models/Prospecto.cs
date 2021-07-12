using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SeguimientoProspecto.Models
{
    public class Prospecto
    {
        public int idProspecto { get; set; }
        public int idEstatus { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Colonia { get; set; }
        public int CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string RFC { get; set; }
        public string ObservacionRechazo { get; set; }
        public DataTable dtDocumentos { get; set; }
    }
}