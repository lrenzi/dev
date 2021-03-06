USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[TipoInformacaoCliente]    Script Date: 03/01/2017 14:32:10 ******/
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
