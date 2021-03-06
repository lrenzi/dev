USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[DocumentoClienteDados]    Script Date: 03/01/2017 14:32:10 ******/
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
