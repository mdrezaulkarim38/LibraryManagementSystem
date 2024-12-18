USE [master]
GO
/****** Object:  Database [LibraryDB]    Script Date: 11/10/2024 9:42:55 PM ******/
CREATE DATABASE [LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\LibraryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\LibraryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LibraryDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LibraryDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibraryDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/10/2024 9:42:55 PM ******/
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
/****** Object:  Table [dbo].[Books]    Script Date: 11/10/2024 9:42:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[TotalCopies] [int] NOT NULL,
	[AvailableCopies] [int] NOT NULL,
	[PublishedDate] [datetime2](7) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BorrowedBooks]    Script Date: 11/10/2024 9:42:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowedBooks](
	[BorrowedBookId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[BorrowedDate] [datetime2](7) NOT NULL,
	[ReturnDueDate] [datetime2](7) NOT NULL,
	[IsReturned] [bit] NOT NULL,
	[IsUserRequestReturn] [bit] NULL,
 CONSTRAINT [PK_BorrowedBooks] PRIMARY KEY CLUSTERED 
(
	[BorrowedBookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BorrowRequests]    Script Date: 11/10/2024 9:42:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowRequests](
	[BorrowRequestId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BorrowRequests] PRIMARY KEY CLUSTERED 
(
	[BorrowRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/10/2024 9:42:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/10/2024 9:42:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NULL,
	[MembershipStartDate] [datetime2](7) NOT NULL,
	[MembershipEndDate] [datetime2](7) NULL,
	[NIDNumber] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (4, N'The 7 Habits of Highly Effective People', N'Stephen R. Covey', N'Covey presents a principle-centered approach to solving personal and professional challenges. The book emphasizes habits like proactivity, goal-setting, and effective communication, offering a path to personal and leadership effectiveness.', 10, 1, CAST(N'1989-01-10T00:00:00.0000000' AS DateTime2), 12, N'/images/books/247d3908-aa4f-4f97-9af0-8ea36b9f17a3.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (5, N'You Are a Badass', N'Jen Sincero', N' A humorous and motivational guide, this book aims to help readers stop self-sabotaging and build confidence. Sincero shares personal anecdotes and actionable advice to help readers create the life they want.', 4, 0, CAST(N'2013-01-05T00:00:00.0000000' AS DateTime2), 12, N'/images/books/e7f04be2-b31b-4b21-a10c-c6725de431f3.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (6, N' The 10X Rule: The Only Difference Between Success and Failure', N'Grant Cardone', N'Cardone introduces the concept of the 10X Rule, where you set goals ten times larger than you think possible and take massive action to achieve them. The book motivates readers to break free from mediocrity and aim for extraordinary results.', 5, 0, CAST(N'2011-05-10T00:00:00.0000000' AS DateTime2), 12, N'/images/books/84171849-9d47-45cd-a503-cc76233bbc3e.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (7, N'Atomic Habits', N'James Clear', N'A guide to developing good habits and breaking bad ones, this book emphasizes the power of small changes to achieve remarkable results. Clear provides actionable strategies backed by scientific research to help readers create lasting habits.', 10, 1, CAST(N'2018-05-10T00:00:00.0000000' AS DateTime2), 7, N'/images/books/4f57bc3d-2036-4ec9-953a-1b7f5754f29e.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (8, N'The Alchemist', N'Paulo Coelho', N'A philosophical novel that tells the story of a young shepherd named Santiago and his quest for treasure. Along the way, he learns valuable lessons about following his dreams and finding his true purpose in life.', 5, 0, CAST(N'1988-01-01T00:00:00.0000000' AS DateTime2), 6, N'/images/books/449e8281-9664-49e3-bbe9-4b59ca52549f.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (9, N'The Midnight Library', N'Matt Haig', N'A novel that explores the concept of parallel lives, following a woman named Nora who finds herself in a library between life and death, where she has the chance to live out different versions of her life based on the choices she made.
Category: Fiction / Fantasy', 5, 0, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 6, N'/images/books/54fca81b-f3bb-445c-bfb2-00ce157ae828.png')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (10, N' A Promised Land', N'Barack Obama', N'The first volume of Barack Obama''s presidential memoirs, detailing his early life, political career, and time in the White House, offering insights into American politics and leadership.', 4, 0, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 10, N'/images/books/14a4d225-3fcf-4008-9323-edf01a9651fd.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (11, N'The Pragmatic Programmer: Your Journey to Mastery', N'Andrew Hunt, David Thomas', N'A classic in software development that offers practical advice on how to improve your coding skills, collaborate with others, and create high-quality software efficiently.', 5, 0, CAST(N'1999-01-01T00:00:00.0000000' AS DateTime2), 9, N'/images/books/cc285798-0d88-419b-84cc-2bdd120640cf.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (12, N'Paradoxical Sajid', N'Arif Azad', N' This bestselling book discusses various philosophical and theological questions related to the existence of God, science, and rationality through the journey of a fictional character named Sajid. The book engages readers with a thought-provoking narrative while blending Islamic theology and modern scientific perspectives.', 100, 0, CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), 11, N'/images/books/546bcfb8-9b04-4719-a4d9-5cfbb8288ef3.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (13, N'Bela Furabar Age', N'Arif Azad', N'his is the third book by Arif Azad, focusing on Islamic spirituality, existential questions, and the reality of life and death. The book explores the purpose of life, the importance of preparing for the hereafter, and the inevitability of death. Through a collection of reflections and essays, Arif Azad encourages readers to ponder the transient nature of life and the importance of leading a righteous and meaningful existence.', 200, 0, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 8, N'/images/books/15c50dea-3b61-4953-b065-5ae1802ffc97.jpeg')
INSERT [dbo].[Books] ([BookId], [Title], [Author], [Description], [TotalCopies], [AvailableCopies], [PublishedDate], [CategoryId], [ImagePath]) VALUES (14, N'ASP.NET Core in Action', N'Andrew Lock', N'his book is a hands-on guide to building modern web applications using ASP.NET Core. It covers topics like building APIs, working with middleware, deploying to the cloud, and unit testing.', 10, 0, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), 9, N'/images/books/d85a326a-d13c-4bce-a7a6-3b20f05d2ed8.jpeg')
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[BorrowedBooks] ON 

INSERT [dbo].[BorrowedBooks] ([BorrowedBookId], [UserId], [BookId], [BorrowedDate], [ReturnDueDate], [IsReturned], [IsUserRequestReturn]) VALUES (2, 2, 7, CAST(N'2024-10-24T21:10:06.0060157' AS DateTime2), CAST(N'2024-11-07T21:10:06.0060716' AS DateTime2), 0, 0)
INSERT [dbo].[BorrowedBooks] ([BorrowedBookId], [UserId], [BookId], [BorrowedDate], [ReturnDueDate], [IsReturned], [IsUserRequestReturn]) VALUES (3, 3, 4, CAST(N'2024-10-25T21:40:35.8462835' AS DateTime2), CAST(N'2024-11-08T21:40:35.8463010' AS DateTime2), 0, 1)
SET IDENTITY_INSERT [dbo].[BorrowedBooks] OFF
GO
SET IDENTITY_INSERT [dbo].[BorrowRequests] ON 

INSERT [dbo].[BorrowRequests] ([BorrowRequestId], [UserId], [BookId], [RequestDate], [Status]) VALUES (3, 2, 7, CAST(N'2024-10-24T21:09:35.6275508' AS DateTime2), N'Approved')
INSERT [dbo].[BorrowRequests] ([BorrowRequestId], [UserId], [BookId], [RequestDate], [Status]) VALUES (4, 3, 4, CAST(N'2024-10-25T21:40:04.4392448' AS DateTime2), N'Approved')
SET IDENTITY_INSERT [dbo].[BorrowRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (6, N'Fiction / Fantasy')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (7, N'Self-help / Productivity')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (8, N'Islamic Studies / Philosophy')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (9, N' Programming ')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (10, N'Biography ')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (11, N'Islamic Philosophy')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (12, N'Motivation ')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FullName], [Email], [Password], [Role], [MembershipStartDate], [MembershipEndDate], [NIDNumber], [Phone], [Status]) VALUES (1, N'Sami Ahsan', N'sami@gmail.com', N'sami@1999', N'Member', CAST(N'2024-10-24T19:58:27.1536377' AS DateTime2), NULL, N'2458793154', N'02167954614', 1)
INSERT [dbo].[Users] ([UserId], [FullName], [Email], [Password], [Role], [MembershipStartDate], [MembershipEndDate], [NIDNumber], [Phone], [Status]) VALUES (2, N'Fahim Rahman', N'fahim@gmail.com', N'123', N'Member', CAST(N'2024-10-24T21:08:42.8455325' AS DateTime2), NULL, N'475869869', N'01571154879', 1)
INSERT [dbo].[Users] ([UserId], [FullName], [Email], [Password], [Role], [MembershipStartDate], [MembershipEndDate], [NIDNumber], [Phone], [Status]) VALUES (3, N'Rezaul karim', N'test@gmail.com', N'12345', N'Member', CAST(N'2024-10-25T21:38:00.2489889' AS DateTime2), NULL, N'5645846541', N'01303316865', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Books_CategoryId]    Script Date: 11/10/2024 9:42:55 PM ******/
CREATE NONCLUSTERED INDEX [IX_Books_CategoryId] ON [dbo].[Books]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BorrowedBooks_BookId]    Script Date: 11/10/2024 9:42:55 PM ******/
CREATE NONCLUSTERED INDEX [IX_BorrowedBooks_BookId] ON [dbo].[BorrowedBooks]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BorrowedBooks_UserId]    Script Date: 11/10/2024 9:42:55 PM ******/
CREATE NONCLUSTERED INDEX [IX_BorrowedBooks_UserId] ON [dbo].[BorrowedBooks]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BorrowRequests_BookId]    Script Date: 11/10/2024 9:42:55 PM ******/
CREATE NONCLUSTERED INDEX [IX_BorrowRequests_BookId] ON [dbo].[BorrowRequests]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BorrowRequests_UserId]    Script Date: 11/10/2024 9:42:55 PM ******/
CREATE NONCLUSTERED INDEX [IX_BorrowRequests_UserId] ON [dbo].[BorrowRequests]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT ((0)) FOR [CategoryId]
GO
ALTER TABLE [dbo].[BorrowedBooks] ADD  DEFAULT ((0)) FOR [IsUserRequestReturn]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [FullName]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [NIDNumber]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [Phone]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories_CategoryId]
GO
ALTER TABLE [dbo].[BorrowedBooks]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedBooks_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BorrowedBooks] CHECK CONSTRAINT [FK_BorrowedBooks_Books_BookId]
GO
ALTER TABLE [dbo].[BorrowedBooks]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedBooks_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BorrowedBooks] CHECK CONSTRAINT [FK_BorrowedBooks_Users_UserId]
GO
ALTER TABLE [dbo].[BorrowRequests]  WITH CHECK ADD  CONSTRAINT [FK_BorrowRequests_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BorrowRequests] CHECK CONSTRAINT [FK_BorrowRequests_Books_BookId]
GO
ALTER TABLE [dbo].[BorrowRequests]  WITH CHECK ADD  CONSTRAINT [FK_BorrowRequests_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BorrowRequests] CHECK CONSTRAINT [FK_BorrowRequests_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [LibraryDB] SET  READ_WRITE 
GO
