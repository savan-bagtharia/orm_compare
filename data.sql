USE [ORMCompare]

----------------------------------------------------------------------------
----------- Clear all the data
----------------------------------------------------------------------------
DELETE FROM dbo.TeamMember
GO
DELETE FROM dbo.Team
GO
DELETE FROM dbo.Project
GO

GO
----------------------------------------------------------------------------
--- Generate data based on max team, project and member count
--- User can modify max project, team and member count according to the need
--- Team members will be equally divided into teams so make sure Team Member count can be equally dividable
--- Teams will be equally divided into Projects so make sure Team count can be equally dividable
----------------------------------------------------------------------------
DECLARE @MaxProject INT = 500, @MaxTeams INT = 1000, @MaxTeamMember INT  = 1000000

DECLARE @ProjetTeamRatio FLOAT = (SELECT (CAST(@MaxProject AS FLOAT))/(CAST(@MaxTeams AS FLOAT)))
DECLARE @TeamMemberTeamRatio FLOAT = (SELECT (CAST(@MaxTeams AS FLOAT))/(CAST( @MaxTeamMember  AS FLOAT)))

DECLARE @ProjCount INT = 1;
WHILE (@ProjCount <= @MaxProject )
BEGIN
	INSERT INTO dbo.Project
	(
	    ID,
	    Name
	)
	VALUES
	(   @ProjCount,  -- ID - int
	    N'Project '+ CAST(@ProjCount AS VARCHAR) -- Name - nvarchar(100)
	)
	SET @ProjCount = @ProjCount +1; 
END

DECLARE @TeamCount INT = 1;
WHILE (@TeamCount <= @MaxTeams )
BEGIN
	DECLARE @tempProjectId INT = (SELECT CEILING(@ProjetTeamRatio*@TeamCount))
	DECLARE @tempCreatedDate DATETIME = (SELECT dateadd(day, (abs(CHECKSUM(newid())) % 3650) * -1, getdate()))
	INSERT INTO dbo.Team
	(
	    ID,
	    Name,
	    CreatedDate,
	    ProjectID
	)
	VALUES
	(   @TeamCount,         -- ID - int
	    N'Team '+ CAST(@TeamCount AS VARCHAR),       -- Name - nvarchar(200)
	    @tempCreatedDate, -- CreatedDate - datetime
	     @tempProjectId         -- ProjectID - int
	)
	SET @TeamCount = @TeamCount +1; 
END

DECLARE @TeamMemberCount INT = 1;
WHILE (@TeamMemberCount <= @MaxTeamMember )
BEGIN
	DECLARE @TempTeamID INT = (SELECT CEILING(@TeamMemberTeamRatio*@TeamMemberCount))
	DECLARE @TempDOB DATETIME = (SELECT dateadd(day, (abs(CHECKSUM(newid())) % 3650) * -1, getdate()))
	INSERT INTO dbo.TeamMember
	(
	    ID,
	    FirstName,
	    LastName,
	    DateOfBirth,
	    TeamID
	)
	VALUES
	(   @TeamMemberCount,         -- ID - int
	    N'First Name' +CAST(@TeamMemberCount as VARCHAR),       -- FirstName - nvarchar(200)
	    N'Last Name' + CAST(@TeamMemberCount AS VARCHAR),       -- LastName - nvarchar(200)
	    @TempDOB, -- DateOfBirth - datetime
	    @TempTeamID         -- TeamID - int
	    )
	SET @TeamMemberCount = @TeamMemberCount +1;
	
END

