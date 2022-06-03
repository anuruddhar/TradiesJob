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
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace TradiesJob.Core.DataAccess.RequestContext {
    public sealed class RequestContextService : IRequestContextService {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContextService(IHttpContextAccessor accessor) {
            _httpContextAccessor = accessor;
        }

        public string UserName { get; set; }

        public string HostName {
            get {
                try {
                    var fullQualifiedName = System.Net.Dns.GetHostEntry(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress).HostName;
                    var nameComponents = fullQualifiedName.Split('.');
                    var hostName = nameComponents.Length > 0 ? nameComponents.First().ToUpper() : fullQualifiedName.ToUpper();
                    return hostName;
                } catch (Exception) {
                    return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                }
            }
        }

    }
}
