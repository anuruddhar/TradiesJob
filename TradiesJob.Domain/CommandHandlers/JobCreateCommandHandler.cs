
#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   Domain

    Copyright   -   Anuruddha Rajapaksha 
 
 Modification History:
 ==================================================================================================================================================
 Date              Version      Modify by              Description
 --------------------------------------------------------------------------------------------------------------------------------------------------
 03/06/2022         1.0      Anuruddha         Initial Version.
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TradiesJob.Core.Cqrs;
using TradiesJob.Core.DataAccess.Database;
using TradiesJob.Domain.Constant;
using TradiesJob.Public.Commands;
using TradiesJob.Public.Results;
#endregion

namespace TradiesJob.Domain.CommandHandlers {


    public sealed class JobCreateCommandHandler : ICommandHandler<JobCreateCommand, AppResult> {

        private readonly IDatabase _database;
        public JobCreateCommandHandler(IDatabase database) {
            _database = database;
        }

        public async Task<AppResult> Handle(JobCreateCommand command) {
            AppResult appResult = new AppResult(false);
            var param = new List<SqlParameter>();

            SqlParameter sqlParameter = new SqlParameter() {
                ParameterName = DBParamConstant.JSON_DATA,
                SqlDbType = SqlDbType.NVarChar,
                Value = JsonConvert.SerializeObject(command)
            };
            param.Add(sqlParameter);
            var result = await _database.ExecuteNonQueryAsync<int>(SPConstant.CREATE_JOB, param);
            if (result.Item1 >= 1) {
                appResult.Success = true;
            }
            return appResult;
        }
    }
}
