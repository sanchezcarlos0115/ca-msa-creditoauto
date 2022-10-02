using System;
using System.Collections.Generic;
using System.Data.Common;
using camsacreditoauto.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

//using System.Data.Entity.DbContext

namespace camsacreditoauto.Repository.Context
{
    public partial class CreditoAutoContext : DbContext
    {
        public CreditoAutoContext()
        {
        }

        public CreditoAutoContext(DbContextOptions<CreditoAutoContext> options)
            : base(options)
        {
        }

        //public CreditoAutoContext(DbConnection connection) : base(connection, contextOwnsConnection: true)
        //{
        //}

        //public CreditoAutoContext(DbConnection connection) : base(connection, true)
        //{
        //}


        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClientePatio> ClientePatios { get; set; } = null!;
        public virtual DbSet<Ejecutivo> Ejecutivos { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<PatioAuto> PatioAutos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Solicitud> Solicituds { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{

            //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CreditoAuto;User ID=sa;Password=root");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.IdentificacionConyuge)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NombresConyuge)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_persona_cliente");
            });

            modelBuilder.Entity<ClientePatio>(entity =>
            {
                entity.ToTable("ClientePatio");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.ClientePatios)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cliente_clientepatio");

                entity.HasOne(d => d.Patio)
                    .WithMany(p => p.ClientePatios)
                    .HasForeignKey(d => d.PatioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_patio_clientepatio");
            });

            modelBuilder.Entity<Ejecutivo>(entity =>
            {
                entity.ToTable("Ejecutivo");

                entity.Property(e => e.Celular)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Patio)
                    .WithMany(p => p.Ejecutivos)
                    .HasForeignKey(d => d.PatioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_patioauto_ejecutivo");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Ejecutivos)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_persona_ejecutivo");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("Marca");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PatioAuto>(entity =>
            {
                entity.HasKey(e => e.PatioId)
                    .HasName("PK__PatioAut__E976F5ABD3253DD7");

                entity.ToTable("PatioAuto");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.ToTable("Solicitud");

                entity.Property(e => e.Cuotas).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Entrada).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FechaElaboracion).HasColumnType("datetime");

                entity.Property(e => e.Observación)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientePatio)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.ClientePatioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_clientepatio_solicitud");

                entity.HasOne(d => d.Ejecutivo)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.EjecutivoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ejecutivo_solicitud");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Estado_solicitud");

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehiculo_solicitud");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");

                entity.Property(e => e.Cilindraje).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.NroChasis)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_marca_vehiculo");

                entity.HasOne(d => d.Patio)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.PatioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_patio_vehiculo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
