USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[DocumentoClienteSituacao]    Script Date: 03/01/2017 14:32:10 ******/
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
ALTER TABLE [dbo].[DocumentoClienteSituacao] ADD  DEFAULT ((0)) FOR [DocObrigatorio]
GO
ALTER TABLE [dbo].[DocumentoClienteSituacao]  WITH CHECK ADD  CONSTRAINT [DocumentoClienteTipo_DocumentoClienteSituacao_fk] FOREIGN KEY([DocCliTipoId])
REFERENCES [dbo].[DocumentoClienteTipo] ([DocCliTipoId])
GO
ALTER TABLE [dbo].[DocumentoClienteSituacao] CHECK CONSTRAINT [DocumentoClienteTipo_DocumentoClienteSituacao_fk]
GO
