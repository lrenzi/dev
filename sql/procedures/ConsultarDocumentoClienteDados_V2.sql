USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteDados]    Script Date: 12/20/2016 7:59:52 PM ******/
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
		(ltrim(rtrim([DocCliDadosValor])) = ltrim(rtrim(@pDocCliDadosValor)) OR @pDocCliDadosValor IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([TipoInfoCliId] = @pTipoInfoCliId OR @pTipoInfoCliId IS NULL) 

END