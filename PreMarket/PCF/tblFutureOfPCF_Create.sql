USE [ETFForBrian]
GO

/****** Object:  Table [dbo].[tblFutureOfPCF]    Script Date: 2016/8/8 上午 08:21:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblFutureOfPCF](
	[DataDate] [datetime] NOT NULL,
	[ETFCode] [varchar](20) NOT NULL,
	[PID] [varchar](20) NOT NULL,
	[Head] [varchar](20) NOT NULL,
	[YM] [varchar](10) NOT NULL,
	[Y] [int] NOT NULL,
	[M] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[PCFUnits] [float] NOT NULL,
	[TotalUnits] [float] NULL,
	[Weights] [float] NULL,
	[YP] [float] NULL,
	[AssignedMP] [float] NULL,
 CONSTRAINT [PK_tblFutureOfPCF] PRIMARY KEY CLUSTERED 
(
	[DataDate] ASC,
	[ETFCode] ASC,
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_PID]  DEFAULT ('') FOR [PID]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_Y]  DEFAULT ((0)) FOR [Y]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_M]  DEFAULT ((0)) FOR [M]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_Units]  DEFAULT ((0)) FOR [PCFUnits]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_TotalUnits]  DEFAULT ((0)) FOR [TotalUnits]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_Weights]  DEFAULT ((0)) FOR [Weights]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_YP]  DEFAULT ((-1)) FOR [YP]
GO

ALTER TABLE [dbo].[tblFutureOfPCF] ADD  CONSTRAINT [DF_tblFutureOfPCF_AssignedMP]  DEFAULT ((-1)) FOR [AssignedMP]
GO


