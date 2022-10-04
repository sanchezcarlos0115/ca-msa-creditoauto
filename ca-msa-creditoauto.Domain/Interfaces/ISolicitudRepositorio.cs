using camsacreditoauto.Entity.Models;
namespace camsacreditoauto.Domain.Interfaces;

public interface ISolicitudRepositorio
{
    Task<bool> ValidarClienteSujetoCredito(int clienteId);
    Task<bool> ExisteSolicitudClienteMismoDiaActiva(int clienteId);

    Task<bool> ValidarSolictudActivaVehiculo(int vehiculoId);

    Task<int> GenerarSolicitudCredito(Solicitud obj);

    Task<bool> ValidarSolicitudActiva_ClientePatio(int Id);
}
