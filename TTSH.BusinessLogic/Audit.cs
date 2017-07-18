using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TTSH.DataAccess;
using TTSH.Entity;


namespace TTSH.BusinessLogic
{
   public class AuditBL
    {
       public static List<Audit> FillGrid_Audit(DateTime FromDate, DateTime ToDate)
        {
            List<Audit> au = new List<Audit>();
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@FromDate";
                parameter[parameter.Count - 1].Value = FromDate;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@ToDate";
                parameter[parameter.Count - 1].Value = ToDate;

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spAudit_Details]", parameter);
                foreach (DataRow dr in gridData.Rows)
                {
                    au.Add(new Audit
                    {
                        ID = Convert.ToString(dr["Project_ID"]),
                        TableName = Convert.ToString(dr["Table_Name"]),
                        Columns = Convert.ToString(dr["Field_Name"]),
                        OldValue = Convert.ToString(dr["Old_Value"]),
                        NewValue = Convert.ToString(dr["New_Value"]),
                        Audit_Action = Convert.ToString(dr["Action"]),
                        UserId = Convert.ToString(dr["User"]),
                        OnDatetime = Convert.ToString(dr["Date"])
                    });
                }
            }
            catch (Exception)
            {


            }
            return au;
        }
    }
}
