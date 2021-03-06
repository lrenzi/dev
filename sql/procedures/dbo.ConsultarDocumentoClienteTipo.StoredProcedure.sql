USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteTipo]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Klein
-- Create date: 13/dez/2016
-- Description:	Consulta os tipos de documentos de um cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClienteTipo] 
(	
	@DocCliTipoId int = null,
	@ClienteId int = null,
	@DocCliTipoNome varchar = null,
	@DocCliTipoDescricao varchar = null,
	@DocCliTipoOrdemApresent smallint = null
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [DocCliTipoId]
			,[ClienteId]
			,[DocCliTipoNome]
			,[DocCliTipoDescricao]
			,[DocCliTipoOrdemApresent]
		FROM [dbo].[DocumentoClienteTipo] AS [DocumentoClienteTipo]
	WHERE 
	([DocCliTipoId] = @DocCliTipoId or @DocCliTipoId is null) and 
	([ClienteId] = @ClienteId or @ClienteId is null) and
	([DocCliTipoNome] = @DocCliTipoNome or @DocCliTipoNome is null) and 
	([DocCliTipoDescricao] = @DocCliTipoDescricao or @DocCliTipoDescricao is null) and 
	([DocCliTipoOrdemApresent] = @DocCliTipoOrdemApresent or @DocCliTipoOrdemApresent is null) 

END

GO
