USE [CreditoAuto]
GO
create table dbo.Persona(
PersonaId int identity(1,1) primary key not null,
Identificacion varchar(20) not null,
Nombres varchar(160) not null,
Apellidos varchar(160) not null, 
Edad int not null,
Direccion varchar(255) not null,
Telefono varchar(15) not null
)
GO
create table dbo.Cliente(
ClienteId int identity(1,1) primary key not null, 
PersonaId int not null,
FechaNacimiento Date not null,
EstadoCivil varchar(1) not null,
IdentificacionConyuge varchar(20) null,
NombresConyuge varchar(250) null,
SujetoCredito bit not null,
CONSTRAINT fk_persona_cliente foreign key (PersonaId) references dbo.Persona(PersonaId)
)
GO
create table dbo.PatioAuto(
PatioId int identity(1,1) primary key not null,
Nombre varchar(60) not null, 
Direccion varchar(255) not null,
Telefono varchar(15) not null,
NumeroPtoVenta int not null
)
GO

create table dbo.Ejecutivo(
EjecutivoId int identity(1,1) primary key not null,
PersonaId int not null,
PatioId int not null,
Celular varchar(15) not null,
CONSTRAINT fk_persona_ejecutivo foreign key (PersonaId) references dbo.Persona(PersonaId),
CONSTRAINT fk_patioauto_ejecutivo foreign key (PatioId) references dbo.PatioAuto(PatioId)
)
GO
create table dbo.Marca(
MarcaId int identity(1,1) primary key not null,
Nombre varchar(40) not null
)
GO

create table dbo.Vehiculo(
VehiculoId int identity(1,1) primary key not null,
Placa varchar(40) not null,
Modelo varchar(60) not null,
NroChasis varchar(40) not null,
MarcaId int not null,
PatioId int not null,
Tipo varchar(15) null,
Cilindraje decimal(18,2) not null,
Avaluo bit not null,
CONSTRAINT fk_marca_vehiculo foreign key (MarcaId) references dbo.Marca(MarcaId),
CONSTRAINT fk_patio_vehiculo foreign key (PatioId) references dbo.PatioAuto(PatioId),
)
GO

create table dbo.ClientePatio(
ClientePatioId int identity(1,1) primary key not null,
ClienteId int not null,
PatioId int not null,
FechaAsignacion DateTime not null,
CONSTRAINT fk_cliente_clientepatio foreign key (ClienteId) references dbo.Cliente(ClienteId),
CONSTRAINT fk_patio_clientepatio foreign key (PatioId) references dbo.PatioAuto(PatioId)
)
GO

create table dbo.Estado(
EstadoId int identity(1,1) primary key not null,
Nombre varchar(40) not null
)
GO

create table dbo.Solicitud(
SolicitudId int identity(1,1) primary key not null,
FechaElaboracion DateTime not null,
ClientePatioId int not null,
VehiculoId int not null,
MesesPlazo int not null,
Cuotas decimal(18,2) not null,
Entrada decimal(18,2) not null,
EjecutivoId int not null,
Observación varchar(250) null,
EstadoId int not null,
CONSTRAINT fk_clientepatio_solicitud foreign key (ClientePatioId) references dbo.ClientePatio(ClientePatioId),
CONSTRAINT fk_vehiculo_solicitud foreign key (VehiculoId) references dbo.Vehiculo(VehiculoId),
CONSTRAINT fk_ejecutivo_solicitud foreign key (EjecutivoId) references dbo.Ejecutivo(EjecutivoId),
CONSTRAINT fk_Estado_solicitud foreign key (EstadoId) references dbo.Estado(EstadoId)
)
GO

