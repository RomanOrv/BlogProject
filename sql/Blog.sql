--CREATE DATABASE Blog
--GO

--USE Blog
--GO

USE todolist
GO

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
isEnable BIT NOT NULL
);

--DROP TABLE [User]



INSERT INTO [User] VALUES
('Орловський', 'Роман', 'orv1979@gmail.com', '2015-05-10', null, '', 'admin', '123', 1, 1 ),
('Сифорова', 'Тетяна', 'siphtat@gmail.com', '2015-06-18', null, '', 'tannyaSif', '123', 0, 1 ),
('Сокуренко', 'Людмила', 'ludmyla@gmail.com', '2015-05-17', null, '', 'ludochka', '123', 0, 1 ),
('Яковенко', 'Юрій', 'yakovenko@gmail.com', '2015-06-12', null, '', 'yuriyYak', '123', 0, 1 ),
('Волинець', 'Василь', 'volynets@gmail.com', '2015-06-15', null, '', 'vasyaVol', '123', 0, 1 )