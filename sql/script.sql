USE [master]
GO
/****** Object:  Database [bsidbdesenv]    Script Date: 03/01/2017 14:24:59 ******/
CREATE DATABASE [bsidbdesenv]
GO
ALTER DATABASE [bsidbdesenv] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bsidbdesenv].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bsidbdesenv] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bsidbdesenv] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bsidbdesenv] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bsidbdesenv] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bsidbdesenv] SET ARITHABORT OFF 
GO
ALTER DATABASE [bsidbdesenv] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bsidbdesenv] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bsidbdesenv] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bsidbdesenv] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bsidbdesenv] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bsidbdesenv] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bsidbdesenv] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bsidbdesenv] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bsidbdesenv] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bsidbdesenv] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bsidbdesenv] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [bsidbdesenv] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bsidbdesenv] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [bsidbdesenv] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bsidbdesenv] SET  MULTI_USER 
GO
ALTER DATABASE [bsidbdesenv] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bsidbdesenv] SET QUERY_STORE = ON
GO
ALTER DATABASE [bsidbdesenv] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [bsidbdesenv]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_diagramobjects]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE FUNCTION [dbo].[fn_diagramobjects]() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
	
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteNome] [varchar](60) NOT NULL,
	[ClientePastaDocumentos] [varchar](8) NOT NULL,
	[ClienteImagemLogoDesktop] [varchar](30) NOT NULL,
	[ClienteImagemLogoMobile] [varchar](30) NOT NULL,
	[ClienteCorPadrao] [varchar](15) NOT NULL,
 CONSTRAINT [ClienteId] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ClienteTipoInformacaoCliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClienteTipoInformacaoCliente](
	[CliTipoInfoCliId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[TipoInfoCliId] [int] NOT NULL,
 CONSTRAINT [CliTipoInfoCliId] PRIMARY KEY CLUSTERED 
(
	[CliTipoInfoCliId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DocumentoCliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentoCliente](
	[DocClienteId] [bigint] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [bigint] NOT NULL,
	[DocClienteNomeArquivoSalvo] [varchar](300) NOT NULL,
	[DocClienteDataUpload] [datetime] NOT NULL,
	[DocCliSituId] [int] NOT NULL,
	[ClienteId] [int] NOT NULL,
	[DocCliTipoId] [int] NOT NULL,
	[DocClienteNomeArquivoOriginal] [varchar](300) NOT NULL,
	[DocClienteTipoArquivo] [varchar](50) NOT NULL,
 CONSTRAINT [DocClienteId] PRIMARY KEY CLUSTERED 
(
	[DocClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DocumentoClienteDados]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentoClienteDados](
	[DocCliDadosId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocCliDadosValor] [varchar](100) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[TipoInfoCliId] [int] NOT NULL,
 CONSTRAINT [DocCliDadosId] PRIMARY KEY CLUSTERED 
(
	[DocCliDadosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DocumentoClienteDadosDoc]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentoClienteDadosDoc](
	[DocCliDadosDocId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocCliDadosId] [bigint] NOT NULL,
	[DocClienteId] [bigint] NOT NULL,
 CONSTRAINT [DocCliDadosDocId] PRIMARY KEY CLUSTERED 
(
	[DocCliDadosDocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DocumentoClienteSituacao]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentoClienteSituacao](
	[DocCliSituId] [int] IDENTITY(1,1) NOT NULL,
	[DocCliSituDescricao] [varchar](200) NOT NULL,
	[DocCliTipoId] [int] NOT NULL,
	[DocCliSituOrdemApresent] [smallint] NOT NULL,
	[DocObrigatorio] [bit] NOT NULL,
 CONSTRAINT [DocCliSituacaoId] PRIMARY KEY CLUSTERED 
(
	[DocCliSituId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[DocumentoClienteTipo]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentoClienteTipo](
	[DocCliTipoId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[DocCliTipoNome] [varchar](50) NOT NULL,
	[DocCliTipoDescricao] [varchar](200) NOT NULL,
	[DocCliTipoOrdemApresent] [smallint] NOT NULL,
 CONSTRAINT [DocCliTipoId] PRIMARY KEY CLUSTERED 
(
	[DocCliTipoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
 CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[TipoInformacaoCliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoInformacaoCliente](
	[TipoInfoCliId] [int] IDENTITY(1,1) NOT NULL,
	[TipoInfoCliDescricao] [varchar](30) NOT NULL,
 CONSTRAINT [TipoInfoCliId] PRIMARY KEY CLUSTERED 
(
	[TipoInfoCliId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Token]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[TokenId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [varchar](128) NOT NULL,
	[IdentityName] [varchar](50) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ClienteId] [int] NOT NULL,
	[ProtectedTicket] [varchar](max) NOT NULL,
 CONSTRAINT [TokenId] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [bigint] IDENTITY(1,1) NOT NULL,
	[UsuarioLogin] [varchar](10) NOT NULL,
	[UsuarioNome] [varchar](200) NOT NULL,
	[UsuarioEmail] [varchar](100) NOT NULL,
	[UsuarioSenha] [varchar](50) NOT NULL,
	[UsuarioAtivo] [bit] NOT NULL,
	[UsuPerfilId] [int] NOT NULL,
	[ClienteId] [int] NOT NULL,
 CONSTRAINT [UsuarioId] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UsuarioPerfil]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioPerfil](
	[UsuPerfilId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[UsuPerfilNome] [varchar](50) NOT NULL,
	[UsuPerfilDescricao] [varchar](200) NOT NULL,
 CONSTRAINT [UsuPerfilId] PRIMARY KEY CLUSTERED 
(
	[UsuPerfilId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[DocumentoClienteSituacao] ADD  DEFAULT ((0)) FOR [DocObrigatorio]
GO
ALTER TABLE [dbo].[ClienteTipoInformacaoCliente]  WITH CHECK ADD  CONSTRAINT [Cliente_ClienteTipoInformacaoCliente_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[ClienteTipoInformacaoCliente] CHECK CONSTRAINT [Cliente_ClienteTipoInformacaoCliente_fk]
GO
ALTER TABLE [dbo].[ClienteTipoInformacaoCliente]  WITH CHECK ADD  CONSTRAINT [TipoInformacaoCliente_ClienteTipoInformacaoCliente_fk] FOREIGN KEY([TipoInfoCliId])
REFERENCES [dbo].[TipoInformacaoCliente] ([TipoInfoCliId])
GO
ALTER TABLE [dbo].[ClienteTipoInformacaoCliente] CHECK CONSTRAINT [TipoInformacaoCliente_ClienteTipoInformacaoCliente_fk]
GO
ALTER TABLE [dbo].[DocumentoCliente]  WITH CHECK ADD  CONSTRAINT [Cliente_DocumentoCliente_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[DocumentoCliente] CHECK CONSTRAINT [Cliente_DocumentoCliente_fk]
GO
ALTER TABLE [dbo].[DocumentoCliente]  WITH CHECK ADD  CONSTRAINT [DocumentoClienteSituacao_DocumentoBradescoFinanceira_fk] FOREIGN KEY([DocCliSituId])
REFERENCES [dbo].[DocumentoClienteSituacao] ([DocCliSituId])
GO
ALTER TABLE [dbo].[DocumentoCliente] CHECK CONSTRAINT [DocumentoClienteSituacao_DocumentoBradescoFinanceira_fk]
GO
ALTER TABLE [dbo].[DocumentoCliente]  WITH CHECK ADD  CONSTRAINT [DocumentoClienteTipo_DocumentoCliente_fk] FOREIGN KEY([DocCliTipoId])
REFERENCES [dbo].[DocumentoClienteTipo] ([DocCliTipoId])
GO
ALTER TABLE [dbo].[DocumentoCliente] CHECK CONSTRAINT [DocumentoClienteTipo_DocumentoCliente_fk]
GO
ALTER TABLE [dbo].[DocumentoCliente]  WITH CHECK ADD  CONSTRAINT [Usuario_DocumentoCliente_fk] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[DocumentoCliente] CHECK CONSTRAINT [Usuario_DocumentoCliente_fk]
GO
ALTER TABLE [dbo].[DocumentoClienteDados]  WITH CHECK ADD  CONSTRAINT [Cliente_DocumentoClienteDados_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[DocumentoClienteDados] CHECK CONSTRAINT [Cliente_DocumentoClienteDados_fk]
GO
ALTER TABLE [dbo].[DocumentoClienteDados]  WITH CHECK ADD  CONSTRAINT [TipoInformacaoCliente_DocumentoClienteDados_fk] FOREIGN KEY([TipoInfoCliId])
REFERENCES [dbo].[TipoInformacaoCliente] ([TipoInfoCliId])
GO
ALTER TABLE [dbo].[DocumentoClienteDados] CHECK CONSTRAINT [TipoInformacaoCliente_DocumentoClienteDados_fk]
GO
ALTER TABLE [dbo].[DocumentoClienteDadosDoc]  WITH CHECK ADD  CONSTRAINT [DocumentoCliente_DocumentoClienteDadosDoc_fk] FOREIGN KEY([DocClienteId])
REFERENCES [dbo].[DocumentoCliente] ([DocClienteId])
GO
ALTER TABLE [dbo].[DocumentoClienteDadosDoc] CHECK CONSTRAINT [DocumentoCliente_DocumentoClienteDadosDoc_fk]
GO
ALTER TABLE [dbo].[DocumentoClienteDadosDoc]  WITH CHECK ADD  CONSTRAINT [DocumentoClienteDados_DocumentoClienteDadosDoc_fk] FOREIGN KEY([DocCliDadosId])
REFERENCES [dbo].[DocumentoClienteDados] ([DocCliDadosId])
GO
ALTER TABLE [dbo].[DocumentoClienteDadosDoc] CHECK CONSTRAINT [DocumentoClienteDados_DocumentoClienteDadosDoc_fk]
GO
ALTER TABLE [dbo].[DocumentoClienteSituacao]  WITH CHECK ADD  CONSTRAINT [DocumentoClienteTipo_DocumentoClienteSituacao_fk] FOREIGN KEY([DocCliTipoId])
REFERENCES [dbo].[DocumentoClienteTipo] ([DocCliTipoId])
GO
ALTER TABLE [dbo].[DocumentoClienteSituacao] CHECK CONSTRAINT [DocumentoClienteTipo_DocumentoClienteSituacao_fk]
GO
ALTER TABLE [dbo].[DocumentoClienteTipo]  WITH CHECK ADD  CONSTRAINT [Cliente_DocumentoClienteTipo_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[DocumentoClienteTipo] CHECK CONSTRAINT [Cliente_DocumentoClienteTipo_fk]
GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD  CONSTRAINT [Cliente_Token_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Token] CHECK CONSTRAINT [Cliente_Token_fk]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [Cliente_Usuario_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [Cliente_Usuario_fk]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [UsuarioPerfil_Usuario_fk] FOREIGN KEY([UsuPerfilId])
REFERENCES [dbo].[UsuarioPerfil] ([UsuPerfilId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [UsuarioPerfil_Usuario_fk]
GO
ALTER TABLE [dbo].[UsuarioPerfil]  WITH CHECK ADD  CONSTRAINT [Cliente_UsuarioPerfilL_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[UsuarioPerfil] CHECK CONSTRAINT [Cliente_UsuarioPerfilL_fk]
GO
/****** Object:  StoredProcedure [dbo].[AutenticarUsuario]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarClienteTipoInformacaoCliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarClienteTipoInformacaoCliente] 
(	
	@pCliTipoInfoCliId int = NULL,
	@pClienteId int = NULL,
	@pTipoInfoCliId int = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[CliTipoInfoCliId],
		[ClienteId],
		[TipoInfoCliId]
	FROM 
		[dbo].[ClienteTipoInformacaoCliente]
	WHERE 
		([CliTipoInfoCliId] = @pCliTipoInfoCliId OR @pCliTipoInfoCliId IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([TipoInfoCliId] = @pTipoInfoCliId OR @pTipoInfoCliId IS NULL) 

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoCliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoCliente] 
(	
	@pDocClienteId bigint = NULL,
	@pUsuarioId bigint = NULL,
	@pDocClienteNomeArquivoSalvo varchar(300) = NULL,
	@pDocClienteDataUpload datetime = NULL,
	@pDocCliSituId int = NULL,
	@pClienteId int = NULL,
	@pDocCliTipoId int = NULL,
	@pDocClienteNomeArquivoOriginal varchar(300) = NULL,
	@pDocClienteTipoArquivo varchar(50) = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[DocClienteId],
		[UsuarioId],
		[DocClienteNomeArquivoSalvo],
		[DocClienteDataUpload],
		[DocCliSituId],
		[ClienteId],
		[DocCliTipoId],
		[DocClienteNomeArquivoOriginal],
		[DocClienteTipoArquivo]
	FROM 
		[dbo].[DocumentoCliente]
	WHERE 
		([DocClienteId] = @pDocClienteId OR @pDocClienteId IS NULL) AND
		([UsuarioId] = @pUsuarioId OR @pUsuarioId IS NULL) AND
		([DocClienteNomeArquivoSalvo] = @pDocClienteNomeArquivoSalvo OR @pDocClienteNomeArquivoSalvo IS NULL) AND
		([DocClienteDataUpload] = @pDocClienteDataUpload OR @pDocClienteDataUpload IS NULL) AND
		([DocCliSituId] = @pDocCliSituId OR @pDocCliSituId IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([DocCliTipoId] = @pDocCliTipoId OR @pDocCliTipoId IS NULL) AND
		([DocClienteNomeArquivoOriginal] = @pDocClienteNomeArquivoOriginal OR @pDocClienteNomeArquivoOriginal IS NULL) AND 
		([DocClienteTipoArquivo] = @pDocClienteTipoArquivo OR @pDocClienteTipoArquivo IS NULL)

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteDados]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClienteDados] 
(	
	@pDocCliDadosId bigint = NULL,
	@pDocCliDadosValor varchar(100) = NULL,
	@pClienteId int = NULL,
	@pTipoInfoCliId int = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		[DocCliDadosId],
		[DocCliDadosValor],
		[ClienteId],
		[TipoInfoCliId]
	FROM 
		[dbo].[DocumentoClienteDados]
	WHERE 
		([DocCliDadosId] = @pDocCliDadosId OR @pDocCliDadosId IS NULL) AND
		(ltrim(rtrim([DocCliDadosValor])) = ltrim(rtrim(@pDocCliDadosValor)) OR @pDocCliDadosValor IS NULL) AND
		([ClienteId] = @pClienteId OR @pClienteId IS NULL) AND
		([TipoInfoCliId] = @pTipoInfoCliId OR @pTipoInfoCliId IS NULL) 

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteDadosPorUsuario]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClientePorDocCliDadosValor]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Klein
-- Create date: 14/dez/2016
-- Description:	Consultar informações do documento por numero da proposta
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClientePorDocCliDadosValor] 
(	
	@pDocCliDadosValor varchar(100)
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
		 DocCliente.[DocClienteId]
		,DocCliente.[UsuarioId]
		,DocCliente.[DocClienteNomeArquivoSalvo]
		,DocCliente.[DocClienteDataUpload]
		,DocCliente.[DocCliSituId]
		,DocCliente.[ClienteId]
		,DocCliente.[DocCliTipoId]
		,DocCliente.[DocClienteNomeArquivoOriginal]
		,DocCliente.[DocClienteTipoArquivo]
	FROM DocumentoClienteDados AS DocClienteDados
		INNER JOIN DocumentoClienteDadosDoc AS  DocClienteDadosDoc ON
			DocClienteDadosDoc.DocCliDadosId = DocClienteDados.DocCliDadosId
		INNER JOIN DocumentoCliente AS DocCliente ON
			DocClienteDadosDoc.DocClienteId = DocCliente.DocClienteId
		WHERE LTRIM(RTRIM(DocCliDadosValor)) = LTRIM(RTRIM(@pDocCliDadosValor))
			
END


GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley klein
-- Create date: 15/dez/16
-- Description:	Consulta documento do cliente por contrato e tipo de arquivo
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId] 
(	
	@pDocCliTipoId int = NULL,
	@pDocCliDadosValor varchar(100) = NULL
)
AS
BEGIN
	

	SELECT DocCliente.[DocClienteId]
		  ,DocCliente.[UsuarioId]
		  ,DocCliente.[DocClienteNomeArquivoSalvo]
		  ,DocCliente.[DocClienteDataUpload]
		  ,DocCliente.[DocCliSituId]
		  ,DocCliente.[ClienteId]
		  ,DocCliente.[DocCliTipoId]
		  ,DocCliente.[DocClienteNomeArquivoOriginal]
		  ,DocCliente.[DocClienteTipoArquivo]
	  FROM [dbo].[DocumentoCliente] as DocCliente
			INNER JOIN [dbo].[DocumentoClienteDadosDoc] as DocCLienteDadosDoc on
			DocCLienteDadosDoc.DocClienteId = DocCliente.DocClienteId
			INNER JOIN [dbo].[DocumentoClienteDados] as DocClienteDados on
			DocClienteDados.DocCliDadosId = DocCLienteDadosDoc.DocCliDadosId
	  WHERE 
			(ltrim(rtrim([DocCliDadosValor])) = ltrim(rtrim(@pDocCliDadosValor)) OR @pDocCliDadosValor IS NULL) 
			AND ([DocCliTipoId] = @pDocCliTipoId OR @pDocCliTipoId IS NULL) 


END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClientePorValorDado]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Kurten
-- Create date: 15/dez/2016
-- Description:	Consulta os documentos existentes para um determinado valor de amarração dos documentos
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDocumentoClientePorValorDado] 
(	
	@pClienteId int = null,
	@pTipoInfoCliId int = null,
	@pDocCliDadosValor varchar(100) = null
)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT
		doc.[DocClienteId],
		doc.[UsuarioId],
		doc.[DocClienteNomeArquivoSalvo],
		doc.[DocClienteDataUpload],
		doc.[DocCliSituId],
		doc.[ClienteId],
		doc.[DocCliTipoId],
		doc.[DocClienteNomeArquivoOriginal],
		doc.[DocClienteTipoArquivo],

		situDoc.[DocCliSituId],
		situDoc.[DocCliSituDescricao],
		situDoc.[DocCliSituOrdemApresent],

		tipoDoc.[DocCliTipoId],
		tipoDoc.[DocCliTipoNome],
		tipoDoc.[DocCliTipoOrdemApresent]
		
		
		
	FROM
		[dbo].[DocumentoClienteSituacao] AS situDoc,
		[dbo].[DocumentoClienteTipo] AS tipoDoc

	INNER JOIN [dbo].[DocumentoClienteDados] AS docCliDado
		ON	docCliDado.[DocCliDadosValor] = @pDocCliDadosValor AND 
			docCliDado.[TipoInfoCliId] = @pTipoInfoCliId
		
	INNER JOIN [dbo].[DocumentoClienteDadosDoc] AS docCliDadoDoc
		ON	docCliDadoDoc.[DocCliDadosId] = docCliDado.[DocCliDadosId]

	INNER JOIN [dbo].[DocumentoCliente] AS doc 
		ON	doc.[DocClienteId] = docCliDadoDoc.[DocClienteId] 

	WHERE
		situDoc.[DocCliTipoId] = tipoDoc.[DocCliTipoId] AND
		doc.[DocCliSituId] = situDoc.[DocCliSituId] AND
		tipoDoc.[ClienteId] = @pClienteId

	ORDER BY
		tipoDoc.[DocCliTipoOrdemApresent] ASC, situDoc.[DocCliSituOrdemApresent] ASC



	--Guardando outra forma de consulta abaixo para medir diferença de performance posteriormente
	--SELECT
	--	tipoDoc.[DocCliTipoId],
	--	tipoDoc.[DocCliTipoNome],
	--	tipoDoc.[DocCliTipoOrdemApresent],
	--	situDoc.[DocCliSituId],
	--	situDoc.[DocCliSituDescricao],
	--	situDoc.[DocCliSituOrdemApresent],
	--	doc.[DocClienteId],
	--	doc.[UsuarioId],
	--	doc.[DocClienteNomeArquivoSalvo],
	--	doc.[DocClienteDataUpload],
	--	doc.[DocCliSituId],
	--	doc.[ClienteId],
	--	doc.[DocCliTipoId],
	--	doc.[DocClienteNomeArquivoOriginal],
	--	doc.[DocClienteTipoArquivo]
	--FROM
	--	[dbo].[DocumentoCliente] AS doc ,
	--	[dbo].[DocumentoClienteSituacao] AS situDoc,
	--	[dbo].[DocumentoClienteTipo] AS tipoDoc,
	--	[dbo].[DocumentoClienteDados] AS docCliDado,
	--	[dbo].[DocumentoClienteDadosDoc] AS docCliDadoDoc	

	--WHERE
	--	situDoc.[DocCliTipoId] = tipoDoc.[DocCliTipoId] AND
	--	doc.[DocCliSituId] = situDoc.[DocCliSituId] AND

	--	doc.[DocClienteId] = docCliDadoDoc.[DocClienteId] AND
	--	docCliDadoDoc.[DocCliDadosId] = docCliDado.[DocCliDadosId] AND
		
	--	doc.[ClienteId] = @pClienteId AND
	--	docCliDado.[DocCliDadosValor] = @pDocCliDadosValor AND 
	--	docCliDado.[TipoInfoCliId] = @pTipoInfoCliId
		
	--ORDER BY
	--	tipoDoc.[DocCliTipoOrdemApresent] ASC, situDoc.[DocCliSituOrdemApresent] ASC

END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteSituacao]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarDocumentoClienteTipo]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarUsuario]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarioPerfil]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[InserirDocumentoClienteDados]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[InserirDocumentoClienteDadosDoc]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[ManterDocumentoCliente]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wesley Warmeling Kurten
-- Create date: 14/dez/16
-- Description:	Realiza o insert ou update de um registro na tabela de documento cliente 
-- =============================================
CREATE PROCEDURE [dbo].[ManterDocumentoCliente] 
(	
	@pDocClienteId bigint = NULL,
	@pUsuarioId bigint = NULL,
	@pDocClienteNomeArquivoSalvo varchar(300) = NULL,
	@pDocClienteDataUpload datetime = NULL,
	@pDocCliSituId int = NULL,
	@pClienteId int = NULL,
	@pDocCliTipoId int  = NULL,
	@pDocClienteNomeArquivoOriginal varchar(300) = NULL,
	@pDocClienteTipoArquivo varchar(50) = NULL
)
AS
BEGIN
	
	SET NOCOUNT ON;

	-- Tenta executar um update
	UPDATE
		[dbo].[DocumentoCliente]
	SET
		[UsuarioId] = ISNULL(@pUsuarioId, UsuarioId),
		[DocClienteNomeArquivoSalvo] = ISNULL(@pDocClienteNomeArquivoSalvo, DocClienteNomeArquivoSalvo),
		[DocClienteDataUpload] = ISNULL(@pDocClienteDataUpload, DocClienteDataUpload),
		[DocCliSituId] = ISNULL(@pDocCliSituId, DocCliSituId),
		[ClienteId] = ISNULL(@pClienteId, ClienteId),
		[DocCliTipoId] = ISNULL(@pDocCliTipoId, DocCliTipoId),
		[DocClienteNomeArquivoOriginal] = ISNULL(@pDocClienteNomeArquivoOriginal, DocClienteNomeArquivoOriginal),
		[DocClienteTipoArquivo] = ISNULL(@pDocClienteTipoArquivo, DocClienteTipoArquivo)
	WHERE
		[DocClienteId] = @pDocClienteId

	-- Verifica se algum registro foi alterado, caso não, irá realizar o insert
	IF (@@ROWCOUNT <= 0)
	BEGIN

		INSERT INTO
			[dbo].[DocumentoCliente]
			(
				[UsuarioId],
				[DocClienteNomeArquivoSalvo],
				[DocClienteDataUpload],
				[DocCliSituId],
				[ClienteId],
				[DocCliTipoId],
				[DocClienteNomeArquivoOriginal],
				[DocClienteTipoArquivo]
			)
		VALUES
			(
				@pUsuarioId,
				@pDocClienteNomeArquivoSalvo,
				@pDocClienteDataUpload,
				@pDocCliSituId,
				@pClienteId,
				@pDocCliTipoId,
				@pDocClienteNomeArquivoOriginal,
				@pDocClienteTipoArquivo
			)
		
		SELECT @@IDENTITY AS [DocClienteId]
		
	END

END
GO
/****** Object:  StoredProcedure [dbo].[ManterUsuario]    Script Date: 03/01/2017 14:24:59 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_alterdiagram]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_alterdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_creatediagram]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_creatediagram]
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_dropdiagram]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_dropdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagramdefinition]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagramdefinition]
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagrams]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagrams]
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_renamediagram]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_renamediagram]
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_upgraddiagrams]    Script Date: 03/01/2017 14:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_upgraddiagrams]
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	
GO
USE [master]
GO
ALTER DATABASE [bsidbdesenv] SET  READ_WRITE 
GO
