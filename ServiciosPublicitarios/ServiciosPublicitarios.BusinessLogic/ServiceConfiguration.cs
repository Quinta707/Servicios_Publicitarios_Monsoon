using Microsoft.Extensions.DependencyInjection;
using ServiciosPublicitarios.BusinessLogic.Service;
using ServiciosPublicitarios.DataAccess;
using ServiciosPublicitarios.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.BusinessLogic
{
    public static class ServiceConfiguration
    {
        public static void DataAcces(this IServiceCollection service, string connectionString)
        {
            service.AddScoped<DepartamentoRepository>();
            service.AddScoped<MunicipioRepository>();
            service.AddScoped<CategoriaRepository>();
            service.AddScoped<MetododePagoRepository>();
            service.AddScoped<CargoRepository>();
            service.AddScoped<EstadoCivilRepository>();


            service.AddScoped<EmpleadoRepository>();
            service.AddScoped<ClienteRepository>();
            service.AddScoped<InsumoRepository>();
            service.AddScoped<ServicioRepository>();
            service.AddScoped<FacturaRepository>();
            service.AddScoped<SucursalRepository>();
            service.AddScoped<ProveedorRepository>();
            service.AddScoped<InsumosPorServicioRepository>();


            service.AddScoped<UsuarioRepository>();
            service.AddScoped<RolRepository>();

            MonsoonContext.BuildConnectionString(connectionString);
        }

        public static void BussinessLogic(this IServiceCollection service)
        {
            service.AddScoped<GeneralService>();
            service.AddScoped<PublicidadService>();
            service.AddScoped<AccessService>();
        }

    }
}
