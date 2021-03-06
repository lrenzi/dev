USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[ManterUsuario]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 27/dez/16
-- Description:	Realiza o insert ou update de um registro na tabela de usuario
-- =============================================
CREATE PROCEDURE [dbo].[ManterUsuario] 
(	
	@pUsuarioLogin varchar(10) = NULL,
	@pUsuarioNome varchar(200) = NULL,
	@pUsuarioEmail varchar(100) = NULL,
	@pUsuarioSenha varchar(50) = NULL,
	@pUsuarioAtivo bit = NULL,
	@pUsuPerfilId int = NULL,
	@pClienteId int  = NULL,
	@pUsuarioId bigint = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	-- Tenta executar um update
	UPDATE
		[dbo].[Usuario]
	SET
		[UsuarioLogin] = ISNULL(@pUsuarioLogin, UsuarioLogin),
		[UsuarioNome] = ISNULL(@pUsuarioNome, UsuarioNome),
		[UsuarioEmail] = ISNULL(@pUsuarioEmail, UsuarioEmail),
		[UsuarioSenha] = ISNULL(@pUsuarioSenha, UsuarioSenha),
		[UsuarioAtivo] = ISNULL(@pUsuarioAtivo, UsuarioAtivo),
		[UsuPerfilId] = ISNULL(@pUsuPerfilId, UsuPerfilId),
		[ClienteId] = ISNULL(@pClienteId, ClienteId)
		
	WHERE
		[UsuarioId] = @pUsuarioId

	-- Verifica se algum registro foi alterado, caso não, irá realizar o insert
	IF (@@ROWCOUNT <= 0)
	BEGIN

		INSERT INTO [dbo].[Usuario]
           ([UsuarioLogin]
           ,[UsuarioNome]
           ,[UsuarioEmail]
           ,[UsuarioSenha]
           ,[UsuarioAtivo]
           ,[UsuPerfilId]
           ,[ClienteId])
     VALUES
           (@pUsuarioLogin
           ,@pUsuarioNome
           ,@pUsuarioEmail
           ,@pUsuarioSenha
           ,@pUsuarioAtivo
           ,@pUsuPerfilId
           ,@pClienteId)

		
		SELECT @@IDENTITY AS [UsuarioId]
		
	END

END
GO
