using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Domain.Comun.Wrappers;

namespace camsacreditoauto.Infrastructure.Services;

public class ClienteInfraestructura : IClienteInfraestructura
{
    private readonly IClienteRepositorio _clienteRepo;
    private readonly IPersonaRepositorio _personaRepo;
    private readonly IClientePatioRepositorio _cl_patio_Repo;

    public ClienteInfraestructura(IClienteRepositorio clienteRepo, IPersonaRepositorio personaRepo, IClientePatioRepositorio cl_patio_Repo)
    {
        _clienteRepo = clienteRepo;
        _personaRepo = personaRepo;
        _cl_patio_Repo = cl_patio_Repo;
    }

    public async Task<ResponseType<IEnumerable<ClienteType>>> ObtenerClientes()
    {
        var objClientes = await _clienteRepo.ObtenerClientes();

        if (objClientes is not null && objClientes.Any())
        {
            var result = (from c in objClientes
                          select new ClienteType()
                          {
                              ClienteId = c.ClienteId,
                              Identificacion = c.Persona.Identificacion,
                              Nombres = c.Persona.Nombres,
                              Apellidos = c.Persona.Apellidos,
                              Direccion = c.Persona.Direccion,
                              Edad = c.Persona.Edad,
                              EstadoCivil = c.EstadoCivil,
                              FechaNacimiento = c.FechaNacimiento,
                              Telefono = c.Persona.Telefono,
                              IdentificacionConyuge = c.IdentificacionConyuge,
                              NombresConyuge = c.NombresConyuge,
                              SujetoCredito = c.SujetoCredito
                          }).AsEnumerable();

            return new ResponseType<IEnumerable<ClienteType>>() { Data = result, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<IEnumerable<ClienteType>>() { Data = null, Succeeded = false, Message = "No existe información para mostrar", StatusCode = "999" };
    }

    public async Task<ResponseType<ClienteType>> ObtenerCliente(int clienteId)
    {
        var cliente =  await _clienteRepo.ObtenerCliente(clienteId);
        if (cliente is not null)
        {
            var objClienteType = new ClienteType() 
            {
                ClienteId = cliente.ClienteId,
                Identificacion = cliente.Persona.Identificacion,
                Nombres = cliente.Persona.Nombres,
                Apellidos = cliente.Persona.Apellidos,
                Direccion = cliente.Persona.Direccion,
                Edad = cliente.Persona.Edad,
                EstadoCivil = cliente.EstadoCivil,
                FechaNacimiento = cliente.FechaNacimiento,
                Telefono = cliente.Persona.Telefono,
                IdentificacionConyuge = cliente.IdentificacionConyuge,
                NombresConyuge = cliente.NombresConyuge,
                SujetoCredito = cliente.SujetoCredito   
            };
            return new ResponseType<ClienteType>() { Data = objClienteType, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<ClienteType>() { Data = null, Succeeded = false, Message = $"No existe información del cliente {clienteId}", StatusCode = "999" };
    }

    public async Task<ResponseType<int>> AgregarCliente(ClienteType cliente)
    {

        if (!_personaRepo.ExistePersona(cliente.Identificacion)) 
        {
            var objNewPersona = new Persona() 
            { 
                 Identificacion = cliente.Identificacion,
                 Nombres = cliente.Nombres,
                 Apellidos = cliente.Apellidos,
                 Direccion = cliente.Direccion,
                 Edad = cliente.Edad,
                 Telefono = cliente.Telefono
            };

            var idNewPersona = await _personaRepo.AgregarPersona(objNewPersona);
            if (idNewPersona > 0)
            {
                var objNewCliente = new Cliente()
                {
                    PersonaId = idNewPersona,
                    EstadoCivil = cliente.EstadoCivil,
                    FechaNacimiento = cliente.FechaNacimiento,
                    IdentificacionConyuge = cliente.IdentificacionConyuge,
                    NombresConyuge = cliente.NombresConyuge,
                    SujetoCredito = cliente.SujetoCredito
                };

                var idNewCliente = await _clienteRepo.AgregarCliente(objNewCliente);
                return new ResponseType<int>() { Data = idNewCliente, Succeeded = true, Message = "Cliente registrado exitosamente", StatusCode = "000" };
            }
            return new ResponseType<int>() { Data = 0, Succeeded = false, Message = $"Hubo un error al intentar registrar el cliente {cliente.Identificacion}", StatusCode = "999" };

        }
        return new ResponseType<int>() { Data = 0, Succeeded = false, Message = $"El cliente {cliente.Identificacion} ya se encuentra registado.", StatusCode = "999" };
    }

    public async Task<ResponseType<ClienteType>> ActualizarCliente(int clienteId, ClienteType cliente)
    {

        var objCliente = await _clienteRepo.ObtenerCliente(clienteId);
        if(objCliente is not null)
        {
           
            var objPersona = new Persona()
            {
                Identificacion = cliente.Identificacion,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Telefono = cliente.Telefono
            };
            var objPersonaAct = await _personaRepo.ActualizarPersona(objCliente.PersonaId, objPersona);
            if (objPersonaAct is not null)
            {
                var objClAct = new Cliente()
                {
                    PersonaId = objCliente.PersonaId,
                    EstadoCivil = cliente.EstadoCivil,
                    FechaNacimiento = cliente.FechaNacimiento,
                    IdentificacionConyuge = cliente.IdentificacionConyuge,
                    NombresConyuge = cliente.NombresConyuge,
                    SujetoCredito = cliente.SujetoCredito
                };
                cliente.ClienteId = clienteId;
                await _clienteRepo.ActualizarCliente(clienteId, objClAct);
                return new ResponseType<ClienteType>() { Data = cliente, Succeeded = true, Message = $"Cliente {cliente.Identificacion} se actualizado exitosamente", StatusCode = "000" };
            }

            return new ResponseType<ClienteType>() { Data = null, Succeeded = false, Message = $"Hubo un error en la actualizacion del cliente {cliente.Identificacion}", StatusCode = "999" };

        }
        return new ResponseType<ClienteType>() { Data = null, Succeeded = false, Message = $"El cliente {cliente.Identificacion} no se encuentra registado.", StatusCode = "999" };
    }


    public async Task<ResponseType<bool>> EliminarCliente(int clienteId)
    {
        //si el cliente esta asignado a un patio no se puede eliminar
        //si al cliente se le ha generado una solicitud no se puede eliminar
        //antes de eliminar validar si no esta asociado
        if (!_cl_patio_Repo.ExisteClienteAsignado(clienteId).Result)
        {
            var result = await _clienteRepo.EliminarCliente(clienteId);
            return new ResponseType<bool>() { Data = result, Succeeded = true, Message = $"El clienteId: {clienteId} fue eliminado exitosamente", StatusCode = "000" };
        }
        return new ResponseType<bool>() { Data = false, Succeeded = false, Message = $"El clienteId: {clienteId} no puede ser eliminado porque se encuentra relacionado", StatusCode = "000" };

    }

}
