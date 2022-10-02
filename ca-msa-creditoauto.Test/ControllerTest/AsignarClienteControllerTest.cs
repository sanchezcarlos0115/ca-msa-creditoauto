using camsacreditoauto.API.Controllers;
using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Test.ControllerTest;

public class AsignarClienteControllerTest
{
    private  AsignarClienteController controller;
    Mock<IAsignarClienteInfraestructure> mockAsignarClienteInfra;

    [OneTimeSetUp]
    public void Setup()
    {
        mockAsignarClienteInfra = new Mock<IAsignarClienteInfraestructure>();
    }

    //[Test]
    //public void AsignarClienteTest()
    //{
    //    //mockAsignarClienteInfra = new Mock<IAsignarClienteInfraestructure>();
    //    ///preparar objeto
    //    controller = new AsignarClienteController(mockAsignarClienteInfra.Object);

    //    ///ejecutar
    //    var cliePatio = new ClientePatioDto();
    //    var action = (OkObjectResult)controller.AsignarClientePatio(cliePatio);
    //    // 
    //    var ok200 = action.StatusCode;
    //    ///obtener resultado
    //    Assert.AreEqual(200, ok200);
    //}

    [Test]
    public  void RetiornarOkString()
    {
        ///preparar objeto
        mockAsignarClienteInfra.Setup(m => m.AgregarAsync(It.IsAny<ClientePatio>()))
                            .ReturnsAsync(201);

        controller = new AsignarClienteController(mockAsignarClienteInfra.Object);

        //var action = controller.AsignarClientePatio(new ClientePatioDto());
        var Action = (OkObjectResult)controller.AsignarClientePatio(new ClientePatioDto());
        var result = Convert.ToInt32(Action.Value);

        Assert.AreEqual(201, result);
        //Resultado
        //Assert.AreEqual(0, result?.Count);
    }


}

