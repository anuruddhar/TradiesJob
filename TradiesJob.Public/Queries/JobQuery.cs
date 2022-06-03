
#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   Public

    Copyright   -   Anuruddha Rajapaksha 
 
 Modification History:
 ==================================================================================================================================================
 Date              Version      Modify by              Description
 --------------------------------------------------------------------------------------------------------------------------------------------------
 03/06/2022         1.0      Anuruddha         Initial Version.
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
using TradiesJob.Core.Cqrs;
using TradiesJob.Public.Enum;
#endregion

namespace TradiesJob.Public.Queries {

    public class JobQuery : IQuery {
        public string JobGuid { get; set; } = string.Empty;
    }
}
