USE [master]
GO
/****** Object:  Database [Domies]    Script Date: 21.03.2025 10:31:47 ******/
CREATE DATABASE [Domies]
CONTAINMENT = NONE
GO
ALTER DATABASE [Domies] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Domies].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Domies] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Domies] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Domies] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Domies] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Domies] SET ARITHABORT OFF 
GO
ALTER DATABASE [Domies] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Domies] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Domies] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Domies] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Domies] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Domies] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Domies] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Domies] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Domies] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Domies] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Domies] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Domies] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Domies] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Domies] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Domies] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Domies] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Domies] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Domies] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Domies] SET  MULTI_USER 
GO
ALTER DATABASE [Domies] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Domies] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Domies] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Domies] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Domies] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Domies] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Domies] SET QUERY_STORE = ON
GO
ALTER DATABASE [Domies] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Domies]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[country] [varchar](50) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[street] [varchar](50) NOT NULL,
	[postal_code] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Animals]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pet_name] [varchar](30) NOT NULL,
	[specific_description] [varchar](max) NULL,
	[owner] [varchar](50) NOT NULL,
	[animal_type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnimalType]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnimalType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[animal_type] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date_start] [datetime] NOT NULL,
	[date_end] [datetime] NOT NULL,
	[offer_id] [int] NOT NULL,
	[applicant] [varchar](50) NOT NULL,
	[application_date_add] [datetime] NULL,
	[note] [text] NULL,
	[application_status] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationsAnimals]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationsAnimals](
	[application_id] [int] NOT NULL,
	[animal_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[application_id] ASC,
	[animal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facilities]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facilities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[facilities_type] [varchar](50) NOT NULL,
	[facilities_description] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfferAnimalType]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferAnimalType](
	[offer_id] [int] NOT NULL,
	[animal_type_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[offer_id] ASC,
	[animal_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfferFacilities]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferFacilities](
	[offer_id] [int] NOT NULL,
	[facilitie_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[offer_id] ASC,
	[facilitie_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offers]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[offer_description] [varchar](max) NULL,
	[host] [varchar](50) NOT NULL,
	[address_id] [int] NOT NULL,
	[date_add] [datetime] NULL,
	[price] [decimal](10, 2) NULL,
	[photo_id] [int] NULL,
	[petSitterDescription] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opinions]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opinions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rating] [int] NULL,
	[comment] [varchar](max) NULL,
	[application_id] [int] NOT NULL,
	[user_email] [varchar](50) NOT NULL,
	[opinion_date_add] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[extension] [nvarchar](10) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[binary_data] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21.03.2025 10:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[email] [varchar](50) NOT NULL,
	[first_name] [varchar](30) NOT NULL,
	[last_name] [varchar](30) NOT NULL,
	[password] [varchar](255) NULL,
	[role_id] [int] NOT NULL,
	[date_add] [datetime] NULL,
	[email_verified] [bit] NOT NULL,
	[email_verification_token] [varchar](255) NULL,
	[phone_number] [char](9) NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Applications] ADD  DEFAULT (getdate()) FOR [application_date_add]
GO
ALTER TABLE [dbo].[Offers] ADD  DEFAULT (getdate()) FOR [date_add]
GO
ALTER TABLE [dbo].[Opinions] ADD  DEFAULT (getdate()) FOR [opinion_date_add]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_RoleId]  DEFAULT ((1)) FOR [role_id]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [date_add]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [email_verified]
GO
ALTER TABLE [dbo].[Animals]  WITH CHECK ADD FOREIGN KEY([animal_type])
REFERENCES [dbo].[AnimalType] ([id])
GO
ALTER TABLE [dbo].[Animals]  WITH CHECK ADD FOREIGN KEY([owner])
REFERENCES [dbo].[Users] ([email])
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK__Applicati__applicant__2B0A656D] FOREIGN KEY([applicant])
REFERENCES [dbo].[Users] ([email])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK__Applicati__applicant__2B0A656D]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD FOREIGN KEY([offer_id])
REFERENCES [dbo].[Offers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationsAnimals]  WITH CHECK ADD FOREIGN KEY([animal_id])
REFERENCES [dbo].[Animals] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationsAnimals]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[Applications] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OfferAnimalType]  WITH CHECK ADD FOREIGN KEY([animal_type_id])
REFERENCES [dbo].[AnimalType] ([id])
GO
ALTER TABLE [dbo].[OfferAnimalType]  WITH CHECK ADD FOREIGN KEY([offer_id])
REFERENCES [dbo].[Offers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OfferFacilities]  WITH CHECK ADD FOREIGN KEY([facilitie_id])
REFERENCES [dbo].[Facilities] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OfferFacilities]  WITH CHECK ADD FOREIGN KEY([offer_id])
REFERENCES [dbo].[Offers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD FOREIGN KEY([address_id])
REFERENCES [dbo].[Addresses] ([id])
GO
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD FOREIGN KEY([host])
REFERENCES [dbo].[Users] ([email])
GO
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD  CONSTRAINT [FK_Offers_Photo] FOREIGN KEY([photo_id])
REFERENCES [dbo].[Photo] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Offers] CHECK CONSTRAINT [FK_Offers_Photo]
GO
ALTER TABLE [dbo].[Opinions]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[Applications] ([id])
GO
ALTER TABLE [dbo].[Opinions]  WITH CHECK ADD FOREIGN KEY([user_email])
REFERENCES [dbo].[Users] ([email])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[Opinions]  WITH CHECK ADD CHECK  (([rating]>=(1) AND [rating]<=(5)))
GO
USE [master]
GO
ALTER DATABASE [Domies] SET  READ_WRITE 
GO
