using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Infrastructure.Services;
using camsacreditoauto.Test.MockData;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace camsacreditoauto.Test.System.Insfraestructura;

public class ClientePatioInfraestructuraTest
{
    private  Mock<IClientePatioInfraestructura> mock_infraestruStub = new();
    private  Mock<IClientePatioRepositorio> mock_repositorioStub = new();
    
    [Fact]
    public async Task AgregarClientePatioAsync_ConNuevoRegistro_RortornaExitosa()
    {
        mock_repositorioStub.Setup(m=> m.ExisteClienteAsignado(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_repositorioStub.Setup(z => z.AgregarClientePatio(It.IsAny<ClientePatio>()))
            .ReturnsAsync(5);

        mock_infraestruStub.Setup(repo => repo.AgregarClientePatio(It.IsAny<ClientePatioType>()))
           .ReturnsAsync(new ResponseType<int>() { Succeeded = true, StatusCode = "000", Data = 1 });

        IClientePatioInfraestructura objInfr = new ClientePatioInfraestructura(mock_repositorioStub.Object);
        var objResult = await objInfr.AgregarClientePatio(ClientePatioMockData.NuevoClientePatio());
       
        objResult?.Data.Should().Be(5);
        objResult?.Succeeded.Should().Be(true);
        objResult?.StatusCode.Should().Be("000");
    }

}
