using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.models
{
    public class MetododePagoViewModel
    {
        public int meto_Id { get; set; }
        public string meto_Descripcion { get; set; }
        public int meto_UsuCreacion { get; set; }
        public DateTime? meto_FechaCreacion { get; set; }
        public int? meto_UsuModificacion { get; set; }
        public DateTime? meto_FechaModificacion { get; set; }
        public bool? meto_Estado { get; set; }
    }
}
