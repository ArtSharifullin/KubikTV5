SET IDENTITY_INSERT [dbo].[Admins] ON
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (1, N'admin', N'123', N'Artur')
SET IDENTITY_INSERT [dbo].[Admins] OFF
