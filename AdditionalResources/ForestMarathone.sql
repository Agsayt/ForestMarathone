USE [master]
GO
/****** Object:  Database [ForestMarathone]    Script Date: 14.10.2021 5:53:34 ******/
CREATE DATABASE [ForestMarathone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ForestMarathone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ForestMarathone.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ForestMarathone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ForestMarathone_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ForestMarathone] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ForestMarathone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ForestMarathone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ForestMarathone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ForestMarathone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ForestMarathone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ForestMarathone] SET ARITHABORT OFF 
GO
ALTER DATABASE [ForestMarathone] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ForestMarathone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ForestMarathone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ForestMarathone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ForestMarathone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ForestMarathone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ForestMarathone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ForestMarathone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ForestMarathone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ForestMarathone] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ForestMarathone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ForestMarathone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ForestMarathone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ForestMarathone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ForestMarathone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ForestMarathone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ForestMarathone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ForestMarathone] SET RECOVERY FULL 
GO
ALTER DATABASE [ForestMarathone] SET  MULTI_USER 
GO
ALTER DATABASE [ForestMarathone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ForestMarathone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ForestMarathone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ForestMarathone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ForestMarathone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ForestMarathone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ForestMarathone', N'ON'
GO
ALTER DATABASE [ForestMarathone] SET QUERY_STORE = OFF
GO
USE [ForestMarathone]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[MiddleName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Administrator_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](64) NOT NULL,
	[IPAddress] [varchar](15) NOT NULL,
	[ConnectionTime] [time](7) NOT NULL,
	[ValidAuth] [bit] NOT NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarathonResults]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarathonResults](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RunnerId] [int] NOT NULL,
	[MarathonTypeId] [int] NOT NULL,
	[SetId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[TimeRun] [decimal](18, 2) NOT NULL,
	[Plate] [int] NOT NULL,
 CONSTRAINT [PK_MarathonResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarathonStatus]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarathonStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_MarathonStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participant]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participant](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[MiddleName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Sex] [bit] NOT NULL,
	[Birthday] [date] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Phone] [char](15) NOT NULL,
	[Wallet] [decimal](18, 0) NOT NULL,
	[Photo] [image] NULL,
 CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sets]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Sets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsoredParticipants]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsoredParticipants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantId] [int] NOT NULL,
	[SponsorId] [int] NOT NULL,
	[SponsoredMoney] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_SponsoredParticipants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sponsors]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsors](
	[Id] [int] NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[MiddleName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[CountryId] [int] NOT NULL,
	[CardValidMonth] [int] NOT NULL,
	[CardValidYear] [int] NOT NULL,
	[TotalSum] [decimal](18, 0) NOT NULL,
	[Logo] [image] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Sponsors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeDistances]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeDistances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TypeDistances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](64) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserStatusId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 14.10.2021 5:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Administrator] ON 

INSERT [dbo].[Administrator] ([Id], [UserId], [FirstName], [MiddleName], [LastName]) VALUES (3, 1, N'Админ', N'Админович', N'Админов')
SET IDENTITY_INSERT [dbo].[Administrator] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] ON 

INSERT [dbo].[LoginHistory] ([Id], [Login], [IPAddress], [ConnectionTime], [ValidAuth]) VALUES (1, N'Admin', N'192.168.56.1', CAST(N'05:50:21.0855564' AS Time), 1)
SET IDENTITY_INSERT [dbo].[LoginHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Title]) VALUES (1, N'Administrarot')
INSERT [dbo].[Roles] ([Id], [Title]) VALUES (2, N'Participant')
INSERT [dbo].[Roles] ([Id], [Title]) VALUES (3, N'Sponsor')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Login], [Password], [RoleId], [UserStatusId]) VALUES (1, N'Admin', N'test', 1, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserStatus] ON 

INSERT [dbo].[UserStatus] ([Id], [Title]) VALUES (1, N'Activated')
INSERT [dbo].[UserStatus] ([Id], [Title]) VALUES (2, N'Suspended')
INSERT [dbo].[UserStatus] ([Id], [Title]) VALUES (3, N'Banned')
SET IDENTITY_INSERT [dbo].[UserStatus] OFF
GO
ALTER TABLE [dbo].[Administrator]  WITH CHECK ADD  CONSTRAINT [FK_Administrator_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Administrator] CHECK CONSTRAINT [FK_Administrator_Users]
GO
ALTER TABLE [dbo].[MarathonResults]  WITH CHECK ADD  CONSTRAINT [FK_MarathonResults_MarathonStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[MarathonStatus] ([Id])
GO
ALTER TABLE [dbo].[MarathonResults] CHECK CONSTRAINT [FK_MarathonResults_MarathonStatus]
GO
ALTER TABLE [dbo].[MarathonResults]  WITH CHECK ADD  CONSTRAINT [FK_MarathonResults_Participant] FOREIGN KEY([RunnerId])
REFERENCES [dbo].[Participant] ([Id])
GO
ALTER TABLE [dbo].[MarathonResults] CHECK CONSTRAINT [FK_MarathonResults_Participant]
GO
ALTER TABLE [dbo].[MarathonResults]  WITH CHECK ADD  CONSTRAINT [FK_MarathonResults_Sets] FOREIGN KEY([SetId])
REFERENCES [dbo].[Sets] ([Id])
GO
ALTER TABLE [dbo].[MarathonResults] CHECK CONSTRAINT [FK_MarathonResults_Sets]
GO
ALTER TABLE [dbo].[MarathonResults]  WITH CHECK ADD  CONSTRAINT [FK_MarathonResults_TypeDistances] FOREIGN KEY([MarathonTypeId])
REFERENCES [dbo].[TypeDistances] ([Id])
GO
ALTER TABLE [dbo].[MarathonResults] CHECK CONSTRAINT [FK_MarathonResults_TypeDistances]
GO
ALTER TABLE [dbo].[Participant]  WITH CHECK ADD  CONSTRAINT [FK_Participant_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Participant] CHECK CONSTRAINT [FK_Participant_Countries]
GO
ALTER TABLE [dbo].[Participant]  WITH CHECK ADD  CONSTRAINT [FK_Participant_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Participant] CHECK CONSTRAINT [FK_Participant_Users]
GO
ALTER TABLE [dbo].[SponsoredParticipants]  WITH CHECK ADD  CONSTRAINT [FK_SponsoredParticipants_Participant] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[Participant] ([Id])
GO
ALTER TABLE [dbo].[SponsoredParticipants] CHECK CONSTRAINT [FK_SponsoredParticipants_Participant]
GO
ALTER TABLE [dbo].[SponsoredParticipants]  WITH CHECK ADD  CONSTRAINT [FK_SponsoredParticipants_Sponsors] FOREIGN KEY([SponsorId])
REFERENCES [dbo].[Sponsors] ([Id])
GO
ALTER TABLE [dbo].[SponsoredParticipants] CHECK CONSTRAINT [FK_SponsoredParticipants_Sponsors]
GO
ALTER TABLE [dbo].[Sponsors]  WITH CHECK ADD  CONSTRAINT [FK_Sponsors_Sponsors] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Sponsors] CHECK CONSTRAINT [FK_Sponsors_Sponsors]
GO
ALTER TABLE [dbo].[Sponsors]  WITH CHECK ADD  CONSTRAINT [FK_Sponsors_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Sponsors] CHECK CONSTRAINT [FK_Sponsors_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserStatus] FOREIGN KEY([UserStatusId])
REFERENCES [dbo].[UserStatus] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserStatus]
GO
USE [master]
GO
ALTER DATABASE [ForestMarathone] SET  READ_WRITE 
GO
