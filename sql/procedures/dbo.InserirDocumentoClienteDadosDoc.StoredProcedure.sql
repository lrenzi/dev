USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[InserirDocumentoClienteDadosDoc]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 14/dez/16
-- Description:	Realiza o insert para relacionar as tabelas DocumentoClienteDados e DocumentoCliente
-- =============================================
CREATE PROCEDURE [dbo].[InserirDocumentoClienteDadosDoc] 
(	
	@pDocCliDadosId bigint,
	@pDocClienteId bigint
)
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT INTO
		[dbo].[DocumentoClienteDadosDoc]
		(
			[DocCliDadosId],
			[DocClienteId]
		)
	VALUES
		(
			@pDocCliDadosId,
			@pDocClienteId
		)

	SELECT @@IDENTITY AS [DocCliDadosDocId]

END
GO
