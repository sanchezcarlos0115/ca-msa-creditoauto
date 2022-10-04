using camsacreditoauto.Entity.Models;

namespace camsacreditoauto.Domain.Interfaces;

public interface IVehiculoRepositorio
{
    Task<bool> ValidarExistenciaVehiculoMismaPlaca(string placa);

    Task<IEnumerable<Vehiculo>> ObtenerVehiculos();

    Task<IEnumerable<Vehiculo>> ObtenerVehiculosFiltrado(string marca, string modelo, bool todos);

    Task<Vehiculo> ObtenerVehiculo(int vehiculoId);

    Task<int> AgregarVehiculo(Vehiculo obj);

    Task<Vehiculo> ActualizarVehiculo(int id, Vehiculo obj);

    Task<bool> EliminarVehiculo(int id);

   
}
