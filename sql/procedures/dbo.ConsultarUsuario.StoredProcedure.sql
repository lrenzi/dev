USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuario]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Renzi
-- Create date: 24/12/2016
-- Description:	Consulta dados da tabela usuario 
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarUsuario] 
	@pUsuarioId int = NULL,
	@pUsuarioLogin varchar(10) = NULL,
	@pUsuarioNome varchar(200) = NULL,
	@pUsuarioEmail varchar(100) = NULL,
	@pUsuarioSenha varchar(50) = NULL,
	@pUsuarioAtivo bit = NULL,
	@pUsuPerfilId int = NULL,
	@pClienteId int = NULL
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	   U.[UsuarioId]	
      ,U.[UsuarioLogin]
      ,U.[UsuarioNome]
      ,U.[UsuarioEmail]
      ,U.[UsuarioSenha]
      ,U.[UsuarioAtivo]
      ,U.[UsuPerfilId]
      ,U.[ClienteId]
	  ,P.[UsuPerfilNome]
  FROM [dbo].[Usuario] AS U
  INNER JOIN [dbo].[UsuarioPerfil] AS P
	  ON(U.UsuPerfilId = P.UsuPerfilId)
	  WHERE
	  (U.[UsuarioLogin] = @pUsuarioLogin OR @pUsuarioLogin IS NULL) AND
	  (U.[UsuarioNome] = @pUsuarioNome OR @pUsuarioNome IS NULL) AND
	  (U.[UsuarioEmail] = @pUsuarioEmail OR @pUsuarioEmail IS NULL) AND
	  (U.[UsuarioSenha] = @pUsuarioSenha OR @pUsuarioSenha IS NULL) AND
	  (U.[UsuarioAtivo] = @pUsuarioAtivo OR @pUsuarioAtivo IS NULL) AND
	  (U.[UsuPerfilId] = @pUsuPerfilId OR @pUsuPerfilId IS NULL) AND
	  (U.[ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
	  (U.[UsuarioId] = @pUsuarioId OR @pUsuarioId IS NULL) 
	  
END

GO
