USE [bsidbdesenv]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 03/01/2017 14:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[TokenId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [varchar](128) NOT NULL,
	[IdentityName] [varchar](50) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ClienteId] [int] NOT NULL,
	[ProtectedTicket] [varchar](max) NOT NULL,
 CONSTRAINT [TokenId] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD  CONSTRAINT [Cliente_Token_fk] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Token] CHECK CONSTRAINT [Cliente_Token_fk]
GO
