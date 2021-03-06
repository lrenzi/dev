USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 03/01/2017 14:32:10 ******/
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
