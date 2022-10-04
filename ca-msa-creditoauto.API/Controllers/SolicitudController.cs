using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace camsacreditoauto.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class SolicitudController : ControllerBase
{
    private readonly ISolicitudInfraestructura _solInfraestructura;

    public SolicitudController(ISolicitudInfraestructura solInfraestructura)
    {
        _solInfraestructura = solInfraestructura;
    }

    [HttpPost]
    public async Task<ActionResult> GenerarSolicitudCreditoAsync([FromBody] SolicitudType obj)
    {
        if (obj == null)
            return BadRequest();

        var result = await _solInfraestructura.GenerarSolicitudCreditoAsync(obj);
        return Ok(result);
       
    }

}
