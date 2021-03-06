USE [WebApiStudentData_16.Models.DatabaseContext]
GO
/****** Object:  Table [dbo].[ContactForms]    Script Date: 28-11-2018 10:45:37 ******/
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
	[ContactNamePhoneNumber] [nvarchar](max) NULL,
	[ContactSubject] [nvarchar](max) NULL,
	[ContactEmailRecipient] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ContactForms] PRIMARY KEY CLUSTERED 
(
	[ContactFormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ContactForms] ON 

INSERT [dbo].[ContactForms] ([ContactFormID], [UserInfoID], [ContactNameFrom], [ContactNameEmail], [ContactText], [ContactNamePhoneNumber], [ContactSubject], [ContactEmailRecipient]) VALUES (1, 2, N'Lars Pedersen', N'ltpe@tcaa.dk', N'Hej med dig !!!', N'+45-29264591', N'Test Emne !!!', N'ltpe@tcaa.dk')
INSERT [dbo].[ContactForms] ([ContactFormID], [UserInfoID], [ContactNameFrom], [ContactNameEmail], [ContactText], [ContactNamePhoneNumber], [ContactSubject], [ContactEmailRecipient]) VALUES (2, 2, N'Lars Pedersen', N'ltpe@tcaa.dk', N'Hej med dig !!!', N'+45-29264591', N'Test Emne !!!', N'ltpe@tcaa.dk')
INSERT [dbo].[ContactForms] ([ContactFormID], [UserInfoID], [ContactNameFrom], [ContactNameEmail], [ContactText], [ContactNamePhoneNumber], [ContactSubject], [ContactEmailRecipient]) VALUES (3, 2, N'Lars Pedersen', N'ltpe@tcaa.dk', N'Hej med dig !!!', N'+45-29264591', N'Test Emne !!!', N'ltpe@tcaa.dk')
INSERT [dbo].[ContactForms] ([ContactFormID], [UserInfoID], [ContactNameFrom], [ContactNameEmail], [ContactText], [ContactNamePhoneNumber], [ContactSubject], [ContactEmailRecipient]) VALUES (4, 2, N'Lars Pedersen', N'ltpe@tcaa.dk', N'Hej med dig !!!', N'+45-29264591', N'Test Emne !!!', N'ltpe@tcaa.dk')
INSERT [dbo].[ContactForms] ([ContactFormID], [UserInfoID], [ContactNameFrom], [ContactNameEmail], [ContactText], [ContactNamePhoneNumber], [ContactSubject], [ContactEmailRecipient]) VALUES (5, 2, N'Lars Pedersen', N'ltpe@tcaa.dk', N'Hej med dig !!!', N'+45-29264591', N'Test Emne !!!', N'jonas-sleiborg@hotmail.com')
SET IDENTITY_INSERT [dbo].[ContactForms] OFF
ALTER TABLE [dbo].[ContactForms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContactForms_dbo.UserInfoes_UserInfoID] FOREIGN KEY([UserInfoID])
REFERENCES [dbo].[UserInfoes] ([UserInfoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactForms] CHECK CONSTRAINT [FK_dbo.ContactForms_dbo.UserInfoes_UserInfoID]
GO
