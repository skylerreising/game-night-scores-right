----------------------------------------------
-- 1. Create the Accounts Table
----------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') 
               AND type in (N'U'))
BEGIN
CREATE TABLE Accounts (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NULL,  -- Linked to Users(Id) via ALTER TABLE below (nullable if account is created first)
    EmailAddress NVARCHAR(256) NOT NULL,
    Role INT NOT NULL,  -- Enum values (e.g., 0 = Admin, 1 = User)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Accounts PRIMARY KEY NONCLUSTERED (Id)
);

-- Create a nonclustered index on EmailAddress for fast lookups.
CREATE UNIQUE NONCLUSTERED INDEX IX_Accounts_EmailAddress ON Accounts(EmailAddress);
CREATE UNIQUE NONCLUSTERED INDEX IX_Accounts_UserId 
ON Accounts(UserId)
WHERE UserId IS NOT NULL;
End
GO

----------------------------------------------
-- 2. Create the Users Table
----------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[Users]') 
               AND type in (N'U'))
BEGIN
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    AccountId UNIQUEIDENTIFIER NOT NULL,  -- Linked to Accounts(Id) via ALTER TABLE below
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Username NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Age INT,
    Pronouns NVARCHAR(50),
    ProfilePictureUrl NVARCHAR(512),
    Bio NVARCHAR(MAX),
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Users PRIMARY KEY NONCLUSTERED (Id)
);

-- Create nonclustered indexes for columns used in queries:
CREATE NONCLUSTERED INDEX IX_Users_FirstName ON Users(FirstName);
CREATE NONCLUSTERED INDEX IX_Users_LastName ON Users(LastName);
-- Unique nonclustered index on Username ensures fast lookups and uniqueness.
CREATE UNIQUE NONCLUSTERED INDEX IX_Users_Username ON Users(Username);
CREATE UNIQUE NONCLUSTERED INDEX IX_Users_AccountId ON Users(AccountId);
End
GO

----------------------------------------------
-- 3. Create the Events Table
----------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[Events]') 
               AND type in (N'U'))
BEGIN
CREATE TABLE Events (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    StartTime DATETIMEOFFSET,
    EndTime DATETIMEOFFSET,
    LocationName NVARCHAR(100),
    LocationAddress NVARCHAR(256),
    LocationWebsite NVARCHAR(256),
    Notes NVARCHAR(MAX),
    EventType INT,   -- Enum (e.g., 0 = BoardGame, 1 = Sports, etc.)
    Status INT,      -- Enum (e.g., 0 = Scheduled, 1 = InProgress, 2 = Completed)
    MaxParticipants INT,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Events PRIMARY KEY NONCLUSTERED (Id)
);

-- Create nonclustered index on Name for quick search.
CREATE NONCLUSTERED INDEX IX_Events_Name ON Events(Name);
End
GO

----------------------------------------------
-- 4. Create the Teams Table
----------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[Teams]') 
               AND type in (N'U'))
BEGIN
CREATE TABLE Teams (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    EventId UNIQUEIDENTIFIER,
    Name NVARCHAR(100) NOT NULL,
    Score DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    Color NVARCHAR(50),
    Mascot NVARCHAR(100),
    Description NVARCHAR(MAX),
    LogoUrl NVARCHAR(512),
    TeamType INT,    -- Enum (e.g., 0 = Competitive, 1 = Casual, etc.)
    MaxParticipants INT,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Teams PRIMARY KEY NONCLUSTERED (Id),
    CONSTRAINT FK_Teams_Events FOREIGN KEY (EventId)
        REFERENCES Events(Id)
);

-- Create nonclustered indexes for Teams table:
CREATE NONCLUSTERED INDEX IX_Teams_EventId ON Teams(EventId);
CREATE NONCLUSTERED INDEX IX_Teams_Name ON Teams(Name);
End
GO

----------------------------------------------
-- 5. Create the Players Table
----------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[Players]') 
               AND type in (N'U'))
BEGIN
CREATE TABLE Players (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Score DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    TeamId UNIQUEIDENTIFIER,
    EventId UNIQUEIDENTIFIER NOT NULL,
    Position INT,  -- Enum (e.g., 0 = Captain, 1 = Player)
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Players PRIMARY KEY NONCLUSTERED (Id),
    CONSTRAINT FK_Players_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id),
    CONSTRAINT FK_Players_Teams FOREIGN KEY (TeamId)
        REFERENCES Teams(Id),
    CONSTRAINT FK_Players_Events FOREIGN KEY (EventId)
        REFERENCES Events(Id)
);

-- Create a nonclustered index on Name for Players.
CREATE NONCLUSTERED INDEX IX_Players_Name ON Players(Name);
End
GO

----------------------------------------------
-- 6. Create the Relationships Table
----------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[Relationships]') 
               AND type in (N'U'))
BEGIN
CREATE TABLE Relationships (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    SenderId UNIQUEIDENTIFIER NOT NULL,
    ReceiverId UNIQUEIDENTIFIER NOT NULL,
    FriendStatus INT NOT NULL,  -- Enum values: 0 = Pending, 1 = Accepted, 2 = Declined, 3 = Blocked, 4 = Cancelled, 5 = Removed
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Relationships PRIMARY KEY NONCLUSTERED (Id),
    CONSTRAINT FK_Relationships_Sender FOREIGN KEY (SenderId)
        REFERENCES Users(Id),
    CONSTRAINT FK_Relationships_Receiver FOREIGN KEY (ReceiverId)
        REFERENCES Users(Id)
);

-- Create nonclustered indexes on SenderId and ReceiverId for fast lookups.
CREATE NONCLUSTERED INDEX IX_Relationships_SenderId ON Relationships(SenderId);
CREATE NONCLUSTERED INDEX IX_Relationships_ReceiverId ON Relationships(ReceiverId);
End
GO

----------------------------------------------
-- 7. Add the Circular Foreign Key Constraints
----------------------------------------------

-- Add FK_Accounts_Users if it doesn't exist
IF NOT EXISTS (
    SELECT * 
    FROM sys.foreign_keys 
    WHERE name = 'FK_Accounts_Users'
)
BEGIN
    ALTER TABLE Accounts
    ADD CONSTRAINT FK_Accounts_Users
    FOREIGN KEY (UserId) REFERENCES Users(Id);
END;
GO

-- Add FK_Users_Accounts if it doesn't exist
IF NOT EXISTS (
    SELECT * 
    FROM sys.foreign_keys 
    WHERE name = 'FK_Users_Accounts'
)
BEGIN
    ALTER TABLE Users
    ADD CONSTRAINT FK_Users_Accounts
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id);
END;
GO
