﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace ServiciosPublicitarios.Entities.Entities
{
    public partial class tbCategorias
    {
        public tbCategorias()
        {
            tbInsumos = new HashSet<tbInsumos>();
        }

        public int cate_Id { get; set; }
        public string cate_Descripcion { get; set; }
        public int cate_UsuCreacion { get; set; }
        public DateTime? cate_FechaCreacion { get; set; }
        public int? cate_UsuModificacion { get; set; }
        public DateTime? cate_FechaModificacion { get; set; }
        public bool? cate_Estado { get; set; }

        public virtual tbUsuarios cate_UsuCreacionNavigation { get; set; }
        public virtual tbUsuarios cate_UsuModificacionNavigation { get; set; }
        public virtual ICollection<tbInsumos> tbInsumos { get; set; }
    }
}