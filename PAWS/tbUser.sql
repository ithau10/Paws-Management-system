CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] NCHAR(10) NOT NULL, 
    [address] NCHAR(10) NOT NULL, 
    [phone] NCHAR(10) NOT NULL, 
    [role] NCHAR(10) NOT NULL, 
    [dob] NCHAR(10) NOT NULL, 
    [password] NCHAR(10) NULL
)
