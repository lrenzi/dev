USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[DocumentoClienteDadosDoc]    Script Date: 03/01/2017 14:32:10 ******/
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
