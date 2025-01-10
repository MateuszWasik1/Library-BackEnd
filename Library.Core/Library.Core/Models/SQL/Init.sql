CREATE TABLE [User] (
	UID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UGID uniqueidentifier NOT NULL,
	URID INT NOT NULL,
	UFirstName nvarchar(50) NOT NULL,
	ULastName nvarchar(50) NOT NULL,
	UUserName nvarchar(100) NOT NULL,
	UEmail nvarchar(100) NOT NULL,
	UPhone nvarchar(100) NOT NULL,
	UPassword nvarchar(max) NOT NULL,
);

CREATE TABLE AppRoles (
	RID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RGID uniqueidentifier NOT NULL,
	RName nvarchar(100) NOT NULL,
)

INSERT INTO AppRoles (RGID, RName) VALUES ('1A29FC40-CA47-1067-B31D-00DD0106621A', 'User'), ('2A29FC40-CA47-1067-B31D-00DD0106621A', 'Premium'), ('3B29FC40-CA47-1067-B31D-00DD0106622B', 'Support'), ('4C29FC40-CA47-1067-B31D-00DD0106623C', 'Admin'); 

CREATE TABLE Authors (
	AID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    AGID uniqueidentifier NOT NULL,
    AFirstName nvarchar(255) NOT NULL,
    AMiddleName nvarchar(255) NULL,
    ALastName nvarchar(255) NOT NULL,
    ANickName nvarchar(255) NULL,
    ANationality nvarchar(255) NULL,
);

CREATE TABLE Books (
	BID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	BGID uniqueidentifier NOT NULL,
	BAuthorGID uniqueidentifier NOT NULL,
	BPublisherGID uniqueidentifier NOT NULL,
	BUID INT NOT NULL,
	BTitle nvarchar(255) NOT NULL,
	BISBN nvarchar(13) NOT NULL,
	BGenre INT NOT NULL,
	BLanguage nvarchar(255) NOT NULL,
	BDescription nvarchar(2000) NULL,
);

CREATE TABLE Publishers (
	PID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PGID uniqueidentifier NOT NULL,
    PName nvarchar(255) NOT NULL,
    PCountry nvarchar(255) NULL,
    PCity nvarchar(255) NULL,
    PEmail nvarchar(255) NULL,
    PPhone nvarchar(255) NULL,
);