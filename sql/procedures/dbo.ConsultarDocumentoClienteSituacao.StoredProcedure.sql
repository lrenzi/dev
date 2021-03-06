USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteSituacao]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Kurten
-- Create date: 14/dez/2016
-- Description:	Consulta os tipos de situação dos documentos de um cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClienteSituacao] 
(	
	@pDocCliSituId int = null,
	@pDocCliSituDescricao varchar(200) = null,
	@pDocCliTipoId int = null,
	@pDocCliSituOrdemApresent smallint = null
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[DocCliSituId],
		[DocCliSituDescricao],
		[DocCliTipoId],
		[DocCliSituOrdemApresent],
		[DocObrigatorio]
	FROM 
		[dbo].[DocumentoClienteSituacao]
	WHERE 
		([DocCliSituId] = @pDocCliSituId OR @pDocCliSituId IS NULL) AND
		([DocCliSituDescricao] = @pDocCliSituDescricao OR @pDocCliSituDescricao IS NULL) AND
		([DocCliTipoId] = @pDocCliTipoId OR @pDocCliTipoId IS NULL) AND
		([DocCliSituOrdemApresent] = @pDocCliSituOrdemApresent OR @pDocCliSituOrdemApresent IS NULL)

END

GO
