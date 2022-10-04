using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace camsacreditoauto.Repository.Data;

public static class DbInitializer
{
    public static void InitializeDb(CreditoAutoContext _context)
    {
        try
        {
            #region LoadData Marcas Autos

            if (_context.Marcas.Any())
            {
                return;
            }
            var pathMarcas = Path.Combine(Directory.GetCurrentDirectory(), @"DataLoad\Marcas.csv");
            using (var streamReaderMarca = new StreamReader(@pathMarcas))
            {
                using var csvReaderMarca = new CsvReader(streamReaderMarca, CultureInfo.InvariantCulture);
                csvReaderMarca.Context.RegisterClassMap<MarcaClassMap>();
                var recordsMarca = csvReaderMarca.GetRecords<Marca>().ToList();
                List<Marca> records = new ();
                foreach (var item in recordsMarca)
                {
                    if (!records.Any(z => z.Nombre.ToLower().Trim() == item.Nombre.ToLower().Trim()))
                    {
                        records.Add(item);
                    }
                }
                _context.Marcas.AddRange(records);
                _context.SaveChanges();
            }

            #endregion

            #region LoadData Ejecutivos de Ventas

            if (_context.Ejecutivos.Any())
            {
                return;
            }
            var pathEjecutivos = Path.Combine(Directory.GetCurrentDirectory(), @"DataLoad\EjecutivosVenta.csv");
            using (var streamReaderEjecutivos = new StreamReader(@pathEjecutivos))
            {
                using var csvReaderEjecutivos = new CsvReader(streamReaderEjecutivos, CultureInfo.InvariantCulture);
                csvReaderEjecutivos.Context.RegisterClassMap<EjecutivoVentasDataClassMap>();
                var recordsEjecutivos = csvReaderEjecutivos.GetRecords<EjecutivoVentasData>().ToList();

                foreach (var item in recordsEjecutivos)
                {
                    int personaId = 0;
                    var objResult = _context.Personas.FirstOrDefault(z => z.Identificacion == item.Identificacion);
                    if (objResult is null)
                    {
                        var objPersona = _context.Personas.Add(
                            new Persona() {
                                Identificacion = item.Identificacion,
                                Nombres = item.Nombres,
                                Apellidos  =item.Apellidos,
                                Direccion = item.Direccion,
                                Edad = item.Edad,
                                Telefono = item.Telefono
                            });
                            _context.SaveChanges();
                        personaId = objPersona.Entity.PersonaId;
                    }else 
                    {
                        personaId = objResult.PersonaId;
                    }
                    var objEjecutivo = _context.Ejecutivos.Add(
                            new Ejecutivo()
                            {
                                PersonaId = personaId,
                                PatioId = item.PatioId,
                                Celular = item.Celular
                            });
                    _context.SaveChanges();

                }
            }

            #endregion

            #region LoadData Clientes

            if (_context.Clientes.Any())
            {
                return;
            }
            var pathClientes = Path.Combine(Directory.GetCurrentDirectory(), @"DataLoad\Clientes.csv");
            using (var streamReaderClientes = new StreamReader(@pathClientes))
            {
                using var csvReaderClientes = new CsvReader(streamReaderClientes, CultureInfo.InvariantCulture);
                csvReaderClientes.Context.RegisterClassMap<ClientesDataClassMap>();
                var recordsClientes = csvReaderClientes.GetRecords<ClienteData>().ToList();

                foreach (var item in recordsClientes)
                {
                    int personaId = 0;
                    var objResult = _context.Personas.FirstOrDefault(z => z.Identificacion == item.Identificacion);
                    if (objResult is null)
                    {
                        var objPersona = _context.Personas.Add(
                            new Persona()
                            {
                                Identificacion = item.Identificacion,
                                Nombres = item.Nombres,
                                Apellidos = item.Apellidos,
                                Direccion = item.Direccion,
                                Edad = item.Edad,
                                Telefono = item.Telefono
                            });
                        _context.SaveChanges();
                        personaId = objPersona.Entity.PersonaId;
                    }
                    else
                    {
                        personaId = objResult.PersonaId;
                    }
                    var objEjecutivo = _context.Clientes.Add(
                            new Cliente()
                            {
                                PersonaId = personaId,
                                EstadoCivil = item.EstadoCivil,
                                FechaNacimiento = Convert.ToDateTime(item.FechaNacimiento),
                                IdentificacionConyuge = item.IdentificacionConyuge,
                                NombresConyuge = item.NombresConyuge,
                                SujetoCredito = item.SujetoCredito  
                            });
                    _context.SaveChanges();

                }
            }

            #endregion

            var contPer = _context.Personas.ToList().Count();
            var conClientes = _context.Clientes.ToList().Count();

        }
        catch (Exception ex)
        {

            throw ApplicationException(ex.Message);
        }
    }

    private static Exception ApplicationException(string v)
    {
        throw new Exception(v);
    }
}

public class MarcaClassMap : ClassMap<Marca>
{ 
    public MarcaClassMap()
    {
        Map(m=> m.MarcaId).Name("MarcaId");
        Map(m => m.Nombre).Name("Nombre");
    }
}

public class EjecutivoVentasDataClassMap : ClassMap<EjecutivoVentasData>
{
    public EjecutivoVentasDataClassMap()
    {
        Map(m => m.Identificacion).Name("Identificacion");
        Map(m => m.Nombres).Name("Nombres");
        Map(m => m.Apellidos).Name("Apellidos");
        Map(m => m.Edad).Name("Edad");
        Map(m => m.Direccion).Name("Direccion");
        Map(m => m.Telefono).Name("Telefono");
        Map(m => m.Celular).Name("Celular");
        Map(m => m.PatioId).Name("PatioId");
    }
}

public class ClientesDataClassMap : ClassMap<ClienteData>
{
    public ClientesDataClassMap()
    {
        Map(m => m.Identificacion).Name("Identificacion");
        Map(m => m.Nombres).Name("Nombres");
        Map(m => m.Apellidos).Name("Apellidos");
        Map(m => m.Edad).Name("Edad");
        Map(m => m.Direccion).Name("Direccion");
        Map(m => m.Telefono).Name("Telefono");
        Map(m => m.FechaNacimiento).Name("FechaNacimiento");
        Map(m => m.EstadoCivil).Name("EstadoCivil");
        Map(m => m.IdentificacionConyuge).Name("IdentificacionConyuge");
        Map(m => m.NombresConyuge).Name("NombresConyuge");
        Map(m => m.SujetoCredito).Name("SujetoCredito");

    }
}

public class EjecutivoVentasData
{
    public string Identificacion { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public int Edad { get; set; }
    public string Direccion { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public int PatioId { get; set; }
    public string Celular { get; set; } = null!;
}

public class ClienteData
{
    public string Identificacion { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public int Edad { get; set; }
    public string Direccion { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string FechaNacimiento { get; set; } = null!;
    public string EstadoCivil { get; set; } = null!;
    public string? IdentificacionConyuge { get; set; }
    public string? NombresConyuge { get; set; }
    public bool SujetoCredito { get; set; }
}