﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace ServiciosPublicitarios.Entities.Entities
{
    public partial class VW_tbservicios
    {
        public int serv_Id { get; set; }
        public string serv_Nombre { get; set; }
        public decimal? serv_Precio { get; set; }
        public string serv_url { get; set; }
        public int serv_UsuCreacion { get; set; }
        public string user_Creacion { get; set; }
        public DateTime serv_FechaCreacion { get; set; }
        public int? serv_UsuModificacion { get; set; }
        public string user_Modificacion { get; set; }
        public DateTime? serv_FechaModificacion { get; set; }
        public bool serv_Estado { get; set; }
    }
}