using camsacreditoauto.Entity.Models;

namespace camsacreditoauto.Domain.Interfaces;

public interface IClienteRepositorio
{
    Task<IEnumerable<Cliente>> ObtenerClientes();

    Task<Cliente> ObtenerCliente(int clienteId);

    Task<int> AgregarCliente(Cliente cliente);
    Task<Cliente> ActualizarCliente(int clienteId, Cliente cliente);

    Task<bool> EliminarCliente(int clienteId);
}
