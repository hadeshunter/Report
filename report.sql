USE [master]
GO
/****** Object:  Database [Report]    Script Date: 05/10/2020 8:37:32 AM ******/
CREATE DATABASE [Report]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Report', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Report.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Report_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Report_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Report] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Report].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Report] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Report] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Report] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Report] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Report] SET ARITHABORT OFF 
GO
ALTER DATABASE [Report] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Report] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Report] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Report] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Report] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Report] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Report] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Report] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Report] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Report] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Report] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Report] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Report] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Report] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Report] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Report] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Report] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Report] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Report] SET  MULTI_USER 
GO
ALTER DATABASE [Report] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Report] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Report] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Report] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Report] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Report]
GO
/****** Object:  User [us_report]    Script Date: 05/10/2020 8:37:32 AM ******/
CREATE USER [us_report] FOR LOGIN [us_report] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [mivtt]    Script Date: 05/10/2020 8:37:32 AM ******/
CREATE USER [mivtt] FOR LOGIN [mivtt] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [us_report]
GO
/****** Object:  Table [dbo].[PhoneSended]    Script Date: 05/10/2020 8:37:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneSended](
	[phoneid] [int] IDENTITY(1,1) NOT NULL,
	[phone] [nvarchar](10) NOT NULL,
	[processid] [int] NOT NULL,
	[createday] [datetime] NULL,
	[updateday] [datetime] NULL,
	[status] [int] NULL,
	[timer] [int] NULL,
	[content] [nvarchar](4000) NULL,
	[result] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[phoneid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Process]    Script Date: 05/10/2020 8:37:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[processid] [int] IDENTITY(1,1) NOT NULL,
	[id_yeu_cau] [int] NOT NULL,
	[status] [int] NULL,
	[sendth] [int] NULL,
	[createday] [datetime] NULL,
	[updateday] [datetime] NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[processid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 05/10/2020 8:37:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[roleid] [int] IDENTITY(1,1) NOT NULL,
	[rolename] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SLAData]    Script Date: 05/10/2020 8:37:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SLAData](
	[slaid] [int] IDENTITY(1,1) NOT NULL,
	[timeksyc] [int] NOT NULL,
	[timetkhd] [int] NOT NULL,
	[timedwst] [int] NOT NULL,
	[userupdate] [int] NULL,
	[createday] [datetime] NULL,
	[updateday] [datetime] NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[slaid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sms]    Script Date: 05/10/2020 8:37:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sms](
	[smsid] [int] IDENTITY(1,1) NOT NULL,
	[phone] [nvarchar](10) NOT NULL,
	[active] [bit] NULL,
	[name] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[smsid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timer]    Script Date: 05/10/2020 8:37:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timer](
	[timeid] [int] NOT NULL,
	[totaltime] [int] NOT NULL,
	[id_giai_doan] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[timeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PhoneSended] ON 
GO
INSERT [dbo].[PhoneSended] ([phoneid], [phone], [processid], [createday], [updateday], [status], [timer], [content], [result]) VALUES (2, N'0833383352', 10, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-01-01T00:00:00.000' AS DateTime), 0, 1200, N'Trễ rồi', NULL)
GO
SET IDENTITY_INSERT [dbo].[PhoneSended] OFF
GO
SET IDENTITY_INSERT [dbo].[Process] ON 
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (1, 7553, 0, 2, CAST(N'2020-08-23T09:10:02.790' AS DateTime), CAST(N'2020-08-23T09:10:02.790' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (2, 7549, 0, 2, CAST(N'2020-08-23T09:10:02.937' AS DateTime), CAST(N'2020-08-23T09:10:02.937' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (3, 7544, 0, 2, CAST(N'2020-08-23T09:10:02.967' AS DateTime), CAST(N'2020-08-23T09:10:02.967' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (4, 7539, 0, 2, CAST(N'2020-08-23T09:10:02.990' AS DateTime), CAST(N'2020-08-23T09:10:02.990' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (5, 7534, 0, 2, CAST(N'2020-08-23T09:10:03.017' AS DateTime), CAST(N'2020-08-23T09:10:03.017' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (6, 7520, 0, 2, CAST(N'2020-08-23T09:10:03.047' AS DateTime), CAST(N'2020-08-23T09:10:03.047' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (7, 7519, 0, 2, CAST(N'2020-08-23T09:10:03.080' AS DateTime), CAST(N'2020-08-23T09:10:03.080' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (8, 7518, 0, 2, CAST(N'2020-08-23T09:10:03.103' AS DateTime), CAST(N'2020-08-23T09:10:03.103' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (9, 7517, 0, 2, CAST(N'2020-08-23T09:10:03.130' AS DateTime), CAST(N'2020-08-23T09:10:03.130' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (10, 7515, 0, 2, CAST(N'2020-08-23T09:10:03.160' AS DateTime), CAST(N'2020-08-23T09:10:03.160' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (11, 7511, 0, 2, CAST(N'2020-08-23T09:10:03.190' AS DateTime), CAST(N'2020-08-23T09:10:03.190' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (12, 7510, 0, 2, CAST(N'2020-08-23T09:10:03.217' AS DateTime), CAST(N'2020-08-23T09:10:03.217' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (13, 7508, 0, 2, CAST(N'2020-08-23T09:10:03.240' AS DateTime), CAST(N'2020-08-23T09:10:03.240' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (14, 7507, 0, 2, CAST(N'2020-08-23T09:10:03.270' AS DateTime), CAST(N'2020-08-23T09:10:03.270' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (15, 7506, 0, 2, CAST(N'2020-08-23T09:10:03.293' AS DateTime), CAST(N'2020-08-23T09:10:03.293' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (16, 7505, 0, 2, CAST(N'2020-08-23T09:10:03.327' AS DateTime), CAST(N'2020-08-23T09:10:03.327' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (17, 7504, 0, 2, CAST(N'2020-08-23T09:10:03.347' AS DateTime), CAST(N'2020-08-23T09:10:03.347' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (18, 7503, 0, 2, CAST(N'2020-08-23T09:10:03.373' AS DateTime), CAST(N'2020-08-23T09:10:03.373' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (19, 7502, 0, 2, CAST(N'2020-08-23T09:10:03.400' AS DateTime), CAST(N'2020-08-23T09:10:03.400' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (20, 7501, 0, 2, CAST(N'2020-08-23T09:10:03.430' AS DateTime), CAST(N'2020-08-23T09:10:03.430' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (21, 7500, 0, 2, CAST(N'2020-08-23T09:10:03.457' AS DateTime), CAST(N'2020-08-23T09:10:03.457' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (22, 7499, 0, 2, CAST(N'2020-08-23T09:10:03.480' AS DateTime), CAST(N'2020-08-23T09:10:03.480' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (23, 7498, 0, 2, CAST(N'2020-08-23T09:10:03.513' AS DateTime), CAST(N'2020-08-23T09:10:03.513' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (24, 7497, 0, 2, CAST(N'2020-08-23T09:10:03.547' AS DateTime), CAST(N'2020-08-23T09:10:03.547' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (25, 7496, 0, 2, CAST(N'2020-08-23T09:10:03.573' AS DateTime), CAST(N'2020-08-23T09:10:03.573' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (26, 7495, 0, 2, CAST(N'2020-08-23T09:10:03.603' AS DateTime), CAST(N'2020-08-23T09:10:03.603' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (27, 7494, 0, 2, CAST(N'2020-08-23T09:10:03.637' AS DateTime), CAST(N'2020-08-23T09:10:03.637' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (28, 7493, 0, 2, CAST(N'2020-08-23T09:10:03.667' AS DateTime), CAST(N'2020-08-23T09:10:03.667' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (29, 7490, 0, 2, CAST(N'2020-08-23T09:10:03.693' AS DateTime), CAST(N'2020-08-23T09:10:03.693' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (30, 7489, 0, 2, CAST(N'2020-08-23T09:10:03.723' AS DateTime), CAST(N'2020-08-23T09:10:03.723' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (31, 7488, 0, 2, CAST(N'2020-08-23T09:10:03.757' AS DateTime), CAST(N'2020-08-23T09:10:03.757' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (32, 7478, 0, 2, CAST(N'2020-08-23T09:10:03.790' AS DateTime), CAST(N'2020-08-23T09:10:03.790' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (33, 7477, 0, 2, CAST(N'2020-08-23T09:10:03.817' AS DateTime), CAST(N'2020-08-23T09:10:03.817' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (34, 7476, 0, 2, CAST(N'2020-08-23T09:10:03.857' AS DateTime), CAST(N'2020-08-23T09:10:03.857' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (35, 7468, 0, 2, CAST(N'2020-08-23T09:10:03.893' AS DateTime), CAST(N'2020-08-23T09:10:03.893' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (36, 7459, 0, 2, CAST(N'2020-08-23T09:10:03.923' AS DateTime), CAST(N'2020-08-23T09:10:03.923' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (37, 7456, 0, 2, CAST(N'2020-08-23T09:10:03.947' AS DateTime), CAST(N'2020-08-23T09:10:03.947' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (38, 7449, 0, 2, CAST(N'2020-08-23T09:10:03.980' AS DateTime), CAST(N'2020-08-23T09:10:03.980' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (39, 7448, 0, 2, CAST(N'2020-08-23T09:10:04.017' AS DateTime), CAST(N'2020-08-23T09:10:04.017' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (40, 7447, 0, 2, CAST(N'2020-08-23T09:10:04.043' AS DateTime), CAST(N'2020-08-23T09:10:04.043' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (41, 7446, 0, 2, CAST(N'2020-08-23T09:10:04.077' AS DateTime), CAST(N'2020-08-23T09:10:04.077' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (42, 7442, 0, 2, CAST(N'2020-08-23T09:10:04.110' AS DateTime), CAST(N'2020-08-23T09:10:04.110' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (43, 7439, 0, 2, CAST(N'2020-08-23T09:10:04.147' AS DateTime), CAST(N'2020-08-23T09:10:04.147' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (44, 7436, 0, 2, CAST(N'2020-08-23T09:10:04.170' AS DateTime), CAST(N'2020-08-23T09:10:04.170' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (45, 7432, 0, 2, CAST(N'2020-08-23T09:10:04.193' AS DateTime), CAST(N'2020-08-23T09:10:04.193' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (46, 7431, 0, 2, CAST(N'2020-08-23T09:10:04.230' AS DateTime), CAST(N'2020-08-23T09:10:04.230' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (47, 7429, 0, 2, CAST(N'2020-08-23T09:10:04.270' AS DateTime), CAST(N'2020-08-23T09:10:04.270' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (48, 7423, 0, 2, CAST(N'2020-08-23T09:10:04.297' AS DateTime), CAST(N'2020-08-23T09:10:04.297' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (49, 7421, 0, 2, CAST(N'2020-08-23T09:10:04.327' AS DateTime), CAST(N'2020-08-23T09:10:04.327' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (50, 7420, 0, 2, CAST(N'2020-08-23T09:10:04.363' AS DateTime), CAST(N'2020-08-23T09:10:04.363' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (51, 7418, 0, 2, CAST(N'2020-08-23T09:10:04.400' AS DateTime), CAST(N'2020-08-23T09:10:04.400' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (52, 7417, 0, 2, CAST(N'2020-08-23T09:10:04.423' AS DateTime), CAST(N'2020-08-23T09:10:04.423' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (53, 7416, 0, 2, CAST(N'2020-08-23T09:10:04.450' AS DateTime), CAST(N'2020-08-23T09:10:04.450' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (54, 7415, 0, 2, CAST(N'2020-08-23T09:10:04.487' AS DateTime), CAST(N'2020-08-23T09:10:04.487' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (55, 7409, 0, 2, CAST(N'2020-08-23T09:10:04.523' AS DateTime), CAST(N'2020-08-23T09:10:04.523' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (56, 7404, 0, 2, CAST(N'2020-08-23T09:10:04.547' AS DateTime), CAST(N'2020-08-23T09:10:04.547' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (57, 7400, 0, 2, CAST(N'2020-08-23T09:10:04.580' AS DateTime), CAST(N'2020-08-23T09:10:04.580' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (58, 7397, 0, 2, CAST(N'2020-08-23T09:10:04.617' AS DateTime), CAST(N'2020-08-23T09:10:04.617' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (59, 7394, 0, 2, CAST(N'2020-08-23T09:10:04.663' AS DateTime), CAST(N'2020-08-23T09:10:04.663' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (60, 7391, 0, 2, CAST(N'2020-08-23T09:10:04.720' AS DateTime), CAST(N'2020-08-23T09:10:04.720' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (61, 7389, 0, 2, CAST(N'2020-08-23T09:10:04.757' AS DateTime), CAST(N'2020-08-23T09:10:04.757' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (62, 7381, 0, 2, CAST(N'2020-08-23T09:10:04.783' AS DateTime), CAST(N'2020-08-23T09:10:04.783' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (63, 7378, 0, 2, CAST(N'2020-08-23T09:10:04.810' AS DateTime), CAST(N'2020-08-23T09:10:04.810' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (64, 7376, 0, 2, CAST(N'2020-08-23T09:10:04.857' AS DateTime), CAST(N'2020-08-23T09:10:04.857' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (65, 7375, 0, 2, CAST(N'2020-08-23T09:10:04.887' AS DateTime), CAST(N'2020-08-23T09:10:04.887' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (66, 7593, 0, 2, CAST(N'2020-08-30T15:00:27.983' AS DateTime), CAST(N'2020-08-30T15:00:27.983' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (67, 7590, 0, 2, CAST(N'2020-08-30T15:00:28.457' AS DateTime), CAST(N'2020-08-30T15:00:28.457' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (68, 7589, 0, 2, CAST(N'2020-08-30T15:00:28.513' AS DateTime), CAST(N'2020-08-30T15:00:28.513' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (69, 7588, 0, 2, CAST(N'2020-08-30T15:00:28.590' AS DateTime), CAST(N'2020-08-30T15:00:28.590' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (70, 7584, 0, 2, CAST(N'2020-08-30T15:00:28.667' AS DateTime), CAST(N'2020-08-30T15:00:28.667' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (71, 7582, 0, 2, CAST(N'2020-08-30T15:00:28.747' AS DateTime), CAST(N'2020-08-30T15:00:28.747' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (72, 7581, 0, 2, CAST(N'2020-08-30T15:00:28.837' AS DateTime), CAST(N'2020-08-30T15:00:28.837' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (73, 7579, 0, 2, CAST(N'2020-08-30T15:00:28.907' AS DateTime), CAST(N'2020-08-30T15:00:28.907' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (74, 7578, 0, 2, CAST(N'2020-08-30T15:00:29.000' AS DateTime), CAST(N'2020-08-30T15:00:29.000' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (75, 7576, 0, 2, CAST(N'2020-08-30T15:00:29.080' AS DateTime), CAST(N'2020-08-30T15:00:29.080' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (76, 7575, 0, 2, CAST(N'2020-08-30T15:00:29.167' AS DateTime), CAST(N'2020-08-30T15:00:29.167' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (77, 7570, 0, 2, CAST(N'2020-08-30T15:00:29.257' AS DateTime), CAST(N'2020-08-30T15:00:29.257' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (78, 7569, 0, 2, CAST(N'2020-08-30T15:00:29.343' AS DateTime), CAST(N'2020-08-30T15:00:29.343' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (79, 7568, 0, 2, CAST(N'2020-08-30T15:00:29.427' AS DateTime), CAST(N'2020-08-30T15:00:29.427' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (80, 7567, 0, 2, CAST(N'2020-08-30T15:00:29.527' AS DateTime), CAST(N'2020-08-30T15:00:29.527' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (81, 7564, 0, 2, CAST(N'2020-08-30T15:00:29.807' AS DateTime), CAST(N'2020-08-30T15:00:29.807' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (82, 7561, 0, 2, CAST(N'2020-08-30T15:00:29.900' AS DateTime), CAST(N'2020-08-30T15:00:29.900' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (83, 7560, 0, 2, CAST(N'2020-08-30T15:00:29.990' AS DateTime), CAST(N'2020-08-30T15:00:29.990' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (84, 7559, 0, 2, CAST(N'2020-08-30T15:00:30.130' AS DateTime), CAST(N'2020-08-30T15:00:30.130' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (85, 7492, 0, 2, CAST(N'2020-08-30T15:00:30.887' AS DateTime), CAST(N'2020-08-30T15:00:30.887' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (86, 7422, 0, 2, CAST(N'2020-08-30T15:00:31.247' AS DateTime), CAST(N'2020-08-30T15:00:31.247' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (87, 7577, 0, 2, CAST(N'2020-08-30T16:00:27.837' AS DateTime), CAST(N'2020-08-30T16:00:27.837' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (88, 7605, 0, 2, CAST(N'2020-08-31T09:40:28.750' AS DateTime), CAST(N'2020-08-31T09:40:28.750' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (89, 7596, 0, 2, CAST(N'2020-08-31T09:40:28.797' AS DateTime), CAST(N'2020-08-31T09:40:28.797' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (90, 7545, 0, 2, CAST(N'2020-08-31T09:40:29.037' AS DateTime), CAST(N'2020-08-31T09:40:29.037' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (91, 7440, 0, 2, CAST(N'2020-08-31T09:40:29.413' AS DateTime), CAST(N'2020-08-31T09:40:29.413' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (92, 7645, 0, 2, CAST(N'2020-08-31T10:00:28.603' AS DateTime), CAST(N'2020-08-31T10:00:28.603' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (93, 7628, 0, 2, CAST(N'2020-08-31T10:00:28.850' AS DateTime), CAST(N'2020-08-31T10:00:28.850' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (94, 7625, 0, 2, CAST(N'2020-08-31T10:00:28.863' AS DateTime), CAST(N'2020-08-31T10:00:28.863' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (95, 7613, 0, 2, CAST(N'2020-08-31T10:00:28.877' AS DateTime), CAST(N'2020-08-31T10:00:28.877' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (96, 7548, 0, 2, CAST(N'2020-08-31T10:00:28.990' AS DateTime), CAST(N'2020-08-31T10:00:28.990' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (97, 7541, 0, 2, CAST(N'2020-08-31T10:00:29.007' AS DateTime), CAST(N'2020-08-31T10:00:29.007' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (98, 7540, 0, 2, CAST(N'2020-08-31T10:00:29.020' AS DateTime), CAST(N'2020-08-31T10:00:29.020' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (99, 7535, 0, 2, CAST(N'2020-08-31T10:00:29.037' AS DateTime), CAST(N'2020-08-31T10:00:29.037' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (100, 7532, 0, 2, CAST(N'2020-08-31T10:00:29.063' AS DateTime), CAST(N'2020-08-31T10:00:29.063' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (101, 7525, 0, 2, CAST(N'2020-08-31T10:00:29.080' AS DateTime), CAST(N'2020-08-31T10:00:29.080' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (102, 7514, 0, 2, CAST(N'2020-08-31T10:00:29.127' AS DateTime), CAST(N'2020-08-31T10:00:29.127' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (103, 7474, 0, 2, CAST(N'2020-08-31T10:00:29.283' AS DateTime), CAST(N'2020-08-31T10:00:29.283' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (104, 7461, 0, 2, CAST(N'2020-08-31T10:00:29.307' AS DateTime), CAST(N'2020-08-31T10:00:29.307' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (105, 7455, 0, 2, CAST(N'2020-08-31T10:00:29.327' AS DateTime), CAST(N'2020-08-31T10:00:29.327' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (106, 7408, 0, 2, CAST(N'2020-08-31T10:00:29.430' AS DateTime), CAST(N'2020-08-31T10:00:29.430' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (107, 7678, 0, 2, CAST(N'2020-09-03T12:00:30.887' AS DateTime), CAST(N'2020-09-03T12:00:30.887' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (108, 7676, 0, 2, CAST(N'2020-09-03T12:00:31.173' AS DateTime), CAST(N'2020-09-03T12:00:31.173' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (109, 7674, 0, 2, CAST(N'2020-09-03T12:00:31.213' AS DateTime), CAST(N'2020-09-03T12:00:31.213' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (110, 7672, 0, 2, CAST(N'2020-09-03T12:00:31.270' AS DateTime), CAST(N'2020-09-03T12:00:31.270' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (111, 7671, 0, 2, CAST(N'2020-09-03T12:00:31.317' AS DateTime), CAST(N'2020-09-03T12:00:31.317' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (112, 7651, 0, 2, CAST(N'2020-09-03T12:00:31.347' AS DateTime), CAST(N'2020-09-03T12:00:31.347' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (113, 7647, 0, 2, CAST(N'2020-09-03T12:00:31.387' AS DateTime), CAST(N'2020-09-03T12:00:31.387' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (114, 7624, 0, 2, CAST(N'2020-09-03T12:00:31.420' AS DateTime), CAST(N'2020-09-03T12:00:31.420' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (115, 7546, 0, 2, CAST(N'2020-09-03T12:00:31.670' AS DateTime), CAST(N'2020-09-03T12:00:31.670' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (116, 7460, 0, 2, CAST(N'2020-09-03T12:00:32.180' AS DateTime), CAST(N'2020-09-03T12:00:32.180' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (117, 7627, 0, 2, CAST(N'2020-09-04T10:00:29.747' AS DateTime), CAST(N'2020-09-04T10:00:29.747' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (118, 7574, 0, 2, CAST(N'2020-09-04T13:40:29.067' AS DateTime), CAST(N'2020-09-04T13:40:29.067' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (119, 7573, 0, 2, CAST(N'2020-09-04T13:40:29.073' AS DateTime), CAST(N'2020-09-04T13:40:29.073' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (120, 7551, 0, 2, CAST(N'2020-09-04T13:40:29.103' AS DateTime), CAST(N'2020-09-04T13:40:29.103' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (121, 7547, 0, 2, CAST(N'2020-09-04T16:20:26.740' AS DateTime), CAST(N'2020-09-04T16:20:26.740' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (122, 7670, 0, 2, CAST(N'2020-09-07T09:00:27.643' AS DateTime), CAST(N'2020-09-07T09:00:27.643' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (123, 7667, 0, 2, CAST(N'2020-09-07T09:00:27.897' AS DateTime), CAST(N'2020-09-07T09:00:27.897' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (124, 7665, 0, 2, CAST(N'2020-09-07T09:00:27.897' AS DateTime), CAST(N'2020-09-07T09:00:27.897' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (125, 7664, 0, 2, CAST(N'2020-09-07T09:00:27.910' AS DateTime), CAST(N'2020-09-07T09:00:27.910' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (126, 7661, 0, 2, CAST(N'2020-09-07T09:00:27.910' AS DateTime), CAST(N'2020-09-07T09:00:27.910' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (127, 7655, 0, 2, CAST(N'2020-09-07T09:00:27.910' AS DateTime), CAST(N'2020-09-07T09:00:27.910' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (128, 7638, 0, 2, CAST(N'2020-09-07T09:00:27.910' AS DateTime), CAST(N'2020-09-07T09:00:27.910' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (129, 7637, 0, 2, CAST(N'2020-09-07T09:00:27.927' AS DateTime), CAST(N'2020-09-07T09:00:27.927' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (130, 7622, 0, 2, CAST(N'2020-09-07T09:00:27.927' AS DateTime), CAST(N'2020-09-07T09:00:27.927' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (131, 7595, 0, 2, CAST(N'2020-09-07T09:00:27.943' AS DateTime), CAST(N'2020-09-07T09:00:27.943' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (132, 7585, 0, 2, CAST(N'2020-09-07T09:00:27.957' AS DateTime), CAST(N'2020-09-07T09:00:27.957' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (133, 7580, 0, 2, CAST(N'2020-09-07T09:00:27.957' AS DateTime), CAST(N'2020-09-07T09:00:27.957' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (134, 7571, 0, 2, CAST(N'2020-09-07T09:00:27.973' AS DateTime), CAST(N'2020-09-07T09:00:27.973' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (135, 7566, 0, 2, CAST(N'2020-09-07T09:00:27.990' AS DateTime), CAST(N'2020-09-07T09:00:27.990' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (136, 7550, 0, 2, CAST(N'2020-09-07T09:00:28.007' AS DateTime), CAST(N'2020-09-07T09:00:28.007' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (137, 7529, 0, 2, CAST(N'2020-09-07T09:00:28.037' AS DateTime), CAST(N'2020-09-07T09:00:28.037' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (138, 7524, 0, 2, CAST(N'2020-09-07T09:00:28.037' AS DateTime), CAST(N'2020-09-07T09:00:28.037' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (139, 7480, 0, 2, CAST(N'2020-09-07T09:00:28.100' AS DateTime), CAST(N'2020-09-07T09:00:28.100' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (140, 7469, 0, 2, CAST(N'2020-09-07T09:00:28.100' AS DateTime), CAST(N'2020-09-07T09:00:28.100' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (141, 7464, 0, 2, CAST(N'2020-09-07T09:00:28.100' AS DateTime), CAST(N'2020-09-07T09:00:28.100' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (142, 7463, 0, 2, CAST(N'2020-09-07T09:00:28.113' AS DateTime), CAST(N'2020-09-07T09:00:28.113' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (143, 7462, 0, 2, CAST(N'2020-09-07T09:00:28.113' AS DateTime), CAST(N'2020-09-07T09:00:28.113' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (144, 7441, 0, 2, CAST(N'2020-09-07T09:00:28.130' AS DateTime), CAST(N'2020-09-07T09:00:28.130' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (145, 7438, 0, 2, CAST(N'2020-09-07T09:00:28.147' AS DateTime), CAST(N'2020-09-07T09:00:28.147' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (146, 7414, 0, 2, CAST(N'2020-09-07T09:00:28.160' AS DateTime), CAST(N'2020-09-07T09:00:28.160' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (147, 7413, 0, 2, CAST(N'2020-09-07T09:00:28.160' AS DateTime), CAST(N'2020-09-07T09:00:28.160' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (148, 7684, 0, 2, CAST(N'2020-09-07T09:40:36.583' AS DateTime), CAST(N'2020-09-07T09:40:36.583' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (149, 7682, 0, 2, CAST(N'2020-09-07T09:40:36.817' AS DateTime), CAST(N'2020-09-07T09:40:36.817' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (150, 7677, 0, 2, CAST(N'2020-09-07T09:40:36.833' AS DateTime), CAST(N'2020-09-07T09:40:36.833' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (151, 7675, 0, 2, CAST(N'2020-09-07T09:40:36.833' AS DateTime), CAST(N'2020-09-07T09:40:36.833' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (152, 7673, 0, 2, CAST(N'2020-09-07T09:40:36.863' AS DateTime), CAST(N'2020-09-07T09:40:36.863' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (153, 7668, 0, 2, CAST(N'2020-09-07T09:40:36.863' AS DateTime), CAST(N'2020-09-07T09:40:36.863' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (154, 7666, 0, 2, CAST(N'2020-09-07T09:40:36.880' AS DateTime), CAST(N'2020-09-07T09:40:36.880' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (155, 7663, 0, 2, CAST(N'2020-09-07T09:40:36.880' AS DateTime), CAST(N'2020-09-07T09:40:36.880' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (156, 7660, 0, 2, CAST(N'2020-09-07T09:40:36.897' AS DateTime), CAST(N'2020-09-07T09:40:36.897' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (157, 7657, 0, 2, CAST(N'2020-09-07T09:40:36.897' AS DateTime), CAST(N'2020-09-07T09:40:36.897' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (158, 7653, 0, 2, CAST(N'2020-09-07T09:40:36.910' AS DateTime), CAST(N'2020-09-07T09:40:36.910' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (159, 7643, 0, 2, CAST(N'2020-09-07T09:40:36.927' AS DateTime), CAST(N'2020-09-07T09:40:36.927' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (160, 7642, 0, 2, CAST(N'2020-09-07T09:40:36.927' AS DateTime), CAST(N'2020-09-07T09:40:36.927' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (161, 7640, 0, 2, CAST(N'2020-09-07T09:40:36.943' AS DateTime), CAST(N'2020-09-07T09:40:36.943' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (162, 7639, 0, 2, CAST(N'2020-09-07T09:40:36.957' AS DateTime), CAST(N'2020-09-07T09:40:36.957' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (163, 7633, 0, 2, CAST(N'2020-09-07T09:40:36.973' AS DateTime), CAST(N'2020-09-07T09:40:36.973' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (164, 7632, 0, 2, CAST(N'2020-09-07T09:40:36.973' AS DateTime), CAST(N'2020-09-07T09:40:36.973' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (165, 7630, 0, 2, CAST(N'2020-09-07T09:40:36.990' AS DateTime), CAST(N'2020-09-07T09:40:36.990' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (166, 7617, 0, 2, CAST(N'2020-09-07T09:40:37.007' AS DateTime), CAST(N'2020-09-07T09:40:37.007' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (167, 7615, 0, 2, CAST(N'2020-09-07T09:40:37.007' AS DateTime), CAST(N'2020-09-07T09:40:37.007' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (168, 7610, 0, 2, CAST(N'2020-09-07T09:40:37.020' AS DateTime), CAST(N'2020-09-07T09:40:37.020' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (169, 7609, 0, 2, CAST(N'2020-09-07T09:40:37.037' AS DateTime), CAST(N'2020-09-07T09:40:37.037' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (170, 7599, 0, 2, CAST(N'2020-09-07T09:40:37.053' AS DateTime), CAST(N'2020-09-07T09:40:37.053' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (171, 7598, 0, 2, CAST(N'2020-09-07T09:40:37.053' AS DateTime), CAST(N'2020-09-07T09:40:37.053' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (172, 7592, 0, 2, CAST(N'2020-09-07T09:40:37.053' AS DateTime), CAST(N'2020-09-07T09:40:37.053' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (173, 7591, 0, 2, CAST(N'2020-09-07T09:40:37.067' AS DateTime), CAST(N'2020-09-07T09:40:37.067' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (174, 7572, 0, 2, CAST(N'2020-09-07T09:40:37.083' AS DateTime), CAST(N'2020-09-07T09:40:37.083' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (175, 7556, 0, 2, CAST(N'2020-09-07T09:40:37.113' AS DateTime), CAST(N'2020-09-07T09:40:37.113' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (176, 7555, 0, 2, CAST(N'2020-09-07T09:40:37.113' AS DateTime), CAST(N'2020-09-07T09:40:37.113' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (177, 7554, 0, 2, CAST(N'2020-09-07T09:40:37.130' AS DateTime), CAST(N'2020-09-07T09:40:37.130' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (178, 7538, 0, 2, CAST(N'2020-09-07T09:40:37.160' AS DateTime), CAST(N'2020-09-07T09:40:37.160' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (179, 7526, 0, 2, CAST(N'2020-09-07T09:40:37.160' AS DateTime), CAST(N'2020-09-07T09:40:37.160' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (180, 7516, 0, 2, CAST(N'2020-09-07T09:40:37.177' AS DateTime), CAST(N'2020-09-07T09:40:37.177' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (181, 7513, 0, 2, CAST(N'2020-09-07T09:40:37.207' AS DateTime), CAST(N'2020-09-07T09:40:37.207' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (182, 7486, 0, 2, CAST(N'2020-09-07T09:40:37.257' AS DateTime), CAST(N'2020-09-07T09:40:37.257' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (183, 7485, 0, 2, CAST(N'2020-09-07T09:40:37.257' AS DateTime), CAST(N'2020-09-07T09:40:37.257' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (184, 7483, 0, 2, CAST(N'2020-09-07T09:40:37.257' AS DateTime), CAST(N'2020-09-07T09:40:37.257' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (185, 7479, 0, 2, CAST(N'2020-09-07T09:40:37.270' AS DateTime), CAST(N'2020-09-07T09:40:37.270' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (186, 7473, 0, 2, CAST(N'2020-09-07T09:40:37.270' AS DateTime), CAST(N'2020-09-07T09:40:37.270' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (187, 7465, 0, 2, CAST(N'2020-09-07T09:40:37.287' AS DateTime), CAST(N'2020-09-07T09:40:37.287' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (188, 7457, 0, 2, CAST(N'2020-09-07T09:40:37.303' AS DateTime), CAST(N'2020-09-07T09:40:37.303' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (189, 7452, 0, 2, CAST(N'2020-09-07T09:40:37.317' AS DateTime), CAST(N'2020-09-07T09:40:37.317' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (190, 7445, 0, 2, CAST(N'2020-09-07T09:40:37.333' AS DateTime), CAST(N'2020-09-07T09:40:37.333' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (191, 7426, 0, 2, CAST(N'2020-09-07T09:40:37.363' AS DateTime), CAST(N'2020-09-07T09:40:37.363' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (192, 7425, 0, 2, CAST(N'2020-09-07T09:40:37.380' AS DateTime), CAST(N'2020-09-07T09:40:37.380' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (193, 7424, 0, 2, CAST(N'2020-09-07T09:40:37.397' AS DateTime), CAST(N'2020-09-07T09:40:37.397' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (194, 7410, 0, 2, CAST(N'2020-09-07T09:40:37.443' AS DateTime), CAST(N'2020-09-07T09:40:37.443' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (195, 7403, 0, 2, CAST(N'2020-09-07T09:40:37.443' AS DateTime), CAST(N'2020-09-07T09:40:37.443' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (196, 7402, 0, 2, CAST(N'2020-09-07T09:40:37.443' AS DateTime), CAST(N'2020-09-07T09:40:37.443' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (197, 7390, 0, 2, CAST(N'2020-09-07T09:40:37.460' AS DateTime), CAST(N'2020-09-07T09:40:37.460' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (198, 7383, 0, 2, CAST(N'2020-09-07T09:40:37.477' AS DateTime), CAST(N'2020-09-07T09:40:37.477' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (199, 7680, 0, 2, CAST(N'2020-09-10T11:20:34.560' AS DateTime), CAST(N'2020-09-10T11:20:34.560' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (200, 7631, 0, 2, CAST(N'2020-09-10T11:20:35.087' AS DateTime), CAST(N'2020-09-10T11:20:35.087' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (201, 7619, 0, 2, CAST(N'2020-09-10T11:20:35.127' AS DateTime), CAST(N'2020-09-10T11:20:35.127' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (202, 7631, 0, 2, CAST(N'2020-09-10T11:20:34.907' AS DateTime), CAST(N'2020-09-10T11:20:34.907' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (203, 7606, 0, 2, CAST(N'2020-09-10T11:20:35.180' AS DateTime), CAST(N'2020-09-10T11:20:35.180' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (204, 7604, 0, 2, CAST(N'2020-09-10T11:20:35.197' AS DateTime), CAST(N'2020-09-10T11:20:35.197' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (205, 7603, 0, 2, CAST(N'2020-09-10T11:20:35.207' AS DateTime), CAST(N'2020-09-10T11:20:35.207' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (206, 7602, 0, 2, CAST(N'2020-09-10T11:20:35.217' AS DateTime), CAST(N'2020-09-10T11:20:35.217' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (207, 7583, 0, 2, CAST(N'2020-09-10T11:20:35.300' AS DateTime), CAST(N'2020-09-10T11:20:35.300' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (208, 7531, 0, 2, CAST(N'2020-09-10T11:20:35.357' AS DateTime), CAST(N'2020-09-10T11:20:35.357' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (209, 7531, 0, 2, CAST(N'2020-09-10T11:20:35.483' AS DateTime), CAST(N'2020-09-10T11:20:35.483' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (210, 7482, 0, 2, CAST(N'2020-09-10T11:20:35.673' AS DateTime), CAST(N'2020-09-10T11:20:35.673' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (211, 7481, 0, 2, CAST(N'2020-09-10T11:20:35.680' AS DateTime), CAST(N'2020-09-10T11:20:35.680' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (212, 7470, 0, 2, CAST(N'2020-09-10T11:20:35.713' AS DateTime), CAST(N'2020-09-10T11:20:35.713' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (213, 7466, 0, 2, CAST(N'2020-09-10T11:20:35.737' AS DateTime), CAST(N'2020-09-10T11:20:35.737' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (214, 7437, 0, 2, CAST(N'2020-09-10T11:20:35.870' AS DateTime), CAST(N'2020-09-10T11:20:35.870' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (215, 7437, 0, 2, CAST(N'2020-09-10T11:20:35.753' AS DateTime), CAST(N'2020-09-10T11:20:35.753' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (216, 7434, 0, 2, CAST(N'2020-09-10T11:20:35.883' AS DateTime), CAST(N'2020-09-10T11:20:35.883' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (217, 7433, 0, 2, CAST(N'2020-09-10T11:20:35.907' AS DateTime), CAST(N'2020-09-10T11:20:35.907' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (218, 7433, 0, 2, CAST(N'2020-09-10T11:20:35.787' AS DateTime), CAST(N'2020-09-10T11:20:35.787' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (219, 7411, 0, 2, CAST(N'2020-09-10T11:20:36.017' AS DateTime), CAST(N'2020-09-10T11:20:36.017' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (220, 7401, 0, 2, CAST(N'2020-09-10T11:20:36.047' AS DateTime), CAST(N'2020-09-10T11:20:36.047' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (221, 7401, 0, 2, CAST(N'2020-09-10T11:20:35.933' AS DateTime), CAST(N'2020-09-10T11:20:35.933' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (222, 7399, 0, 2, CAST(N'2020-09-10T11:20:36.083' AS DateTime), CAST(N'2020-09-10T11:20:36.083' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (223, 7398, 0, 2, CAST(N'2020-09-10T11:20:36.097' AS DateTime), CAST(N'2020-09-10T11:20:36.097' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (224, 7396, 0, 2, CAST(N'2020-09-10T11:20:36.117' AS DateTime), CAST(N'2020-09-10T11:20:36.117' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (225, 7395, 0, 2, CAST(N'2020-09-10T11:20:36.130' AS DateTime), CAST(N'2020-09-10T11:20:36.130' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (226, 7395, 0, 2, CAST(N'2020-09-10T11:20:36.010' AS DateTime), CAST(N'2020-09-10T11:20:36.010' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (227, 7393, 0, 2, CAST(N'2020-09-10T11:20:36.147' AS DateTime), CAST(N'2020-09-10T11:20:36.147' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (228, 7392, 0, 2, CAST(N'2020-09-10T11:20:36.160' AS DateTime), CAST(N'2020-09-10T11:20:36.160' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (229, 7652, 0, 2, CAST(N'2020-09-14T14:00:34.857' AS DateTime), CAST(N'2020-09-14T14:00:34.857' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (230, 7635, 0, 2, CAST(N'2020-09-14T14:00:35.083' AS DateTime), CAST(N'2020-09-14T14:00:35.083' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (231, 7620, 0, 2, CAST(N'2020-09-14T14:00:35.247' AS DateTime), CAST(N'2020-09-14T14:00:35.247' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (232, 7607, 0, 2, CAST(N'2020-09-14T14:00:35.297' AS DateTime), CAST(N'2020-09-14T14:00:35.297' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (233, 7597, 0, 2, CAST(N'2020-09-14T14:00:35.367' AS DateTime), CAST(N'2020-09-14T14:00:35.367' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (234, 7563, 0, 2, CAST(N'2020-09-14T14:00:35.553' AS DateTime), CAST(N'2020-09-14T14:00:35.553' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (235, 7527, 0, 2, CAST(N'2020-09-14T14:00:35.730' AS DateTime), CAST(N'2020-09-14T14:00:35.730' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (236, 7523, 0, 2, CAST(N'2020-09-14T14:00:35.773' AS DateTime), CAST(N'2020-09-14T14:00:35.773' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (237, 7471, 0, 2, CAST(N'2020-09-14T14:00:36.407' AS DateTime), CAST(N'2020-09-14T14:00:36.407' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (238, 7405, 0, 2, CAST(N'2020-09-14T14:00:36.707' AS DateTime), CAST(N'2020-09-14T14:00:36.707' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (239, 7552, 0, 2, CAST(N'2020-09-23T11:20:29.893' AS DateTime), CAST(N'2020-09-23T11:20:29.893' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (240, 7636, 0, 2, CAST(N'2020-09-23T14:20:32.553' AS DateTime), CAST(N'2020-09-23T14:20:32.553' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (241, 7608, 0, 2, CAST(N'2020-09-23T14:20:32.847' AS DateTime), CAST(N'2020-09-23T14:20:32.847' AS DateTime), 1)
GO
INSERT [dbo].[Process] ([processid], [id_yeu_cau], [status], [sendth], [createday], [updateday], [active]) VALUES (242, 7467, 0, 2, CAST(N'2020-09-29T09:40:34.083' AS DateTime), CAST(N'2020-09-29T09:40:34.083' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Process] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([roleid], [rolename]) VALUES (1, N'admin')
GO
INSERT [dbo].[Roles] ([roleid], [rolename]) VALUES (2, N'user')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Sms] ON 
GO
INSERT [dbo].[Sms] ([smsid], [phone], [active], [name]) VALUES (1, N'0833383352', 0, N'Nguyễn Văn A')
GO
INSERT [dbo].[Sms] ([smsid], [phone], [active], [name]) VALUES (23, N'0968110143', 1, N'Nguyễn Quang Huy')
GO
SET IDENTITY_INSERT [dbo].[Sms] OFF
GO
INSERT [dbo].[Timer] ([timeid], [totaltime], [id_giai_doan]) VALUES (1, 1200, 6)
GO
INSERT [dbo].[Timer] ([timeid], [totaltime], [id_giai_doan]) VALUES (2, 43200, 7)
GO
INSERT [dbo].[Timer] ([timeid], [totaltime], [id_giai_doan]) VALUES (3, 5400, 8)
GO
ALTER TABLE [dbo].[PhoneSended]  WITH CHECK ADD FOREIGN KEY([processid])
REFERENCES [dbo].[Process] ([processid])
GO
USE [master]
GO
ALTER DATABASE [Report] SET  READ_WRITE 
GO
