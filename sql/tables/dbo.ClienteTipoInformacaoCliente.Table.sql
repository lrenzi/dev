USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[ClienteTipoInformacaoCliente]    Script Date: 03/01/2017 14:32:10 ******/
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
