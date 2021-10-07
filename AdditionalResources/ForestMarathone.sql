USE [master]
GO
/****** Object:  Database [ForestMarathone]    Script Date: 07.10.2021 4:00:38 ******/
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
/****** Object:  Table [dbo].[Countries]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[Id] [int] NOT NULL,
	[Login] [varchar](64) NOT NULL,
	[IPAddress] [varchar](15) NOT NULL,
	[ConnectionTime] [time](7) NOT NULL,
	[ValidAuth] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarathonResults]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarathonResults](
	[Id] [int] NOT NULL,
	[RunnerId] [int] NOT NULL,
	[MarathonTypeId] [int] NOT NULL,
	[SetId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[TimeRun] [decimal](18, 2) NOT NULL,
	[Plate] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarathonStatus]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarathonStatus](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](64) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participant]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participant](
	[Id] [int] NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[MiddleName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Sex] [bit] NOT NULL,
	[Birthday] [date] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Phone] [char](15) NOT NULL,
	[Wallet] [decimal](18, 0) NOT NULL,
	[Photo] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](15) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sets]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sets](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsoredParticipants]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsoredParticipants](
	[Id] [int] NOT NULL,
	[ParticipantId] [int] NOT NULL,
	[SponsorId] [int] NOT NULL,
	[SponsoredMoney] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sponsors]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsors](
	[Id] [int] NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[MiddleName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[CardValidMonth] [int] NOT NULL,
	[CardValidYear] [int] NOT NULL,
	[TotalSum] [decimal](18, 0) NOT NULL,
	[Logo] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeDistances]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeDistances](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07.10.2021 4:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
	[Login] [varchar](64) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[RoleId] [int] NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ForestMarathone] SET  READ_WRITE 
GO
