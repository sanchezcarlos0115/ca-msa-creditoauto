using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Domain.Comun.Wrappers;

namespace camsacreditoauto.Infrastructure.Services;

public class PatioAutoInfraestructura : IPatioAutoInfraestructura
{
    private readonly IPatioAutoRepositorio _patioRepo;
    private readonly IClientePatioRepositorio _cl_patio_Repo;
    public PatioAutoInfraestructura(IPatioAutoRepositorio patioRepo, IClientePatioRepositorio cl_patio_Repo)
    {
        _patioRepo = patioRepo;
        _cl_patio_Repo = cl_patio_Repo;
    }

    public async Task<ResponseType<IEnumerable<PatioAutoType>>> ObtenerPatioAutos()
    {
        var objPatios = await _patioRepo.ObtenerPatioAutos();

        if (objPatios is not null && objPatios.Any())
        {
            var result = (from c in objPatios
                          select new PatioAutoType()
                          {
                            PatioId = c.PatioId,
                            Direccion = c.Direccion,
                            Nombre = c.Nombre,
                            NumeroPtoVenta = c.NumeroPtoVenta,
                            Telefono = c.Telefono  
                          }).AsEnumerable();

            return new ResponseType<IEnumerable<PatioAutoType>>() { Data = result, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<IEnumerable<PatioAutoType>>() { Data = null, Succeeded = false, Message = "No existe información para mostrar", StatusCode = "999" };
    }

    public async Task<ResponseType<PatioAutoType>> ObtenerPatioAuto(int patioId)
    {
        var obj =  await _patioRepo.ObtenerPatioAuto(patioId);
        if (obj is not null)
        {
            var objPatioType = new PatioAutoType() 
            {
                PatioId = obj.PatioId,
                Direccion = obj.Direccion,
                Nombre = obj.Nombre,
                NumeroPtoVenta = obj.NumeroPtoVenta,
                Telefono = obj.Telefono
            };
            return new ResponseType<PatioAutoType>() { Data = objPatioType, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<PatioAutoType>() { Data = null, Succeeded = false, Message = $"No existe información del Patio Auto {patioId}", StatusCode = "999" };
    }

    public async Task<ResponseType<int>> AgregarPatioAuto(PatioAutoType patio)
    {

        if (!_patioRepo.ExistePatioAuto(patio.Nombre).Result) 
        {
            var objNew = new PatioAuto()
            {
                Direccion = patio.Direccion,
                Nombre = patio.Nombre,
                NumeroPtoVenta = patio.NumeroPtoVenta,
                Telefono = patio.Telefono
            };
            var idNewPatio = await _patioRepo.AgregarPatioAuto(objNew);
            return new ResponseType<int>() { Data = idNewPatio, Succeeded = true, Message = "Patio Auto registrado exitosamente", StatusCode = "000" };
                  
        }
        return new ResponseType<int>() { Data = 0, Succeeded = false, Message = $"El patio auto: {patio.Nombre} ya se encuentra registado.", StatusCode = "999" };
    }

    public async Task<ResponseType<PatioAutoType>> ActualizarPatioAuto(int patioId, PatioAutoType patio)
    {
        var objPatio = await _patioRepo.ObtenerPatioAuto(patioId);
        if(objPatio is not null)
        {
            var objNew = new PatioAuto()
            {
                Direccion = patio.Direccion,
                Nombre = patio.Nombre,
                NumeroPtoVenta = patio.NumeroPtoVenta,
                Telefono = patio.Telefono
            };
            patio.PatioId = objPatio.PatioId;
            await _patioRepo.ActualizarPatioAuto(patioId, objNew);
            return new ResponseType<PatioAutoType>() { Data = patio, Succeeded = true, Message = $"El patio auto {patio.Nombre} se actualizo exitosamente", StatusCode = "000" };
            
        }
        return new ResponseType<PatioAutoType>() { Data = null, Succeeded = false, Message = $"El patio auto {patio.Nombre} no se encuentra registado.", StatusCode = "999" };
    }


    public async Task<ResponseType<bool>> EliminarPatioAuto(int patioId)
    {
        
        if (!_cl_patio_Repo.ExistePatioAutoAsignado(patioId).Result)
        {
            var result = await _patioRepo.EliminarPatioAuto(patioId);
            return new ResponseType<bool>() { Data = result, Succeeded = true, Message = $"El patioId: {patioId} fue eliminado exitosamente", StatusCode = "000" };
        }
        return new ResponseType<bool>() { Data = false, Succeeded = false, Message = $"El patioId: {patioId} no puede ser eliminado porque se encuentra relacionado.", StatusCode = "000" };

    }

}
