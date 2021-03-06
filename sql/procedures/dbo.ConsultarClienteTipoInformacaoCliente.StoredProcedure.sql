USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarClienteTipoInformacaoCliente]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarClienteTipoInformacaoCliente] 
(	
	@pCliTipoInfoCliId int = NULL,
	@pClienteId int = NULL,
	@pTipoInfoCliId int = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[CliTipoInfoCliId],
		[ClienteId],
		[TipoInfoCliId]
	FROM 
		[dbo].[ClienteTipoInformacaoCliente]
	WHERE 
		([CliTipoInfoCliId] = @pCliTipoInfoCliId OR @pCliTipoInfoCliId IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([TipoInfoCliId] = @pTipoInfoCliId OR @pTipoInfoCliId IS NULL) 

END
GO
