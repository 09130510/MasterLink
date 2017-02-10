USE [ETFForBrian]
GO

/****** Object:  Table [dbo].[tblETF]    Script Date: 2016/8/8 上午 08:19:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblETF](
	[ETFCode] [varchar](20) NOT NULL,
	[Name] [char](15) NULL,
	[Market] [varchar](10) NOT NULL,
	[ETFType] [varchar](5) NULL,
	[Broker] [varchar](50) NOT NULL,
	[TradeUnit] [int] NOT NULL,
	[PRUnit] [int] NOT NULL,
	[Address] [varchar](500) NULL,
	[SettingItem] [varchar](5) NULL,
 CONSTRAINT [PK_tblETF] PRIMARY KEY CLUSTERED 
(
	[ETFCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_ETFCode]  DEFAULT ('') FOR [ETFCode]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_Market]  DEFAULT ('') FOR [Market]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_ETFType]  DEFAULT ('') FOR [ETFType]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_Broker]  DEFAULT ('') FOR [Broker]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_TradeUnit]  DEFAULT ((0)) FOR [TradeUnit]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_PRUnit]  DEFAULT ((0)) FOR [PRUnit]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_Address]  DEFAULT ('') FOR [Address]
GO

ALTER TABLE [dbo].[tblETF] ADD  CONSTRAINT [DF_tblETF_SettingItem]  DEFAULT ('') FOR [SettingItem]
GO


