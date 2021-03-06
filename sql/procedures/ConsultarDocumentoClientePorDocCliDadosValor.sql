USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarNumeroPropostaPorUsuario]    Script Date: 12/19/2016 11:48:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Kurten
-- Create date: 14/dez/2016
-- Description:	Consultar informações do documento por numero da proposta
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClientePorDocCliDadosValor] 
(	
	@pDocCliDadosValor varchar(100)
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		 DocCliente.[DocClienteId]
		,DocCliente.[UsuarioId]
		,DocCliente.[DocClienteNomeArquivoSalvo]
		,DocCliente.[DocClienteDataUpload]
		,DocCliente.[DocCliSituId]
		,DocCliente.[ClienteId]
		,DocCliente.[DocCliTipoId]
		,DocCliente.[DocClienteNomeArquivoOriginal]
		,DocCliente.[DocClienteTipoArquivo]
	FROM DocumentoClienteDados AS DocClienteDados
		INNER JOIN DocumentoClienteDadosDoc AS  DocClienteDadosDoc ON
			DocClienteDadosDoc.DocCliDadosId = DocClienteDados.DocCliDadosId
		INNER JOIN DocumentoCliente AS DocCliente ON
			DocClienteDadosDoc.DocClienteId = DocCliente.DocClienteId
		WHERE LTRIM(RTRIM(DocCliDadosValor)) = LTRIM(RTRIM(@pDocCliDadosValor))
			
END
