
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
            return CreatedAtRoute("Get", result);
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
