
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
using System.Collections.Generic;
#endregion

namespace TradiesJob.Public.Results {
    public class AppResult {
        public AppResult() : this(true) { }
        public AppResult(bool val) : this(val, null) { }

        public AppResult(bool val, Exception exception) {
            this.Success = val;
            this.ResultID = string.Empty;
        }

        public bool Success { get; set; }
        public string ResultID { get; set; }
        public object Result { get; set; }
        public string UserMessage { get; set; }
        public Dictionary<string, List<KeyValuePair<string, int>>> ResultValue { get; set; } = new Dictionary<string, List<KeyValuePair<string, int>>>();

    }
}
