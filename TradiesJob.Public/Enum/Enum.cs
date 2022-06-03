
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
using System.Runtime.Serialization;
#endregion

namespace TradiesJob.Public.Enum {
	public enum Status {
		[EnumMember]
		Scheduled = 0
	   ,
		[EnumMember]
		Active = 1
	   ,
		[EnumMember]
		Invoicing = 2
	   ,
		[EnumMember]
		ToPriced = 3
	   ,
		[EnumMember]
		Completed = 4
	}

	public enum RecordStatus {
		None = 0,
		Inserted = 1,
		Updated = 2,
		Deleted = 3
	}
}
