using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Configuration;
using System.Data.Common;

namespace TTSH.DataAccess
{
    /// <summary>
    /// Author: TTSH Dev Team
    /// Created Date: 04/06/2015
    /// Description: -
    /// </summary>
    public class DataHelper
    {
        #region Private Member Variables

        /// <summary>
        /// Provider name from configuration file
        /// </summary>
        private static string _providerName = System.Configuration.ConfigurationManager.AppSettings["BackendProvider"];

        /// <summary>
        /// Connection String from configuration file
        /// </summary>
        private static string _connectionString = _providerName == "SQL"
            ? System.Configuration.ConfigurationManager.ConnectionStrings["SQLServerDBConnectionString"].ConnectionString
            : System.Configuration.ConfigurationManager.ConnectionStrings["MySQLServerDBConnectionString"].ConnectionString;

        /// <summary>
        /// Interface of Backend helper
        /// </summary>
        private static IDataHelper _helper = null;

        #endregion

        #region Private Member Functions

        #endregion

        #region Public Member Functions

        /// <summary>
        /// Initialize Helper
        /// </summary>
        public void InitializedHelper()
        {
            switch (_providerName)
            {
                case "SQL":
                    _helper = new SQLHelper(_connectionString);
                    break;
                case "MySQL":
                    _helper = new MySQLHelper();
                    break;
                default:
                    throw new Exception("Invalid Database provider!");
            }
        }

        /// <summary>
        /// Initialize DbParameter
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter()
        {
            switch (_providerName)
            {
                case "SQL":
                    return new System.Data.SqlClient.SqlParameter();
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlParameter();
                default:
                    throw new Exception("Invalid Database provider!");
            }
        }

        public void ConvertStructureParameter(System.Data.Common.DbParameter parameter)
        {
            switch (_providerName)
            {
                case "SQL":
                    AddSQLStructure((System.Data.SqlClient.SqlParameter)parameter);
                    break;
                case "MySQL":
                    throw new Exception("Invalid Database provider!");
                default:
                    throw new Exception("Invalid Database provider!");
            }
        }

        private void AddSQLStructure(System.Data.SqlClient.SqlParameter parameter)
        {
            switch (_providerName)
            {
                case "SQL":
                    parameter.SqlDbType = SqlDbType.Structured;
                break;
                case "MySQL":
                    throw new Exception("Invalid Database provider!");
                default:
                    throw new Exception("Invalid Database provider!");
            }
        }

        //public  CreateDbType()
        //{
        //    switch (_providerName)
        //    {
        //        case "SQL":
        //            return System.Data.SqlDbType;
        //        case "MySQL":
        //            throw new Exception("Invalid Database provider!");
        //        default:
        //            throw new Exception("Invalid Database provider!");
        //    }
        //}

        /// <summary>
        /// Get Data from Backend Database
        /// </summary>
        /// <param name="cmdText">Command Text/Stored Procedure Name</param>
        /// <param name="cmdParms">Parameter List</param>
        /// <returns></returns>
        public DataTable GetData(string cmdText, List<DbParameter> cmdParms)
        {
            return _helper.GetData(cmdText, cmdParms);
        }

        /// <summary>
        /// Insert/Update/Delete Record in Backend Database
        /// </summary>
        /// <param name="cmdText">Command Text/Stored Procedure Name</param>
        /// <param name="cmdParms">Parameter List</param>
        /// <returns></returns>
        public int DMLOperation(string cmdText, List<DbParameter> cmdParms)
        {
            return _helper.DMLOperation(cmdText, cmdParms);
        }

        /// <summary>
        /// Insert/Update/Delete Record in Backend Database with Output Parameters
        /// </summary>
        /// <param name="cmdText">Command Text/Stored Procedure Name</param>
        /// <param name="cmdParms">Parameter List</param>
        /// <returns></returns>
        public Dictionary<string, string> DMLOperationWithOutputParameter(string cmdText, List<DbParameter> cmdParms)
        {
            return _helper.DMLOperationWithOutputParameter(cmdText, cmdParms);
        }

        public DataSet GetDataSet(string cmdText, List<DbParameter> cmdParams)
        {
            return _helper.GetDataSet(cmdText, cmdParams);
        }
        #endregion

    }
}
