using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Entity.Dto;

namespace camsacreditoauto.Domain.Interfaces;

public interface IPatioAutoInfraestructura
{

    Task<ResponseType<IEnumerable<PatioAutoType>>> ObtenerPatioAutos();

    Task<ResponseType<PatioAutoType>> ObtenerPatioAuto(int patioId);

    Task<ResponseType<int>> AgregarPatioAuto(PatioAutoType patio);

    Task<ResponseType<PatioAutoType>> ActualizarPatioAuto(int patioId, PatioAutoType patio);

    Task<ResponseType<bool>> EliminarPatioAuto(int patioId);
}
