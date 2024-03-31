USE [TaskManagement]
GO

ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [DF__Projects__Create__49C3F6B7]
GO

/****** Object:  Table [dbo].[Projects]    Script Date: 31-03-2024 20:45:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
DROP TABLE [dbo].[Projects]
GO

/****** Object:  Table [dbo].[Projects]    Script Date: 31-03-2024 20:45:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO


