
#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   Api

    Copyright   -   Anuruddha Rajapaksha 
 
 Modification History:
 ==================================================================================================================================================
 Date              Version      Modify by              Description
 --------------------------------------------------------------------------------------------------------------------------------------------------
 03/06/2022         1.0      Anuruddha                  Initial Version
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradiesJob.Core.Cqrs;
using TradiesJob.Public.Commands;
using TradiesJob.Public.Queries;
using TradiesJob.Public.Results;
#endregion

namespace TradiesJob.Api.Controllers {
    [Route("api/job")]
    [ApiController]
    public class JobController : ControllerBase {
        private readonly Messages _messages;

        public JobController(Messages messages)  {
            _messages = messages;
        }

        [HttpGet("search")]
        public async Task<IActionResult> search([FromQuery] JobSearchQuery query) {
            var appResult = await _messages.Dispatch<List<JobSearchResult>>(query);
            return Ok(appResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> get(string id) {
            JobQuery query = new JobQuery();
            query.JobGuid = id;
            var appResult = await _messages.Dispatch<JobResult>(query);
            return Ok(appResult);
        }


        [HttpPost]
        public async Task<IActionResult> save([FromBody] JobCreateCommand command) {
            var appResult = await _messages.Dispatch<AppResult>(command);
            return Ok(appResult);
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] JobUpdateCommand command) {
            var appResult = await _messages.Dispatch<AppResult>(command);
            return Ok(appResult);
        }

    }
}
