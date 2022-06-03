
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
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TradiesJob.Core.DataAccess.Encrypter;
using TradiesJob.Core.DataAccess.UserHelper;
#endregion

namespace TradiesJob.Core.DataAccess.Connection {
    public sealed class ConnectionStrings {
        private readonly IEncrypter _encrypter;
        private readonly IUserHelper _userHelper;
        private string _commandsConnectionString;
        private string _queriesConnectionString;
        private string _userId = string.Empty;
        private string _workstationId = string.Empty;
        private int _connectionTimeOut = 30;
        IOptions<DatabaseConnections> _databaseConnectionOptions;

        public ConnectionStrings(IEncrypter encrypter, IUserHelper userHelper, IOptions<DatabaseConnections> databaseConnectionOptions) {
            _encrypter = encrypter;
            _userHelper = userHelper;
            _databaseConnectionOptions = databaseConnectionOptions;
            _userId = _userHelper.GetUsernameWithDomain();
            _workstationId = _userHelper.GetWorkstation();
        }


        public string CommandsConnectionString {
            get {
                if (string.IsNullOrEmpty(_commandsConnectionString))
                    _commandsConnectionString = GetConnection(_databaseConnectionOptions.Value.CommandConnection);

                return _commandsConnectionString;
            }
        }
        public string QueriesConnectionString {
            get {
                if (string.IsNullOrEmpty(_queriesConnectionString))
                    _queriesConnectionString = GetConnection(_databaseConnectionOptions.Value.QueryConnection);

                return _queriesConnectionString;
            }
        }

        private string GetConnection(string connectionString) {
            /*
            var planConnectionString = _encrypter.Decrypt(connectionString);
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(planConnectionString);
            sqlConnectionStringBuilder.WorkstationID = _workstationId;
            sqlConnectionStringBuilder.ConnectTimeout = _connectionTimeOut;
            sqlConnectionStringBuilder.UserID = _userId;
            return sqlConnectionStringBuilder.ToString();
            */
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TradiesJob;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        /*
        private string FullConnectionString {
            get {
                SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
                sqlConnectionStringBuilder.WorkstationID = WorkstationId;
                sqlConnectionStringBuilder.ConnectTimeout = _connectionTimeOut;
                return sqlConnectionStringBuilder.ToString();
            }
        }
        */

    }
}
