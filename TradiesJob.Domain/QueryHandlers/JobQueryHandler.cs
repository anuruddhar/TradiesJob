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
    public sealed class JobQueryHandler : IQueryHandler<JobQuery, JobResult> {

        private readonly IDatabase _database;
        public JobQueryHandler(IDatabase database) {
            _database = database;
        }

        public async Task<JobResult> Handle(JobQuery query) {

            var item = new JobResult();
            var noteList = new List<NoteResult>();
            var note = new NoteResult();

            var param = new List<SqlParameter>();
            param.Add(new SqlParameter() {
                ParameterName = DBParamConstant.GUID, SqlDbType = SqlDbType.NVarChar, Value = query.JobGuid
            });

            using (var data = await _database.ExecuteDataReaderAsync(SPConstant.GET_JOB, param)) {
                if (data.Read()) {
                    item.Name = data["NAME"].ToString();
                    item.MobileNumber = data["MOBILE_NUMBER"].ToString();
                    item.Status = (Status)Convert.ToInt32(data["STATUS"]);
                }
                data.NextResult();
                while (data.Read()) {
                    note = new NoteResult();
                    note.Comment = data["COMMENT"].ToString();
                    noteList.Add(note);
                }
            }
            item.NoteList = noteList;
            return item;
        }
    }
}
