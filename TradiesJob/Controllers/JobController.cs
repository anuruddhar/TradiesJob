
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
using System.Linq;
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

        // http://localhost:61281/api/job/search?Name=Create&MobileNumber=84816894&Status=0&PageNumber=1&ItemsPerPage=10
        [HttpGet("search")]
        public async Task<IActionResult> search([FromQuery] JobSearchQuery query) {
            if(query== null) {
                return BadRequest();
            }
            var appResult = await _messages.Dispatch<List<JobSearchResult>>(query);
            if (appResult == null) {
                return NotFound();
            }
            if (query.OrderBy == "Mobile") {
                appResult = appResult.OrderBy(i => i.MobileNumber).ToList();
            } else if (query.OrderBy == "Name") {
                appResult = appResult.OrderBy(i => i.Name).ToList();
            }
            return Ok(appResult);
        }

        // http://localhost:61281/api/job/4d60a220-cc93-4f3a-8ed3-5b986619e158
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> get(string id) {
            if (id == null) {
                return BadRequest();
            }
            JobQuery query = new JobQuery();
            query.JobGuid = id;
            var appResult = await _messages.Dispatch<JobResult>(query);
            if (appResult == null) {
                return NotFound();
            }
            return Ok(appResult);
        }

        /*
        http://localhost:61281/api/job
        {
	        "JobGuid":"d75d0c78-c4ce-44a8-8df1-2e4e7c07ee8f",
	        "Name":"Test",
	        "MobileNumber":"+94713045780",
            "Status":0,
            "CreatedUserId":"Test",
            "CreatedDateTime":"2022-06-04"
        }
        */
        [HttpPost]
        public async Task<IActionResult> save([FromBody] JobCreateCommand command) {
            if (command == null) {
                return BadRequest();
            }
            var appResult = await _messages.Dispatch<AppResult>(command);
            if (appResult == null || !appResult.Success) {
                return NotFound();
            }
            // Todo: Instead manual mapping later introduce AutoMapper. 
            JobResult result = new JobResult();
            result.Name = command.Name;
            result.MobileNumber = command.MobileNumber;
            result.Status = command.Status;

            return CreatedAtRoute("Get", routeValues: new { id = command.JobGuid }, value: result);
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] JobUpdateCommand command) {
            if (command == null) {
                return BadRequest();
            }
            var appResult = await _messages.Dispatch<AppResult>(command);
            if (appResult == null || !appResult.Success) {
                return NotFound();
            }
            return NoContent();
        }

    }
}
