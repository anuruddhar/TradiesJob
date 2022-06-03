#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   Domain

    Copyright   -   Anuruddha Rajapaksha 
 
 Modification History:
 ==================================================================================================================================================
 Date              Version      Modify by              Description
 --------------------------------------------------------------------------------------------------------------------------------------------------
 03/06/2022         1.0      Anuruddha         Initial Version.
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
#endregion

namespace TradiesJob.Domain.Constant {
    public class SPConstant {
        public const string SEARCH_JOB = "[dbo].[Sp_SearchJob]";
        public const string GET_JOB = "[dbo].[Sp_GetJob]";
        public const string CREATE_JOB = "[dbo].[Sp_CreateJob]";
        public const string UPDATE_JOB = "[dbo].[Sp_UpdateJob]";
        public const string CREATE_NOTE = "[dbo].[Sp_CreateNote]";
        public const string UPDATE_NOTE = "[dbo].[Sp_UpdateNote]";
    }
}
