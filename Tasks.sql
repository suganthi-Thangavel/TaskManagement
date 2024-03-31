USE [TaskManagement]
GO

ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK__Tasks__ProjectId__5165187F]
GO

/****** Object:  Table [dbo].[Tasks]    Script Date: 31-03-2024 20:50:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
DROP TABLE [dbo].[Tasks]
GO

/****** Object:  Table [dbo].[Tasks]    Script Date: 31-03-2024 20:50:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Completed] [bit] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ProjectId] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Assignee] [nvarchar](100) NULL,
	[Priority] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Labels] [nvarchar](max) NULL,
	[Components] [nvarchar](max) NULL,
	[Versions] [nvarchar](max) NULL,
	[Attachments] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO


