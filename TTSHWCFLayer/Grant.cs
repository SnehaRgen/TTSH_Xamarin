using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TTSH.Entity;
using TTSH.DataAccess;
using TTSH.BusinessLogic;
using System.Data;
using System.Data.SqlClient;

namespace TTSHWCFLayer
{
    class Grant
    {
        #region " Grant Module "
        public List<Grant_Master> GetAllGrant_MasterDetail()
        {
            List<Grant_Master> Gm = new List<Grant_Master>();
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "StatementType"; //------------Mode
                parameter[parameter.Count - 1].Value = "GetAllProject";         //------------ModuleName
                DataTable ProjectsData = new DataTable();
                ProjectsData = _helper.GetData("UAT.spProjectSelect", parameter);
                foreach (DataRow dr in ProjectsData.Rows)
                {

                    Gm.Add(new Grant_Master()
                    {
                        i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                        i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                        s_Application_ID = (dr.IsNull("s_Application_ID") ? string.Empty : Convert.ToString(dr["s_Application_ID"])),
                        i_GrantType_ID = (dr.IsNull("i_GrantType_ID") ? 0 : Convert.ToInt32(dr["i_GrantType_ID"])),
                        //s_SubmissionStatus = (dr.IsNull("s_SubmissionStatus") ? string.Empty : Convert.ToString(dr["s_SubmissionStatus"])),
                        i_SubmissionStatus = (dr.IsNull("s_SubmissionStatus") ?0 : Convert.ToInt32(dr["s_SubmissionStatus"])),
                        s_Old_Application_ID = (dr.IsNull("s_Old_Application_ID") ? string.Empty : Convert.ToString(dr["s_Old_Application_ID"])),
                        i_Currency_ID = (dr.IsNull("i_Currency_ID") ? 0 : Convert.ToInt32(dr["i_Currency_ID"])),
                        i_Amount_Requested = (dr.IsNull("i_Amount_Requested") ? 0 : Convert.ToInt32(dr["i_Amount_Requested"])),
                        dt_Closing_Date = (dr.IsNull("dt_Closing_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Closing_Date"])),
                        s_Duration = (dr.IsNull("s_Duration") ? string.Empty : Convert.ToString(dr["s_Duration"])),
                        s_Mentor = (dr.IsNull("s_Mentor") ? string.Empty : Convert.ToString(dr["s_Mentor"])),
                        i_FTE = (dr.IsNull("i_FTE") ? 0 : Convert.ToInt32(dr["i_FTE"])),
                       // b_Outcome = (dr.IsNull("b_Outcome") ? false : Convert.ToBoolean(dr["b_Outcome"])),
                       i_Outcome = (dr.IsNull("i_Outcome"))?0:Convert.ToInt32(dr["i_Outcome"]),
                        s_Reviewers_Comments = (dr.IsNull("s_Reviewers_Comments") ? string.Empty : Convert.ToString(dr["s_Reviewers_Comments"])),
                        s_Created_By = (dr.IsNull("s_Created_By") ? string.Empty : Convert.ToString(dr["s_Created_By"])),
                        dt_Created_Date = (dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"])),
                        s_Modified_By = (dr.IsNull("s_Modified_By") ? string.Empty : Convert.ToString(dr["s_Modified_By"])),
                        dt_Modified_Date = (dr.IsNull("dt_Modified_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modified_Date"]))


                    });

                }
            }
            catch (Exception e) { }

            return Gm;

        }

        public Grant_Master GetGrant_MasterDetailsByID(int ID)
        {
            Grant_Master _Grant_Master = new Grant_Master();
            {
                try
                {
                    DataHelper _helper = new DataHelper();
                    _helper.InitializedHelper();
                    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "StatementType";
                    parameter[parameter.Count - 1].Value = "GetProjectByID";
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ID";
                    parameter[parameter.Count - 1].Value = ID;
                    DataTable ProjectsData = new DataTable();
                    ProjectsData = _helper.GetData("UAT.spProjectSelect", parameter);
                    foreach (DataRow dr in ProjectsData.Rows)
                    {
                        _Grant_Master = new Grant_Master()
                        {

                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            s_Application_ID = (dr.IsNull("s_Application_ID") ? string.Empty : Convert.ToString(dr["s_Application_ID"])),
                            i_GrantType_ID = (dr.IsNull("i_GrantType_ID") ? 0 : Convert.ToInt32(dr["i_GrantType_ID"])),
                            //s_SubmissionStatus = (dr.IsNull("s_SubmissionStatus") ? string.Empty : Convert.ToString(dr["s_SubmissionStatus"])),
                            i_SubmissionStatus = (dr.IsNull("s_SubmissionStatus") ? 0 : Convert.ToInt32(dr["s_SubmissionStatus"])),
                            s_Old_Application_ID = (dr.IsNull("s_Old_Application_ID") ? string.Empty : Convert.ToString(dr["s_Old_Application_ID"])),
                            i_Currency_ID = (dr.IsNull("i_Currency_ID") ? 0 : Convert.ToInt32(dr["i_Currency_ID"])),
                            i_Amount_Requested = (dr.IsNull("i_Amount_Requested") ? 0 : Convert.ToInt32(dr["i_Amount_Requested"])),
                            dt_Closing_Date = (dr.IsNull("dt_Closing_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Closing_Date"])),
                            s_Duration = (dr.IsNull("s_Duration") ? string.Empty : Convert.ToString(dr["s_Duration"])),
                            s_Mentor = (dr.IsNull("s_Mentor") ? string.Empty : Convert.ToString(dr["s_Mentor"])),
                            i_FTE = (dr.IsNull("i_FTE") ? 0 : Convert.ToInt32(dr["i_FTE"])),
                           // b_Outcome = (dr.IsNull("b_Outcome") ? false : Convert.ToBoolean(dr["b_Outcome"])),
                            i_Outcome = (dr.IsNull("i_Outcome")) ? 0 : Convert.ToInt32(dr["i_Outcome"]),
                            s_Reviewers_Comments = (dr.IsNull("s_Reviewers_Comments") ? string.Empty : Convert.ToString(dr["s_Reviewers_Comments"])),
                            s_Created_By = (dr.IsNull("s_Created_By") ? string.Empty : Convert.ToString(dr["s_Created_By"])),
                            dt_Created_Date = (dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"])),
                            s_Modified_By = (dr.IsNull("s_Modified_By") ? string.Empty : Convert.ToString(dr["s_Modified_By"])),
                            dt_Modified_Date = (dr.IsNull("dt_Modified_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modified_Date"]))
                        };
                    }
                }
                catch (Exception e) { }
                return _Grant_Master;
            }
        }

        #endregion
    }
}
