CREATE TABLE Reports (
	RID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    RGID uniqueidentifier NOT NULL,
    PName nvarchar(255) NOT NULL,
    RGenerationDate DATETIME2 NOT NULL,
    string nvarchar(max) NOT NULL,
);
