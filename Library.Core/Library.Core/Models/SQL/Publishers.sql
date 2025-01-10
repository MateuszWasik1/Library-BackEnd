CREATE TABLE Publishers (
	PID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PGID uniqueidentifier NOT NULL,
    PName nvarchar(255) NOT NULL,
    PCountry nvarchar(255) NULL,
    PCity nvarchar(255) NULL,
    PEmail nvarchar(255) NULL,
    PPhone nvarchar(255) NULL,
);