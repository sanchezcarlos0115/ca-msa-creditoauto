using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Domain.Comun.Wrappers;

namespace camsacreditoauto.Infrastructure.Services;

public class ClientePatioInfraestructura : IClientePatioInfraestructura
{
    private readonly IClientePatioRepositorio _clienteRepo;
   
    public ClientePatioInfraestructura(IClientePatioRepositorio clienteRepo)
    {
        _clienteRepo = clienteRepo;

    }

    public async Task<ResponseType<IEnumerable<ClientePatioType>>> ObtenerClientePatios()
    {
        var objClientes = await _clienteRepo.ObtenerClientePatios();
        if (objClientes is not null && objClientes.Any())
        {
            var result = (from c in objClientes
                          select new ClientePatioType()
                          {
                              ClientePatioId = c.ClientePatioId,
                              ClienteId = c.ClienteId,
                              PatioId = c.PatioId,
                              FechaAsignacion = c.FechaAsignacion
                          }).AsEnumerable();

            return new ResponseType<IEnumerable<ClientePatioType>>() { Data = result, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<IEnumerable<ClientePatioType>>() { Data = null, Succeeded = false, Message = "No existe información para mostrar", StatusCode = "999" };
    }

    public async Task<ResponseType<ClientePatioType>> ObtenerClientePatio(int id)
    {
        var cliente =  await _clienteRepo.ObtenerClientePatio(id);
        if (cliente is not null)
        {
            var objType = new ClientePatioType() 
            {
                ClientePatioId = cliente.ClientePatioId,
                ClienteId = cliente.ClienteId,
                PatioId = cliente.PatioId,
                FechaAsignacion = cliente.FechaAsignacion
            };
            return new ResponseType<ClientePatioType>() { Data = objType, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<ClientePatioType>() { Data = null, Succeeded = false, Message = $"No existe información con el Id: {id} de asignación", StatusCode = "999" };
    }

    public async Task<ResponseType<int>> AgregarClientePatio(ClientePatioType cliente)
    {

        if (!_clienteRepo.ExisteClienteAsignado(cliente.ClienteId).Result) 
        {
            var objType = new ClientePatio()
            {
                ClienteId = cliente.ClienteId,
                PatioId = cliente.PatioId,
                FechaAsignacion = DateTime.Now
            };
            var idClientePatio = await _clienteRepo.AgregarClientePatio(objType);
            if(idClientePatio >0) 
                return new ResponseType<int>() { Data = idClientePatio, Succeeded = true, Message = "Asignación de cliente a patio de autos exitosamente", StatusCode = "000" };

            return new ResponseType<int>() { Data = idClientePatio, Succeeded = false, Message = "Hubo un error al intentar asignar el cliente a un patio de autos", StatusCode = "999" };
            
        }
        return new ResponseType<int>() { Data = 0, Succeeded = false, Message = $"El cliente: {cliente.ClienteId} ya se encuentra asignado a un patio de auto", StatusCode = "999" };
    }

    public async Task<ResponseType<ClientePatioType>> ActualizarClientePatio(int id, ClientePatioType cliente)
    {

        var objCliente = await _clienteRepo.ObtenerClientePatio(id);
        if(objCliente is not null)
        {

            var objType = new ClientePatio()
            {
                ClienteId = cliente.ClienteId,
                PatioId = cliente.PatioId,
                FechaAsignacion = DateTime.Now
            };
            cliente.ClientePatioId = id;
            await _clienteRepo.ActualizarClientePatio(id, objType);
            return new ResponseType<ClientePatioType>() { Data = cliente, Succeeded = true, Message = $"Se actualizo la asignacion del cliente con Id: {cliente.ClienteId} exitosamente", StatusCode = "000" };
        }
        return new ResponseType<ClientePatioType>() { Data = null, Succeeded = false, Message = $"El cliente {cliente.ClienteId} no se encuentra asignado o no existe.", StatusCode = "999" };
    }


    public async Task<ResponseType<bool>> EliminarClientePatio(int id)
    {
       
        //si al cliente se le ha generado una solicitud no se puede eliminar
        var result = await _clienteRepo.EliminarClientePatio(id);

        return new ResponseType<bool>() { Data = result, Succeeded = true, Message = $"La asignación del clientePatioId: {id} fue eliminado exitosamente", StatusCode = "000"};
     
    }

}
