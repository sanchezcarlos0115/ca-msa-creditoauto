using camsacreditoauto.API.Controllers;
using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace camsacreditoauto.Test.System.Controllers;

public class ClientePatioControllerTest
{
    private readonly Mock<IClientePatioInfraestructura> mock_infraestruStub = new();

    #region Agregar

    [Fact]
    public async Task AgregarClientePatioAsync_ConNuevoRegistro_RetornaBadRequest()
    {
        // Arrange
        var controller = new ClientePatiosController(mock_infraestruStub.Object);
        // Act
        var objresult = await controller.AgregarClientePatioAsync(null!);
        // Assert
        objresult.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task AgregarClientePatioAsync_ConNuevoRegistro_Exitosa()
    {
        // Arrange
        var nuevoClientePatio = ClientePatioMockData.NuevoClientePatio();
        var controller = new ClientePatiosController(mock_infraestruStub.Object);

        mock_infraestruStub.Setup(repo => repo.AgregarClientePatio(It.IsAny<ClientePatioType>()))
            .ReturnsAsync(new ResponseType<int>(){ Succeeded = true,StatusCode="000", Data = 1});

        // Act
        var objresult = (OkObjectResult) await controller.AgregarClientePatioAsync(nuevoClientePatio);
        var objAction = objresult.Value as ResponseType<int>;
       
        // Assert
        objAction?.Data.Should().Be(1);
        objAction?.Succeeded.Should().Be(true);
        objAction?.StatusCode.Should().Be("000");
    }

    #endregion

    #region Actualizar

    [Fact]
    public async Task ActualizarClientePatioAsync_ConRegistroNoExistente_RetornaNoFound()
    {
        // Arrange
        ClientePatioType existingItem = ClientePatioMockData.NuevoClientePatio();
        mock_infraestruStub.Setup(repo => repo.ObtenerClientePatio(It.IsAny<int>()))
            .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = false, StatusCode = "999", Data = null });

        var Id = existingItem.ClientePatioId;
        var controller = new ClientePatiosController(mock_infraestruStub.Object);
        // Act
        var result = await controller.ActualizarClientePatioAsync(Id, existingItem);
        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task ActualizarClientePatioAsync_ConRegistroExistente_RetornaExitosa()
    {
        // Arrange
        ClientePatioType existingItem = ClientePatioMockData.NuevoClientePatio();
        mock_infraestruStub.Setup(repo => repo.ObtenerClientePatio(It.IsAny<int>()))
            .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = true, StatusCode = "000", Data = null });

        var Id = existingItem.ClientePatioId;
        var itemToUpdate = new ClientePatioType
        {
            ClientePatioId = Id,
            ClienteId = 1,
            PatioId = 2,
            FechaAsignacion = DateTime.Now
        };

        mock_infraestruStub.Setup(repo => repo.ActualizarClientePatio(It.IsAny<int>(), It.IsAny<ClientePatioType>()))
           .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = true, StatusCode = "000", Data = itemToUpdate });

        var controller = new ClientePatiosController(mock_infraestruStub.Object);
        // Act
        var result = (OkObjectResult) await controller.ActualizarClientePatioAsync(Id, itemToUpdate);
        var objAction = result.Value as ResponseType<ClientePatioType>;
        // Assert
        (objAction?.Data?.ClienteId ?? 0).Should().Be(1);
        (objAction?.Succeeded ?? false).Should().Be(true);
        (objAction?.StatusCode ?? "999").Should().Be("000");

    }

    #endregion

    #region Eliminar

    [Fact]
    public async Task EliminarClientePatioAsync_ConRegistroNoExistente_RetornaNoFound()
    {
        // Arrange
       
        mock_infraestruStub.Setup(repo => repo.ObtenerClientePatio(It.IsAny<int>()))
            .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = false, StatusCode = "999", Data = null });

        var Id = 1;
        var controller = new ClientePatiosController(mock_infraestruStub.Object);
        // Act
        var result = await controller.EliminarClientePatioAsync(Id);
        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task EliminarClientePatioAsync_ConRegistroExistente_RetornaExitosa()
    {
        // Arrange
        mock_infraestruStub.Setup(repo => repo.ObtenerClientePatio(It.IsAny<int>()))
            .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = true, StatusCode = "000", Data = null });
        var Id = 1;
        var controller = new ClientePatiosController(mock_infraestruStub.Object);

        mock_infraestruStub.Setup(repo => repo.EliminarClientePatio(It.IsAny<int>()))
          .ReturnsAsync(new ResponseType<bool>() { Succeeded = true, StatusCode = "000", Data = true });
        // Act
        var result = (OkObjectResult) await controller.EliminarClientePatioAsync(Id);
        var objAction = result.Value as ResponseType<bool>;
        // Assert
        (objAction?.Data).Should().Be(true);
        (objAction?.Succeeded ?? false).Should().Be(true);
        (objAction?.StatusCode ?? "999").Should().Be("000");
    }

    #endregion
}
