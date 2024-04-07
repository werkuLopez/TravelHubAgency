USE [master]
GO
/****** Object:  Database [travelhubagency]    Script Date: 08/04/2024 0:04:32 ******/
CREATE DATABASE [travelhubagency]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'travelhubagency', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\travelhubagency.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'travelhubagency_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\travelhubagency_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [travelhubagency] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [travelhubagency].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [travelhubagency] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [travelhubagency] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [travelhubagency] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [travelhubagency] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [travelhubagency] SET ARITHABORT OFF 
GO
ALTER DATABASE [travelhubagency] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [travelhubagency] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [travelhubagency] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [travelhubagency] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [travelhubagency] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [travelhubagency] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [travelhubagency] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [travelhubagency] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [travelhubagency] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [travelhubagency] SET  DISABLE_BROKER 
GO
ALTER DATABASE [travelhubagency] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [travelhubagency] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [travelhubagency] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [travelhubagency] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [travelhubagency] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [travelhubagency] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [travelhubagency] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [travelhubagency] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [travelhubagency] SET  MULTI_USER 
GO
ALTER DATABASE [travelhubagency] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [travelhubagency] SET DB_CHAINING OFF 
GO
ALTER DATABASE [travelhubagency] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [travelhubagency] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [travelhubagency] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [travelhubagency] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [travelhubagency] SET QUERY_STORE = OFF
GO
USE [travelhubagency]
GO
/****** Object:  Table [dbo].[CONTINENTES]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONTINENTES](
	[ID_Continente] [int] NOT NULL,
	[Nombre] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Continente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DESTINOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DESTINOS](
	[ID_Destino] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Pais] [int] NOT NULL,
	[Region] [varchar](255) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Imagen] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Destino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ESTADORESERVA]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADORESERVA](
	[ID_Estado] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOTELES]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOTELES](
	[ID_Hotel] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Destino] [int] NOT NULL,
	[Categoria] [int] NOT NULL,
	[Direccion] [varchar](255) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Email] [varchar](255) NULL,
	[Sitio_Web] [varchar](255) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Imagen] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Hotel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITINERARIOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITINERARIOS](
	[ID_Itinerario] [int] NOT NULL,
	[Reserva] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Actividad] [varchar](255) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Duracion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Itinerario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAGOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAGOS](
	[PagoID] [int] NOT NULL,
	[ReservaID] [int] NOT NULL,
	[FechaPago] [date] NOT NULL,
	[MetodoPago] [varchar](50) NOT NULL,
	[Importe] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PagoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAISES]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAISES](
	[ID_Pais] [int] NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[Latitud] [nvarchar](255) NOT NULL,
	[Longitud] [nvarchar](255) NOT NULL,
	[Continente] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Pais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAQUETES]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAQUETES](
	[ID_Paquete] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[ID_Destino] [int] NOT NULL,
	[Fecha_Inicio] [date] NOT NULL,
	[Fecha_Fin] [date] NOT NULL,
	[Duracion] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Imagen] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Paquete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS](
	[ID_Reserva] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Paquete] [int] NOT NULL,
	[Fecha_Reserva] [date] NOT NULL,
	[Numero_Personas] [int] NOT NULL,
	[Precio_Total] [decimal](18, 2) NOT NULL,
	[ID_Estado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS_HOTELES]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS_HOTELES](
	[ID_Reserva_hotel] [int] NOT NULL,
	[Reserva] [int] NOT NULL,
	[Hotel] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva_hotel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS_VUELOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS_VUELOS](
	[ID_Reserva_vuelo] [int] NOT NULL,
	[Reserva] [int] NOT NULL,
	[Vuelo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva_vuelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPOSUSUARIOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPOSUSUARIOS](
	[ID_Tipousuario] [int] NOT NULL,
	[TIPO] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Tipousuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[ID_Usuario] [int] NOT NULL,
	[Nombre] [nvarchar](60) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [varbinary](max) NULL,
	[Salt] [nvarchar](50) NULL,
	[Tipousuario] [int] NULL,
	[Aactivo] [bit] NULL,
	[Token] [nchar](20) NULL,
	[Imagen] [nvarchar](max) NULL,
	[Pais] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VUELOS]    Script Date: 08/04/2024 0:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VUELOS](
	[ID_Vuelo] [int] NOT NULL,
	[Origen] [int] NOT NULL,
	[Destino] [int] NOT NULL,
	[Aerolinea] [varchar](255) NOT NULL,
	[Fecha_Salida] [date] NOT NULL,
	[Fecha_Llegada] [date] NOT NULL,
	[Duracion] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Clase] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Vuelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (1, N'Africa')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (2, N'America')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (3, N'Asia')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (4, N'Europa')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (5, N'Oceanía')
GO
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (1, N'Argelia', N'', N'', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (2, N'Egipto', N'', N'', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (3, N'Marruecos', N'', N'', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (4, N'Costa de Marfil', N'', N'', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (5, N'Kenia', N'', N'', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (6, N'Estados Unidos', N'', N'', 2)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (7, N'Canada', N'', N'', 2)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (8, N'Brasil', N'', N'', 2)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (9, N'España', N'', N'', 4)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (10, N'Rusia', N'', N'', 4)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Latitud], [Longitud], [Continente]) VALUES (11, N'Francia', N'', N'', 4)
GO
ALTER TABLE [dbo].[DESTINOS]  WITH CHECK ADD FOREIGN KEY([Pais])
REFERENCES [dbo].[PAISES] ([ID_Pais])
GO
ALTER TABLE [dbo].[HOTELES]  WITH CHECK ADD FOREIGN KEY([Destino])
REFERENCES [dbo].[DESTINOS] ([ID_Destino])
GO
ALTER TABLE [dbo].[ITINERARIOS]  WITH CHECK ADD FOREIGN KEY([Reserva])
REFERENCES [dbo].[RESERVAS] ([ID_Reserva])
GO
ALTER TABLE [dbo].[PAGOS]  WITH CHECK ADD FOREIGN KEY([ReservaID])
REFERENCES [dbo].[RESERVAS] ([ID_Reserva])
GO
ALTER TABLE [dbo].[PAISES]  WITH CHECK ADD FOREIGN KEY([Continente])
REFERENCES [dbo].[CONTINENTES] ([ID_Continente])
GO
ALTER TABLE [dbo].[PAQUETES]  WITH CHECK ADD FOREIGN KEY([ID_Destino])
REFERENCES [dbo].[DESTINOS] ([ID_Destino])
GO
ALTER TABLE [dbo].[RESERVAS]  WITH CHECK ADD FOREIGN KEY([ID_Estado])
REFERENCES [dbo].[ESTADORESERVA] ([ID_Estado])
GO
ALTER TABLE [dbo].[RESERVAS]  WITH CHECK ADD FOREIGN KEY([Paquete])
REFERENCES [dbo].[PAQUETES] ([ID_Paquete])
GO
ALTER TABLE [dbo].[RESERVAS]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[RESERVAS_HOTELES]  WITH CHECK ADD FOREIGN KEY([Hotel])
REFERENCES [dbo].[HOTELES] ([ID_Hotel])
GO
ALTER TABLE [dbo].[RESERVAS_HOTELES]  WITH CHECK ADD FOREIGN KEY([Reserva])
REFERENCES [dbo].[RESERVAS] ([ID_Reserva])
GO
ALTER TABLE [dbo].[RESERVAS_VUELOS]  WITH CHECK ADD FOREIGN KEY([Reserva])
REFERENCES [dbo].[RESERVAS] ([ID_Reserva])
GO
ALTER TABLE [dbo].[RESERVAS_VUELOS]  WITH CHECK ADD FOREIGN KEY([Vuelo])
REFERENCES [dbo].[VUELOS] ([ID_Vuelo])
GO
ALTER TABLE [dbo].[USUARIOS]  WITH CHECK ADD FOREIGN KEY([Tipousuario])
REFERENCES [dbo].[TIPOSUSUARIOS] ([ID_Tipousuario])
GO
ALTER TABLE [dbo].[VUELOS]  WITH CHECK ADD FOREIGN KEY([Destino])
REFERENCES [dbo].[DESTINOS] ([ID_Destino])
GO
ALTER TABLE [dbo].[VUELOS]  WITH CHECK ADD FOREIGN KEY([Origen])
REFERENCES [dbo].[DESTINOS] ([ID_Destino])
GO
USE [master]
GO
ALTER DATABASE [travelhubagency] SET  READ_WRITE 
GO
