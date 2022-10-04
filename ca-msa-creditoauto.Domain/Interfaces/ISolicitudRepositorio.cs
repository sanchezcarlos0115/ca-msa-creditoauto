using camsacreditoauto.Entity.Models;
namespace camsacreditoauto.Domain.Interfaces;

public interface ISolicitudRepositorio
{

    Task<bool> ExisteSolicitudClienteMismoDiaActiva(int clienteId);

    Task<bool> ValidarSolictudActivaVehiculo(int vehiculoId);

    Task<int> GenerarSolicitudCredito(Solicitud obj);

    
}
