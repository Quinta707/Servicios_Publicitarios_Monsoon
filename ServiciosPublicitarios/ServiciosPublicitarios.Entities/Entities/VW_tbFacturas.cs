﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace ServiciosPublicitarios.Entities.Entities
{
    public partial class VW_tbFacturas
    {
        public int fact_Id { get; set; }
        public int clie_Id { get; set; }
        public string clie_NombreCompleto { get; set; }
        public int empe_Id { get; set; }
        public string empe_NombreCompleto { get; set; }
        public int sucu_Id { get; set; }
        public string sucu_Nombre { get; set; }
        public int meto_Id { get; set; }
        public string meto_Descripcion { get; set; }
        public DateTime fact_FechaCompra { get; set; }
        public int fact_UsuCreacion { get; set; }
        public string user_Creacion { get; set; }
        public DateTime fact_FechaCreacion { get; set; }
        public int? fact_UsuModificacion { get; set; }
        public string user_Modificacion { get; set; }
        public DateTime? fact_FechaModificacion { get; set; }
        public bool fact_Estado { get; set; }
    }
}