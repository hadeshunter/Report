USE [master]
GO
/****** Object:  Database [AuthReport]    Script Date: 05/10/2020 8:38:50 AM ******/
CREATE DATABASE [AuthReport]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AuthDashboard', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AuthDashboard.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AuthDashboard_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AuthDashboard_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AuthReport] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AuthReport].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AuthReport] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AuthReport] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AuthReport] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AuthReport] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AuthReport] SET ARITHABORT OFF 
GO
ALTER DATABASE [AuthReport] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AuthReport] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AuthReport] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AuthReport] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AuthReport] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AuthReport] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AuthReport] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AuthReport] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AuthReport] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AuthReport] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AuthReport] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AuthReport] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AuthReport] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AuthReport] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AuthReport] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AuthReport] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AuthReport] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AuthReport] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AuthReport] SET  MULTI_USER 
GO
ALTER DATABASE [AuthReport] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AuthReport] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AuthReport] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AuthReport] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AuthReport] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AuthReport]
GO
/****** Object:  User [us_report]    Script Date: 05/10/2020 8:38:50 AM ******/
CREATE USER [us_report] FOR LOGIN [us_report] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [us_report]
GO
/****** Object:  Table [dbo].[GroupPermissions]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermissions](
	[groupid] [int] NOT NULL,
	[perid] [int] NOT NULL,
	[createday] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[groupid] ASC,
	[perid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[groupid] [int] IDENTITY(1,1) NOT NULL,
	[groupname] [nvarchar](4000) NOT NULL,
	[active] [bit] NULL,
	[note] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[groupid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[langid] [int] IDENTITY(1,1) NOT NULL,
	[langname] [nvarchar](2000) NOT NULL,
	[active] [bit] NULL,
	[languages] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[langid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[perid] [int] IDENTITY(1,1) NOT NULL,
	[pername] [nvarchar](4000) NOT NULL,
	[policy] [nvarchar](4000) NOT NULL,
	[active] [bit] NULL,
	[link] [nvarchar](4000) NULL,
	[action] [nvarchar](1000) NULL,
	[note] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[perid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionTranslations]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionTranslations](
	[transid] [int] NOT NULL,
	[perid] [int] NOT NULL,
	[languages] [nvarchar](2000) NOT NULL,
	[pername] [nvarchar](4000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[transid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[roleid] [int] IDENTITY(1,1) NOT NULL,
	[rolename] [nvarchar](2000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[usid] [int] NOT NULL,
	[groupid] [int] NOT NULL,
	[createday] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[usid] ASC,
	[groupid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/10/2020 8:38:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[usid] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](4000) NOT NULL,
	[email] [nvarchar](2000) NULL,
	[password] [nvarchar](4000) NOT NULL,
	[phone] [nvarchar](10) NULL,
	[role] [int] NOT NULL,
	[address] [nvarchar](4000) NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[usid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (1, 3, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (1, 4, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (1, 5, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (1, 6, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (1, 7, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (2, 3, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (2, 4, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (3, 3, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[GroupPermissions] ([groupid], [perid], [createday]) VALUES (3, 4, CAST(N'2020-09-28T13:34:40.500' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 
GO
INSERT [dbo].[Groups] ([groupid], [groupname], [active], [note]) VALUES (1, N'admin', 1, NULL)
GO
INSERT [dbo].[Groups] ([groupid], [groupname], [active], [note]) VALUES (2, N'user', 1, N'user')
GO
INSERT [dbo].[Groups] ([groupid], [groupname], [active], [note]) VALUES (3, N'mod', 1, N'moderator')
GO
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Languages] ON 
GO
INSERT [dbo].[Languages] ([langid], [langname], [active], [languages]) VALUES (1, N'vi', 1, N'vi')
GO
INSERT [dbo].[Languages] ([langid], [langname], [active], [languages]) VALUES (2, N'en', 1, N'en')
GO
SET IDENTITY_INSERT [dbo].[Languages] OFF
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 
GO
INSERT [dbo].[Permissions] ([perid], [pername], [policy], [active], [link], [action], [note]) VALUES (3, N'OBSERVATION', N'observationComponent', 1, N'observation', NULL, NULL)
GO
INSERT [dbo].[Permissions] ([perid], [pername], [policy], [active], [link], [action], [note]) VALUES (4, N'REPORT_LIST_SLA', N'reportListSLAComponent', 1, N'reportListSLA', NULL, NULL)
GO
INSERT [dbo].[Permissions] ([perid], [pername], [policy], [active], [link], [action], [note]) VALUES (5, N'SETTING_SLA', N'settingComponent', 1, N'setting', NULL, NULL)
GO
INSERT [dbo].[Permissions] ([perid], [pername], [policy], [active], [link], [action], [note]) VALUES (6, N'USER', N'userComponent', 1, N'user', NULL, NULL)
GO
INSERT [dbo].[Permissions] ([perid], [pername], [policy], [active], [link], [action], [note]) VALUES (7, N'PERMISSION', N'permissionComponent', 1, N'permission', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (1, 3, N'vi', N'Giám sát yêu cầu')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (2, 3, N'en', N'Observation')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (3, 4, N'vi', N'Thống kê danh sách SLA')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (4, 4, N'en', N'Statistic Report list SLA')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (5, 5, N'vi', N'Cài đặt SLA')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (6, 5, N'en', N'Setting SLA')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (7, 6, N'vi', N'Người dùng')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (8, 6, N'en', N'User')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (9, 7, N'vi', N'Phân quyền')
GO
INSERT [dbo].[PermissionTranslations] ([transid], [perid], [languages], [pername]) VALUES (10, 7, N'en', N'Permission')
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([roleid], [rolename]) VALUES (1, N'admin')
GO
INSERT [dbo].[Roles] ([roleid], [rolename]) VALUES (2, N'user')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (6, 1, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (7, 2, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (8, 3, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (14, 1, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (15, 1, CAST(N'2020-09-28T10:49:53.220' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (54, 1, CAST(N'2020-10-02T09:51:26.737' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (81, 1, CAST(N'2020-10-02T09:58:33.427' AS DateTime))
GO
INSERT [dbo].[UserPermissions] ([usid], [groupid], [createday]) VALUES (85, 2, CAST(N'2020-10-02T09:58:33.427' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (6, N'admin', NULL, N'AQAAAAEAAAPoAAAAEEOJDkYXzr7CHJZ9rOw0zNUAcRmVh05C9JXdgGCY7M8SYSmeqFVbtmWiuSRTwkJGag==', NULL, 1, N'12/1', 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (7, N'user', NULL, N'AQAAAAEAAAPoAAAAEPGVKf9EUtbVZVpMg9iaUNQMKlRwbTRkb+w+SYj/PwrIXy2SYDMZj7H30GkqyACiTQ==', NULL, 1, N'dfasdfk', 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (8, N'mod', NULL, N'AQAAAAEAAAPoAAAAEB5Hj8FThAQjkI93/bDHVLVuIgEM91S8UAMop8KhoIFICPPz2To3fwJoo3bHuTI4iw==', NULL, 1, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (14, N'hampn.hcm', NULL, N'AQAAAAEAAAPoAAAAEB3nKqbv9yl8/VNW2zWU3U5205gkOSBG6ZSJwxXT6mYnH+6I7ZbMSAAv10A97Alqng==', N'0919991745', 1, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (15, N'nganph.hcm', NULL, N'AQAAAAEAAAPoAAAAEMGSNDb4EY+9FGmmcI0nqlawn6iOwIp90a+JguYigprd+sSaObf53tenFZpp5nHKKA==', N'0947837878', 1, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (20, N'test', NULL, N'AQAAAAEAAAPoAAAAEIrKg/BF3nRQYS7FHc5kQHDB5niAS2gg0gXNsIpAsqQQLoAh/j7eGpQ78x4VpvAEPQ==', N'12345678', 2, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (23, N'test1', NULL, N'AQAAAAEAAAPoAAAAEFab+sW/YEk6YYeH39aW05iXNjExwNYEZ4M/XBby+GQ0gNcCirDRwX9+W/Aks0s2Mg==', N'', 2, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (54, N'hentvh.hcm', NULL, N'AQAAAAEAAAPoAAAAEL6VqXuhNIBJPZoCgXA9lQjzT2jXm/GyD0j+N0j1we+dzdgTwLV6PPCdSVdwjcP6pA==', NULL, 2, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (81, N'minhtri.hcm', NULL, N'AQAAAAEAAAPoAAAAELjbc/JsJKgyetgXxLzJnsHVDuW4CPQdDC5cfnFQWmvfQGpo6IxUzfLNd4tX3vNLiQ==', NULL, 2, NULL, 1)
GO
INSERT [dbo].[Users] ([usid], [username], [email], [password], [phone], [role], [address], [active]) VALUES (85, N'huynq2.hcm', NULL, N'AQAAAAEAAAPoAAAAEKUHx8OyCFWOPQO2froAIbsYVQ/0GfZZWNAFYBnOrGsv6jM+vviveDnyfp9HLp61IA==', NULL, 2, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD FOREIGN KEY([groupid])
REFERENCES [dbo].[Groups] ([groupid])
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD FOREIGN KEY([perid])
REFERENCES [dbo].[Permissions] ([perid])
GO
ALTER TABLE [dbo].[PermissionTranslations]  WITH CHECK ADD FOREIGN KEY([perid])
REFERENCES [dbo].[Permissions] ([perid])
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD FOREIGN KEY([groupid])
REFERENCES [dbo].[Groups] ([groupid])
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD FOREIGN KEY([usid])
REFERENCES [dbo].[Users] ([usid])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([role])
REFERENCES [dbo].[Roles] ([roleid])
GO
USE [master]
GO
ALTER DATABASE [AuthReport] SET  READ_WRITE 
GO
