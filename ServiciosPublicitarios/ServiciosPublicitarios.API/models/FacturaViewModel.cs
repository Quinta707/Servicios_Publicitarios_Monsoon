using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.models
{
    public class FacturaViewModel
    {
        public int fact_Id { get; set; }
        public int clie_Id { get; set; }
        public int empe_Id { get; set; }
        public int meto_Id { get; set; }
        public DateTime fact_FechaCompra { get; set; }
        public int fact_UsuCreacion { get; set; }
        public DateTime fact_FechaCreacion { get; set; }
        public int? fact_UsuModificacion { get; set; }
        public DateTime? fact_FechaModificacion { get; set; }
        public bool? fact_Estado { get; set; }
    }
}
