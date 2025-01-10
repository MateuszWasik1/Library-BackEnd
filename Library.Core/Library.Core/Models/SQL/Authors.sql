CREATE TABLE Authors (
	AID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    AGID uniqueidentifier NOT NULL,
    AFirstName nvarchar(255) NOT NULL,
    AMiddleName nvarchar(255) NULL,
    ALastName nvarchar(255) NOT NULL,
    ANickName nvarchar(255) NULL,
    ANationality nvarchar(255) NULL,
);