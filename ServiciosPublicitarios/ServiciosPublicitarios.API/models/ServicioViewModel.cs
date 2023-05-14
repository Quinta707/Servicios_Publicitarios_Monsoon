using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.models
{
    public class ServicioViewModel
    {
        public int serv_Id { get; set; }
        public string serv_Nombre { get; set; }
        public decimal? serv_Precio { get; set; }
        public int serv_UsuCreacion { get; set; }
        public DateTime serv_FechaCreacion { get; set; }
        public int? serv_UsuModificacion { get; set; }
        public DateTime? serv_FechaModificacion { get; set; }
        public bool? serv_Estado { get; set; }
        public string serv_Url { get; set; }



    }
}
