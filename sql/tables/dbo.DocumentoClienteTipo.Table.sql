USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[DocumentoClienteTipo]    Script Date: 03/01/2017 14:32:10 ******/
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
ALTER TABLE [dbo].[DocumentoClienteTipo]  WITH CHECK ADD  CONSTRAINT [Cliente_DocumentoClienteTipo_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[DocumentoClienteTipo] CHECK CONSTRAINT [Cliente_DocumentoClienteTipo_fk]
GO
