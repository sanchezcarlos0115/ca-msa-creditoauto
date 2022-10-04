using camsacreditoauto.Entity.Dto;
using camsacreditoauto.Entity.Models;
using System;
using System.Collections.Generic;

namespace camsacreditoauto.Test.MockData;

public class SolicitudMockData
{



    public static SolicitudType NuevaSolicitud()
    {
        return new SolicitudType
        {
            SolicitudId = 1,
            ClienteId = 1,
            Cuotas = 72,
            EjecutivoId = 1,
            Entrada = 5000,
            EstadoId = 1,
            FechaElaboracion = DateTime.Now,
            MesesPlazo = 48,
            PatioId = 1,
            VehiculoId = 1,
            Observación = string.Empty
        };
    }
}
