#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   DataAccess

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

namespace TradiesJob.Core.DataAccess.UserHelper {
    public interface IUserHelper {
        string GetFullName();

        string GetUsername();

        string GetUsernameWithDomain();

        string GetWorkstation();
    }
}
