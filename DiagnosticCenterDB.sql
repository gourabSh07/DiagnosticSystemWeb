/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4206)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [DiagnosticCenterDB]    Script Date: 24-Sep-17 11:41:36 PM ******/
CREATE DATABASE [DiagnosticCenterDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DiagnosticCenterDB', FILENAME = N'F:\Microsoft SQL\MSSQL13.GOURAB2016\MSSQL\DATA\DiagnosticCenterDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DiagnosticCenterDB_log', FILENAME = N'F:\Microsoft SQL\MSSQL13.GOURAB2016\MSSQL\DATA\DiagnosticCenterDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DiagnosticCenterDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiagnosticCenterDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiagnosticCenterDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiagnosticCenterDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiagnosticCenterDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DiagnosticCenterDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiagnosticCenterDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DiagnosticCenterDB] SET  MULTI_USER 
GO
ALTER DATABASE [DiagnosticCenterDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiagnosticCenterDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiagnosticCenterDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiagnosticCenterDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DiagnosticCenterDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DiagnosticCenterDB', N'ON'
GO
ALTER DATABASE [DiagnosticCenterDB] SET QUERY_STORE = OFF
GO
USE [DiagnosticCenterDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DiagnosticCenterDB]
GO
/****** Object:  Table [dbo].[TestSetup]    Script Date: 24-Sep-17 11:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestSetup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[testName] [varchar](100) NULL,
	[fee] [decimal](10, 2) NULL,
	[typeID] [int] NULL,
	[createdAt] [date] NULL,
 CONSTRAINT [PK_TestSetup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestType]    Script Date: 24-Sep-17 11:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[createdAt] [date] NULL,
 CONSTRAINT [PK_TestType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TestSetup_View]    Script Date: 24-Sep-17 11:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[TestSetup_View]
AS
SELECT ts.testname, ts.fee, tt.name
FROM dbo.TestType AS tt INNER JOIN
dbo.TestSetup AS ts ON tt.id = ts.typeID
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 24-Sep-17 11:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patientName] [varchar](150) NULL,
	[dateOfBirth] [date] NULL,
	[billNo] [varchar](1000) NULL,
	[mobile] [varchar](11) NULL,
	[paymentStatus] [bit] NULL,
	[createdAt] [date] NULL,
	[dueAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientTest]    Script Date: 24-Sep-17 11:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientTest](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patientID] [int] NULL,
	[testSetupID] [int] NULL,
	[billNo] [varchar](100) NULL,
	[createdAt] [date] NULL,
 CONSTRAINT [PK_PatientTest] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (13, N'Rubel', CAST(N'2017-08-31' AS Date), N'2430024582649306', N'01735000000', 0, CAST(N'2017-09-24' AS Date), CAST(1200.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (14, N'Rubel', CAST(N'2017-08-31' AS Date), N'2430024589044676', N'01735000000', 1, CAST(N'2017-09-24' AS Date), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (15, N'Rahaman', CAST(N'2017-09-18' AS Date), N'2430024746311343', N'32222222222', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (16, N'Mizan', CAST(N'2017-08-27' AS Date), N'2430024799722222', N'35555555555', 0, CAST(N'2017-09-24' AS Date), CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (17, N'Rahaman', CAST(N'2017-08-27' AS Date), N'2430026235983796', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(550.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (18, N'Rahaman', CAST(N'2017-08-27' AS Date), N'2430026237710069', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(550.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (19, N'Rahaman', CAST(N'2017-08-28' AS Date), N'2430026358394676', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (20, N'Rrr', CAST(N'2017-09-01' AS Date), N'2430026396455787', N'22222222222', 0, CAST(N'2017-09-24' AS Date), CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (21, N'sdas', CAST(N'2017-08-27' AS Date), N'243002642433912', N'22222222222', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (22, N'ggg', CAST(N'2017-08-27' AS Date), N'2430026454857986', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (23, N'fsfsf', CAST(N'2017-09-28' AS Date), N'2430026467123032', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (24, N'fsfsf', CAST(N'2017-09-28' AS Date), N'2430026482784722', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (25, N'fsfsf', CAST(N'2017-09-28' AS Date), N'2430026483785301', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(550.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (26, N'sdsad', CAST(N'2017-09-08' AS Date), N'243002648573287', N'23333333333', 0, CAST(N'2017-09-24' AS Date), CAST(450.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (27, N'ggg', CAST(N'2017-08-31' AS Date), N'2430026502334606', N'44444444444', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1017, N'honey', CAST(N'2017-09-14' AS Date), N'2430029312483912', N'33333333333', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1018, N'Gonie', CAST(N'2017-09-04' AS Date), N'243002935544838', N'23232323232', 0, CAST(N'2017-09-24' AS Date), CAST(450.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1019, N'Miaz', CAST(N'2017-09-06' AS Date), N'2430029401353704', N'32232321134', 0, CAST(N'2017-09-24' AS Date), CAST(350.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1020, N'ghghgh', CAST(N'2017-09-07' AS Date), N'2430029416308796', N'23245445455', 0, CAST(N'2017-09-24' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1021, N'ggj', CAST(N'2017-09-21' AS Date), N'243002944480162', N'23232323232', 0, CAST(N'2017-09-24' AS Date), CAST(350.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1022, N'ggj', CAST(N'2017-09-21' AS Date), N'2430029451259838', N'23232323232', 0, CAST(N'2017-09-24' AS Date), CAST(750.00 AS Decimal(18, 2)))
INSERT [dbo].[Patient] ([id], [patientName], [dateOfBirth], [billNo], [mobile], [paymentStatus], [createdAt], [dueAmount]) VALUES (1023, N'MdRuebl', CAST(N'2017-08-27' AS Date), N'2430029559973495', N'34343434343', 0, CAST(N'2017-09-24' AS Date), CAST(1400.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET IDENTITY_INSERT [dbo].[PatientTest] ON 

INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (14, 13, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (15, 13, 4, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (16, 13, 5, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (17, 14, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (18, 14, 4, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (19, 14, 5, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (20, 15, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (21, 16, 6, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (22, 17, 7, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (23, 18, 7, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (24, 19, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (25, 20, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (26, 22, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (27, 23, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (28, 24, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (29, 25, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (30, 25, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (31, 26, 5, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (32, 27, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1022, 1017, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1023, 1018, 5, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1024, 1019, 4, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1025, 1020, 2, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1026, 1021, 4, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1027, 1022, 4, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1028, 1022, 3, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1029, 1023, 1012, NULL, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[PatientTest] ([id], [patientID], [testSetupID], [billNo], [createdAt]) VALUES (1030, 1023, 3, NULL, CAST(N'2017-09-24' AS Date))
SET IDENTITY_INSERT [dbo].[PatientTest] OFF
SET IDENTITY_INSERT [dbo].[TestSetup] ON 

INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (2, N'RBS', CAST(150.00 AS Decimal(10, 2)), 12, CAST(N'2017-09-21' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (3, N'Complete Blood count (Total Count-Differential Count-ESR, Hb %)', CAST(400.00 AS Decimal(10, 2)), 12, CAST(N'2017-09-22' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (4, N'S. Creatinine', CAST(350.00 AS Decimal(10, 2)), 12, CAST(N'2017-09-22' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (5, N'Lipid profile', CAST(450.00 AS Decimal(10, 2)), 12, CAST(N'2017-09-22' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (6, N'Hand X-ray', CAST(200.00 AS Decimal(10, 2)), 13, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (7, N'Lower Abdomen', CAST(550.00 AS Decimal(10, 2)), 14, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (1007, N'Feet X-ray', CAST(300.00 AS Decimal(10, 2)), 13, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (1008, N'LS Spine', CAST(1100.00 AS Decimal(10, 2)), 13, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (1009, N'Whole Abdomen', CAST(800.00 AS Decimal(10, 2)), 14, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (1010, N'Pregnancy profile', CAST(550.00 AS Decimal(10, 2)), 14, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (1011, N'ECG', CAST(150.00 AS Decimal(10, 2)), 15, CAST(N'2017-09-24' AS Date))
INSERT [dbo].[TestSetup] ([id], [testName], [fee], [typeID], [createdAt]) VALUES (1012, N'Echo', CAST(1000.00 AS Decimal(10, 2)), 16, CAST(N'2017-09-24' AS Date))
SET IDENTITY_INSERT [dbo].[TestSetup] OFF
SET IDENTITY_INSERT [dbo].[TestType] ON 

INSERT [dbo].[TestType] ([id], [name], [createdAt]) VALUES (12, N'Blood', CAST(N'2017-09-21' AS Date))
INSERT [dbo].[TestType] ([id], [name], [createdAt]) VALUES (13, N'X-Ray', CAST(N'2017-09-21' AS Date))
INSERT [dbo].[TestType] ([id], [name], [createdAt]) VALUES (14, N'USG', CAST(N'2017-09-21' AS Date))
INSERT [dbo].[TestType] ([id], [name], [createdAt]) VALUES (15, N'ECG', CAST(N'2017-09-21' AS Date))
INSERT [dbo].[TestType] ([id], [name], [createdAt]) VALUES (16, N'Echo', CAST(N'2017-09-21' AS Date))
SET IDENTITY_INSERT [dbo].[TestType] OFF
ALTER TABLE [dbo].[PatientTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientTest_Patient] FOREIGN KEY([patientID])
REFERENCES [dbo].[Patient] ([id])
GO
ALTER TABLE [dbo].[PatientTest] CHECK CONSTRAINT [FK_PatientTest_Patient]
GO
ALTER TABLE [dbo].[PatientTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientTest_TestSetup] FOREIGN KEY([testSetupID])
REFERENCES [dbo].[TestSetup] ([id])
GO
ALTER TABLE [dbo].[PatientTest] CHECK CONSTRAINT [FK_PatientTest_TestSetup]
GO
ALTER TABLE [dbo].[TestSetup]  WITH CHECK ADD  CONSTRAINT [FK_TestSetup_TestType] FOREIGN KEY([typeID])
REFERENCES [dbo].[TestType] ([id])
GO
ALTER TABLE [dbo].[TestSetup] CHECK CONSTRAINT [FK_TestSetup_TestType]
GO
USE [master]
GO
ALTER DATABASE [DiagnosticCenterDB] SET  READ_WRITE 
GO
