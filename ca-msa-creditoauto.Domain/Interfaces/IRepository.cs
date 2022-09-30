namespace camsacreditoauto.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<int> AgregarAsync(T entity);
    Task<int> AgregarRangoAsync(IEnumerable<T> entities);
    Task<bool> ActualizarAsync(T entity);

    Task<IEnumerable<T>> ObtenerTodosAsync();

    Task<T> ObtenerPorIdAsync(int id);

    Task<bool> EliminarAsync(T entity);
}