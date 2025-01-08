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