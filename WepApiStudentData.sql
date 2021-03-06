USE [WebApiStudentData_15.Models.DatabaseContext]
GO
/****** Object:  Table [dbo].[Character13Scale]    Script Date: 19-11-2018 09:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Character13Scale](
	[Character13ScaleID] [int] IDENTITY(1,1) NOT NULL,
	[Character13ScaleValue] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Character13Scale] PRIMARY KEY CLUSTERED 
(
	[Character13ScaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Character7Scale]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Character7Scale](
	[Character7ScaleID] [int] IDENTITY(1,1) NOT NULL,
	[Character7ScaleValue] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Character7Scale] PRIMARY KEY CLUSTERED 
(
	[Character7ScaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactForms]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactForms](
	[ContactFormID] [int] IDENTITY(1,1) NOT NULL,
	[UserInfoID] [int] NOT NULL,
	[ContactNameFrom] [nvarchar](max) NOT NULL,
	[ContactNameEmail] [nvarchar](max) NOT NULL,
	[ContactText] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.ContactForms] PRIMARY KEY CLUSTERED 
(
	[ContactFormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Courses] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationLines]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationLines](
	[EducationLineID] [int] IDENTITY(1,1) NOT NULL,
	[EducationLineName] [nvarchar](max) NULL,
	[EducationID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.EducationLines] PRIMARY KEY CLUSTERED 
(
	[EducationLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Educations]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Educations](
	[EducationID] [int] IDENTITY(1,1) NOT NULL,
	[EducationName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Educations] PRIMARY KEY CLUSTERED 
(
	[EducationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Education_Character_Course_Collection]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Education_Character_Course_Collection](
	[User_Education_Character_Course_CollectionID] [int] IDENTITY(1,1) NOT NULL,
	[User_Education_Time_CollectionID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[WhichCharacterScaleID] [int] NULL,
	[CharacterValueCourse] [int] NULL,
	[AbsencePercentageCourse] [real] NULL,
 CONSTRAINT [PK_dbo.User_Education_Character_Course_Collection] PRIMARY KEY CLUSTERED 
(
	[User_Education_Character_Course_CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Education_Time_Collection]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Education_Time_Collection](
	[User_Education_Time_CollectionID] [int] IDENTITY(1,1) NOT NULL,
	[UserInfoID] [int] NOT NULL,
	[EducationLineID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[AbsencePercentageEducation] [real] NULL,
	[WhichCharacterScaleID] [int] NULL,
	[CharacterValueEducation] [int] NULL,
 CONSTRAINT [PK_dbo.User_Education_Time_Collection] PRIMARY KEY CLUSTERED 
(
	[User_Education_Time_CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFiles]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFiles](
	[UserFileID] [int] IDENTITY(1,1) NOT NULL,
	[UserInfoID] [int] NOT NULL,
	[UserFileUrl] [nvarchar](max) NOT NULL,
	[userFileAlt] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserFiles] PRIMARY KEY CLUSTERED 
(
	[UserFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfoes]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoes](
	[UserInfoID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[UserPassword] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.UserInfoes] PRIMARY KEY CLUSTERED 
(
	[UserInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WhichCharacterScales]    Script Date: 19-11-2018 09:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WhichCharacterScales](
	[WhichCharacterScaleID] [int] IDENTITY(1,1) NOT NULL,
	[WhichCharacterScaleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.WhichCharacterScales] PRIMARY KEY CLUSTERED 
(
	[WhichCharacterScaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Character13Scale] ON 

INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (1, 0)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (2, 3)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (3, 5)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (4, 6)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (5, 7)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (6, 8)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (7, 9)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (8, 10)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (9, 11)
INSERT [dbo].[Character13Scale] ([Character13ScaleID], [Character13ScaleValue]) VALUES (10, 13)
SET IDENTITY_INSERT [dbo].[Character13Scale] OFF
SET IDENTITY_INSERT [dbo].[Character7Scale] ON 

INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (1, -3)
INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (2, 0)
INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (3, 2)
INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (4, 4)
INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (5, 7)
INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (6, 10)
INSERT [dbo].[Character7Scale] ([Character7ScaleID], [Character7ScaleValue]) VALUES (7, 12)
SET IDENTITY_INSERT [dbo].[Character7Scale] OFF
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (1, N'Matamatik D-niveau')
INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (2, N'Engelsk D-niveau')
INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (3, N'Dansk D-niveau')
INSERT [dbo].[Courses] ([CourseID], [CourseName]) VALUES (4, N'Clientside II')
SET IDENTITY_INSERT [dbo].[Courses] OFF
SET IDENTITY_INSERT [dbo].[EducationLines] ON 

INSERT [dbo].[EducationLines] ([EducationLineID], [EducationLineName], [EducationID]) VALUES (1, N'Datatekniker Grundforløb', 1)
INSERT [dbo].[EducationLines] ([EducationLineID], [EducationLineName], [EducationID]) VALUES (2, N'Datatekniker Hovedforløb Programmering', 1)
SET IDENTITY_INSERT [dbo].[EducationLines] OFF
SET IDENTITY_INSERT [dbo].[Educations] ON 

INSERT [dbo].[Educations] ([EducationID], [EducationName]) VALUES (1, N'Tech College Aalborg')
SET IDENTITY_INSERT [dbo].[Educations] OFF
SET IDENTITY_INSERT [dbo].[UserInfoes] ON 

INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (2, N'Lars-Lærer', N'mDubngl/eK5g12RAeMN60g==')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (3, N'Slei', N'sfEjJiAMMZjrP9B/dpN2cw==')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (4, N'madsb', N'WnkCJCFYWr8Du9nOwiPBbQ==')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (5, N'lars6305', N'WnkCJCFYWr8Du9nOwiPBbQ==')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (6, N'Lauda', N'TzSqmPnD44pz8qVAKr1Lgg==')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (7, N'jaco306c', N'tiZ74NDe9kH4WFzMQhUg3dabJtbGEcD9P8mks8a5v14=')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (8, N'Æblekage', N'vTCPWvTO8G9lW/+klQTMVw==')
INSERT [dbo].[UserInfoes] ([UserInfoID], [UserName], [UserPassword]) VALUES (1002, N'Rasmus99', N'0rgb89wNNar/Fvqk3B7LWg==')
SET IDENTITY_INSERT [dbo].[UserInfoes] OFF
SET IDENTITY_INSERT [dbo].[WhichCharacterScales] ON 

INSERT [dbo].[WhichCharacterScales] ([WhichCharacterScaleID], [WhichCharacterScaleName]) VALUES (1, N'7-trins skalaen')
INSERT [dbo].[WhichCharacterScales] ([WhichCharacterScaleID], [WhichCharacterScaleName]) VALUES (2, N'13 skalaen')
SET IDENTITY_INSERT [dbo].[WhichCharacterScales] OFF
ALTER TABLE [dbo].[ContactForms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContactForms_dbo.UserInfoes_UserInfoID] FOREIGN KEY([UserInfoID])
REFERENCES [dbo].[UserInfoes] ([UserInfoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactForms] CHECK CONSTRAINT [FK_dbo.ContactForms_dbo.UserInfoes_UserInfoID]
GO
ALTER TABLE [dbo].[EducationLines]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EducationLines_dbo.Educations_EducationID] FOREIGN KEY([EducationID])
REFERENCES [dbo].[Educations] ([EducationID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EducationLines] CHECK CONSTRAINT [FK_dbo.EducationLines_dbo.Educations_EducationID]
GO
ALTER TABLE [dbo].[User_Education_Character_Course_Collection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_Education_Character_Course_Collection_dbo.Courses_CourseID] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Education_Character_Course_Collection] CHECK CONSTRAINT [FK_dbo.User_Education_Character_Course_Collection_dbo.Courses_CourseID]
GO
ALTER TABLE [dbo].[User_Education_Character_Course_Collection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_Education_Character_Course_Collection_dbo.User_Education_Time_Collection_User_Education_Time_CollectionID] FOREIGN KEY([User_Education_Time_CollectionID])
REFERENCES [dbo].[User_Education_Time_Collection] ([User_Education_Time_CollectionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Education_Character_Course_Collection] CHECK CONSTRAINT [FK_dbo.User_Education_Character_Course_Collection_dbo.User_Education_Time_Collection_User_Education_Time_CollectionID]
GO
ALTER TABLE [dbo].[User_Education_Character_Course_Collection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_Education_Character_Course_Collection_dbo.WhichCharacterScales_WhichCharacterScaleID] FOREIGN KEY([WhichCharacterScaleID])
REFERENCES [dbo].[WhichCharacterScales] ([WhichCharacterScaleID])
GO
ALTER TABLE [dbo].[User_Education_Character_Course_Collection] CHECK CONSTRAINT [FK_dbo.User_Education_Character_Course_Collection_dbo.WhichCharacterScales_WhichCharacterScaleID]
GO
ALTER TABLE [dbo].[User_Education_Time_Collection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_Education_Time_Collection_dbo.EducationLines_EducationLineID] FOREIGN KEY([EducationLineID])
REFERENCES [dbo].[EducationLines] ([EducationLineID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Education_Time_Collection] CHECK CONSTRAINT [FK_dbo.User_Education_Time_Collection_dbo.EducationLines_EducationLineID]
GO
ALTER TABLE [dbo].[User_Education_Time_Collection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_Education_Time_Collection_dbo.UserInfoes_UserInfoID] FOREIGN KEY([UserInfoID])
REFERENCES [dbo].[UserInfoes] ([UserInfoID])
GO
ALTER TABLE [dbo].[User_Education_Time_Collection] CHECK CONSTRAINT [FK_dbo.User_Education_Time_Collection_dbo.UserInfoes_UserInfoID]
GO
ALTER TABLE [dbo].[User_Education_Time_Collection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_Education_Time_Collection_dbo.WhichCharacterScales_WhichCharacterScaleID] FOREIGN KEY([WhichCharacterScaleID])
REFERENCES [dbo].[WhichCharacterScales] ([WhichCharacterScaleID])
GO
ALTER TABLE [dbo].[User_Education_Time_Collection] CHECK CONSTRAINT [FK_dbo.User_Education_Time_Collection_dbo.WhichCharacterScales_WhichCharacterScaleID]
GO
ALTER TABLE [dbo].[UserFiles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserFiles_dbo.UserInfoes_UserInfoID] FOREIGN KEY([UserInfoID])
REFERENCES [dbo].[UserInfoes] ([UserInfoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserFiles] CHECK CONSTRAINT [FK_dbo.UserFiles_dbo.UserInfoes_UserInfoID]
GO
