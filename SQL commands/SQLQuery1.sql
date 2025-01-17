CREATE TABLE [dbo].[Admins] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Login]    VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Login] ASC)
);

SET IDENTITY_INSERT [dbo].[Admins] ON
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (1, N'admin', N'123', N'Artur')
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (2, N'ahahahah', N'555', N'Kolya')
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (3, N'neveryu', N'999', N'Radmir')
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (4, N'KakjeTak', N'777', N'Denis')
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (5, N'Poka', N'Pokofon', N'5000')
INSERT INTO [dbo].[Admins] ([Id], [Login], [Password], [Name]) VALUES (6, N'lololol', N'ahahhahaha', N'5555')
SET IDENTITY_INSERT [dbo].[Admins] OFF

CREATE TABLE [dbo].[Entities] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (255) NOT NULL,
    [Value] INT            NOT NULL,
    [Login] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Movies] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [EnglishName] VARCHAR (50)   NOT NULL,
    [Year]        INT            NULL,
    [Country]     NVARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [Genre]       NVARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [Director]    NVARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [Actors]      NVARCHAR (255) COLLATE Latin1_General_CI_AS NULL,
    [Description] NVARCHAR (400) COLLATE Latin1_General_CI_AS NULL,
    [MovieLink]   NVARCHAR (255) NULL,
    [ImageLink]   NVARCHAR (255) NULL,
    [Link]        VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

SET IDENTITY_INSERT [dbo].[Movies] ON
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (1, N'Веном: Последний танец', N'Venom: The Last Dance', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/2945394.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (2, N'Жребий', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/zhrebij.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (3, N'Жребий (нет)', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/1711567515_ohotniki-za-prividenijami-ledenjaschij-uzhas.webp', N'/movie')


INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (4, N'Не жребий', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/blobid0.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (5, N'Жребий еще один', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/akolit.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (6, N'Всем', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/osnovanie.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (7, N'Привет', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/goljak.webp', N'/movie')


INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (8, N'Меня', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/jeto-vse-agata.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (9, N'Зовут', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/ono-slushaet.webp', N'/movie')
INSERT INTO [dbo].[Movies] ([Id], [Name], [EnglishName], [Year], [Country], [Genre], [Director], [Actors], [Description], [MovieLink], [ImageLink], [Link]) VALUES (10, N'Артур', N'Zhrebij', 2024, N'США, Великобритания, Канада', N'фантастика, боевик, ужасы', N'Келли Марсел', N'Том Харди, Джуно Темпл, Чиветель Эджиофор, Рис Иванс, Стивен Грэм, Пегги Лу, Кларк Бако, Аланна Юбак, Кристо Фернандес, Джаред Абрахамсон', N'Эдди Брок и его симбиот Веном нашли хрупкий баланс в совместной жизни, став командой, борющейся с преступностью. Но их спокойствие разрушает новая угроза: военные преследуют Эдди, а инопланетные сородичи Венома планируют уничтожить всё живое на Земле. Перед лицом глобальной опасности дуэту предстоит сразиться с врагами и доказать, что вместе они сильнее любых испытаний.', N'https://www.videolive.fun/?token_movie=22290bdebc9940727f2e7f6aa90b3a&token=0115442e9d77410531d7eb4be96069', N'assets/images/djedpul-i-rosomaha.webp', N'/movie')
SET IDENTITY_INSERT [dbo].[Movies] OFF


CREATE TABLE [dbo].[users] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Login] ASC)
);


SET IDENTITY_INSERT [dbo].[users] ON
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (1, N'denis', N'222')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (2, N'lol', N'111')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (4, N'abrakadabra', N'4444')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (5, N'user', N'user')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (6, N'Kolya', N'22222')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (7, N'Avdotiy', N'555')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (8, N'lalallala2222', N'122121')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (9, N'lolkek', N'2222')
INSERT INTO [dbo].[users] ([Id], [Login], [Password]) VALUES (10, N'go', N'3333')
SET IDENTITY_INSERT [dbo].[users] OFF
