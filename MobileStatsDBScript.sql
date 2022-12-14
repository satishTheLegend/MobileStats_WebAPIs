USE [master]
GO
/****** Object:  Database [MobileStats]    Script Date: 08-09-2022 09:59:33 ******/
CREATE DATABASE [MobileStats]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MobileStats', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SATISHPC\MSSQL\DATA\MobileStats.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MobileStats_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SATISHPC\MSSQL\DATA\MobileStats_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MobileStats] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MobileStats].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MobileStats] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MobileStats] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MobileStats] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MobileStats] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MobileStats] SET ARITHABORT OFF 
GO
ALTER DATABASE [MobileStats] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MobileStats] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MobileStats] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MobileStats] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MobileStats] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MobileStats] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MobileStats] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MobileStats] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MobileStats] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MobileStats] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MobileStats] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MobileStats] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MobileStats] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MobileStats] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MobileStats] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MobileStats] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MobileStats] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MobileStats] SET RECOVERY FULL 
GO
ALTER DATABASE [MobileStats] SET  MULTI_USER 
GO
ALTER DATABASE [MobileStats] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MobileStats] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MobileStats] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MobileStats] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MobileStats] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MobileStats] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MobileStats', N'ON'
GO
ALTER DATABASE [MobileStats] SET QUERY_STORE = OFF
GO
USE [MobileStats]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 08-09-2022 09:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MobileBrands]    Script Date: 08-09-2022 09:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MobileBrands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Brand_id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Remove_dt] [nvarchar](max) NULL,
	[Added_dt] [nvarchar](max) NULL,
	[Updated_dt] [nvarchar](max) NULL,
 CONSTRAINT [PK_MobileBrands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MobileDevices]    Script Date: 08-09-2022 09:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MobileDevices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Device_id] [int] NOT NULL,
	[Url_hash] [nvarchar](max) NULL,
	[Brand_id] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Picture] [nvarchar](max) NULL,
	[Released_at] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Os] [nvarchar](max) NULL,
	[Storage] [nvarchar](max) NULL,
	[Display_size] [nvarchar](max) NULL,
	[Display_resolution] [nvarchar](max) NULL,
	[Camera_pixels] [nvarchar](max) NULL,
	[Video_pixels] [nvarchar](max) NULL,
	[Ram] [nvarchar](max) NULL,
	[Chipset] [nvarchar](max) NULL,
	[Battery_size] [nvarchar](max) NULL,
	[Battery_type] [nvarchar](max) NULL,
	[Specifications] [nvarchar](max) NULL,
	[Deleted_at] [nvarchar](max) NULL,
	[Created_at] [nvarchar](max) NULL,
	[Updated_at] [nvarchar](max) NULL,
	[Price] [int] NOT NULL,
	[Dis_Price] [int] NULL,
	[Max_discount] [int] NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_MobileDevices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MobileSellingRecords]    Script Date: 08-09-2022 09:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MobileSellingRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_Id] [nvarchar](max) NOT NULL,
	[Brand] [nvarchar](max) NULL,
	[Brand_id] [nvarchar](max) NULL,
	[DeviceName] [nvarchar](max) NULL,
	[Device_id] [nvarchar](max) NULL,
	[IMEI_number] [nvarchar](max) NULL,
	[Original_price] [nvarchar](max) NULL,
	[MaxDiscount_price] [nvarchar](max) NULL,
	[Selling_price] [nvarchar](max) NULL,
	[Max_discount] [nvarchar](max) NULL,
	[Given_discount] [nvarchar](max) NULL,
	[IsDiscountApplied] [bit] NULL,
	[Selling_dt] [datetime2](7) NULL,
	[TransactionUpdated_at] [nvarchar](max) NULL,
	[Buyer_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_MobileSellingRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [MobileStats] SET  READ_WRITE 
GO
