USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClientePorValorDado]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Kurten
-- Create date: 15/dez/2016
-- Description:	Consulta os documentos existentes para um determinado valor de amarração dos documentos
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClientePorValorDado] 
(	
	@pClienteId int = null,
	@pTipoInfoCliId int = null,
	@pDocCliDadosValor varchar(100) = null
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT
		doc.[DocClienteId],
		doc.[UsuarioId],
		doc.[DocClienteNomeArquivoSalvo],
		doc.[DocClienteDataUpload],
		doc.[DocCliSituId],
		doc.[ClienteId],
		doc.[DocCliTipoId],
		doc.[DocClienteNomeArquivoOriginal],
		doc.[DocClienteTipoArquivo],

		situDoc.[DocCliSituId],
		situDoc.[DocCliSituDescricao],
		situDoc.[DocCliSituOrdemApresent],

		tipoDoc.[DocCliTipoId],
		tipoDoc.[DocCliTipoNome],
		tipoDoc.[DocCliTipoOrdemApresent]
		
		
		
	FROM
		[dbo].[DocumentoClienteSituacao] AS situDoc,
		[dbo].[DocumentoClienteTipo] AS tipoDoc

	INNER JOIN [dbo].[DocumentoClienteDados] AS docCliDado
		ON	docCliDado.[DocCliDadosValor] = @pDocCliDadosValor AND 
			docCliDado.[TipoInfoCliId] = @pTipoInfoCliId
		
	INNER JOIN [dbo].[DocumentoClienteDadosDoc] AS docCliDadoDoc
		ON	docCliDadoDoc.[DocCliDadosId] = docCliDado.[DocCliDadosId]

	INNER JOIN [dbo].[DocumentoCliente] AS doc 
		ON	doc.[DocClienteId] = docCliDadoDoc.[DocClienteId] 

	WHERE
		situDoc.[DocCliTipoId] = tipoDoc.[DocCliTipoId] AND
		doc.[DocCliSituId] = situDoc.[DocCliSituId] AND
		tipoDoc.[ClienteId] = @pClienteId

	ORDER BY
		tipoDoc.[DocCliTipoOrdemApresent] ASC, situDoc.[DocCliSituOrdemApresent] ASC



	--Guardando outra forma de consulta abaixo para medir diferença de performance posteriormente
	--SELECT
	--	tipoDoc.[DocCliTipoId],
	--	tipoDoc.[DocCliTipoNome],
	--	tipoDoc.[DocCliTipoOrdemApresent],
	--	situDoc.[DocCliSituId],
	--	situDoc.[DocCliSituDescricao],
	--	situDoc.[DocCliSituOrdemApresent],
	--	doc.[DocClienteId],
	--	doc.[UsuarioId],
	--	doc.[DocClienteNomeArquivoSalvo],
	--	doc.[DocClienteDataUpload],
	--	doc.[DocCliSituId],
	--	doc.[ClienteId],
	--	doc.[DocCliTipoId],
	--	doc.[DocClienteNomeArquivoOriginal],
	--	doc.[DocClienteTipoArquivo]
	--FROM
	--	[dbo].[DocumentoCliente] AS doc ,
	--	[dbo].[DocumentoClienteSituacao] AS situDoc,
	--	[dbo].[DocumentoClienteTipo] AS tipoDoc,
	--	[dbo].[DocumentoClienteDados] AS docCliDado,
	--	[dbo].[DocumentoClienteDadosDoc] AS docCliDadoDoc	

	--WHERE
	--	situDoc.[DocCliTipoId] = tipoDoc.[DocCliTipoId] AND
	--	doc.[DocCliSituId] = situDoc.[DocCliSituId] AND

	--	doc.[DocClienteId] = docCliDadoDoc.[DocClienteId] AND
	--	docCliDadoDoc.[DocCliDadosId] = docCliDado.[DocCliDadosId] AND
		
	--	doc.[ClienteId] = @pClienteId AND
	--	docCliDado.[DocCliDadosValor] = @pDocCliDadosValor AND 
	--	docCliDado.[TipoInfoCliId] = @pTipoInfoCliId
		
	--ORDER BY
	--	tipoDoc.[DocCliTipoOrdemApresent] ASC, situDoc.[DocCliSituOrdemApresent] ASC

END

GO
