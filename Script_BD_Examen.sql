﻿USE [examen]
GO
/****** Object:  User [eddy]    Script Date: 04/03/2021 04:33:20 a. m. ******/
CREATE USER [eddy] FOR LOGIN [eddy] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [eddy]
GO
/****** Object:  Table [dbo].[Recibo]    Script Date: 04/03/2021 04:33:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recibo](
	[idRecibo] [int] IDENTITY(1,1) NOT NULL,
	[proveedor] [varchar](50) NOT NULL,
	[monto] [decimal](9, 2) NOT NULL,
	[moneda] [char](20) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[comentario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Recibo] PRIMARY KEY CLUSTERED 
(
	[idRecibo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Recibo] ADD  CONSTRAINT [DF_Recibo_fecha]  DEFAULT (getdate()) FOR [fecha]
GO
/****** Object:  StoredProcedure [dbo].[spAdministrar_Recibos]    Script Date: 04/03/2021 04:33:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAdministrar_Recibos]
@proveedor varchar(50) = null,
@monto decimal(9,2) = null,
@moneda char(20) = null,
@comentario varchar(50) = null,
@opcion char(15) = null,
@id int = null
AS 
BEGIN
	IF @opcion='Insertar'
	BEGIN
		INSERT INTO Recibo
				(
				PROVEEDOR,
				MONTO,
				MONEDA,
				COMENTARIO
				)
		VALUES (
				@proveedor,
				@monto,
				@moneda,
				@comentario
				)
	END

	ELSE IF @opcion='Actualizar'
	BEGIN
		UPDATE Recibo
			   SET PROVEEDOR=@proveedor,
				   MONTO=@monto,
				   MONEDA=@moneda,
				   COMENTARIO=@comentario
			   WHERE idRecibo=@id
	END
	ELSE IF @opcion='Borrar'
	BEGIN
			DELETE 
			FROM Recibo
		    WHERE idRecibo=@id
	END
	ELSE IF @opcion='Ultimo'
	BEGIN
			SELECT TOP 1 MAX(idRecibo)+1 AS idRecibo, proveedor, monto, moneda, fecha, comentario 
			FROM Recibo
			GROUP BY idRecibo, proveedor, monto, moneda, fecha, comentario
			ORDER BY idRecibo DESC
	END
	ELSE
	BEGIN
		SELECT *
		FROM Recibo
	END
	
END
GO
