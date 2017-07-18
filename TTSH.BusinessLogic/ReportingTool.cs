using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TTSH.DataAccess;

namespace TTSH.BusinessLogic
{
    public class ReportingTool
    {
        public static DataSet GetSelectedTables(int _ModuleID, string _TablesID)
        {
            DataSet _dataSet = new DataSet();
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@ModueleID";
                parameter[parameter.Count - 1].Value = _ModuleID;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@TablesID";
                parameter[parameter.Count - 1].Value = _TablesID;
                _dataSet = _helper.GetDataSet("dbo.spReportingTool", parameter);
            }
            catch (Exception)
            {

                throw;
            }

            return _dataSet;
        }
    }
}
