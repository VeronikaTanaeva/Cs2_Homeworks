1. Создать БД с именем Cs2_WPF_Tanaeva

2. Создать таблицу Departaments:

CREATE TABLE [dbo].[Departaments] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



3. Создать таблицу Employees:

CREATE TABLE[dbo].[Employees] (
[Id] INT IDENTITY(1, 1) NOT NULL,
[Name] NVARCHAR(30) COLLATE
Cyrillic_General_CI_AS NOT NULL,
[Surname] NVARCHAR(50) NOT NULL, 
    [Departament] NVARCHAR(50) NOT NULL, 
    [Age] INT NOT NULL, 
    [Salary] INT NOT NULL, 
    CONSTRAINT[PK_dbo.People] PRIMARY KEY
CLUSTERED([Id] ASC)
);

4. Таблицы заполняются тестовыми данными вызовом FillInit(connectionString);  (раскомментировать вызов при запуске приложения)

