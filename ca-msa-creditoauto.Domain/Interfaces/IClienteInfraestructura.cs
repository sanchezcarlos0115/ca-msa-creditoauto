using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Entity.Dto;

namespace camsacreditoauto.Domain.Interfaces;

public interface IClienteInfraestructura
{
   
    Task<ResponseType<IEnumerable<ClienteType>>> ObtenerClientes();

    Task<ResponseType<ClienteType>> ObtenerCliente(int clienteId);

    Task<ResponseType<int>> AgregarCliente(ClienteType cliente);

    Task<ResponseType<ClienteType>> ActualizarCliente(int clienteId, ClienteType cliente);

    Task<ResponseType<bool>> EliminarCliente(int clienteId);
}
