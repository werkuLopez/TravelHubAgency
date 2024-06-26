
/****** Object:  Table [dbo].[COMENTARIOS]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMENTARIOS](
	[ID_Comentario] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Contenido] [nvarchar](max) NOT NULL,
	[Fecha] [datetime] NULL,
	[Post] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Comentario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[CONTINENTES]    Script Date: 26/05/2024 15:12:58 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DESTINOS]    Script Date: 26/05/2024 15:12:58 ******/
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
	[Latitud] [nvarchar](255) NOT NULL,
	[Longitud] [nvarchar](255) NOT NULL,
	[Precio] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Destino] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ESTADORESERVA]    Script Date: 26/05/2024 15:12:58 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ETIQUETAS]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ETIQUETAS](
	[ID_Etiqueta] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Etiqueta] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[HOTELES]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOTELES](
	[ID_Hotel] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Imagen] [nvarchar](max) NOT NULL,
	[Precio] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Hotel] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITINERARIOS]    Script Date: 26/05/2024 15:12:58 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PAGOS]    Script Date: 26/05/2024 15:12:58 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAGOS_VUELO]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAGOS_VUELO](
	[ID_Pago] [int] NOT NULL,
	[Vuelo] [int] NOT NULL,
	[FechaPago] [date] NOT NULL,
	[MetodoPago] [varchar](50) NOT NULL,
	[Importe] [decimal](10, 2) NOT NULL,
	[USUARIO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Pago] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAISES]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAISES](
	[ID_Pais] [int] NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[Continente] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Pais] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAQUETES]    Script Date: 26/05/2024 15:12:58 ******/
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
	[Num_Personas] [int] NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[Imagen] [nvarchar](max) NULL,
	[ID_Hotel] [int] NULL,
	[ID_Vuelo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Paquete] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PUBLICACIONES]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PUBLICACIONES](
	[ID_Post] [int] NOT NULL,
	[Titulo] [nvarchar](255) NOT NULL,
	[Contenido] [nvarchar](max) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Imagen] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Post] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REPLAYSCOMENTARIO]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REPLAYSCOMENTARIO](
	[ID_Replay] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Contenido] [nvarchar](max) NULL,
	[Comentario] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Replay] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS]    Script Date: 26/05/2024 15:12:58 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS_DESTINO]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS_DESTINO](
	[ID_Reserva_Destino] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Destino] [int] NOT NULL,
	[Fecha_Reserva] [date] NOT NULL,
	[Fecha_Inicio] [date] NOT NULL,
	[Fecha_Fin] [date] NOT NULL,
	[Personas] [int] NOT NULL,
	[ID_Estado_Reserva] [int] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva_Destino] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS_HOTELES]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS_HOTELES](
	[ID_Reserva_hotel] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Hotel] [int] NOT NULL,
	[Precio] [decimal](18, 0) NOT NULL,
	[Num_Personas] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva_hotel] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS_PACK]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS_PACK](
	[ID_Reserva_Pack] [int] NOT NULL,
	[Usuario] [int] NOT NULL,
	[Paquete] [int] NOT NULL,
	[Fecha_Reserva] [date] NOT NULL,
	[ID_Estado_Reserva] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva_Pack] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVAS_VUELOS]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVAS_VUELOS](
	[ID_Reserva_vuelo] [int] NOT NULL,
	[Vuelo] [int] NOT NULL,
	[Usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reserva_vuelo] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[TIPOSUSUARIOS]    Script Date: 26/05/2024 15:12:58 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[ID_Usuario] [int] NOT NULL,
	[Nombre] [nvarchar](60) NOT NULL,
	[Apellido] [nvarchar](60) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [varbinary](max) NULL,
	[Salt] [nvarchar](50) NULL,
	[Tipousuario] [int] NULL,
	[Token] [nchar](20) NULL,
	[Imagen] [nvarchar](max) NULL,
	[Pais] [varchar](255) NOT NULL,
	[Telefono] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VUELOS]    Script Date: 26/05/2024 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VUELOS](
	[ID_Vuelo] [int] NOT NULL,
	[Origen] [varchar](255) NOT NULL,
	[Destino] [int] NOT NULL,
	[Aerolinea] [varchar](255) NOT NULL,
	[Fecha_Salida] [date] NOT NULL,
	[Fecha_Llegada] [date] NOT NULL,
	[Duracion] [nvarchar](50) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Vuelo] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[COMENTARIOS] ([ID_Comentario], [Usuario], [Contenido], [Fecha], [Post]) VALUES (1, 1, N'Kasd diam tempor rebum magna dolores sed sed eirmod ipsum. Gubergren clita aliquyam consetetur sadipscing, at tempor amet ipsum diam tempor consetetur at sit.', CAST(N'2024-04-18T21:11:01.710' AS DateTime), 1)
INSERT [dbo].[COMENTARIOS] ([ID_Comentario], [Usuario], [Contenido], [Fecha], [Post]) VALUES (2, 4, N'Un clima muy cálido, por no hablar de las playas que ofrece. He de decir que me esperaba mejores cosas, pero me ha gustado igual.', CAST(N'2024-04-19T00:22:16.533' AS DateTime), 1)
INSERT [dbo].[COMENTARIOS] ([ID_Comentario], [Usuario], [Contenido], [Fecha], [Post]) VALUES (3, 4, N' Te has equivocado de página web Juan !! Pero yo también la  he visto, goo0d!', CAST(N'2024-05-08T03:45:59.007' AS DateTime), 5)
GO
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (1, N'Africa')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (2, N'America')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (3, N'Asia')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (4, N'Europa')
INSERT [dbo].[CONTINENTES] ([ID_Continente], [Nombre]) VALUES (5, N'Oceanía')
GO
INSERT [dbo].[DESTINOS] ([ID_Destino], [Nombre], [Pais], [Region], [Descripcion], [Imagen], [Latitud], [Longitud], [Precio]) VALUES (1, N'Paseo de la Castellana', 9, N'Madrid', N'El paseo de la Castellana es una avenida de Madrid que recorre la ciudad desde la plaza de Colón, en el centro, hasta el nudo norte.', N'destino1.jpg', N'40.4533356', N'-3.689517', CAST(750 AS Decimal(18, 0)))
INSERT [dbo].[DESTINOS] ([ID_Destino], [Nombre], [Pais], [Region], [Descripcion], [Imagen], [Latitud], [Longitud], [Precio]) VALUES (2, N'Paseo de la Castellana', 9, N'Madrid', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat', N'destino2.jpg', N'40.4533356', N'-3.689517', CAST(250 AS Decimal(18, 0)))
INSERT [dbo].[DESTINOS] ([ID_Destino], [Nombre], [Pais], [Region], [Descripcion], [Imagen], [Latitud], [Longitud], [Precio]) VALUES (3, N'París', 11, N'Francia', N'El recorrido propuesto por nuestros guías te hará descubrir las obras más insignes del museo (La Gioconda, la Victoria de Samotracia, la Venus de Milo…), los espacios más emblemáticos del antiguo palacio del Louvre y su historia.', N'destino3.jpg', N'48.8606111', N'2.337644', CAST(750 AS Decimal(18, 0)))
INSERT [dbo].[DESTINOS] ([ID_Destino], [Nombre], [Pais], [Region], [Descripcion], [Imagen], [Latitud], [Longitud], [Precio]) VALUES (4, N'Valencia', 9, N'España', N'La Ciudad de las Artes y las Ciencias es un complejo arquitectónico, cultural y de entretenimiento de la ciudad de Valencia. Si te gusta la geografía este es tu escapada', N'destino4.jpg', N'39.4699075', N'-0.3762881', CAST(580 AS Decimal(18, 0)))
INSERT [dbo].[DESTINOS] ([ID_Destino], [Nombre], [Pais], [Region], [Descripcion], [Imagen], [Latitud], [Longitud], [Precio]) VALUES (5, N'Nueva York', 6, N'Hudson Valley', N'Para aquellos que tienen poco tiempo mientras viajan, esta experiencia de senderismo y bodega de 2 días en Hudson Valley incluye visitas a Nostrano Vineyards y Benmarl , tambien cuenta con alojamiento de una noche. ', N'destino5.jpg', N'40.7127753', N'-74.0059728', CAST(1989 AS Decimal(18, 0)))
GO
INSERT [dbo].[ESTADORESERVA] ([ID_Estado], [Nombre]) VALUES (1, N'Pendiente')
INSERT [dbo].[ESTADORESERVA] ([ID_Estado], [Nombre]) VALUES (2, N'Confirmada')
GO
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (1, N'Playa')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (2, N'Montaña')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (3, N'Ciudad')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (4, N'Aventura')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (5, N'Cultura')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (6, N'Historia')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (7, N'Gastronomía')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (8, N'Naturaleza')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (9, N'Familia')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (10, N'Lujo')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (11, N'Aventura')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (12, N'Relax')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (13, N'Arte')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (14, N'Deportes')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (15, N'Ecoturismo')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (16, N'Gastronomía Internacional')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (17, N'Playas Vírgenes')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (18, N'Senderismo')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (19, N'Turismo Rural')
INSERT [dbo].[ETIQUETAS] ([ID_Etiqueta], [Nombre]) VALUES (20, N'Bienestar')
GO
INSERT [dbo].[HOTELES] ([ID_Hotel], [Nombre], [Telefono], [Descripcion], [Imagen], [Precio]) VALUES (1, N'Hotel Versalles', N'123456789', N'El Gran Hotel Versalles está situado en una zona tranquila del centro de Madrid, junto a la estación de metro Alonso Martínez.', N'hotel1.jpg', CAST(250 AS Decimal(18, 0)))
INSERT [dbo].[HOTELES] ([ID_Hotel], [Nombre], [Telefono], [Descripcion], [Imagen], [Precio]) VALUES (2, N'Hotel Legazpi', N'987654321', N'El Hotel Gran Legazpi es un hotel económico con servicios magníficos y una ubicación práctica en los alrededores de Calle 30, una de las zonas de mayor crecimiento de Madrid. Hay WiFi gratuita.', N'hotel2.jpg', CAST(270 AS Decimal(18, 0)))
INSERT [dbo].[HOTELES] ([ID_Hotel], [Nombre], [Telefono], [Descripcion], [Imagen], [Precio]) VALUES (3, N'Guadalmedina', N'1324576809', N'Este hotel se encuentra frente al Centro de Arte Contemporáneo de Málaga y ofrece WiFi gratuita y habitaciones elegantes con aire acondicionado y TV vía satélite de pantalla plana.', N'hotel3.jpg', CAST(980 AS Decimal(18, 0)))
INSERT [dbo].[HOTELES] ([ID_Hotel], [Nombre], [Telefono], [Descripcion], [Imagen], [Precio]) VALUES (4, N'Hotel Nelva', N'987654321', N'Hotel informal con restaurante y piscina', N'hotel4.jpg', CAST(75 AS Decimal(18, 0)))
GO
INSERT [dbo].[PAGOS_VUELO] ([ID_Pago], [Vuelo], [FechaPago], [MetodoPago], [Importe], [USUARIO]) VALUES (1, 1, CAST(N'2024-05-03' AS Date), N'Tarjeta de credito', CAST(1335.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (1, N'Argelia', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (2, N'Egipto', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (3, N'Marruecos', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (4, N'Costa de Marfil', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (5, N'Kenia', 1)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (6, N'Estados Unidos', 2)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (7, N'Canada', 2)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (8, N'Brasil', 2)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (9, N'España', 4)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (10, N'Rusia', 4)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (11, N'Francia', 4)
INSERT [dbo].[PAISES] ([ID_Pais], [Nombre], [Continente]) VALUES (12, N'Noruega', 4)
GO
INSERT [dbo].[PAQUETES] ([ID_Paquete], [Nombre], [ID_Destino], [Fecha_Inicio], [Fecha_Fin], [Duracion], [Precio], [Num_Personas], [Descripcion], [Imagen], [ID_Hotel], [ID_Vuelo]) VALUES (1, N'Valencia', 4, CAST(N'2024-03-10' AS Date), CAST(N'2024-03-13' AS Date), 3, CAST(1740.00 AS Decimal(18, 2)), 3, N'La Ciudad de las Artes y las Ciencias es un complejo arquitectónico, cultural y de entretenimiento de la ciudad de Valencia. Si te gusta la geografía este es tu escapada', N'package1.jpg', NULL, NULL)
INSERT [dbo].[PAQUETES] ([ID_Paquete], [Nombre], [ID_Destino], [Fecha_Inicio], [Fecha_Fin], [Duracion], [Precio], [Num_Personas], [Descripcion], [Imagen], [ID_Hotel], [ID_Vuelo]) VALUES (4, N'Francia', 3, CAST(N'2024-05-06' AS Date), CAST(N'2024-05-09' AS Date), 3, CAST(289.00 AS Decimal(18, 2)), 3, N'Si eres amante del arte, París te permite disfrutar de algunas de las obras más famosas de la historia.', N'package4.jpg', NULL, 2)
INSERT [dbo].[PAQUETES] ([ID_Paquete], [Nombre], [ID_Destino], [Fecha_Inicio], [Fecha_Fin], [Duracion], [Precio], [Num_Personas], [Descripcion], [Imagen], [ID_Hotel], [ID_Vuelo]) VALUES (5, N'Madrid', 2, CAST(N'2024-05-13' AS Date), CAST(N'2024-05-18' AS Date), 5, CAST(2550.00 AS Decimal(18, 2)), 2, N'Cinco días de actividades turística', N'package5.jpg', 1, NULL)
INSERT [dbo].[PAQUETES] ([ID_Paquete], [Nombre], [ID_Destino], [Fecha_Inicio], [Fecha_Fin], [Duracion], [Precio], [Num_Personas], [Descripcion], [Imagen], [ID_Hotel], [ID_Vuelo]) VALUES (6, N'Madrid', 1, CAST(N'2024-05-13' AS Date), CAST(N'2024-05-20' AS Date), 7, CAST(2550.00 AS Decimal(18, 2)), 2, N'Una semana para visitar Madrid. Un turismo diferente, divertido, interesante y muy atractivo, visitando los lugares más emblemáticos (la Gran vía, la Plaza Mayor, la Puerta del Sol, etc..),', N'package6.jpg', 2, 1)
INSERT [dbo].[PAQUETES] ([ID_Paquete], [Nombre], [ID_Destino], [Fecha_Inicio], [Fecha_Fin], [Duracion], [Precio], [Num_Personas], [Descripcion], [Imagen], [ID_Hotel], [ID_Vuelo]) VALUES (7, N'Caminata/Bodegas/Paseo sobre el Hudson/1 noche de hotel/Hudson Valley', 5, CAST(N'2024-05-20' AS Date), CAST(N'2024-05-21' AS Date), 1, CAST(1750.00 AS Decimal(18, 2)), 2, N'Para aquellos que tienen poco tiempo mientras viajan, esta experiencia de senderismo y bodega de 2 días en Hudson Valley incluye visitas a Nostrano Vineyards y Benmarl , tambien cuenta con alojamiento de una noche. ', N'package7.jpg', 3, 2)
INSERT [dbo].[PAQUETES] ([ID_Paquete], [Nombre], [ID_Destino], [Fecha_Inicio], [Fecha_Fin], [Duracion], [Precio], [Num_Personas], [Descripcion], [Imagen], [ID_Hotel], [ID_Vuelo]) VALUES (8, N'Caminata/Bodegas/Paseo sobre el Hudson/1 noche de hotel/Hudson Valley', 5, CAST(N'2024-05-20' AS Date), CAST(N'2024-05-21' AS Date), 0, CAST(1750.00 AS Decimal(18, 2)), 2, N'Para aquellos que tienen poco tiempo mientras viajan, esta experiencia de senderismo y bodega de 2 días en Hudson Valley incluye visitas a Nostrano Vineyards y Benmarl , tambien cuenta con alojamiento de una noche. ', N'package8.jpg', 3, 2)
GO
INSERT [dbo].[PUBLICACIONES] ([ID_Post], [Titulo], [Contenido], [Fecha], [Usuario], [Imagen]) VALUES (1, N'Islas Maldivas', N'Una de las mejores playas, con un clima estupendo. 
', CAST(N'2024-05-08T19:25:00.533' AS DateTime), 1, N'post1.jpg')
INSERT [dbo].[PUBLICACIONES] ([ID_Post], [Titulo], [Contenido], [Fecha], [Usuario], [Imagen]) VALUES (3, N'Nueva York ', N'Bienestar, playa, montaña, relax, etc..', CAST(N'2024-05-08T18:44:48.450' AS DateTime), 5, N'post3.jpg')
INSERT [dbo].[PUBLICACIONES] ([ID_Post], [Titulo], [Contenido], [Fecha], [Usuario], [Imagen]) VALUES (4, N'París, La ciudad del Amor', N'Además de ser una de las capitales gastronómicas más reputadas del mundo, París seduce por su ambiente lleno de romanticismo y lujo. Ver la imponente Torre Eiffel, perderse en los Campos Elíseos de casi 2 kilómetros de longitud, disfrutar de un espectáculo de cabaret en Moulin Rouge o pasear por el bohemio barrio de Montmartre. Algunas de estas cosas son las que hacen de París una ciudad única, un lugar de ensueño donde perderte y vivir un cuento.
', CAST(N'2024-05-08T03:29:34.040' AS DateTime), 1, N'post4.jpg')
INSERT [dbo].[PUBLICACIONES] ([ID_Post], [Titulo], [Contenido], [Fecha], [Usuario], [Imagen]) VALUES (5, N'Rey León', N'El otro día fui al cine a ver el rey león 2, qué pasada.', CAST(N'2024-05-08T03:41:21.703' AS DateTime), 3, N'post5.jpg')
GO
INSERT [dbo].[REPLAYSCOMENTARIO] ([ID_Replay], [Usuario], [Contenido], [Comentario], [Fecha]) VALUES (1, 4, N'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABBBBBBBBBBBBBBBBBBBBBBBBBBB', 2, CAST(N'2024-04-19T10:39:03.187' AS DateTime))
GO
INSERT [dbo].[RESERVAS_VUELOS] ([ID_Reserva_vuelo], [Vuelo], [Usuario]) VALUES (4, 1, 1)
GO

INSERT [dbo].[TIPOSUSUARIOS] ([ID_Tipousuario], [TIPO]) VALUES (1, N'Admin')
INSERT [dbo].[TIPOSUSUARIOS] ([ID_Tipousuario], [TIPO]) VALUES (2, N'Usuario')
GO
INSERT [dbo].[USUARIOS] ([ID_Usuario], [Nombre], [Apellido], [Email], [Password], [Salt], [Tipousuario], [Token], [Imagen], [Pais], [Telefono]) VALUES (1, N'Marta', N'Gutierrez', N'marta.gutierrez@gmail.com', 0xD165779540A002795DE012E297DA7BC81A089855BD918B2D935160D20C1A38E2CC34D0EF477A9482FD8BE5DEF3D1865093DBC52C39B9C0CB62F8578D6108E01D, N'ÒÑª#ëdo}Na3ïÓ	àþ¶@ÉùÛ¬Ð5Iß.e+»´ªoé©Ñrª', 2, N'FUMqDiGMrn_hcl      ', N'usuario1.jpg', N'España', N'987654321')
INSERT [dbo].[USUARIOS] ([ID_Usuario], [Nombre], [Apellido], [Email], [Password], [Salt], [Tipousuario], [Token], [Imagen], [Pais], [Telefono]) VALUES (2, N'Manuel ', N'Fernandez de la Hera', N'manuel.fdz@gmail.com', 0xB13B3944E22377C6B9B1B94DD8F9934378B7416AB6C594EFBCA60FAD346AA397A71DEB2FE20A177E958B19300401A2D7044D62249D3BA8717245CA32E557576F, N'ö5ª[ß¦ê[o.Kñ½dóp¢å±¢tC1ein¨Öû®	eõCHJÃL', 1, N'M[`[UlEluXnnLy      ', N'usuario2.jpg', N'Argentina', N'123456789')
INSERT [dbo].[USUARIOS] ([ID_Usuario], [Nombre], [Apellido], [Email], [Password], [Salt], [Tipousuario], [Token], [Imagen], [Pais], [Telefono]) VALUES (3, N'Juan Alberto', N'La Que Has Lio', N'juanalberto@gmail.com', 0xF3433B4646DACB3259DFCE846667FFABF4172A42AA567720061944582CFDD96122125285176DB19101F0D56B175C017CE619CA864355FC40AAACBB23D9970ADB, N'H­¦ªÜ¥½æïú°þ"¯ÌÛè¨öÙ4¸èÞÐ MÌ¬ÔðiýB¶äv+]', 2, N'B^dUcgrv^g^]Ac      ', N'usuario3.jpg', N'China', N'333247617')
INSERT [dbo].[USUARIOS] ([ID_Usuario], [Nombre], [Apellido], [Email], [Password], [Salt], [Tipousuario], [Token], [Imagen], [Pais], [Telefono]) VALUES (4, N'Javier Sierra ', N'Del Segura Neval', N'javiersierra@gmail.com', 0x4B41F0D0900383AB5F810390C0FC44A508A4884C3E893FCC1CD06FB5D6F8DF2F6FDAB7A5CD432F523972F56A8BDD86D67FB6F2D253A18F7895E09D30E63E3E44, N'5ýÝL²ý¡DYfuì±-ì[P\ïbnï:°M½,7ËHº1 ó', 2, N'egDYitbPgrmC\B      ', N'usuario4.jpg', N'España', N'1324576809')
INSERT [dbo].[USUARIOS] ([ID_Usuario], [Nombre], [Apellido], [Email], [Password], [Salt], [Tipousuario], [Token], [Imagen], [Pais], [Telefono]) VALUES (5, N'Arturo', N'Boya', N'arturo@gmail.com', 0x406DE89C03852383AFA21FA206D5A8A280EBDBE27058509703264C8B3BFF0F73D0F6EED9B24039E0B26D3BBC4C938D6947BAF804021CCCF2FAC401ADB1B36C24, N'úa	0Æ®?x1Ò2nïûÃTÚACÛ«-ê[£*¤,yÕpBÂ{ô2	Ü^', 2, N'BmerTRw^hncbSA      ', N'usuario5.jpg', N'Andorra', N'123456789')
GO
INSERT [dbo].[VUELOS] ([ID_Vuelo], [Origen], [Destino], [Aerolinea], [Fecha_Salida], [Fecha_Llegada], [Duracion], [Precio]) VALUES (1, N'Aeropuerto De San Javier (MSJ/MJV)', 4, N'Zapata', CAST(N'2024-04-30' AS Date), CAST(N'2024-04-30' AS Date), N'00:09:36', CAST(1335.00 AS Decimal(18, 2)))
INSERT [dbo].[VUELOS] ([ID_Vuelo], [Origen], [Destino], [Aerolinea], [Fecha_Salida], [Fecha_Llegada], [Duracion], [Precio]) VALUES (2, N'Aeropuerto De San Javier (MSJ/MJV)', 3, N'Air Europa', CAST(N'2024-08-20' AS Date), CAST(N'2024-08-20' AS Date), N'8:41:20', CAST(2680.00 AS Decimal(18, 2)))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UNIQUE_DNI]    Script Date: 26/05/2024 15:13:17 ******/
ALTER TABLE [dbo].[COMENTARIOS]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[COMENTARIOS]  WITH CHECK ADD FOREIGN KEY([Post])
REFERENCES [dbo].[PUBLICACIONES] ([ID_Post])
GO
ALTER TABLE [dbo].[DESTINOS]  WITH CHECK ADD FOREIGN KEY([Pais])
REFERENCES [dbo].[PAISES] ([ID_Pais])
GO
ALTER TABLE [dbo].[ITINERARIOS]  WITH CHECK ADD FOREIGN KEY([Reserva])
REFERENCES [dbo].[RESERVAS] ([ID_Reserva])
GO
ALTER TABLE [dbo].[PAGOS]  WITH CHECK ADD FOREIGN KEY([ReservaID])
REFERENCES [dbo].[RESERVAS] ([ID_Reserva])
GO
ALTER TABLE [dbo].[PAGOS_VUELO]  WITH CHECK ADD FOREIGN KEY([USUARIO])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[PAGOS_VUELO]  WITH CHECK ADD FOREIGN KEY([Vuelo])
REFERENCES [dbo].[VUELOS] ([ID_Vuelo])
GO
ALTER TABLE [dbo].[PAISES]  WITH CHECK ADD FOREIGN KEY([Continente])
REFERENCES [dbo].[CONTINENTES] ([ID_Continente])
GO
ALTER TABLE [dbo].[PAQUETES]  WITH CHECK ADD FOREIGN KEY([ID_Destino])
REFERENCES [dbo].[DESTINOS] ([ID_Destino])
GO
ALTER TABLE [dbo].[PAQUETES]  WITH CHECK ADD FOREIGN KEY([ID_Hotel])
REFERENCES [dbo].[HOTELES] ([ID_Hotel])
GO
ALTER TABLE [dbo].[PAQUETES]  WITH CHECK ADD FOREIGN KEY([ID_Vuelo])
REFERENCES [dbo].[VUELOS] ([ID_Vuelo])
GO
ALTER TABLE [dbo].[PUBLICACIONES]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[REPLAYSCOMENTARIO]  WITH CHECK ADD FOREIGN KEY([Comentario])
REFERENCES [dbo].[COMENTARIOS] ([ID_Comentario])
GO
ALTER TABLE [dbo].[REPLAYSCOMENTARIO]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
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
ALTER TABLE [dbo].[RESERVAS_DESTINO]  WITH CHECK ADD FOREIGN KEY([Destino])
REFERENCES [dbo].[DESTINOS] ([ID_Destino])
GO
ALTER TABLE [dbo].[RESERVAS_DESTINO]  WITH CHECK ADD FOREIGN KEY([ID_Estado_Reserva])
REFERENCES [dbo].[ESTADORESERVA] ([ID_Estado])
GO
ALTER TABLE [dbo].[RESERVAS_DESTINO]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[RESERVAS_HOTELES]  WITH CHECK ADD FOREIGN KEY([Hotel])
REFERENCES [dbo].[HOTELES] ([ID_Hotel])
GO
ALTER TABLE [dbo].[RESERVAS_HOTELES]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[RESERVAS_PACK]  WITH CHECK ADD FOREIGN KEY([ID_Estado_Reserva])
REFERENCES [dbo].[ESTADORESERVA] ([ID_Estado])
GO
ALTER TABLE [dbo].[RESERVAS_PACK]  WITH CHECK ADD FOREIGN KEY([Paquete])
REFERENCES [dbo].[PAQUETES] ([ID_Paquete])
GO
ALTER TABLE [dbo].[RESERVAS_PACK]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
GO
ALTER TABLE [dbo].[RESERVAS_VUELOS]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[USUARIOS] ([ID_Usuario])
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

/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_VUELO]    Script Date: 26/05/2024 15:13:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_VUELO]
	(@Origen NVARCHAR(100),
    @IdDestino INT,
    @Aerolinea NVARCHAR(100),
    @FechaSalida DATETIME,
    @FechaLlegada DATETIME,
    @Precio DECIMAL(18,2))
AS
BEGIN
	
	DECLARE @IdVuelo INT;
    DECLARE @DuracionHoras INT;
    DECLARE @DuracionMinutos INT;
	DECLARE @DuracionSegundos INT;
    DECLARE @Duracion VARCHAR(20);

    -- Calcular la duración del vuelo en horas , minutos , segundos
    SET @DuracionMinutos = DATEDIFF(MINUTE, @FechaSalida, @FechaLlegada);
SET @DuracionHoras = @DuracionSegundos / 3600;
    SET @DuracionMinutos = (@DuracionSegundos % 3600) / 60;
    SET @DuracionSegundos = @DuracionSegundos % 60;

    -- Formatear la duración en formato "HH:mm"
    SET @Duracion = RIGHT('0' + CONVERT(VARCHAR(2), @DuracionHoras), 2) + ':' +
                    RIGHT('0' + CONVERT(VARCHAR(2), @DuracionMinutos), 2) + ':' +
                    RIGHT('0' + CONVERT(VARCHAR(2), @DuracionSegundos), 2);

	select @IdVuelo=max(ID_Vuelo)+1 from VUELOS;

    -- Insertar el vuelo en la tabla
    INSERT INTO VUELOS VALUES 
	(@IdVuelo,@Origen, @IdDestino, @Aerolinea, @FechaSalida, @FechaLlegada, @Duracion, @Precio);
END
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGINACION_DESTINOS]    Script Date: 26/05/2024 15:13:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_PAGINACION_DESTINOS]
(@posicion INT)
AS
	SELECT ID_Destino, Nombre, Descripcion, Imagen, Precio, Pais, Region, Latitud, Longitud FROM
(SELECT 
	CAST(
	ROW_NUMBER() OVER (ORDER BY D.NOMBRE) AS INT) AS POSICION,
   D.ID_Destino, D.NOMBRE, D.DESCRIPCION, D.IMAGEN, D.Precio, D.Pais, D.Region, D.Latitud, D.Longitud
	FROM DESTINOS D) AS QUERY

	WHERE QUERY.POSICION >= @posicion AND QUERY.POSICION < (@posicion + 8)
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGINAR_BLOG]    Script Date: 26/05/2024 15:13:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_PAGINAR_BLOG]

(@page INT)
AS
	SELECT ID_Post, Titulo, Contenido, Imagen, Fecha, Usuario FROM
(SELECT 
	CAST(
	ROW_NUMBER() OVER (ORDER BY P.Titulo) AS INT) AS POSICION,
   ID_Post, Titulo, Contenido, Imagen, Fecha, Usuario
	FROM PUBLICACIONES P) AS QUERY

	WHERE QUERY.POSICION >= @page AND QUERY.POSICION < (@page + 5)
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_VUELO]    Script Date: 26/05/2024 15:13:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_VUELO]
	(@IdVuelo INT,
	 @Origen NVARCHAR(100),
     @IdDestino INT,
     @Aerolinea NVARCHAR(100),
     @FechaSalida DATETIME,
     @FechaLlegada DATETIME,
     @Precio DECIMAL(18,2))
AS
BEGIN
	
    DECLARE @DuracionHoras INT;
    DECLARE @DuracionMinutos INT;
    DECLARE @DuracionSegundos INT;
    DECLARE @Duracion VARCHAR(20);

    -- Calcular la duración del vuelo en horas, minutos, segundos
    SET @DuracionSegundos = DATEDIFF(SECOND, @FechaSalida, @FechaLlegada);
    SET @DuracionHoras = @DuracionSegundos / 3600;
    SET @DuracionSegundos = @DuracionSegundos % 3600;
    SET @DuracionMinutos = @DuracionSegundos / 60;
    SET @DuracionSegundos = @DuracionSegundos % 60;

    -- Formatear la duración en formato "HH:mm:ss"
    SET @Duracion = RIGHT('0' + CONVERT(VARCHAR(2), @DuracionHoras), 2) + ':' +
                    RIGHT('0' + CONVERT(VARCHAR(2), @DuracionMinutos), 2) + ':' +
                    RIGHT('0' + CONVERT(VARCHAR(2), @DuracionSegundos), 2);

    -- actualizamos el vuelo seleccionado
    UPDATE VUELOS 
    SET Origen = @Origen, 
        Destino = @IdDestino, 
        Aerolinea = @Aerolinea, 
        Fecha_Salida = @FechaSalida, 
        Fecha_Llegada = @FechaLlegada, 
        Duracion = @Duracion, 
        Precio = @Precio
    WHERE ID_Vuelo = @IdVuelo;
END
GO
