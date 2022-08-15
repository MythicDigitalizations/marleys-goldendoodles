IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WaitList]') AND type in (N'U'))
DROP TABLE [dbo].[WaitList]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
DROP TABLE [dbo].[Person]
GO

CREATE TABLE Person (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    LastName nvarchar(50) NOT NULL,
    FirstName nvarchar(50) NOT NULL,
    PhoneNumber nvarchar(10) NOT NULL,
	DateCreated datetime NOT NULL,
	CreatedBy nvarchar(50) NOT NULL,
	DateUpdated datetime,
	UpdatedBy nvarchar(50),
	IsActive bit NOT NULL
)
GO

CREATE TABLE WaitList (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PersonId int NOT NULL FOREIGN KEY REFERENCES Person(Id),
	AmountOwed numeric NOT NULL,
	DatePuppySecured datetime,
	DateCreated datetime NOT NULL,
	CreatedBy nvarchar(50) NOT NULL,
	DateUpdated datetime,
	UpdatedBy nvarchar(50),
	IsActive bit NOT NULL
)
GO

DROP PROCEDURE [dbo].[CreateWaitlistEntry]
GO

CREATE PROCEDURE [dbo].[CreateWaitlistEntry] @FirstName nvarchar(50), @LastName nvarchar(50), @PhoneNumber nvarchar(10)
AS
BEGIN
	INSERT INTO	Person(FirstName, LastName, PhoneNumber, DateCreated, CreatedBy, DateUpdated, UpdatedBy, IsActive)
	VALUES		(@FirstName, @LastName, @PhoneNumber, CURRENT_TIMESTAMP, 'CreateWaitlistEntry', NULL, NULL, 1)
	INSERT INTO	WaitList(PersonId, AmountOwed, DatePuppySecured, DateCreated, CreatedBy, DateUpdated, UpdatedBy, IsActive)
	VALUES		(SCOPE_IDENTITY(), 3000, NULL, CURRENT_TIMESTAMP, 'CreateWaitlistEntry', NULL, NULL, 1)
END
GO

DROP PROCEDURE [dbo].[UpdateWaitlistEntry]
GO

CREATE PROCEDURE [dbo].[UpdateWaitlistEntry] @WaitListId int, @AmountPaid numeric
AS
BEGIN
	DECLARE @PreAmountOwed AS numeric;
	DECLARE @PostAmountOwed As numeric;
	SET @PreAmountOwed = (SELECT AmountOwed FROM WaitList WHERE Id = @WaitListId)
	IF (@PreAmountOwed IS NULL) THROW 50001, 'Error: Cannot find the amount owed for this waitlist entry.', 1
    SET @PostAmountOwed = @PreAmountOwed - @AmountPaid
	IF (@PostAmountOwed < 0) THROW 50002, 'Error: Amount owed after update is negative.', 2
	IF (@PostAmountOwed = 0) UPDATE WaitList SET DatePuppySecured = CURRENT_TIMESTAMP WHERE Id = @WaitListId
	UPDATE WaitList SET AmountOwed = @PostAmountOwed, DateUpdated = CURRENT_TIMESTAMP, UpdatedBy = 'UpdateWaitlistEntry' WHERE Id = @WaitListId
END
GO

DROP PROCEDURE [dbo].[RemoveWaitlistEntry]
GO

CREATE PROCEDURE [dbo].[RemoveWaitlistEntry] @WaitListId int
AS
BEGIN
	DECLARE @PersonId AS int;
	SET @PersonId = (SELECT PersonId FROM WaitList WHERE Id = @WaitListId)
	IF (@PersonId IS NULL) THROW 50003, 'Error: Cannot find the person id associated with this waitlist entry.', 1
    UPDATE Person SET IsActive = 0, DateUpdated = CURRENT_TIMESTAMP, UpdatedBy = 'RemoveWaitlistEntry' WHERE Id = @PersonId
	UPDATE WaitList SET IsActive = 0, DateUpdated = CURRENT_TIMESTAMP, UpdatedBy = 'RemoveWaitlistEntry' WHERE Id = @WaitListId
END;