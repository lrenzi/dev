USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[UsuarioPerfil]    Script Date: 03/01/2017 14:32:10 ******/
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
ALTER TABLE [dbo].[UsuarioPerfil]  WITH CHECK ADD  CONSTRAINT [Cliente_UsuarioPerfilL_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[UsuarioPerfil] CHECK CONSTRAINT [Cliente_UsuarioPerfilL_fk]
GO
