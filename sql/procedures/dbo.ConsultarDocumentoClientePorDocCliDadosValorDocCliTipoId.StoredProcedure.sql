USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley klein
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente por contrato e tipo de arquivo
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId] 
(	
	@pDocCliTipoId int = NULL,
	@pDocCliDadosValor varchar(100) = NULL
)
AS
BEGIN
	

	SELECT DocCliente.[DocClienteId]
		  ,DocCliente.[UsuarioId]
		  ,DocCliente.[DocClienteNomeArquivoSalvo]
		  ,DocCliente.[DocClienteDataUpload]
		  ,DocCliente.[DocCliSituId]
		  ,DocCliente.[ClienteId]
		  ,DocCliente.[DocCliTipoId]
		  ,DocCliente.[DocClienteNomeArquivoOriginal]
		  ,DocCliente.[DocClienteTipoArquivo]
	  FROM [dbo].[DocumentoCliente] as DocCliente
			INNER JOIN [dbo].[DocumentoClienteDadosDoc] as DocCLienteDadosDoc on
			DocCLienteDadosDoc.DocClienteId = DocCliente.DocClienteId
			INNER JOIN [dbo].[DocumentoClienteDados] as DocClienteDados on
			DocClienteDados.DocCliDadosId = DocCLienteDadosDoc.DocCliDadosId
	  WHERE 
			(ltrim(rtrim([DocCliDadosValor])) = ltrim(rtrim(@pDocCliDadosValor)) OR @pDocCliDadosValor IS NULL) 
			AND ([DocCliTipoId] = @pDocCliTipoId OR @pDocCliTipoId IS NULL) 


END
GO
