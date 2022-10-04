using camsacreditoauto.Entity.Models;
namespace camsacreditoauto.Domain.Interfaces;

public interface IPersonaRepositorio
{
   
    Task<Persona> ObtenerPersona(int personaId);
    bool ExistePersona(string identificacion);
    Task<int> AgregarPersona(Persona persona);
    Task<Persona> ActualizarPersona(int personaId, Persona persona);

    Task<bool> EliminarPersona(int personaId);
}
