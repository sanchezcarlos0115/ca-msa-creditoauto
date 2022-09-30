using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace camsacreditoauto.Infrastructure.Services;

public class Repository<T> : IRepository<T> where T : class
{
    public readonly CreditoAutoContext _context;
    public Repository(CreditoAutoContext context)
    {
        _context = context;
    }

    public async Task<int> AgregarAsync(T entity)
    {
        _context.Add(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> AgregarRangoAsync(IEnumerable<T> entities)
    {
        _context.AddRange(entities);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> EliminarAsync(T entity)
    {
        _context.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<T> ObtenerPorIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<bool> ActualizarAsync(T entity)
    {
        _context.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

}