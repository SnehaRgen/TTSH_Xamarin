using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.Collections;
using System.Data.Common;

namespace TTSH.DataAccess
{
    /// <summary>
    /// Author: TTSH Dev Team
    /// Created Date: 03/11/2014
    /// Description: Class to interact with SQL as a backend.
    /// </summary>
    internal class SQLHelper : IDataHelper
    {
        #region Private Member Variables

        /// <summary>
        /// Connection for SQL backend
        /// </summary>
        private SqlConnection conn = null;

        #endregion

        #region Private Member Functions

        // Hashtable to store cached parameters
        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());


        /// <summary>
        /// Create and execute a command to return DataReader after binding to a single parameter.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="singleParm">The single SqlParameter object to bind to the query.</param>
        private SqlDataReader ExecuteReaderSingleParm(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParm)
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 100;
            cmd.Parameters.Add(singleParm);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            return rdr;
        }

        /// <summary>
        /// Create and execute a command to return a single-row DataReader after binding to a single parameter.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="singleParm">The single SqlParameter object to bind to the query.</param>
        private SqlDataReader ExecuteReaderSingleRowSingleParm(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParm)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(singleParm);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            return rdr;
        }

        /// <summary>
        /// Create and execute a command to return a single-row DataReader after binding to multiple parameters.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="cmdParms">An array of SqlParameter objects to bind to the query.</param>
        private SqlDataReader ExecuteReaderSingleRow(SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            PrepareCommand(cmd, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            return rdr;
        }

        /// <summary>
        /// Create and execute a command to return a DataReader, no parameters used in the command.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        private SqlDataReader ExecuteReaderNoParm(SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            return rdr;
        }

        /// <summary>
        /// Create and execute a command to return a DataReader after binding to multiple parameters.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="cmdParms">An array of SqlParameter objects to bind to the query.</param>
        private SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText,List<SqlParameter> cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            PrepareCommand(cmd, cmdParms);
            cmd.CommandTimeout = 100;/*100 seconds timeout wait period, added to solve timeout issue*/
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            return rdr;
        }

        /// <summary>
        /// Create and execute a command to return a single scalar (int) value after binding to multiple parameters.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="cmdParms">An array of SqlParameter objects to bind to the query.</param>
        private int ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            PrepareCommand(cmd, cmdParms);
            int val = Convert.ToInt32(cmd.ExecuteScalar());
            return val;
        }


        /// <summary>
        /// Create and execute a command to return a single scalar (int) value after binding to a single parameter.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="singleParm">A SqlParameter object to bind to the query.</param>
        private int ExecuteScalarSingleParm(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParm)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.Parameters.Add(singleParm);
            int val = Convert.ToInt32(cmd.ExecuteScalar());
            return val;
        }

        /// <summary>
        /// Create and execute a command to return a single scalar (int) value. No parameters will be bound to the command.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="singleParm">A SqlParameter object to bind to the query.</param>
        private object ExecuteScalarNoParm(SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            object val = cmd.ExecuteScalar();
            return val;
        }

        /// <summary>
        /// Create and execute a command that returns no result set after binding to multiple parameters.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="cmdParms">An array of SqlParameter objects to bind to the query.</param>
        private int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 100;/*100 seconds timeout wait period, added to solve timeout issue*/
            PrepareCommand(cmd, cmdParms);
            int val = cmd.ExecuteNonQuery();
            return val;
        }

        /// <summary>
        /// Create and execute a command that returns no result set after binding to a single parameter.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="singleParam">A SqlParameter object to bind to the query.</param>
        private int ExecuteNonQuerySingleParm(SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParam)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(singleParam);
            cmd.CommandTimeout = 100;/*100 seconds timeout wait period, added to solve timeout issue*/
            int val = cmd.ExecuteNonQuery();
            return val;
        }

        /// <summary>
        /// Create and execute a command that returns no result set after binding to a single parameter.
        /// </summary>
        /// <param name="trans">ADO transaction.  If null, will not be attached to the command</param>
        /// <param name="cmdType">Type of ADO command; such as Text or Procedure</param>
        /// <param name="cmdText">The actual SQL or the name of the Stored Procedure depending on command type</param>
        /// <param name="singleParam">A SqlParameter object to bind to the query.</param>
        private int ExecuteNonQueryNoParm(SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 100;/*100 seconds timeout wait period, added to solve timeout issue*/
            int val = cmd.ExecuteNonQuery();
            return val;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        private void CacheParameters(string cacheKey, List<SqlParameter> cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        private List<SqlParameter> GetCacheParameters(string cacheKey)
        {
            List<SqlParameter> cachedParms = (List<SqlParameter>)parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            List<SqlParameter> clonedParms = new List<SqlParameter>(cachedParms.Count);

            for (int i = 0, j = cachedParms.Count; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private void PrepareCommand(SqlCommand cmd, List<SqlParameter> cmdParms)
        {
            if (cmdParms != null)
            {
                for (int i = 0; i < cmdParms.Count; i++)
                {
                    SqlParameter parm = (SqlParameter)cmdParms[i];
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #endregion

        #region Public Member Functions

        /// <summary>
        /// Initialize connection with sql server
        /// </summary>
        /// <param name="connectionString">Connection String of SQL Server</param>
        public SQLHelper(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Get Data from SQL backend
        /// </summary>
        /// <param name="cmdText">Command Text/Stored Procedure Name</param>
        /// <param name="cmdParms">Parameter List</param>
        /// <returns></returns>
        public DataTable GetData(string cmdText, List<DbParameter> cmdParms)
        {
            SqlDataReader dr = null;
            DataTable dt = new DataTable();

            if (cmdParms.Count == 0)
                dr = ExecuteReaderNoParm(trans: null,
                    cmdType: CommandType.StoredProcedure,
                    cmdText: cmdText);

            else if (cmdParms.Count == 1)
                dr = ExecuteReaderSingleParm(trans: null,
                    cmdType: CommandType.StoredProcedure,
                    cmdText: cmdText,
                    singleParm: (SqlParameter)cmdParms[0]);

            else
                dr = ExecuteReader(trans: null,
                    cmdType: CommandType.StoredProcedure,
                    cmdText: cmdText,
                    cmdParms: cmdParms.Cast<SqlParameter>().ToList());

            if (dr.HasRows)
                dt.Load(dr);

            if (conn.State == ConnectionState.Open)
                conn.Close();

            return dt;
        }

        /// <summary>
        /// Insert/Update/Delete Record in SQL Database
        /// </summary>
        /// <param name="cmdText">Command Text/Stored Procedure Name</param>
        /// <param name="cmdParms">Parameter List</param>
        /// <returns></returns>
        public int DMLOperation(string cmdText, List<DbParameter> cmdParms)
        {
            int result = 0;

            if (cmdParms.Count == 0)
                result = ExecuteNonQueryNoParm(trans: null,
                    cmdType: CommandType.StoredProcedure,
                    cmdText: cmdText);

            else if (cmdParms.Count == 1)
                result = ExecuteNonQuerySingleParm(trans: null,
                   cmdType: CommandType.StoredProcedure,
                   cmdText: cmdText,
                   singleParam: (SqlParameter)cmdParms[0]);

            else
                result = ExecuteNonQuery(trans: null,
                  cmdType: CommandType.StoredProcedure,
                  cmdText: cmdText,
                  cmdParms: cmdParms.Cast<SqlParameter>().ToList());

            if (conn.State == ConnectionState.Open)
                conn.Close();

            return result;
        }

        /// <summary>
        /// Insert/Update/Delete Record in SQL Database with Output Parameters
        /// </summary>
        /// <param name="cmdText">Command Text/Stored Procedure Name</param>
        /// <param name="cmdParms">Parameter List</param>
        /// <returns></returns>
        public Dictionary<string, string> DMLOperationWithOutputParameter(string cmdText, List<DbParameter> cmdParms)
        {
            Dictionary<string, string> _result = new Dictionary<string, string>();

            if (cmdParms.Count == 0)
                ExecuteNonQueryNoParm(trans: null,
                    cmdType: CommandType.StoredProcedure,
                    cmdText: cmdText);

            else if (cmdParms.Count == 1)
                ExecuteNonQuerySingleParm(trans: null,
                   cmdType: CommandType.StoredProcedure,
                   cmdText: cmdText,
                   singleParam: (SqlParameter)cmdParms[0]);

            else
                ExecuteNonQuery(trans: null,
                  cmdType: CommandType.StoredProcedure,
                  cmdText: cmdText,
                  cmdParms: cmdParms.Cast<SqlParameter>().ToList());

            var _outputParameters = cmdParms.Where(p => p.Direction == ParameterDirection.Output).ToList();
            foreach (var op in _outputParameters)
                _result.Add(op.ParameterName, op.Value.ToString());

            if (conn.State == ConnectionState.Open)
                conn.Close();

            return _result;
        }

        public DataSet GetDataSet(string cmdText, List<DbParameter> cmdParms)
        {
            try
            {
                    SqlDataAdapter da = null;
                    DataSet ds = new DataSet();
            
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (cmdParms != null)
                    {
                        for (int i = 0; i < cmdParms.Count; i++)
                        {
                            SqlParameter parm = (SqlParameter)cmdParms[i];
                            cmd.Parameters.Add(parm);
                        }
                    }
            
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    conn.Close();
                    return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /*
         private SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText,List<SqlParameter> cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            PrepareCommand(cmd, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            return rdr;
        }
         */
        #endregion
    }
}
