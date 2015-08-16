CREATE DATABASE Blog
GO


USE Blog
GO


CREATE TABLE [Picture](
Id INT IDENTITY(1,1) PRIMARY KEY, 
FileData VARBINARY(MAX) not null,
ImageMimeType VARCHAR(50),
Src NVARCHAR(MAX)
);

alter table Picture
alter column Src NVARCHAR(MAX)



CREATE TABLE [User](
Id INT IDENTITY(1,1) PRIMARY KEY, 
Firstname NVARCHAR(50) NOT NULL,
Surname NVARCHAR(50) NOT NULL,
Email NVARCHAR(50) NOT NULL,
DateRegister DATE NOT NULL,
DateDisable DATE,
[Description] NVARCHAR(255),
Username NVARCHAR(50) NOT NULL,
[Password] NVARCHAR(50) NOT NULL,
isAdmin BIT NOT NULL,
isEnable BIT NOT NULL,
PictureId INT NOT NULL REFERENCES [Picture](Id) ON DELETE CASCADE,
);


--alter table [User] drop column imgFileName




--DROP TABLE [User]
--DROP TABLE [Article]



CREATE TABLE Article
(
	Id INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
	AuthorId INT NOT NULL REFERENCES [User](Id) ON DELETE CASCADE,
	Title NVARCHAR(255) NOT NULL,
	CreationTime DATETIME NOT NULL,
	Content NVARCHAR(MAX) NULL,
	Published BIT NOT NULL
 );



 CREATE TABLE Comment
 (
	 Id INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
	 AuthorId INT NOT NULL REFERENCES [User](Id),
	 ArticleId INT NOT NULL REFERENCES [Article](Id),
	 Content NVARCHAR(MAX) NULL,
	 CommDate DATETIME NOT NULL
 );




--Truncate table [User]
--Truncate table Article


