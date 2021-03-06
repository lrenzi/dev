USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarioPerfil]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Renzi
-- Create date: 24/12/2016
-- Description:	Consulta dados da tabela usuario perfil
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarUsuarioPerfil] 
	@pUsuPerfilId int = NULL,
	@pClienteId int = NULL,
	@pUsuPerfilNome varchar(50) = NULL,
	@pUsuPerfilDescricao varchar(200) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT  [UsuPerfilId]
		  ,[ClienteId]
		  ,[UsuPerfilNome]
		  ,[UsuPerfilDescricao]
	  FROM [dbo].[UsuarioPerfil]
	  WHERE
	  ([UsuPerfilId] = @pUsuPerfilId OR @pUsuPerfilId IS NULL) AND
	  ([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
	  ([UsuPerfilNome] = @pUsuPerfilNome OR @pUsuPerfilNome IS NULL) AND
	  ([UsuPerfilDescricao] = @pUsuPerfilDescricao OR @pUsuPerfilDescricao IS NULL)
END

GO
