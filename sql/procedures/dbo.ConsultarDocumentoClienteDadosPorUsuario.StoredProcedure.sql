USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteDadosPorUsuario]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Kurten
-- Create date: 14/dez/2016
-- Description:	Seleciona as chaves de amarracao de documentos por cliente e usuário
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClienteDadosPorUsuario] 
(	
	@pClienteId int = null,
	@pUsuarioId bigint = null,
	@pDocCliDadosValor varchar(100) = null
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT DISTINCT
		docCliDado.[DocCliDadosId],
		docCliDado.[DocCliDadosValor],
		docCliDado.[ClienteId],
		docCliDado.[TipoInfoCliId]
	FROM 
		[dbo].[DocumentoClienteDados] docCliDado,
		[dbo].[DocumentoClienteDadosDoc] docCliDadoDoc,
		[dbo].[DocumentoCliente] docCli
	WHERE 
		docCliDado.[ClienteId] = @pClienteId AND
		docCliDado.[DocCliDadosId] = docCliDadoDoc.[DocCliDadosId] AND
		docCliDadoDoc.[DocClienteId] = docCli.[DocClienteId] AND
		docCli.[UsuarioId] = @pUsuarioId AND
		docCliDado.[DocCliDadosValor] = ISNULL(@pDocCliDadosValor, docCliDado.[DocCliDadosValor])

END

GO
