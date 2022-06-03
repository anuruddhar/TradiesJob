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
using System;
#endregion


namespace TradiesJob.Domain.Entity {
    public abstract class AuditInfo {
        public virtual string CreatedUserId { get; set; } = string.Empty;
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual string UpdatedUserId { get; set; } = string.Empty;
        public virtual DateTime UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
