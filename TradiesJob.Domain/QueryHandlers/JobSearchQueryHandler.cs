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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TradiesJob.Core.Cqrs;
using TradiesJob.Core.DataAccess.Database;
using TradiesJob.Domain.Constant;
using TradiesJob.Public.Enum;
using TradiesJob.Public.Queries;
using TradiesJob.Public.Results;
#endregion

namespace TradiesJob.Domain.QueryHandlers {
    public sealed class JobSearchQueryHandler : IQueryHandler<JobSearchQuery, List<JobSearchResult>> {

        private readonly IDatabase _database;
        public JobSearchQueryHandler(IDatabase database) {
            _database = database;
        }

        public async Task<List<JobSearchResult>> Handle(JobSearchQuery query) {

            var result = new List<JobSearchResult>();
            var item = new JobSearchResult();

            var param = new List<SqlParameter>();
            param.Add(new SqlParameter() {
                ParameterName = DBParamConstant.NAME, SqlDbType = SqlDbType.NVarChar, Value = query.Name
            });
            param.Add(new SqlParameter() {
                ParameterName = DBParamConstant.MOBILE_NUMBER, SqlDbType = SqlDbType.VarChar, Value = query.MobileNumber
            });
            param.Add(new SqlParameter() {
                ParameterName = DBParamConstant.STATUS, SqlDbType = SqlDbType.Int, Value = query.Status
            });
            param.Add(new SqlParameter() {
                ParameterName = DBParamConstant.CURRENT_PAGE, SqlDbType = SqlDbType.Int, Value = query.PageNumber
            });
            param.Add(new SqlParameter() {
                ParameterName = DBParamConstant.PAGE_SIZE, SqlDbType = SqlDbType.Int, Value = query.ItemsPerPage
            });

            using (var data = await _database.ExecuteDataReaderAsync(SPConstant.SEARCH_JOB, param)) {
                while (data.Read()) {
                    item = new JobSearchResult();
                    item.JobGuid = data["JOB_GUID"].ToString();
                    item.Name = data["NAME"].ToString();
                    item.MobileNumber = data["MOBILE_NUMBER"].ToString();
                    item.Status = (Status)Convert.ToInt32(data["STATUS"]);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
