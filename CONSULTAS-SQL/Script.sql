USE [travelhubagency]
GO
/****** Object:  Table [dbo].[DESTINOS]    Script Date: 15/04/2024 14:22:09 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[DESTINOS] ([ID_Destino], [Nombre], [Pais], [Region], [Descripcion], [Imagen], [Latitud], [Longitud], [Precio]) VALUES (1, N'Madrid', 9, N'Paseo de la Castellana', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat', N'Europa.jpeg', N'40.4533356', N'-3.689517', CAST(250 AS Decimal(18, 0)))
GO
ALTER TABLE [dbo].[DESTINOS]  WITH CHECK ADD FOREIGN KEY([Pais])
REFERENCES [dbo].[PAISES] ([ID_Pais])
GO
