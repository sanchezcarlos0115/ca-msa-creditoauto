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

public class SolicitudInfraestructuraTest
{
    private  Mock<ISolicitudInfraestructura> mock_infraestruStub = new();
    private  Mock<ISolicitudRepositorio> mock_repositorioStub = new();
    private Mock<IClientePatioInfraestructura> mock_patio_repoStub = new();

    [Fact]
    public async Task ValidaCliente_SujetoCredito()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
            .ReturnsAsync(false);

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Data.Should().Be(-1);
        objResult?.Succeeded.Should().Be(false);
        objResult?.StatusCode.Should().Be("993");

    }


    [Fact]
    public async Task ValidarExistenciaSolicitud_ClienteMismoDia()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
           .ReturnsAsync(true);

        mock_repositorioStub.Setup(m => m.ExisteSolicitudClienteMismoDiaActiva(It.IsAny<int>()))
            .ReturnsAsync(true);

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Data.Should().Be(-1);
        objResult?.Succeeded.Should().Be(false);
        objResult?.StatusCode.Should().Be("998");

    }

    [Fact]
    public async Task ValidarSolictudActivaVehiculo()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
          .ReturnsAsync(true);

        mock_repositorioStub.Setup(m => m.ExisteSolicitudClienteMismoDiaActiva(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_repositorioStub.Setup(z => z.ValidarSolictudActivaVehiculo(It.IsAny<int>()))
            .ReturnsAsync(true);

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Data.Should().Be(-1);
        objResult?.Succeeded.Should().Be(false);
        objResult?.StatusCode.Should().Be("997");

    }

    [Fact]
    public async Task AsignacionClientePatio_Existente()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
          .ReturnsAsync(true);

        mock_repositorioStub.Setup(m => m.ExisteSolicitudClienteMismoDiaActiva(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_repositorioStub.Setup(z => z.ValidarSolictudActivaVehiculo(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_patio_repoStub.Setup(repo => repo.AgregarClientePatio(It.IsAny<ClientePatioType>()))
           .ReturnsAsync(new ResponseType<int>() { Succeeded = false, StatusCode = "996", Data = -1 });

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Data.Should().Be(-1);
        objResult?.Succeeded.Should().Be(false);
        objResult?.StatusCode.Should().Be("996");

    }

    [Fact]
    public async Task ObtenerAsignacionClientePatio_Fallida()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
          .ReturnsAsync(true);

        mock_repositorioStub.Setup(m => m.ExisteSolicitudClienteMismoDiaActiva(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_repositorioStub.Setup(z => z.ValidarSolictudActivaVehiculo(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_patio_repoStub.Setup(repo => repo.AgregarClientePatio(It.IsAny<ClientePatioType>()))
           .ReturnsAsync(new ResponseType<int>() { Succeeded = true, StatusCode = "000", Data = 4 });

        mock_patio_repoStub.Setup(repo => repo.ObtenerClientePatioPorIdCliente(It.IsAny<int>()))
          .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = false, StatusCode = "994", Data = null });

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Succeeded.Should().Be(false);
        objResult?.StatusCode.Should().Be("994");

    }

    [Fact]
    public async Task GenerarSolicitudCredito_Fallida()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
          .ReturnsAsync(true);

        mock_repositorioStub.Setup(m => m.ExisteSolicitudClienteMismoDiaActiva(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_repositorioStub.Setup(z => z.ValidarSolictudActivaVehiculo(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_patio_repoStub.Setup(repo => repo.AgregarClientePatio(It.IsAny<ClientePatioType>()))
           .ReturnsAsync(new ResponseType<int>() { Succeeded = true, StatusCode = "000", Data = 4 });

        mock_patio_repoStub.Setup(repo => repo.ObtenerClientePatioPorIdCliente(It.IsAny<int>()))
          .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = true, StatusCode = "000", Data = null });

        mock_repositorioStub.Setup(repo => repo.GenerarSolicitudCredito(It.IsAny<Solicitud>()))
         .ReturnsAsync(-1);

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Data.Should().Be(-1);
        objResult?.Succeeded.Should().Be(false);
        objResult?.StatusCode.Should().Be("995");

    }

    [Fact]
    public async Task GenerarSolicitudCredito_Exitosa()
    {
        mock_repositorioStub.Setup(m => m.ValidarClienteSujetoCredito(It.IsAny<int>()))
          .ReturnsAsync(true);

        mock_repositorioStub.Setup(m => m.ExisteSolicitudClienteMismoDiaActiva(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_repositorioStub.Setup(z => z.ValidarSolictudActivaVehiculo(It.IsAny<int>()))
            .ReturnsAsync(false);

        mock_patio_repoStub.Setup(repo => repo.AgregarClientePatio(It.IsAny<ClientePatioType>()))
           .ReturnsAsync(new ResponseType<int>() { Succeeded = true, StatusCode = "000", Data = 4 });

        mock_patio_repoStub.Setup(repo => repo.ObtenerClientePatioPorIdCliente(It.IsAny<int>()))
          .ReturnsAsync(new ResponseType<ClientePatioType>() { Succeeded = true, StatusCode = "000", Data = null });

        mock_repositorioStub.Setup(repo => repo.GenerarSolicitudCredito(It.IsAny<Solicitud>()))
         .ReturnsAsync(4);

        ISolicitudInfraestructura objInfr = new SolicitudInfraestructura(mock_repositorioStub.Object, mock_patio_repoStub.Object);
        var objResult = await objInfr.GenerarSolicitudCreditoAsync(SolicitudMockData.NuevaSolicitud());

        objResult?.Data.Should().Be(4);
        objResult?.Succeeded.Should().Be(true);
        objResult?.StatusCode.Should().Be("000");

    }
}
