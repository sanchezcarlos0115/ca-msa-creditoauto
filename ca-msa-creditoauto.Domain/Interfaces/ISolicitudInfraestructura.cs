using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Entity.Dto;

namespace camsacreditoauto.Domain.Interfaces;

public interface ISolicitudInfraestructura
{
    
    Task<ResponseType<int>> GenerarSolicitudCreditoAsync(SolicitudType sol);
}
