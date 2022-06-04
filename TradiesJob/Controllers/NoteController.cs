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
using System.Threading.Tasks;
using TradiesJob.Core.Cqrs;
using TradiesJob.Public.Commands;
using TradiesJob.Public.Results;
#endregion

namespace TradiesJob.Api.Controllers {
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase {

        private readonly Messages _messages;

        public NoteController(Messages messages) {
            _messages = messages;
        }

        [HttpPost]
        public async Task<IActionResult> save([FromBody] NoteCreateCommand command) {
            if (command == null) {
                return BadRequest();
            }
            var appResult = await _messages.Dispatch<AppResult>(command);
            if (appResult == null || !appResult.Success) {
                return NotFound();
            }
            return CreatedAtRoute("", "");
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] NoteUpdateCommand command) {
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
