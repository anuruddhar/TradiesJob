
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

    public class JobCreateCommand : ICommand {
	public string JobGuid { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string MobileNumber { get; set; } = string.Empty;
	public Status Status { get; set; } = Status.Scheduled;
	public string CreatedUserId { get; set; } = string.Empty;
	public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}

}
