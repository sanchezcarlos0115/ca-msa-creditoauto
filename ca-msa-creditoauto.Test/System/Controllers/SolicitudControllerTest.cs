using camsacreditoauto.API.Controllers;
using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace camsacreditoauto.Test.System.Controllers;

public class SolicitudControllerTest
{
    private readonly Mock<ISolicitudInfraestructura> mock_infraestruStub = new();

    [Fact]
    public async Task GenerarSolicitudAsync_ConNuevoRegistro_RetornaBadRequest()
    {
        // Arrange
        var controller = new SolicitudController(mock_infraestruStub.Object);
        // Act
        var objresult = await controller.GenerarSolicitudCreditoAsync(null!);
        // Assert
        objresult.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task GenerarSolicitudAsync_ConNuevoRegistro_RetornaExitosa()
    {
        // Arrange
        var nuevaSol = SolicitudMockData.NuevaSolicitud();
        var controller = new SolicitudController(mock_infraestruStub.Object);

        mock_infraestruStub.Setup(repo => repo.GenerarSolicitudCreditoAsync(It.IsAny<SolicitudType>()))
            .ReturnsAsync(new ResponseType<int>() { Succeeded = true, StatusCode = "000", Data = 5 });

        // Act
        var objresult = (OkObjectResult)await controller.GenerarSolicitudCreditoAsync(nuevaSol);
        var objResponse = objresult.Value as ResponseType<int>;

        // Assert
        objResponse?.Data.Should().Be(5);
        objResponse?.Succeeded.Should().Be(true);
        objResponse?.StatusCode.Should().Be("000");
    }
}
