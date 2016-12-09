-- =============================================================================================
-- Create Stored Procedure Template for Azure SQL Database and Azure SQL Data Warehouse Database
-- =============================================================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ConsultarDocumentoClienteTipoPorCliente 
(	-- Add the parameters for the stored procedure here
	@ClienteId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [DocCliTipoId]
			,[ClienteId]
			,[DocCliTipoNome]
			,[DocCliTipoDescricao]
			,[DocCliTipoOrdemApresent]
		FROM [dbo].[DocumentoClienteTipo] AS [DocumentoClienteTipo]
	WHERE [DocumentoClienteTipo].[ClienteId] = @ClienteId

END
GO
