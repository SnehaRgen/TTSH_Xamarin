using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

using System.Data.Common;

namespace TTSH.DataAccess
{
    /// <summary>
    /// Author: TTSH Dev Team
    /// Created Date: 03/11/2014
    /// Description: -
    /// </summary>
    internal class MySQLHelper : IDataHelper
    {
        public DataTable GetData(string cmdText, List<DbParameter> cmdParms)
        {
            throw new Exception("Method Not Implemented");
        }

        public int DMLOperation(string cmdText, List<DbParameter> cmdParms)
        {
            throw new Exception("Method Not Implemented");
        }

        public Dictionary<string, string> DMLOperationWithOutputParameter(string cmdText, List<DbParameter> cmdParms)
        {
            throw new Exception("Method Not Implemented");
        }

        public DataSet GetDataSet(string cmdText, List<DbParameter> cmdParms)
        {
            throw new Exception("Method Not Implemented");
        }
    }
}
