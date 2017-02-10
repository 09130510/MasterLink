USE [ETFForBrian]
GO

/****** Object:  Table [dbo].[tblFuture]    Script Date: 2016/8/8 上午 08:21:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblFuture](
	[Exchange] [varchar](10) NOT NULL,
	[Head] [varchar](20) NOT NULL,
	[Currency] [varchar](20) NOT NULL,
	[CValue] [float] NOT NULL,
	[CapitalFormat] [varchar](50) NOT NULL,
	[PATSFormat] [varchar](50) NOT NULL,
	[iPushFormat] [varchar](50) NOT NULL,
	[RedisFormat] [varchar](50) NOT NULL,
	[xQuoteFormat] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblFuture] PRIMARY KEY CLUSTERED 
(
	[Exchange] ASC,
	[Head] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_Currency]  DEFAULT ('') FOR [Currency]
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_CValue]  DEFAULT ((0)) FOR [CValue]
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_CapitalFormat]  DEFAULT ('') FOR [CapitalFormat]
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_PatFormat]  DEFAULT ('') FOR [PATSFormat]
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_iPushFormat]  DEFAULT ('') FOR [iPushFormat]
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_SelfParseFormat]  DEFAULT ('') FOR [RedisFormat]
GO

ALTER TABLE [dbo].[tblFuture] ADD  CONSTRAINT [DF_tblFuture_xQuoteFormat]  DEFAULT ('') FOR [xQuoteFormat]
GO


