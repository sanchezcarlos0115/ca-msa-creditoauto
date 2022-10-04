using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace camsacreditoauto.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class PatioAutosController : ControllerBase
{
    private readonly IPatioAutoInfraestructura _infraestructura;

    public PatioAutosController(IPatioAutoInfraestructura infraestructura)
    {
        _infraestructura = infraestructura;
    }

    [HttpGet("GetPatioAutosAsync")]
    public async Task<IActionResult> GetPatioAutosAsync()
    {
       return Ok(await _infraestructura.ObtenerPatioAutos());
    }

    [HttpGet]
    public async Task<IActionResult> GetPatioAutoAsync(int id)
    {
        var result = await _infraestructura.ObtenerPatioAuto(id);
        if (!result.Succeeded) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AgregarPatioAutoAsync([FromBody] PatioAutoType patio)
    {
        if (patio == null)
            return BadRequest();
        return Ok(await _infraestructura.AgregarPatioAuto(patio));
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarPatioAutoAsync(int id,[FromBody] PatioAutoType patio)
    {
       
        var patioAct = await _infraestructura.ObtenerPatioAuto(id);
        if (!patioAct.Succeeded)
            return NotFound();

        return Ok(await _infraestructura.ActualizarPatioAuto(id, patio));
       
    }


    [HttpDelete]
    public async Task<IActionResult> EliminarPatioAutoAsync(int id)
    {
       
        var objEliminar = await _infraestructura.ObtenerPatioAuto(id);
        if (!objEliminar.Succeeded)
        {
            return NotFound();
        }
        return Ok(await _infraestructura.EliminarPatioAuto(id));
       
    }

}
