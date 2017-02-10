USE [ETFForBrian]
GO

/****** Object:  Table [dbo].[tblStockOfPCF]    Script Date: 2016/8/8 上午 08:22:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblStockOfPCF](
	[DataDate] [datetime] NOT NULL,
	[ETFCode] [varchar](20) NOT NULL,
	[Exchange] [varchar](10) NOT NULL,
	[PID] [varchar](20) NOT NULL,
	[Name] [varchar](50) NULL,
	[PCFUnits] [int] NOT NULL,
	[TotalUnits] [int] NULL,
	[Weights] [float] NULL,
	[ReplaceWithCash] [varchar](1) NOT NULL,
	[PhysicalPurchase] [varchar](1) NOT NULL,
	[YP] [float] NULL,
	[PriceAdjFactor] [float] NULL,
	[AssignedMP] [float] NULL,
 CONSTRAINT [PK_tblStockOfPCF_1] PRIMARY KEY CLUSTERED 
(
	[DataDate] ASC,
	[ETFCode] ASC,
	[Exchange] ASC,
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_ETFCode]  DEFAULT ('') FOR [ETFCode]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_Exchange]  DEFAULT ('') FOR [Exchange]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_PID]  DEFAULT ('') FOR [PID]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_Units]  DEFAULT ((0)) FOR [PCFUnits]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_TotalUnits]  DEFAULT ((0)) FOR [TotalUnits]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_Weights]  DEFAULT ((0)) FOR [Weights]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_Table_1_ReplaceWithCase]  DEFAULT ((1)) FOR [ReplaceWithCash]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_Price]  DEFAULT ((-1)) FOR [YP]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_PriceAdjFactor]  DEFAULT ((0)) FOR [PriceAdjFactor]
GO

ALTER TABLE [dbo].[tblStockOfPCF] ADD  CONSTRAINT [DF_tblStockOfPCF_AssignedMP]  DEFAULT ((-1)) FOR [AssignedMP]
GO


