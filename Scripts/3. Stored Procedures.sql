
 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
														  STORED PROCEDURE


	PROGRAM ID					: 	[dbo].[Sp_SearchJob]
	PROGRAM DESCRIPTION			: 	Search Job
	ONLINE/BATCH				: 	ONLINE
	PROCEDURES CALLED          	: 	-
	PROCEDURES CALLING THIS     : 	-
	INPUT PARAMETERS            : 	-
	OUTPUT PARAMETER         	: 	-

MODIFICATION LOG            
**************************************************************************************************************************************************************

	     Ver       	 	Author           	  Changes made          										  Date		
     	 1.0	        Anuruddha		    Original Version												03-06-2022
***************************************************************************************************************************************************************/ 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_SearchJob]  
(
	
	  @vName				NVARCHAR(50)
	 ,@vMobileNumber		VARCHAR(50)
	 ,@iStatus				INT
	 ,@iCurrentPage			INT  = 1  
	 ,@iPageSize			INT = 50
)
AS    
BEGIN TRY

   SET @vName		= '%' + @vName + '%'    
   SET @vMobileNumber		= '%' + @vMobileNumber + '%' 
  
	SELECT * FROM 
	(    
		SELECT 
			 ROW_NUMBER() OVER (ORDER BY ID) AS ROWNUM
			 ,COUNT(*) OVER () AS TOTAL_COUNT 
			 ,[JOB_GUID]
			 ,[NAME] 
			 ,MOBILE_NUMBER 
			 ,[STATUS]
		FROM
			dbo.JOB
		WHERE
			[NAME] LIKE @vName 
			AND MOBILE_NUMBER LIKE @vMobileNumber 
	)
	AS Job
   WHERE ROWNUM BETWEEN (SELECT [dbo].[fn_PageStart](@iCurrentPage,@iPageSize))  AND (SELECT [dbo].[fn_PageEnd](@iCurrentPage,@iPageSize))  

END TRY  
BEGIN CATCH  
	BEGIN
		DECLARE @vErrorMessage VARCHAR(1000)  = ERROR_MESSAGE()
		DECLARE @vErrorProc    VARCHAR(200)   = ERROR_PROCEDURE()
		RAISERROR (@vErrorMessage,16,1)  
	END 
END CATCH  
GO



 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
														  STORED PROCEDURE


	PROGRAM ID					: 	[dbo].[Sp_GetJob]
	PROGRAM DESCRIPTION			: 	Get Job
	ONLINE/BATCH				: 	ONLINE
	PROCEDURES CALLED          	: 	-
	PROCEDURES CALLING THIS     : 	-
	INPUT PARAMETERS            : 	-
	OUTPUT PARAMETER         	: 	-

MODIFICATION LOG            
**************************************************************************************************************************************************************

	     Ver       	 	Author           	  Changes made          										  Date		
     	 1.0	        Anuruddha		    Original Version												03-06-2022
***************************************************************************************************************************************************************/ 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_GetJob]  
(
	  @vGuid	VARCHAR(50)
)
AS    
BEGIN TRY

	SELECT 
		 [JOB_GUID]
		,[NAME] 
		,MOBILE_NUMBER 
		,[STATUS]
	FROM
		dbo.JOB
	WHERE
		 [JOB_GUID] = @vGuid 

	SELECT 
		 COMMENT
	FROM
		dbo.NOTE
	WHERE
		 [JOB_GUID] = @vGuid 

END TRY  
BEGIN CATCH  
	BEGIN
		DECLARE @vErrorMessage VARCHAR(1000)  = ERROR_MESSAGE()
		DECLARE @vErrorProc    VARCHAR(200)   = ERROR_PROCEDURE()
		RAISERROR (@vErrorMessage,16,1)  
	END 
END CATCH  
GO
 


 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
														  STORED PROCEDURE


	PROGRAM ID					: 	[dbo].[Sp_CreateJob]
	PROGRAM DESCRIPTION			: 	Create Job
	ONLINE/BATCH				: 	ONLINE
	PROCEDURES CALLED          	: 	-
	PROCEDURES CALLING THIS     : 	-
	INPUT PARAMETERS            : 	-
	OUTPUT PARAMETER         	: 	-

MODIFICATION LOG            
**************************************************************************************************************************************************************

	     Ver       	 	Author           	  Changes made          										  Date		
     	 1.0	        Anuruddha		    Original Version												03-06-2022
***************************************************************************************************************************************************************/ 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_CreateJob]  
(
	 @vJson NVARCHAR(MAX)
)
AS    
BEGIN TRY

	INSERT dbo.JOB
	(
		 [JOB_GUID]
		,[NAME]
		,[MOBILE_NUMBER]
		,[STATUS]
		,[CREATED_USER_ID]
		,[CREATED_DATETIME]
	)
	SELECT 
		 JobGuid
		,[Name]	
		,MobileNumber 
		,[Status]
		,CreatedUserId
		,CreatedDateTime
	FROM OPENJSON (@vJson)  
	WITH (  
		 JobGuid		 VARCHAR(50)  
        ,[Name]			 VARCHAR(50) 
		,MobileNumber	 VARCHAR(50) 
		,[Status]		 INT 
		,CreatedUserId	 VARCHAR(50) 
		,CreatedDateTime DATETIME  
	 )

END TRY  
BEGIN CATCH  
	BEGIN
		DECLARE @vErrorMessage VARCHAR(1000)  = ERROR_MESSAGE()
		DECLARE @vErrorProc    VARCHAR(200)   = ERROR_PROCEDURE()
		RAISERROR (@vErrorMessage,16,1)  
	END 
END CATCH  
GO



 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
														  STORED PROCEDURE


	PROGRAM ID					: 	[dbo].[Sp_UpdateJob]
	PROGRAM DESCRIPTION			: 	Update Job
	ONLINE/BATCH				: 	ONLINE
	PROCEDURES CALLED          	: 	-
	PROCEDURES CALLING THIS     : 	-
	INPUT PARAMETERS            : 	-
	OUTPUT PARAMETER         	: 	-

MODIFICATION LOG            
**************************************************************************************************************************************************************

	     Ver       	 	Author           	  Changes made          										  Date		
     	 1.0	        Anuruddha		    Original Version												03-06-2022
***************************************************************************************************************************************************************/ 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_UpdateJob]  
(
	 @vJson NVARCHAR(MAX)
)
AS    
BEGIN TRY

	UPDATE 
		J
	SET 
		 J.[STATUS]			  = D.[Status]
		,J.[UPDATED_USER_ID]  = D.UpdatedUserId
		,J.[UPDATED_DATETIME] = D.UpdatedDateTime 
	FROM
		dbo.JOB J
		INNER JOIN (
			SELECT 
				 JobGuid
				,[Status]
				,UpdatedUserId
				,UpdatedDateTime
			FROM OPENJSON (@vJson)  
			WITH (  
				 JobGuid		 VARCHAR(50)  
				,[Status]		 INT 
				,UpdatedUserId	 VARCHAR(50) 
				,UpdatedDateTime DATETIME  
			 )
		) AS D ON J.JOB_GUID = D.JobGuid

END TRY  
BEGIN CATCH  
	BEGIN
		DECLARE @vErrorMessage VARCHAR(1000)  = ERROR_MESSAGE()
		DECLARE @vErrorProc    VARCHAR(200)   = ERROR_PROCEDURE()
		RAISERROR (@vErrorMessage,16,1)  
	END 
END CATCH  
GO




 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
														  STORED PROCEDURE


	PROGRAM ID					: 	[dbo].[Sp_CreateNote]
	PROGRAM DESCRIPTION			: 	Create Note
	ONLINE/BATCH				: 	ONLINE
	PROCEDURES CALLED          	: 	-
	PROCEDURES CALLING THIS     : 	-
	INPUT PARAMETERS            : 	-
	OUTPUT PARAMETER         	: 	-

MODIFICATION LOG            
**************************************************************************************************************************************************************

	     Ver       	 	Author           	  Changes made          										  Date		
     	 1.0	        Anuruddha		    Original Version												03-06-2022
***************************************************************************************************************************************************************/ 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_CreateNote]  
(
	 @vJson NVARCHAR(MAX)
)
AS    
BEGIN TRY

	INSERT dbo.NOTE
	(
		 [NOTE_GUID]
		,[JOB_GUID]
		,[COMMENT]
		,[CREATED_USER_ID]
		,[CREATED_DATETIME]
	)
	SELECT 
		 NoteGuid
		,JobGuid
		,Comment
		,CreatedUserId
		,CreatedDateTime
	FROM OPENJSON (@vJson)  
	WITH (  
		 JobGuid		 VARCHAR(50)  
        ,NoteGuid		 VARCHAR(50) 
		,Comment		 NVARCHAR(MAX) 
		,CreatedUserId	 VARCHAR(50) 
		,CreatedDateTime DATETIME  
	 )

END TRY  
BEGIN CATCH  
	BEGIN
		DECLARE @vErrorMessage VARCHAR(1000)  = ERROR_MESSAGE()
		DECLARE @vErrorProc    VARCHAR(200)   = ERROR_PROCEDURE()
		RAISERROR (@vErrorMessage,16,1)  
	END 
END CATCH  
GO



 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
														  STORED PROCEDURE


	PROGRAM ID					: 	[dbo].[Sp_UpdateNote]
	PROGRAM DESCRIPTION			: 	Update Job
	ONLINE/BATCH				: 	ONLINE
	PROCEDURES CALLED          	: 	-
	PROCEDURES CALLING THIS     : 	-
	INPUT PARAMETERS            : 	-
	OUTPUT PARAMETER         	: 	-

MODIFICATION LOG            
**************************************************************************************************************************************************************

	     Ver       	 	Author           	  Changes made          										  Date		
     	 1.0	        Anuruddha		    Original Version												03-06-2022
***************************************************************************************************************************************************************/ 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_UpdateNote]  
(
	 @vJson NVARCHAR(MAX)
)
AS    
BEGIN TRY

	UPDATE 
		N
	SET 
		 N.[COMMENT]		  = D.Comment
		,N.[UPDATED_USER_ID]  = D.UpdatedUserId
		,N.[UPDATED_DATETIME] = D.UpdatedDateTime 
	FROM
		dbo.NOTE N
		INNER JOIN (
			SELECT 
				 NoteGuid
				,Comment
				,UpdatedUserId
				,UpdatedDateTime
			FROM OPENJSON (@vJson)  
			WITH (  
				 NoteGuid		 VARCHAR(50)  
				,Comment		 NVARCHAR(MAX)  
				,UpdatedUserId	 VARCHAR(50) 
				,UpdatedDateTime DATETIME  
			 )
		) AS D ON N.[NOTE_GUID] = D.NoteGuid

END TRY  
BEGIN CATCH  
	BEGIN
		DECLARE @vErrorMessage VARCHAR(1000)  = ERROR_MESSAGE()
		DECLARE @vErrorProc    VARCHAR(200)   = ERROR_PROCEDURE()
		RAISERROR (@vErrorMessage,16,1)  
	END 
END CATCH  
GO
