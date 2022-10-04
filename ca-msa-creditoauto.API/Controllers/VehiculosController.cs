using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace camsacreditoauto.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoInfraestructura _vehInfraestructura;

    public VehiculosController(IVehiculoInfraestructura vehInfraestructura)
    {
        _vehInfraestructura = vehInfraestructura;
    }

    [HttpGet("GetVehiculosAsync")]
    public async Task<IActionResult> GetVehiculosAsync()
    {
       return Ok(await _vehInfraestructura.ObtenerVehiculos());     
    }

    [HttpGet("GetVehiculosFiltradoAsync")]
    public async Task<IActionResult> GetVehiculosFiltradoAsync(string? marca,string? modelo)
    {
        return Ok(await _vehInfraestructura.ObtenerVehiculosFiltrado(marca??string.Empty, modelo ?? string.Empty));
    }

    [HttpGet]
    public async Task<IActionResult> GetVehiculoAsync(int id)
    {
        var result = await _vehInfraestructura.ObtenerVehiculo(id);
        if (!result.Succeeded) return NotFound();
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AgregarVehiculoAsync([FromBody] VehiculoType vehiculo)
    {
        if (vehiculo == null)
            return BadRequest();

        return Ok(await _vehInfraestructura.AgregarVehiculo(vehiculo));
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarVehiculoAsync(int id, [FromBody] VehiculoType vehiculo)
    {
        var vehiculoAct = await _vehInfraestructura.ObtenerVehiculo(id);
        if (!vehiculoAct.Succeeded)
            return NotFound();

        return Ok(await _vehInfraestructura.ActualizarVehiculo(id, vehiculo));
    }


    [HttpDelete]
    public async Task<IActionResult> EliminarVehiculoAsync(int id)
    {
        var vehEliminar = await _vehInfraestructura.ObtenerVehiculo(id);
        if (!vehEliminar.Succeeded)
        {
            return NotFound();
        }
        return Ok(await _vehInfraestructura.EliminarVehiculo(id));
    }
}
