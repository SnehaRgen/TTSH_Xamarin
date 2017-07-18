using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TTSH.DataAccess;
using TTSH.Entity;
using System.Xml;
using System.IO;
using System.Web.Script.Serialization;

namespace TTSH.BusinessLogic
{
    public class RptSelectedProjectChart
    {
        public static List<RptSelectedProject> GetProjectDetails(string Sdate,string Edate)       
        {
            List<RptSelectedProject> Rptobj = new List<RptSelectedProject>();
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Startdate";
                parameter[parameter.Count - 1].Value = Sdate;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Enddate";
                parameter[parameter.Count - 1].Value = Edate;
                DataTable ProjectsData = new DataTable();
                ProjectsData = _helper.GetData("dbo.spRptSelectedProject", parameter);
                foreach (DataRow dr in ProjectsData.Rows)
                {
                    Rptobj.Add(new RptSelectedProject
                    {
                        ID = (dr.IsNull("i_ProjectID") ? 0 : Convert.ToInt32(dr["i_ProjectID"])),
                        ProjectTitle = (dr.IsNull("s_ProjectTitle") ? string.Empty : Convert.ToString(dr["s_ProjectTitle"])),
                        DisplayProjectID = (dr.IsNull("s_DisplayID") ? string.Empty : Convert.ToString(dr["s_DisplayID"]))
                    });
                }  
            }
            catch (Exception)
            {
                
                throw;
            }

            return Rptobj;
        }
    }
    
}
