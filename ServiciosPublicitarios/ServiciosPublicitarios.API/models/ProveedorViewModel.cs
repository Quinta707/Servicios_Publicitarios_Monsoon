using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.models
{
    public class ProveedorViewModel
    {
        public int prov_Id { get; set; }
        public string prov_Nombre { get; set; }
        public int? muni_Id { get; set; }
        public string prov_Direccion { get; set; }
        public string prov_Correo { get; set; }
        public int prov_UsuCreacion { get; set; }
        public DateTime prov_FechaCreacion { get; set; }
        public int? prov_UsuModificacion { get; set; }
        public DateTime? prov_FechaModificacion { get; set; }
        public bool? prov_Estado { get; set; }
    }
}
