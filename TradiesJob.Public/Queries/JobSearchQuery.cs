
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
    public class JobSearchQuery : IQuery {
		public string Name { get; set; } = string.Empty;
		public string MobileNumber { get; set; } = string.Empty;
		public Status Status { get; set; } = Status.Scheduled;
        public string OrderBy{ get; set; }
        public int PageNumber { get; set; } = 1;
		public int ItemsPerPage { get; set; } = 20;
	}
}
