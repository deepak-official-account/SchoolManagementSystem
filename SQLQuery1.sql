use db;
Select * From Student;

CREATE PROCEDURE [dbo].[AddStudent]
  @Name NVARCHAR(100),
  @Surname NVARCHAR(100),
  @Age INT,
  @PhoneNumber NVARCHAR(10)
AS
BEGIN
 INSERT INTO Student (Name,Surname,Age,PhoneNumber) VALUES
 (
 @Name,
 @Surname,
 @Age,
 @PhoneNumber
 );
END;
GO

select * from Student;



CREATE PROCEDURE [dbo].[GetAllStudents]
AS
BEGIN
 Select RollNo,Name,Surname,Age,PhoneNumber from Student;
END;
GO




CREATE PROCEDURE [dbo].[UpdateStudent]
@RollNo INT,
  @Name NVARCHAR(100),
  @Surname NVARCHAR(100),
  @Age INT,
  @PhoneNumber NVARCHAR(10),
  @IsUpdated BIT OUTPUT
AS
BEGIN
  IF EXISTS (SELECT 1 FROM Student WHERE RollNo = @RollNo)
  BEGIN
UPDATE Student set Name=@Name,Surname=@Surname,Age=@Age,PhoneNumber=@PhoneNumber where
  RollNo=@RollNo;
  SET @IsUpdated=1;
  END
  ELSE
  BEGIN
  SET @IsUpdated=0;
  END
END
GO




CREATE PROCEDURE [dbo].[DeleteStudent]
    @RollNo INT,
    @IsDeleted BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Student WHERE RollNo = @RollNo)
    BEGIN
        DELETE FROM Student WHERE RollNo = @RollNo;

        SET @IsDeleted = 1;
    END
    ELSE
    BEGIN
        SET @IsDeleted = 0;
    END
END
GO

drop procedure dbo.UpdateStudent;