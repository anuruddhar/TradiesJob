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
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TradiesJob.Core.DataAccess.Connection;
#endregion

namespace TradiesJob.Core.DataAccess.Database {
    public class Database : IDatabase {

        private readonly ConnectionStrings _connectionStrings;
        private readonly int _commandTimeout;

        public Database(ConnectionStrings connectionStrings) {
            _connectionStrings = connectionStrings;
            _commandTimeout = 90;
        }

        #region ExecuteDataSet
        public async Task<DataSet> ExecuteDataSetAsync(string storedProcedure,
                                                        List<SqlParameter> parameters,
                                                        SqlTransaction transaction = null,
                                                        int commandTimeout = 60) {
            var con = GetQueryConnection();
            try {
                using (var command = new SqlCommand(storedProcedure, con)) {

                    if (transaction != null) {
                        command.Transaction = transaction;
                    }

                    foreach (var sqlParameter in parameters) {
                        command.Parameters.Add(sqlParameter);
                    }

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = commandTimeout;
                    var da = new SqlDataAdapter(command);

                    con.Open();
                    var ds = new DataSet();
                    //da.Fill(ds);
                    await Task.Run(() => da.Fill(ds));
                    //await Task.FromResult(da.Fill(ds));
                    return ds;
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (transaction == null) {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        #endregion

        #region ExecuteNonQuery
        public async Task<Tuple<T, Dictionary<string, object>>> ExecuteNonQueryAsync<T>(string storedProcedure,
                            List<SqlParameter> parameters,
                            List<string> outputParameterNames = null,
                            SqlTransaction transaction = null,
                            SqlConnection connection = null,
                            int commandTimeout = 60) {
            Dictionary<string, object> outputParameters;
            var con = connection ?? GetCommandConnection();
            try {
                using (var command = new SqlCommand(storedProcedure, con)) {
                    if (transaction != null) {
                        command.Transaction = transaction;
                    }

                    foreach (var sqlParameter in parameters) {
                        command.Parameters.Add(sqlParameter);
                    }

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = commandTimeout;
                    // Execute the command & return the results
                    if (connection == null) {
                        con.Open();
                    }
                    object retval = await command.ExecuteNonQueryAsync();

                    if (outputParameterNames != null && outputParameterNames.Any()) {
                        outputParameters = outputParameterNames.ToDictionary(outputParameter => outputParameter,
                            outputParameter => command.Parameters[outputParameter].Value);
                    } else {
                        outputParameters = new Dictionary<string, object>();
                    }
                    return new Tuple<T, Dictionary<string, object>>((T)retval, outputParameters);
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (transaction == null) {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        #endregion

        #region ExecuteScalar
        public async Task<Tuple<T, Dictionary<string, object>>> ExecuteScalarAsync<T>(string storedProcedure,
                                                                                 List<SqlParameter> parameters,
                                                                                 List<string> outputParameterNames = null,
                                                                                 SqlTransaction transaction = null,
                                                                                 SqlConnection connection = null) {

            var con = GetQueryConnection();
            Dictionary<string, object> outputParameters;

            try {
                using (var command = new SqlCommand(storedProcedure, con)) {

                    if (transaction != null) {
                        command.Transaction = transaction;
                    }


                    foreach (var sqlParameter in parameters) {
                        command.Parameters.Add(sqlParameter);
                    }

                    command.CommandType = CommandType.StoredProcedure;
                    // Execute the command & return the results
                    if (connection == null) {
                        con.Open();
                    }
                    var retval = await command.ExecuteScalarAsync();

                    if (outputParameterNames != null && outputParameterNames.Any()) {
                        outputParameters = outputParameterNames.ToDictionary(outputParameter => outputParameter,
                            outputParameter => command.Parameters[outputParameter].Value);
                    } else {
                        outputParameters = new Dictionary<string, object>();
                    }
                    return new Tuple<T, Dictionary<string, object>>((T)retval, outputParameters);
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (transaction == null) {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        #endregion

        #region ExecuteReader

        public async Task<IDataReader> ExecuteDataReaderAsync(string storedProcedure,
                                                                List<SqlParameter> parameters = null,
                                                                int commandTimeout = 60) {
            var connection = GetQueryConnection();
            var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedure;

            if (parameters != null) {
                foreach (var sqlParameter in parameters) {
                    command.Parameters.Add(sqlParameter);
                }
            }
            connection.Open();
            try {
                command.CommandTimeout = commandTimeout;
                IDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                if (parameters != null) {
                    var outputParameters = GetOutputParameters(parameters);
                    foreach (var outputParameter in outputParameters) {
                        // Todo: Read Outputs
                        /*parameters.Dictionary[outputParameter.ParameterName].Value =
                            command.Parameters[outputParameter.ParameterName].Value;*/
                    }
                }

                command.Dispose();
                command = null;
                return reader;
            } catch (Exception ex) {
                connection.Close();
                connection = null;
                throw ex;
            }
        }

        private List<SqlParameter> GetOutputParameters(List<SqlParameter> Parameters) {
            return
                Parameters.Where(
                    x => x.Direction == ParameterDirection.Output || x.Direction == ParameterDirection.InputOutput)
                    .ToList();
        }

        #endregion

        public SqlConnection GetCommandConnection() {
            return new SqlConnection(_connectionStrings.CommandsConnectionString);
        }

        public SqlConnection GetQueryConnection() {
            return new SqlConnection(_connectionStrings.QueriesConnectionString);
        }

    }
}
