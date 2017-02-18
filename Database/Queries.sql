/*============================== START TABLE [dbo].[Users] ==============================*/

/* Drop table dbo.Users */
DROP TABLE [dbo].[Users];

/* Create dbo.Users table */
CREATE TABLE [dbo].[Users]
(
	ID INT NOT NULL IDENTITY(1,1),
	Email VARCHAR(50) NOT NULL UNIQUE,
	[Password] NVARCHAR(MAX) NOT NULL,
	FirstName VARCHAR(25) NOT NULL,
	LastName VARCHAR(25) NOT NULL,
	DateJoined DATE NOT NULL DEFAULT GETDATE(),
	EmailVerified BIT NOT NULL DEFAULT 0,
	PRIMARY Key (ID)
);

/* Create indexes on dbo.Users */
CREATE UNIQUE INDEX EmailIndex ON [dbo].[Users] (Email);
CREATE INDEX EmailVerifiedIndex ON [dbo].[Users] (EmailVerified);

/* Insert an un-verified test user into dbo.Users */
INSERT INTO [dbo].[Users] (Email, [Password], FirstName, LastName)
ValueS ('Test-Unverified-User@example.com', 'unencrypted-password', 'John', 'Doe');

/* Insert verified test user into dbo.Users */
INSERT INTO [dbo].[Users] (Email, [Password], FirstName, LastName, EmailVerified)
ValueS ('Test-Verified-User@example.com', 'unencrypted-password', 'Jane', 'Doe', '1');

/* SELECT ALL from dbo.Users */
SELECT * FROM [dbo].[Users];

/*============================== END TABLE [dbo].[Users] ==============================*/

/*============================== START TABLE [dbo].[Vaults] ==============================*/

/* Drop table dbo.Vaults */
DROP TABLE [dbo].[Vaults];

/* Create table dbo.Vaults */
CREATE TABLE [dbo].[Vaults]
(
	U_ID INT NOT NULL,
	ID INT NOT NULL IDENTITY(1,1),
	Name VARCHAR(25) NOT NULL,
	DateCreated DATE NOT NULL DEFAULT GETDATE(),
	PRIMARY Key (ID),
	FOREIGN Key (U_ID) REFERENCES dbo.Users(ID) 
);

/* Create indexes on dbo.Vaults */
CREATE INDEX UserIndex ON [dbo].Vaults (U_ID);
CREATE UNIQUE INDEX VaultIndex ON [dbo].Vaults (ID);

/* Insert test vaults into dbo.Vaults */
INSERT INTO [dbo].[Vaults] (U_ID, Name)
ValueS (1, 'John''s Vault 1');
INSERT INTO [dbo].[Vaults] (U_ID, Name)
ValueS (1, 'John''s Vault 2');
INSERT INTO [dbo].[Vaults] (U_ID, Name)
ValueS (2, 'Jane''s Vault 1');
INSERT INTO [dbo].[Vaults] (U_ID, Name)
ValueS (2, 'Jane''s Vault 2');
INSERT INTO [dbo].[Vaults] (U_ID, Name)
ValueS (2, 'Jane''s Vault 3');

/* SELECT ALL from dbo.Vaults */
SELECT * FROM [dbo].Vaults;

/*============================== END TABLE [dbo].[Vaults] ==============================*/

/*============================== START TABLE [dbo].[Keys] ==============================*/

/* Drop table dbo.Keys */
DROP TABLE [dbo].[Keys];

/* Create table dbo.Keys */
CREATE TABLE [dbo].[Keys]
(
	V_ID INT NOT NULL,
	ID INT NOT NULL IDENTITY(1,1),
	DateCreated DATE NOT NULL DEFAULT GETDATE(),
	[Key] VARCHAR(50) NOT NULL,
	[Value] VARCHAR(255) NOT NULL,
	[Description] VARCHAR(255),
	[Hidden] BIT NOT NULL DEFAULT 1,
	PRIMARY Key (ID),
	FOREIGN Key (V_ID) REFERENCES [dbo].[Vaults](ID)
);

/* Create indexes on dbo.Keys */
CREATE INDEX VaultIndex ON [dbo].Keys (V_ID);
CREATE UNIQUE INDEX KeyIDIndex ON [dbo].Keys (ID);
CREATE INDEX KeyValueIndex ON [dbo].Keys ([Key]);

/* Insert test Keys into dbo.Keys */
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (1, 'A', 'Alfa', 'John''s Key 1 for Vault 1');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (1, 'B', 'Bravo', 'John''s Key 2 for Vault 1');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (1, 'C', 'Charlie', 'John''s Key 3 for Vault 1');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (2, 'D', 'Delta', 'John''s Key 1 for Vault 2');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (3, 'E', 'Echo', 'Jane''s Key 1 for Vault 1');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (3, 'F', 'Foxtrot', 'Jane''s Key 2 for Vault 1');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (4, 'G', 'Golf', 'Jane''s Key 1 for Vault 2');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (4, 'H', 'Hotel', 'Jane''s Key 2 for Vault 2');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (4, 'I', 'India', 'Jane''s Key 3 for Vault 2');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (4, 'J', 'Juliett', 'Jane''s Key 4 for Vault 2');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (4, 'K', 'Kilo', 'Jane''s Key 5 for Vault 2');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (5, 'L', 'Lima', 'Jane''s Key 1 for Vault 3');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (5, 'M', 'Mike', 'Jane''s Key 2 for Vault 3');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (5, 'N', 'November', 'Jane''s Key 3 for Vault 3');
INSERT INTO [dbo].Keys (V_ID, [Key], [Value], [Description])
ValueS (5, 'O', 'Oscar', 'Jane''s Key 4 for Vault 3');

/* Select all from dbo.Keys */
SELECT * FROM [dbo].Keys;

/*============================== END TABLE [dbo].[Keys] ==============================*/

/*============================== USER SPECIFIC QUERY EXAMPLES ==============================*/

/* Select the user's first name, last name, and individual Key entries based on vault names */
SELECT u.LastName, u.FirstName, v.NAME, k.[Key], k.[Value], k.[Description] FROM dbo.Users u
JOIN dbo.Vaults v
ON u.ID = v.U_ID
JOIN dbo.Keys k
ON v.ID = k.V_ID
ORDER BY u.LastName, u.FirstName, v.NAME, v.DateCreated;

/* Select the user's first name, last name, and individual Key entries based on vault names and vaults date created ONLY from today's date */
SELECT u.LastName, u.FirstName, v.NAME, v.DateCreated, k.[Key], k.[Value], k.[Description] FROM dbo.Users u
JOIN dbo.Vaults v
ON u.ID = v.U_ID
JOIN dbo.Keys k
ON v.ID = k.V_ID
WHERE CAST(v.DateCreated AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY u.LastName, u.FirstName, v.NAME, v.DateCreated;