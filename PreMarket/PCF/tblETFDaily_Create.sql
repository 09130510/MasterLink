USE [ETFForBrian]
GO

/****** Object:  Table [dbo].[tblETFDaily]    Script Date: 2016/8/8 上午 08:20:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblETFDaily](
	[DataDate] [datetime] NOT NULL,
	[ETFCode] [varchar](20) NOT NULL,
	[FundAssetValue] [float] NOT NULL,
	[PublicShares] [float] NOT NULL,
	[PublicSharesDiff] [float] NOT NULL,
	[EstPublicShares] [float] NOT NULL,
	[NAV] [float] NOT NULL,
	[CashDiff] [float] NOT NULL,
	[PreAllot] [float] NOT NULL,
	[Allot] [float] NOT NULL,
	[EstCValue] [float] NOT NULL,
	[EstDValue] [float] NOT NULL,
 CONSTRAINT [PK_tblETFDaily] PRIMARY KEY CLUSTERED 
(
	[DataDate] ASC,
	[ETFCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_FundAssetValue]  DEFAULT ((0)) FOR [FundAssetValue]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_PublicShares]  DEFAULT ((0)) FOR [PublicShares]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_PublicSharesDiff]  DEFAULT ((0)) FOR [PublicSharesDiff]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_EstPublicShares]  DEFAULT ((0)) FOR [EstPublicShares]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_NAV]  DEFAULT ((0)) FOR [NAV]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_CashDiff]  DEFAULT ((0)) FOR [CashDiff]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_ValuePerPR]  DEFAULT ((0)) FOR [PreAllot]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_Allot]  DEFAULT ((0)) FOR [Allot]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_EstCValue]  DEFAULT ((0)) FOR [EstCValue]
GO

ALTER TABLE [dbo].[tblETFDaily] ADD  CONSTRAINT [DF_tblETFDaily_EstDValue]  DEFAULT ((0)) FOR [EstDValue]
GO


