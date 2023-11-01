CREATE TABLE [dbo].[UserTab]
(
	[UserID] INT NOT NULL IDENTITY, 
    [Name] NCHAR(10) NULL, 
    [Email] NCHAR(10) NULL, 
    CONSTRAINT [PK_TestTab] PRIMARY KEY ([UserID]) 
)
