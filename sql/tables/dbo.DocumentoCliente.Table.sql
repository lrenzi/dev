USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[DocumentoCliente]    Script Date: 03/01/2017 14:32:10 ******/
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
