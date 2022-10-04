using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Domain.Comun.Wrappers;
using System.Numerics;

namespace camsacreditoauto.Infrastructure.Services;

public class VehiculoInfraestructura : IVehiculoInfraestructura
{
    private readonly IVehiculoRepositorio _vehiculoRepo;
    private readonly ISolicitudRepositorio _solRepo;

    public VehiculoInfraestructura(IVehiculoRepositorio vehiculoRepo, ISolicitudRepositorio solRepo)
    {
        _vehiculoRepo = vehiculoRepo;
        _solRepo = solRepo;
    }

    public async Task<ResponseType<IEnumerable<VehiculoType>>> ObtenerVehiculos()
    {
        var objVehiculos = await _vehiculoRepo.ObtenerVehiculos();

        if (objVehiculos is not null && objVehiculos.Any())
        {
            var result = (from c in objVehiculos
                          select new VehiculoType()
                          {
                                VehiculoId = c.VehiculoId,
                                Avaluo = c.Avaluo,
                                Cilindraje = c.Cilindraje,
                                Modelo = c.Modelo,
                                MarcaId = c.MarcaId,
                                NroChasis = c.NroChasis,
                                PatioId = c.PatioId,
                                Placa = c.Placa,
                                Tipo = c.Tipo
                          }).AsEnumerable();

            return new ResponseType<IEnumerable<VehiculoType>>() { Data = result, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<IEnumerable<VehiculoType>>() { Data = null, Succeeded = false, Message = "No existe información para mostrar", StatusCode = "999" };
    }

    public async Task<ResponseType<IEnumerable<VehiculoType>>> ObtenerVehiculosFiltrado(string marca,string modelo)
    {
        bool todos = false;
        if (string.IsNullOrEmpty(marca) && string.IsNullOrEmpty(modelo)) { todos = true; }

        var objVehiculos = await _vehiculoRepo.ObtenerVehiculosFiltrado(marca, modelo,todos);

        if (objVehiculos is not null && objVehiculos.Any())
        {
            var result = (from c in objVehiculos
                          select new VehiculoType()
                          {
                              VehiculoId = c.VehiculoId,
                              Avaluo = c.Avaluo,
                              Cilindraje = c.Cilindraje,
                              Modelo = c.Modelo,
                              MarcaId = c.MarcaId,
                              NroChasis = c.NroChasis,
                              PatioId = c.PatioId,
                              Placa = c.Placa,
                              Tipo = c.Tipo
                          }).AsEnumerable();

            return new ResponseType<IEnumerable<VehiculoType>>() { Data = result, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<IEnumerable<VehiculoType>>() { Data = null, Succeeded = false, Message = "No existe información para mostrar", StatusCode = "999" };
    }

    public async Task<ResponseType<VehiculoType>> ObtenerVehiculo(int vehiculoId)
    {
        var vehiculo = await _vehiculoRepo.ObtenerVehiculo(vehiculoId);
        if (vehiculo is not null)
        {
            var objClienteType = new VehiculoType()
            {
                VehiculoId = vehiculoId,
                Avaluo = vehiculo.Avaluo,
                Cilindraje = vehiculo.Cilindraje,
                Modelo = vehiculo.Modelo,
                MarcaId = vehiculo.MarcaId,
                NroChasis = vehiculo.NroChasis,
                PatioId = vehiculo.PatioId,
                Placa = vehiculo.Placa,
                Tipo = vehiculo.Tipo
            };
            return new ResponseType<VehiculoType>() { Data = objClienteType, Succeeded = true, Message = "Consulta exitosa", StatusCode = "000" };
        }
        return new ResponseType<VehiculoType>() { Data = null, Succeeded = false, Message = $"No existe información del vehiculo {vehiculoId}", StatusCode = "999" };
    }

    public async Task<ResponseType<int>> AgregarVehiculo(VehiculoType vehiculo)
    {

        if (!(_vehiculoRepo.ValidarExistenciaVehiculoMismaPlaca(vehiculo.Placa).Result))
        {
            var objNewVehiculo = new Vehiculo()
            {
                Avaluo = vehiculo.Avaluo,
                Cilindraje = vehiculo.Cilindraje,
                Modelo = vehiculo.Modelo,
                MarcaId = vehiculo.MarcaId,
                NroChasis = vehiculo.NroChasis,
                PatioId = vehiculo.PatioId,
                Placa = vehiculo.Placa,
                Tipo = vehiculo.Tipo
            };

            var idNewVehiculo = await _vehiculoRepo.AgregarVehiculo(objNewVehiculo);
            return new ResponseType<int>() { Data = idNewVehiculo, Succeeded = true, Message = "Vehiculo registrado exitosamente", StatusCode = "000" };
        }
        return new ResponseType<int>() { Data = -1, Succeeded = false, Message = $"Ya existe registrado un vehiculo con la misma placa {vehiculo.Placa}.", StatusCode = "999" };
    }

    public async Task<ResponseType<VehiculoType>> ActualizarVehiculo(int vehiculoId, VehiculoType vehiculo)
    {

        var objVehiculo = await _vehiculoRepo.ObtenerVehiculo(vehiculoId);
        if (objVehiculo is not null)
        {
            var objAct = new Vehiculo()
            {
                Avaluo = vehiculo.Avaluo,
                Cilindraje = vehiculo.Cilindraje,
                Modelo = vehiculo.Modelo,
                MarcaId = vehiculo.MarcaId,
                NroChasis = vehiculo.NroChasis,
                PatioId = vehiculo.PatioId,
                Placa = vehiculo.Placa,
                Tipo = vehiculo.Tipo
            };

            vehiculo.VehiculoId = vehiculoId;
            await _vehiculoRepo.ActualizarVehiculo(vehiculoId, objAct);
            return new ResponseType<VehiculoType>() { Data = vehiculo, Succeeded = true, Message = $"El vehiculo con Id: {vehiculoId} se actualizado exitosamente", StatusCode = "000" };

        }
        return new ResponseType<VehiculoType>() { Data = null, Succeeded = false, Message = $"El vehiculo con Id: {vehiculoId} no se encuentra registado.", StatusCode = "999" };
    }


    public async Task<ResponseType<bool>> EliminarVehiculo(int vehiculoId)
    {

        if (!_solRepo.ValidarSolictudActivaVehiculo(vehiculoId).Result)
        {
            var result = await _vehiculoRepo.EliminarVehiculo(vehiculoId);
            return new ResponseType<bool>() { Data = result, Succeeded = true, Message = $"El vehiculoId: {vehiculoId} fue eliminado exitosamente", StatusCode = "000" };
        }
        return new ResponseType<bool>() { Data = false, Succeeded = false, Message = $"El vehiculoId: {vehiculoId} no puede ser eliminado porque se encuentra relacionado", StatusCode = "999" };

    }

}
