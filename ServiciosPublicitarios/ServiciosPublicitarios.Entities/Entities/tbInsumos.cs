﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace ServiciosPublicitarios.Entities.Entities
{
    public partial class tbInsumos
    {
        public tbInsumos()
        {
            tbInsumosPorServicios = new HashSet<tbInsumosPorServicios>();
        }

        public int insu_Id { get; set; }
        public string insu_Nombre { get; set; }
        public int? cate_Id { get; set; }
        public decimal? insu_Precio { get; set; }
        public int? prov_Id { get; set; }
        public int insu_UsuCreacion { get; set; }
        public DateTime insu_FechaCreacion { get; set; }
        public int? insu_UsuModificacion { get; set; }
        public DateTime? insu_FechaModificacion { get; set; }
        public bool? insu_Estado { get; set; }

        public virtual tbCategorias cate { get; set; }
        public virtual tbUsuarios insu_UsuCreacionNavigation { get; set; }
        public virtual tbUsuarios insu_UsuModificacionNavigation { get; set; }
        public virtual tbProveedores prov { get; set; }
        public virtual ICollection<tbInsumosPorServicios> tbInsumosPorServicios { get; set; }
    }
}