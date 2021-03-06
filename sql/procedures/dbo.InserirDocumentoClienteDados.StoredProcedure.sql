USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[InserirDocumentoClienteDados]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 14/dez/16
-- Description:	Realiza o insert do valor da informação que irá amarrar os documentos para os cliente
--
--				******* ATENÇÂO ******* 
--				Não deve ser feito insert de nenhum dado com pontuação na coluna [DocCliDadosValor],
--				a pontuação deve ser tratada em client ou em server
-- =============================================
CREATE PROCEDURE [dbo].[InserirDocumentoClienteDados] 
(	
	@pDocCliDadosValor varchar(100),
	@pClienteId int,
	@pTipoInfoCliId int
)
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT INTO
		[dbo].[DocumentoClienteDados]
		(
			[DocCliDadosValor],
			[ClienteId],
			[TipoInfoCliId]
		)
	VALUES
		(
			@pDocCliDadosValor,
			@pClienteId,
			@pTipoInfoCliId
		)

	SELECT @@IDENTITY AS DocCliDadosId

END
GO
