using camsacreditoauto.Entity.Models;

namespace camsacreditoauto.Domain.Interfaces;

public interface IPatioAutoRepositorio
{
    Task<IEnumerable<PatioAuto>> ObtenerPatioAutos();

    Task<PatioAuto> ObtenerPatioAuto(int patioAutoId);

    Task<bool> ExistePatioAuto(string nombre);
    Task<int> AgregarPatioAuto(PatioAuto obj);

    Task<PatioAuto> ActualizarPatioAuto(int patioId, PatioAuto obj);

    Task<bool> EliminarPatioAuto(int patioId);
}
