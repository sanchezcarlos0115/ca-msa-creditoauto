using camsacreditoauto.Entity.Models;

namespace camsacreditoauto.Domain.Interfaces;

public interface IClientePatioRepositorio
{
    Task<IEnumerable<ClientePatio>> ObtenerClientePatios();

    Task<bool> ExisteClienteAsignado(int clienteId);

    Task<bool> ExistePatioAutoAsignado(int patioId);

    Task<ClientePatio> ObtenerClientePatio(int id);

    Task<int> AgregarClientePatio(ClientePatio obj);

    Task<ClientePatio> ActualizarClientePatio(int id, ClientePatio obj);

    Task<bool> EliminarClientePatio(int id);
}
