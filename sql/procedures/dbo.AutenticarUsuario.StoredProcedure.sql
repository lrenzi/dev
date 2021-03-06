USE [bsidbdesenv]
GO
/****** Object:  StoredProcedure [dbo].[AutenticarUsuario]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 13/dez/16
-- Description:	Realiza a autenticação de usuário por login e senha (md5 hash)
-- Alter Data:	19/dez/16
-- Reason:		Alterado a forma de retorno do status e mensagem de processamento de output parameter para result set
-- =============================================
CREATE PROCEDURE [dbo].[AutenticarUsuario] 
(	
	@UsuarioLogin varchar(10),
	@UsuarioSenha varchar(32)
)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @usuid bigint
	DECLARE @ativo bit

	-- Efetua o select para verificar se encontra usuário e senha
	SELECT 
		@usuid = usu.[UsuarioId],
		@ativo = usu.[UsuarioAtivo]
	FROM 
		[dbo].[Usuario] as usu
	WHERE	
		usu.[UsuarioLogin] = @UsuarioLogin AND
		usu.[UsuarioSenha] = @UsuarioSenha

	-- Verifica se o usuário existe
	IF (@usuid IS NULL OR @usuid = '')	
		BEGIN

		-- Caso não encontre o match de Login e Senha
		SELECT 
			2 AS StatusProcessamento,
			'Usuário ou senha inválido' AS MensagemProcessamento
		
		END
	ELSE
		BEGIN
		IF (@ativo = 0) 
			BEGIN
			
			-- Caso encontre o match de Login e Senha
			-- Caso não encontre o match de Login e Senha
			SELECT 
				1 AS StatusProcessamento,
				'Usuário inativo, entre em contato com um administrador.' AS MensagemProcessamento

			END
		ELSE
			BEGIN
			
			SELECT 
				0 AS StatusProcessamento,
				'OK' AS MensagemProcessamento
			
			SELECT
				Usuario.[UsuarioId],
				Usuario.[UsuarioLogin],
				Usuario.[UsuarioNome],
				Usuario.[UsuarioEmail],
				UsuarioPerfil.[UsuPerfilId],
				UsuarioPerfil.[UsuPerfilNome],
				UsuarioPerfil.[UsuPerfilDescricao],
				Cliente.[ClienteId],
				Cliente.[ClienteNome],
				Cliente.[ClientePastaDocumentos],
				Cliente.[ClienteImagemLogoDesktop],
				Cliente.[ClienteImagemLogoMobile],
				Cliente.[ClienteCorPadrao]
			FROM
				[dbo].[Usuario] as Usuario,
				[dbo].[Cliente] as Cliente,
				[dbo].[UsuarioPerfil] as UsuarioPerfil
			WHERE
				Usuario.ClienteId = Cliente.ClienteId and
				Usuario.[UsuPerfilId] = UsuarioPerfil.[UsuPerfilId] and
				Usuario.[UsuarioId] = @usuid

			END

		END
	

END

GO
