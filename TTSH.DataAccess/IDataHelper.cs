using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;

namespace TTSH.DataAccess
{
    /// <summary>
    /// Author: TTSH Dev Team
    /// Created Date: 04/06/2015
    /// Description: The interface is created to define contract to perform CRUD Operation in TTSH database. 

    /// </summary>
    internal interface IDataHelper
    {
        DataTable GetData(string cmdText, List<DbParameter> cmdParms);

        int DMLOperation(string cmdText, List<DbParameter> cmdParms);

        Dictionary<string, string> DMLOperationWithOutputParameter(string cmdText, List<DbParameter> cmdParms);

        DataSet GetDataSet(string cmdText, List<DbParameter> cmdParms);
    }
}
