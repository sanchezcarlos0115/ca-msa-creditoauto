using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace camsacreditoauto.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class AsignarClienteController : ControllerBase
{
   
    private readonly IAsignarCliente _repository;
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    public AsignarClienteController(IAsignarCliente repository)
    {
        _repository = repository;
    }


   /// <summary>
   /// 
   /// </summary>
   /// <returns></returns>
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _repository.ObtenerTodosAsync();
        if (!result.Any())
        {
            return NoContent();
        }
        return Ok(result);
    }


    //[HttpGet("{id:int}", Name = "GetAsignacionClientePatio")]
    //[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    ////[SwaggerOperation("Get a Customer", "Specific customer from database")]
    ////[SwaggerResponse((int)HttpStatusCode.OK, "Customer by id", typeof(Models.Dto.Customer))]
    ////[SwaggerResponse((int)HttpStatusCode.NoContent, "No customer")]
    //public ActionResult<Models.Dto.Customer> GetCustomer([FromRoute] int id)
    //{
    //    if (id <= 0) return BadRequest();

    //    var result = _repository.Read(id);
    //    if (result == null) return NotFound();
    //    return Ok(_mapper.Map<Models.Dto.Customer>(result));
    //}


    //[HttpPost]
    //[Route("AsignarClientePatio")]
    //[ProducesResponseType(typeof(ResponseType<string>), StatusCodes.Status201Created)]
    //public async Task<IActionResult> SaveAsync([FromBody] ClientePatio asignar)
    //{

    //    //await _repository..SaveAsync(newTodo);
    //    return Ok();
    //}

    //[HttpPost]
    //[ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public ActionResult<Models.Dto.Customer> Post([FromBody] Models.Dto.Customer customer)
    //{
    //    if (customer == null) return BadRequest();

    //    _repository.Create(_mapper.Map<Customer>(customer));

    //    return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    //}


}
