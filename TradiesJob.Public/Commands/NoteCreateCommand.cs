
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
 03/06/2022         1.0      Anuruddha                  Initial Version
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
using System;
using TradiesJob.Core.Cqrs;
#endregion


namespace TradiesJob.Public.Commands {
    public class NoteCreateCommand : ICommand {
		public string NoteGuid { get; set; } = string.Empty;
		public string JobGuid { get; set; } = string.Empty;
		public string Comment { get; set; } = string.Empty;
		public string CreatedUserId { get; set; } = string.Empty;
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
	}
}
