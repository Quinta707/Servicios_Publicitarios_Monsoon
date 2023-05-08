using AutoMapper;
using ServiciosPublicitarios.API.models;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.Extensions
{
    public class MappingProfileExntensions : Profile
    {
        public MappingProfileExntensions()
        {
            CreateMap<EmpleadoViewModel,        tbEmpleados>().ReverseMap();
            CreateMap<ClienteVIewModel,         tbClientes>().ReverseMap();
            CreateMap<InsumoViewModel,          tbInsumos>().ReverseMap();
            CreateMap<ServicioViewModel,        tbServicios>().ReverseMap();
            CreateMap<FacturaViewModel,         tbFacturas>().ReverseMap();
            CreateMap<SucursalViewModel,        tbSucursales>().ReverseMap();
            CreateMap<ProveedorViewModel,       tbProveedores>().ReverseMap();


            CreateMap<DepartamentoViewModel,    tbDepartamentos>().ReverseMap();
            CreateMap<MunicipioViewModel,       tbMunicipios>().ReverseMap();
            CreateMap<CategoriaViewModel,       tbCategorias>().ReverseMap();
            CreateMap<MetododePagoViewModel,    tbMetodosdePago>().ReverseMap();
            CreateMap<CargoViewModel,           tbCargos>().ReverseMap();
            CreateMap<tbEstadosCiviles,         tbEstadosCiviles>().ReverseMap();


            CreateMap<UsuarioViewModel,         tbUsuarios>().ReverseMap();
            CreateMap<RolViewModel,             tbRoles>().ReverseMap();
        }

    }
}
