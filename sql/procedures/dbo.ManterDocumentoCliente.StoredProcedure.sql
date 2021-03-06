USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ManterDocumentoCliente]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 14/dez/16
-- Description:	Realiza o insert ou update de um registro na tabela de documento cliente 
-- =============================================
CREATE PROCEDURE [dbo].[ManterDocumentoCliente] 
(	
	@pDocClienteId bigint = NULL,
	@pUsuarioId bigint = NULL,
	@pDocClienteNomeArquivoSalvo varchar(300) = NULL,
	@pDocClienteDataUpload datetime = NULL,
	@pDocCliSituId int = NULL,
	@pClienteId int = NULL,
	@pDocCliTipoId int  = NULL,
	@pDocClienteNomeArquivoOriginal varchar(300) = NULL,
	@pDocClienteTipoArquivo varchar(50) = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	-- Tenta executar um update
	UPDATE
		[dbo].[DocumentoCliente]
	SET
		[UsuarioId] = ISNULL(@pUsuarioId, UsuarioId),
		[DocClienteNomeArquivoSalvo] = ISNULL(@pDocClienteNomeArquivoSalvo, DocClienteNomeArquivoSalvo),
		[DocClienteDataUpload] = ISNULL(@pDocClienteDataUpload, DocClienteDataUpload),
		[DocCliSituId] = ISNULL(@pDocCliSituId, DocCliSituId),
		[ClienteId] = ISNULL(@pClienteId, ClienteId),
		[DocCliTipoId] = ISNULL(@pDocCliTipoId, DocCliTipoId),
		[DocClienteNomeArquivoOriginal] = ISNULL(@pDocClienteNomeArquivoOriginal, DocClienteNomeArquivoOriginal),
		[DocClienteTipoArquivo] = ISNULL(@pDocClienteTipoArquivo, DocClienteTipoArquivo)
	WHERE
		[DocClienteId] = @pDocClienteId

	-- Verifica se algum registro foi alterado, caso não, irá realizar o insert
	IF (@@ROWCOUNT <= 0)
	BEGIN

		INSERT INTO
			[dbo].[DocumentoCliente]
			(
				[UsuarioId],
				[DocClienteNomeArquivoSalvo],
				[DocClienteDataUpload],
				[DocCliSituId],
				[ClienteId],
				[DocCliTipoId],
				[DocClienteNomeArquivoOriginal],
				[DocClienteTipoArquivo]
			)
		VALUES
			(
				@pUsuarioId,
				@pDocClienteNomeArquivoSalvo,
				@pDocClienteDataUpload,
				@pDocCliSituId,
				@pClienteId,
				@pDocCliTipoId,
				@pDocClienteNomeArquivoOriginal,
				@pDocClienteTipoArquivo
			)
		
		SELECT @@IDENTITY AS [DocClienteId]
		
	END

END
GO
