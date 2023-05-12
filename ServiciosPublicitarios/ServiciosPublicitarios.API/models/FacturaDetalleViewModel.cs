using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.Controllers
{
    public class FacturaDetalleViewModel
    {
        public int fdet_Id { get; set; }
        public int fact_Id { get; set; }
        public int serv_Id { get; set; }
        public int fdet_Cantidad { get; set; }
        public int fdet_UsuCreacion { get; set; }
        public DateTime fdet_FechaCreacion { get; set; }
        public int? fdet_UsuModificacion { get; set; }
        public DateTime? fdet_FechaModificacion { get; set; }
        public bool? fdet_Estado { get; set; }
    }
}
