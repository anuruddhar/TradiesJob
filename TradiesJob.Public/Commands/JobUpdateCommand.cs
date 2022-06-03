
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
using System;
using TradiesJob.Core.Cqrs;
using TradiesJob.Public.Enum;
#endregion

namespace TradiesJob.Public.Commands {
    public class JobUpdateCommand : ICommand {
        public string JobGuid { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Scheduled;
        public string UpdatedUserId { get; set; } = string.Empty;
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
