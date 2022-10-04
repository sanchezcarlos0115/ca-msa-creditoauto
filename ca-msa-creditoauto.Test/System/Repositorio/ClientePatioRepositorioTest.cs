using camsacreditoauto.Domain.Comun.Wrappers;
using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Infrastructure.Services;
using camsacreditoauto.Repository;
using camsacreditoauto.Repository.Context;
using camsacreditoauto.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace camsacreditoauto.Test.System.Repositorio;

public class ClientePatioRepositorioTest
{

    protected readonly CreditoAutoContext _context;
    public ClientePatioRepositorioTest()
    {
        var options = new DbContextOptionsBuilder<CreditoAutoContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new CreditoAutoContext(options);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task SaveAsync_NuevoClientePatio()
    {
        // Arrange
        var newClientePatio = ClientePatioMockData.NuevoClientePatioRepo();
        var objRepo = new ClientePatioRepositorio(_context);
        // Act
        await objRepo.AgregarClientePatio(newClientePatio);
        //Assert
        _context.ClientePatios.Count().Should().Be(1);
    }


}
