USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoCliente]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoCliente] 
(	
	@pDocClienteId bigint = NULL,
	@pUsuarioId bigint = NULL,
	@pDocClienteNomeArquivoSalvo varchar(300) = NULL,
	@pDocClienteDataUpload datetime = NULL,
	@pDocCliSituId int = NULL,
	@pClienteId int = NULL,
	@pDocCliTipoId int = NULL,
	@pDocClienteNomeArquivoOriginal varchar(300) = NULL,
	@pDocClienteTipoArquivo varchar(50) = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[DocClienteId],
		[UsuarioId],
		[DocClienteNomeArquivoSalvo],
		[DocClienteDataUpload],
		[DocCliSituId],
		[ClienteId],
		[DocCliTipoId],
		[DocClienteNomeArquivoOriginal],
		[DocClienteTipoArquivo]
	FROM 
		[dbo].[DocumentoCliente]
	WHERE 
		([DocClienteId] = @pDocClienteId OR @pDocClienteId IS NULL) AND
		([UsuarioId] = @pUsuarioId OR @pUsuarioId IS NULL) AND
		([DocClienteNomeArquivoSalvo] = @pDocClienteNomeArquivoSalvo OR @pDocClienteNomeArquivoSalvo IS NULL) AND
		([DocClienteDataUpload] = @pDocClienteDataUpload OR @pDocClienteDataUpload IS NULL) AND
		([DocCliSituId] = @pDocCliSituId OR @pDocCliSituId IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([DocCliTipoId] = @pDocCliTipoId OR @pDocCliTipoId IS NULL) AND
		([DocClienteNomeArquivoOriginal] = @pDocClienteNomeArquivoOriginal OR @pDocClienteNomeArquivoOriginal IS NULL) AND 
		([DocClienteTipoArquivo] = @pDocClienteTipoArquivo OR @pDocClienteTipoArquivo IS NULL)

END
GO
