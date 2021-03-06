USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 03/01/2017 14:32:10 ******/
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
