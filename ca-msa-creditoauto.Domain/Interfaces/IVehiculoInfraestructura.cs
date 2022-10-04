using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Entity.Dto;

namespace camsacreditoauto.Domain.Interfaces;

public interface IVehiculoInfraestructura
{
    Task<ResponseType<IEnumerable<VehiculoType>>> ObtenerVehiculos();

    Task<ResponseType<IEnumerable<VehiculoType>>> ObtenerVehiculosFiltrado(string marca, string modelo);

    Task<ResponseType<VehiculoType>> ObtenerVehiculo(int vehiculoId);

    Task<ResponseType<int>> AgregarVehiculo(VehiculoType vehiculo);

    Task<ResponseType<VehiculoType>> ActualizarVehiculo(int vehiculoId, VehiculoType vehiculo);

    Task<ResponseType<bool>> EliminarVehiculo(int vehiculoId);
    
}
