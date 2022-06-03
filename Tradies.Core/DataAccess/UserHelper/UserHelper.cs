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
using TradiesJob.Core.DataAccess.RequestContext;
#endregion


namespace TradiesJob.Core.DataAccess.UserHelper {
    public class UserHelper : IUserHelper {
        private readonly IRequestContextService _requestContextService;

        public UserHelper(IRequestContextService requestContextService) {
            _requestContextService = requestContextService;
        }
        public string GetUsername() {
            if (!string.IsNullOrEmpty(_requestContextService.UserName)) {
                return _requestContextService.UserName.ToUpper();
            }
            return string.Empty;
        }

        public string GetUsernameWithDomain() {
            var hostNameWithUser = "";

            if (!string.IsNullOrEmpty(_requestContextService.UserName)) {
                hostNameWithUser += $"Domain\\{ _requestContextService.UserName}";
            }
            return hostNameWithUser.ToUpper();
        }

        public string GetFullName() {
            return GetUsername();
        }

        public string GetWorkstation() {

            if (!string.IsNullOrEmpty(_requestContextService.HostName)) {
                return _requestContextService.HostName.ToUpper();
            }

            return string.Empty;
        }
    }
}
