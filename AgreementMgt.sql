USE [master]
GO
/****** Object:  Database [AgreementMgt]    Script Date: 25-10-2021 07:43:35 ******/
CREATE DATABASE [AgreementMgt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AgreementMgt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSRV\MSSQL\DATA\AgreementMgt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AgreementMgt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSRV\MSSQL\DATA\AgreementMgt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AgreementMgt] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgreementMgt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgreementMgt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgreementMgt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgreementMgt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgreementMgt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgreementMgt] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgreementMgt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AgreementMgt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgreementMgt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgreementMgt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgreementMgt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgreementMgt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgreementMgt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgreementMgt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgreementMgt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgreementMgt] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgreementMgt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgreementMgt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgreementMgt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgreementMgt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgreementMgt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgreementMgt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AgreementMgt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgreementMgt] SET RECOVERY FULL 
GO
ALTER DATABASE [AgreementMgt] SET  MULTI_USER 
GO
ALTER DATABASE [AgreementMgt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgreementMgt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgreementMgt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgreementMgt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AgreementMgt] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AgreementMgt] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AgreementMgt', N'ON'
GO
ALTER DATABASE [AgreementMgt] SET QUERY_STORE = OFF
GO
USE [AgreementMgt]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](16) NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[Users]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[Users]
As
	Select U.Email,U.Id,U.FirstName,U.LastName,R.Name RoleName
	From AspNetUsers U
	Inner Join AspNetUserRoles UR On U.Id = UR.UserId
	Inner Join AspNetRoles R On UR.RoleId = R.Id
GO
/****** Object:  Table [dbo].[ProductGroup]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupDescription] [varchar](1024) NOT NULL,
	[GroupCode] [varchar](16) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Agreement]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agreement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ProductGroupId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[EffectiveDate] [date] NULL,
	[ExpirationDate] [date] NULL,
	[NewPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Agreement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductGroupId] [int] NOT NULL,
	[ProductDescription] [varchar](1024) NOT NULL,
	[ProductNumber] [varchar](16) NOT NULL,
	[Price] [decimal](10, 2) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UserAgreements]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[UserAgreements]
As
Select A.[Id]
      ,A.[UserId]
      ,A.[ProductGroupId]
      ,A.[ProductId]
      ,CONVERT(varchar, A.[EffectiveDate],101) AS EffectiveDate
      ,CONVERT(varchar, A.[ExpirationDate],101) AS ExpirationDate
      ,A.[NewPrice],
	  P.ProductNumber,P.ProductDescription,P.Price ProductPrice,PG.GroupDescription,PG.GroupCode
,U.FirstName,U.LastName,U.Email
From Agreement A 
Inner Join Product P On A.ProductId = P.Id
Inner Join ProductGroup PG On P.ProductGroupId = PG.Id
Inner Join Users U On A.UserId = U.Id
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25-10-2021 07:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Agreement] ON 
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (1, N'1', 1, 1, NULL, NULL, CAST(800.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (2, N'1', 2, 12, CAST(N'2021-10-22' AS Date), NULL, CAST(730.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (3, N'2', 3, 19, NULL, NULL, CAST(85.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (6, N'1', 2, 12, CAST(N'2021-09-03' AS Date), CAST(N'2021-10-09' AS Date), CAST(455.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (7, N'1', 1, 1, CAST(N'2021-10-07' AS Date), CAST(N'2021-11-06' AS Date), CAST(550.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (8, N'1', 1, 8, CAST(N'2021-09-27' AS Date), CAST(N'2021-11-12' AS Date), CAST(630.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (9, N'1', 1, 10, CAST(N'2021-08-21' AS Date), CAST(N'2021-11-06' AS Date), CAST(900.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Agreement] ([Id], [UserId], [ProductGroupId], [ProductId], [EffectiveDate], [ExpirationDate], [NewPrice]) VALUES (11, N'1', 3, 18, CAST(N'2021-10-05' AS Date), CAST(N'2021-10-09' AS Date), CAST(730.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Agreement] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'COO')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'CTO')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Principal')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'Designer')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'QA')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4', N'4')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5', N'5')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [FirstName], [LastName], [EmailConfirmed], [PasswordHash], [PhoneNumber], [UserName]) VALUES (N'1', N'vladica@mvp-soft.com', N'Vladica', N'Ognjanovic', 1, N'1234', N'+381523689', N'vladica@mvp-soft.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [FirstName], [LastName], [EmailConfirmed], [PasswordHash], [PhoneNumber], [UserName]) VALUES (N'2', N'milos@mvp-soft.com', N'Milos', N'Rasuo', 1, N'1234', N'+381569874', N'milos@mvp-soft.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [FirstName], [LastName], [EmailConfirmed], [PasswordHash], [PhoneNumber], [UserName]) VALUES (N'3', N'predrag@mvp-soft.com', N'Predrag', N'Filipovic', 1, N'1234', N'+381856934', N'predrag@mvp-soft.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [FirstName], [LastName], [EmailConfirmed], [PasswordHash], [PhoneNumber], [UserName]) VALUES (N'4', N'darko@mvp-soft.com', N'Darko', N'Medenica', 1, N'1234', N'+381526498', N'darko@mvp-soft.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [FirstName], [LastName], [EmailConfirmed], [PasswordHash], [PhoneNumber], [UserName]) VALUES (N'5', N'marjan@mvp-soft.com', N'Marjan', N'Stojnev', 1, N'1234', N'+381523698', N'marjan@mvp-soft.com')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (1, 1, N'DELL Vostro Core i3', N'101', CAST(950.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (3, 1, N'DELL G15 Core i5 10th Gen', N'102', CAST(900.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (4, 1, N'DELL G15 Core i7 10th Gen', N'103', CAST(800.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (5, 1, N'HP Chromebook MT8183', N'104', CAST(850.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (6, 1, N'HP HP Pavilion Ryzen 5 Hexa Core 4600H', N'105', CAST(700.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (8, 1, N'HP Pavilion Ryzen 5 Hexa Core 5600H', N'106', CAST(650.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (9, 1, N'Lenovo Thinkbook Core i3 11th Gen', N'107', CAST(900.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (10, 1, N'Lenovo IdeaPad 3 Core i3 10th Gen', N'108', CAST(930.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (11, 1, N'Lenovo Thinkbook Core i7 11th Gen', N'109', CAST(1000.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (12, 2, N'APPLE iPhone 12', N'110', CAST(500.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (13, 2, N'Apple iPhone XR ((PRODUCT)RED, 128 GB) ', N'111', CAST(600.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (14, 2, N'APPLE iPhone 13 Pro (Gold, 1 TB)', N'112', CAST(700.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (15, 2, N'SAMSUNG Galaxy F22', N'113', CAST(150.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (16, 2, N'SAMSUNG Galaxy M12 (Blue, 64 GB)  (4 GB RAM)', N'114', CAST(130.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (17, 2, N'SAMSUNG Galaxy A21s (Black, 128 GB)  (6 GB RAM)', N'115', CAST(120.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (18, 3, N'Flipkart SmartBuy D68AV Dual Screens Real 4K 16MP Wifi HDR Video Sports and Action Camera  (Black, 16 MP)', N'116', CAST(100.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (19, 3, N'SONY ILCE-7C/BQ IN5 Mirrorless Camera Mirrorless  (Black)', N'117', CAST(90.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[Product] ([Id], [ProductGroupId], [ProductDescription], [ProductNumber], [Price], [Active]) VALUES (20, 3, N'Canon EOS 1500D DSLR Camera Body+ 18-55 mm IS II Lens  (Black)', N'118', CAST(150.00 AS Decimal(10, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductGroup] ON 
GO
INSERT [dbo].[ProductGroup] ([Id], [GroupDescription], [GroupCode], [Active]) VALUES (1, N'Laptop', N'L101', 1)
GO
INSERT [dbo].[ProductGroup] ([Id], [GroupDescription], [GroupCode], [Active]) VALUES (2, N'Mobile', N'M102', 1)
GO
INSERT [dbo].[ProductGroup] ([Id], [GroupDescription], [GroupCode], [Active]) VALUES (3, N'Camera', N'C103', 1)
GO
SET IDENTITY_INSERT [dbo].[ProductGroup] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_ProductNumber]    Script Date: 25-10-2021 07:43:35 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [UK_ProductNumber] UNIQUE NONCLUSTERED 
(
	[ProductNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_GroupCode]    Script Date: 25-10-2021 07:43:35 ******/
ALTER TABLE [dbo].[ProductGroup] ADD  CONSTRAINT [UK_GroupCode] UNIQUE NONCLUSTERED 
(
	[GroupCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY([ProductGroupId])
REFERENCES [dbo].[ProductGroup] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductGroup]
GO
USE [master]
GO
ALTER DATABASE [AgreementMgt] SET  READ_WRITE 
GO
