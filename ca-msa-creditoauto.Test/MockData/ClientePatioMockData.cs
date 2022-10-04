using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using System;
using System.Collections.Generic;

namespace camsacreditoauto.Test.MockData;

public class ClientePatioMockData
{

    public static List<ClientePatio> GetClientePatiosRepo()
    {
        return new List<ClientePatio>{
            new ClientePatio
            {
                ClientePatioId = 1,
                ClienteId = 1,
                PatioId = 1,
                FechaAsignacion = DateTime.Now
            },
            new ClientePatio
            {
                ClientePatioId = 2,
                ClienteId = 2,
                PatioId = 2,
                FechaAsignacion = DateTime.Now
            },
            new ClientePatio
            {
                ClientePatioId = 3,
                ClienteId = 3,
                PatioId = 2,
                FechaAsignacion = DateTime.Now
            }
         };
    }

    public static ClientePatioType NuevoClientePatio()
    {
        return new ClientePatioType
        {
            ClientePatioId = 1,
            ClienteId = 1,
            PatioId = 1,
            FechaAsignacion = DateTime.Now
        };
    }

    public static ClientePatio NuevoClientePatioRepo()
    {
        return new ClientePatio
        {
            ClientePatioId = 1,
            ClienteId = 1,
            PatioId = 1,
            FechaAsignacion = DateTime.Now
        };
    }

}
