 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
																FUNCTION


	PROGRAM ID					: 	[dbo].[fn_PageStart]
	PROGRAM DESCRIPTION			: 	GetStartPage
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
CREATE OR ALTER FUNCTION [dbo].[fn_PageStart]
(	
	@iCurrentPage		INT  = 1  
   ,@iPageSize			INT = 50
)
RETURNS INT
  AS
BEGIN

  RETURN ((@iCurrentPage -1) * @iPageSize)+1
	
END
GO

 /******************************************************************************************************************************************************************************************

													FERGUS SOFTWARE LTD NEW ZEALAND     
															  TRADIES JOB
																FUNCTION


	PROGRAM ID					: 	[dbo].[fn_PageEnd]
	PROGRAM DESCRIPTION			: 	GetEndPage
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
CREATE OR ALTER FUNCTION [dbo].[fn_PageEnd]
(	
	@iCurrentPage		INT  = 1  
   ,@iPageSize			INT = 50
)
RETURNS INT
  AS
BEGIN

  RETURN (@iCurrentPage * @iPageSize)
	
END
GO

