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
#endregion


namespace TradiesJob.Core.DataAccess.Database {
    public interface IDatabase {
        #region ExecuteDataSet
        Task<DataSet> ExecuteDataSetAsync(string storedProcedure,
                                                        List<SqlParameter> parameters,
                                                        SqlTransaction transaction = null,
                                                        int commandTimeout = 60);
        #endregion

        #region ExecuteNonQuery

        Task<Tuple<T, Dictionary<string, object>>> ExecuteNonQueryAsync<T>(string storedProcedure,
                            List<SqlParameter> parameters,
                            List<string> outputParameterNames = null,
                            SqlTransaction transaction = null,
                            SqlConnection connection = null,
                            int commandTimeout = 60);

        #endregion

        #region ExecuteScalar
        Task<Tuple<T, Dictionary<string, object>>> ExecuteScalarAsync<T>(string storedProcedure,
                                                                                 List<SqlParameter> parameters,
                                                                                 List<string> outputParameterNames = null,
                                                                                 SqlTransaction transaction = null,
                                                                                 SqlConnection connection = null);
        #endregion

        #region ExecuteReader
        Task<IDataReader> ExecuteDataReaderAsync(string storedProcedure,
                                                                List<SqlParameter> parameters = null,
                                                                int commandTimeout = 60);
        #endregion
    }
}
