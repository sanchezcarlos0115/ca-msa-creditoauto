using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Domain.Comun.Wrappers;

namespace camsacreditoauto.Infrastructure.Services;

public class SolicitudInfraestructura : ISolicitudInfraestructura
{
    private readonly ISolicitudRepositorio _solRepo;
    private readonly IClientePatioInfraestructura _clPatioInfra;
    public SolicitudInfraestructura(ISolicitudRepositorio solRepo, IClientePatioInfraestructura clPatioInfra)
    {
        _solRepo = solRepo;
        _clPatioInfra = clPatioInfra;
    }

    public async Task<ResponseType<int>> GenerarSolicitudCreditoAsync(SolicitudType sol)
    {

        if (!_solRepo.ExisteSolicitudClienteMismoDiaActiva(sol.ClienteId).Result) 
        {
            if (!_solRepo.ValidarSolictudActivaVehiculo(sol.VehiculoId).Result)
            {

                var objReg = await _clPatioInfra.AgregarClientePatio(
                    new ClientePatioType() 
                    { 
                        ClienteId = sol.ClienteId,
                        PatioId = sol.PatioId,
                        FechaAsignacion = DateTime.Now
                    });

                if (objReg.Succeeded)
                {
                    var clpatio = await _clPatioInfra.ObtenerClientePatioPorIdCliente(sol.ClienteId);
                    if(!clpatio.Succeeded)
                        return new ResponseType<int>() { Data = -1, Succeeded = false, Message = "El cliente no posee asignacion de patio auto", StatusCode = "994" };
                    
                    var objSol = new Solicitud()
                    {
                        ClientePatioId = Convert.ToInt32(clpatio.Data?.ClientePatioId),
                        Cuotas = sol.Cuotas,
                        EjecutivoId = sol.EjecutivoId,
                        Entrada = sol.Entrada,
                        EstadoId = sol.EstadoId,
                        FechaElaboracion = sol.FechaElaboracion,
                        MesesPlazo = sol.MesesPlazo,
                        Observación = sol.Observación,
                        VehiculoId = sol.VehiculoId
                    };

                    var idSol = await _solRepo.GenerarSolicitudCredito(objSol);
                    if(idSol > 0)
                        return new ResponseType<int>() { Data = idSol, Succeeded = true, Message = "Solicitud ingresada exitosamente", StatusCode = "000" };
                    
                    return new ResponseType<int>() { Data = -1, Succeeded = false, Message = "Hubo un error en el ingreso de la solictud de credito", StatusCode = "995" };
                }

                return new ResponseType<int>() { Data = -1, Succeeded = false, Message = "Hubo un error en la asignacion del cliente a un patio de auto", StatusCode = "996" };
            }
            return new ResponseType<int>() { Data = -1, Succeeded = false, Message = "Ya existe una solicitud de credito en curso para este vehiculo", StatusCode = "997" };
        }

        return new ResponseType<int>() { Data = -1, Succeeded = false, Message = "El cliente ya posee una solicitud de credito activa para este dia", StatusCode = "998" };

    }

}
