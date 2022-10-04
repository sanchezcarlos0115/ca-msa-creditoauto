using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Entity.Dto;

namespace camsacreditoauto.Domain.Interfaces;

public interface IClientePatioInfraestructura
{

    Task<ResponseType<IEnumerable<ClientePatioType>>> ObtenerClientePatios();

    Task<ResponseType<ClientePatioType>> ObtenerClientePatio(int id);

    Task<ResponseType<int>> AgregarClientePatio(ClientePatioType cliente);

    Task<ResponseType<ClientePatioType>> ActualizarClientePatio(int id, ClientePatioType cliente);

    Task<ResponseType<bool>> EliminarClientePatio(int id);
}
