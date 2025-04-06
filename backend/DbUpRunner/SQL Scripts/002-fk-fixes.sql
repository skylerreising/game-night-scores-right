-------------------------------------------------------
-- Purpose: Remove the circular foreign key constraint from
--          Accounts that prevents creating an Account without
--          an existing User.
-------------------------------------------------------

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Accounts_Users')
BEGIN
    ALTER TABLE Accounts DROP CONSTRAINT FK_Accounts_Users;
END
GO