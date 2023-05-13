using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.models
{
    public class InsumosPorServicioViewModel
    {
        public int insu_Id { get; set; }
        public string insu_Nombre { get; set; }
        public int? cate_Id { get; set; }
        public decimal? insu_Precio { get; set; }
        public int? prov_Id { get; set; }
        public int insu_UsuCreacion { get; set; }
        public DateTime insu_FechaCreacion { get; set; }
        public int? insu_UsuModificacion { get; set; }
        public DateTime? insu_FechaModificacion { get; set; }
        public bool insu_Estado { get; set; }
        public int inse_Id { get; set; }
        public int? serv_Id { get; set; }
        public int inse_UsuCreacion { get; set; }
        public DateTime inse_FechaCreacion { get; set; }
        public int? inse_UsuModificacion { get; set; }
        public DateTime? inse_FechaModificacion { get; set; }
        public bool inse_Estado { get; set; }
    }
}
