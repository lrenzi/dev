USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoCliente]    Script Date: 12/19/2016 10:59:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClienteDados] 
(	
	@pDocCliDadosId bigint = NULL,
	@pDocCliDadosValor varchar(100) = NULL,
	@pClienteId int = NULL,
	@pTipoInfoCliId int = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[DocCliDadosId],
		[DocCliDadosValor],
		[ClienteId],
		[TipoInfoCliId]
	FROM 
		[dbo].[DocumentoClienteDados]
	WHERE 
		([DocCliDadosId] = @pDocCliDadosId OR @pDocCliDadosId IS NULL) AND
		([DocCliDadosValor] = @pDocCliDadosValor OR @pDocCliDadosValor IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([TipoInfoCliId] = @pTipoInfoCliId OR @pTipoInfoCliId IS NULL) 

END