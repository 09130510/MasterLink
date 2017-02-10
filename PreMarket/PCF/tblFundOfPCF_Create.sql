USE [ETFForBrian]
GO

/****** Object:  Table [dbo].[tblFundOfPCF]    Script Date: 2016/8/8 上午 08:20:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblFundOfPCF](
	[DataDate] [datetime] NOT NULL,
	[ETFCode] [varchar](20) NOT NULL,
	[Exchange] [varchar](10) NOT NULL,
	[PID] [varchar](20) NOT NULL,
	[Name] [varchar](50) NULL,
	[PCFUnits] [int] NOT NULL,
	[TotalUnits] [int] NULL,
	[Weights] [float] NULL,
	[YP] [float] NULL,
	[AssignedMP] [float] NULL,
 CONSTRAINT [PK_tblFundOfPCF] PRIMARY KEY CLUSTERED 
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

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_Exchange]  DEFAULT ('') FOR [Exchange]
GO

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_Units]  DEFAULT ((0)) FOR [PCFUnits]
GO

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_TotalUnits]  DEFAULT ((0)) FOR [TotalUnits]
GO

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_Weights]  DEFAULT ((0)) FOR [Weights]
GO

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_YP]  DEFAULT ((0)) FOR [YP]
GO

ALTER TABLE [dbo].[tblFundOfPCF] ADD  CONSTRAINT [DF_tblFundOfPCF_AssignedMP]  DEFAULT ((-1)) FOR [AssignedMP]
GO


