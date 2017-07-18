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
using System.Reflection;

namespace TTSH.BusinessLogic
{
    public sealed class GranMaster
    {
        public static List<GrantApplication> FillGrid_Grant_Master()
        {
            List<GrantApplication> pm = new List<GrantApplication>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spGrant_MasterDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    pm.Add(new GrantApplication
                    {
                        i_Project_ID = (dr.IsNull("i_Project_ID")) == true ? 0 : Convert.ToInt32(dr["i_Project_ID"]),
                        s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID")) == true ? "" : Convert.ToString(dr["s_Display_Project_ID"]),
                        s_Project_Title = (dr.IsNull("s_Project_Title")) == true ? "" : Convert.ToString(dr["s_Project_Title"]),
                        s_IRB_No = (dr.IsNull("s_IRB_No")) == true ? "" : Convert.ToString(dr["s_IRB_No"]),
                        PI_NAME = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                        i_ID = (dr.IsNull("i_ID")) == true ? 0 : Convert.ToInt32(dr["i_ID"]),
                        s_SubmissionStatus = (dr.IsNull("s_SubmissionStatus")) == true ? "" : Convert.ToString(dr["s_SubmissionStatus"]),
                        s_OutcomeStatus = (dr.IsNull("s_OutcomeStatus")) == true ? "" : Convert.ToString(dr["s_OutcomeStatus"]),

                        ParentProject = (dr.IsNull("ParentProject")) == true ? "" : Convert.ToString(dr["ParentProject"]),
                        ChildParentProject = (dr.IsNull("ChildParentProject")) == true ? "" : Convert.ToString(dr["ChildParentProject"]),
                        parentProjectCount = (dr.IsNull("parentProjectCount")) == true ? "" : Convert.ToString(dr["parentProjectCount"]),
                        IsChildorParent = (dr.IsNull("IsChildorParent")) == true ? "" : Convert.ToString(dr["IsChildorParent"]),

                        Prog = dr.Table.Columns.Contains("Prog") ? (dr.IsNull("Prog")) == true ? "" : Convert.ToString(dr["Prog"]) : "",
                        Mutli = dr.Table.Columns.Contains("Mutli") ? (dr.IsNull("Mutli")) == true ? "" : Convert.ToString(dr["Mutli"]) : "",

                        Project_Status_Disable = (dr["Project_Status"] != DBNull.Value) ? Convert.ToString(dr["Project_Status"]) : "",
                    });

                }

            }
            catch (Exception)
            {
                throw;

            }

            return pm;

        }

        public static string GrantApplication(Grant_Master grant_master, List<Project_Dept_PI> pdi, string mode)
        {
            string result = "";
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = mode.ToString();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = grant_master.i_ID;

                if (mode.ToString() != "Delete")
                {


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = grant_master.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Application_ID";
                    parameter[parameter.Count - 1].Value = grant_master.s_Application_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_GrantType_ID";
                    parameter[parameter.Count - 1].Value = grant_master.i_GrantType_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Grant_SubType_ID";
                    parameter[parameter.Count - 1].Value = grant_master.i_Grant_SubType_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Grant_Sub_SubType_ID";
                    parameter[parameter.Count - 1].Value = grant_master.i_Grant_Sub_SubType_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Grant_Sub_Sub_SubType_ID";
                    parameter[parameter.Count - 1].Value = grant_master.i_Grant_Sub_Sub_SubType_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_SubmissionStatus";
                    parameter[parameter.Count - 1].Value = grant_master.i_SubmissionStatus;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Old_Application_ID";
                    parameter[parameter.Count - 1].Value = grant_master.s_Old_Application_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Amount_Requested";
                    parameter[parameter.Count - 1].Value = grant_master.i_Amount_Requested;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Closing_Date";
                    parameter[parameter.Count - 1].Value = grant_master.dt_Closing_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Duration";
                    parameter[parameter.Count - 1].Value = grant_master.s_Duration;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Mentor";
                    parameter[parameter.Count - 1].Value = grant_master.s_Mentor;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_FTE";
                    parameter[parameter.Count - 1].Value = grant_master.i_FTE;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Outcome";
                    parameter[parameter.Count - 1].Value = grant_master.i_Outcome;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Outcome_Date";
                    parameter[parameter.Count - 1].Value = grant_master.dt_Outcome_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Reviewers_Comments";
                    parameter[parameter.Count - 1].Value = grant_master.s_Reviewers_Comments;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_ApplicationDate";
                    parameter[parameter.Count - 1].Value = grant_master.dt_ApplicationDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_AwardOrganization";
                    parameter[parameter.Count - 1].Value = grant_master.i_AwardOrganization;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_AwardDate";
                    parameter[parameter.Count - 1].Value = grant_master.dt_AwardDate;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
                    parameter[parameter.Count - 1].Value = pdi.ListToDatatable().getColumns(1);

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Grant_Name";
                    parameter[parameter.Count - 1].Value = grant_master.s_Grant_Name.ToString();

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_AwardCountryID";
                    parameter[parameter.Count - 1].Value = grant_master.i_AwardCountryID.ToString();



                }
                //----------- Added by Atul.s
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserName";
                parameter[parameter.Count - 1].Value = grant_master.UserName;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserID";
                parameter[parameter.Count - 1].Value = grant_master.UserID;
                //------- END ------------------------------------------
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spgrant_masterdml", parameter)))
                {
                    result = "Success" + " | " + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) {
                result = ex.Message.Replace("'","");
            }
            return result;
        }

        public static GrantMasterDetails GetGrantApplicationDetails(int grantid)
        {
            Grant_Master gm = new Grant_Master();
            GrantMasterDetails grantMasterDetails = new GrantMasterDetails();
            List<PI_Master> piList = new List<PI_Master>();
            List<Project_Master> pjmasterlist = new List<Project_Master>();
            GrantParentProjectData parentProject = new GrantParentProjectData();
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = grantid;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                DataTable grantMaster = new DataTable();
                grantMaster = _helper.GetData("[dbo].[spGrant_MasterDML]", parameter);
                if (grantMaster != null)
                {
                    if (grantMaster.Rows.Count > 0)
                    {
                        gm = new Grant_Master()
                        {
                            i_ID = grantMaster.Rows[0].IsNull("i_ID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_ID"]),
                            i_Project_ID = grantMaster.Rows[0].IsNull("i_Project_ID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_Project_ID"]),
                            s_Application_ID = grantMaster.Rows[0].IsNull("i_ID") ? "" : Convert.ToString(grantMaster.Rows[0]["s_Application_ID"]),
                            i_GrantType_ID = grantMaster.Rows[0].IsNull("i_GrantType_ID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_GrantType_ID"]),
                            i_Grant_SubType_ID = grantMaster.Rows[0].IsNull("i_Grant_SubType_ID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_Grant_SubType_ID"]),
                            i_Grant_Sub_SubType_ID = grantMaster.Rows[0].IsNull("i_Grant_Sub_SubType_ID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_Grant_Sub_SubType_ID"]),
                            i_Grant_Sub_Sub_SubType_ID = grantMaster.Rows[0].IsNull("i_Grant_Sub_Sub_SubType_ID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_Grant_Sub_Sub_SubType_ID"]),

                            i_SubmissionStatus = grantMaster.Rows[0].IsNull("i_SubmissionStatus") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_SubmissionStatus"]),
                            s_Old_Application_ID = grantMaster.Rows[0].IsNull("s_Old_Application_ID") ? "" : Convert.ToString(grantMaster.Rows[0]["s_Old_Application_ID"]),
                            i_Amount_Requested = grantMaster.Rows[0].IsNull("i_Amount_Requested") ? 0 : Convert.ToDouble(grantMaster.Rows[0]["i_Amount_Requested"]),
                            dt_Closing_Date = grantMaster.Rows[0].IsNull("dt_Closing_Date") ? null :(DateTime?) Convert.ToDateTime(grantMaster.Rows[0]["dt_Closing_Date"].ToString()),
                            s_Duration = grantMaster.Rows[0].IsNull("s_Duration") ? "" : grantMaster.Rows[0]["s_Duration"].ToString(),
                            s_Mentor = grantMaster.Rows[0].IsNull("s_Mentor") ? "" : grantMaster.Rows[0]["s_Mentor"].ToString(),
                            i_FTE = grantMaster.Rows[0].IsNull("i_FTE") ? 0 : Convert.ToDouble(grantMaster.Rows[0]["i_FTE"]),
                            i_Outcome = grantMaster.Rows[0].IsNull("i_Outcome") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_Outcome"]),
                            dt_Outcome_Date = grantMaster.Rows[0].IsNull("dt_Outcome_Date") ? DateTime.Now : Convert.ToDateTime(grantMaster.Rows[0]["dt_Outcome_Date"].ToString()),
                            s_Reviewers_Comments = grantMaster.Rows[0].IsNull("s_Reviewers_Comments") ? "" : grantMaster.Rows[0]["s_Reviewers_Comments"].ToString(),
                            dt_ApplicationDate = grantMaster.Rows[0].IsNull("dt_ApplicationDate") ? DateTime.Now : Convert.ToDateTime(grantMaster.Rows[0]["dt_ApplicationDate"].ToString()),
                            dt_AwardDate = grantMaster.Rows[0].IsNull("dt_AwardDate") ?null :(DateTime?) Convert.ToDateTime(grantMaster.Rows[0]["dt_AwardDate"].ToString()),
                            i_AwardOrganization = grantMaster.Rows[0].IsNull("i_AwardOrganization") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_AwardOrganization"].ToString()),
                            i_AwardCountryID = grantMaster.Rows[0].IsNull("i_AwardCountryID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_AwardCountryID"].ToString()),
                            s_Grant_Name = grantMaster.Rows[0].IsNull("s_Grant_Name") ? "" : grantMaster.Rows[0]["s_Grant_Name"].ToString(),
                            CountryName = grantMaster.Rows[0].IsNull("CountryName") ? "" : grantMaster.Rows[0]["CountryName"].ToString(),
                            GrantDetails_Applied = grantMaster.Rows[0].IsNull("GrantDetails_Applied") ? false : System.Convert.ToBoolean(grantMaster.Rows[0]["GrantDetails_Applied"].ToString()),
                            Total_ChildAmount = grantMaster.Rows[0].IsNull("Total_ChildAmount") ? 0 : Convert.ToDecimal(grantMaster.Rows[0]["Total_ChildAmount"]),
                            i_Child_DurationID = grantMaster.Rows[0].IsNull("i_Child_DurationID") ? 0 : Convert.ToInt32(grantMaster.Rows[0]["i_Child_DurationID"]),
                            s_Child_Duration = grantMaster.Rows[0].IsNull("s_Child_Duration") ? "" : grantMaster.Rows[0]["s_Child_Duration"].ToString()
                        };
                    }
                    //Dept PI
                    string xmlTestManager = (grantMaster.Rows[0].IsNull("DEPT_PI")) == true ? "" : Convert.ToString(grantMaster.Rows[0]["DEPT_PI"]);
                    if (xmlTestManager != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
                            foreach (XmlNode node in xmlNodeList)
                            {
                                PI_Master pi = new PI_Master();

                                if (node["i_Dept_ID"] != null)
                                    pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
                                if (node["i_ID"] != null)
                                    pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
                                if (node["s_Email"] != null)
                                    pi.s_Email = (node["s_Email"].InnerText);
                                if (node["s_Phone_no"] != null)
                                    pi.s_Phone_no = (node["s_Phone_no"].InnerText);
                                if (node["s_MCR_No"] != null)
                                    pi.s_MCR_No = (node["s_MCR_No"].InnerText);
                                if (node["Dept_Name"] != null)
                                    pi.s_DeptName = (node["Dept_Name"].InnerText);
                                if (node["s_PI_Name"] != null)
                                    pi.s_PIName = (node["s_PI_Name"].InnerText);
                                if (node["NOTDELETED_GDetail"] != null)
                                    pi.NOTDELETED_GDetail = Convert.ToInt32(node["NOTDELETED_GDetail"].InnerText);
                                piList.Add(pi);
                            }
                        }

                    }

                    string PJmasterList = (grantMaster.Rows[0].IsNull("PROJECT_DATA")) == true ? "" : Convert.ToString(grantMaster.Rows[0]["PROJECT_DATA"]);
                    if (PJmasterList != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(PJmasterList)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/PROJECT_DATA");

                            foreach (XmlNode node in xmlNodeList)
                            {
                                Project_Master pmMaster = new Project_Master();
                                if (node["ProjmID"] != null)
                                    pmMaster.i_ID = Convert.ToInt32(node["ProjmID"].InnerText);
                                if (node["s_Project_Title"] != null)
                                    pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                                if (node["s_Display_Project_ID"] != null)
                                    pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                                if (node["s_Short_Title"] != null)
                                    pmMaster.s_Short_Title = Convert.ToString(node["s_Short_Title"].InnerText);
                                if (node["Project_Category_Name"] != null)
                                    pmMaster.Project_Category_Name = Convert.ToString(node["Project_Category_Name"].InnerText);
                                if (node["s_Project_Alias1"] != null)
                                    pmMaster.s_Project_Alias1 = Convert.ToString(node["s_Project_Alias1"].InnerText);
                                if (node["s_Project_Alias2"] != null)
                                    pmMaster.s_Project_Alias2 = Convert.ToString(node["s_Project_Alias2"].InnerText);
                                if (node["s_IRB_No"] != null)
                                    pmMaster.s_IRB_No = Convert.ToString(node["s_IRB_No"].InnerText);
                                if (node["b_Ischild"] != null)
                                    pmMaster.b_Ischild = Convert.ToBoolean(node["b_Ischild"].InnerText);
                                if (node["i_Parent_ProjectID"] != null)
                                    pmMaster.i_Parent_ProjectID = Convert.ToInt32(node["i_Parent_ProjectID"].InnerText);

                                pjmasterlist.Add(pmMaster);
                            }
                        }

                    }
                    string parentProjectdata = (grantMaster.Rows[0].IsNull("Parent_Project_Data")) == true ? "" : Convert.ToString(grantMaster.Rows[0]["Parent_Project_Data"]);
                    if (parentProjectdata != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(parentProjectdata)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/Parent_Project_Data");
                            foreach (XmlNode node in xmlNodeList)
                            {


                                if (node["s_Project_Title"] != null)
                                    parentProject.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                                if (node["s_Display_Project_ID"] != null)
                                    parentProject.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                                if (node["GrantApplied"] != null)
                                    parentProject.GrantApplied = Convert.ToBoolean(node["GrantApplied"].InnerText);
                                if (node["Total_Amount"] != null)
                                    parentProject.Total_Amount = Convert.ToDecimal(node["Total_Amount"].InnerText);
                                if (node["i_DurationID"] != null)
                                    parentProject.i_DurationID = Convert.ToInt32(node["i_DurationID"].InnerText);
                                if (node["Remaining_Amount"] != null)
                                    parentProject.Remaining_Amount = Convert.ToDecimal(node["Remaining_Amount"].InnerText);
                                if (node["s_Duration"] != null)
                                    parentProject.s_Duration = Convert.ToString(node["s_Duration"].InnerText);

                            }
                        }
                    }
                    grantMasterDetails = new GrantMasterDetails()
                    {
                        parentProject = parentProject,
                        Pilisst = piList,
                        project = pjmasterlist[0],
                        grant_Master = gm

                    };


                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return grantMasterDetails;

        }

        public static GrantMasterDetails GetNewProjectDetails(int ProjectId)
        {
            GrantMasterDetails grantMasDet = new GrantMasterDetails();
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                List<PI_Master> piList = new List<PI_Master>();
                List<Project_Master> pjmasterlist = new List<Project_Master>();

                GrantParentProjectData parentProject = new GrantParentProjectData();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "ByProjectId";
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = ProjectId;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                DataTable ProjectsData = new DataTable();
                ProjectsData = _helper.GetData("[dbo].[spGrant_MasterDML]", parameter);
                foreach (DataRow dr in ProjectsData.Rows)
                {

                    string xmlTestManager = (dr.IsNull("DEPT_PI")) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
                    if (xmlTestManager != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
                            foreach (XmlNode node in xmlNodeList)
                            {
                                PI_Master pi = new PI_Master();

                                if (node["i_Dept_ID"] != null)
                                    pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
                                if (node["i_ID"] != null)
                                    pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
                                if (node["s_Email"] != null)
                                    pi.s_Email = (node["s_Email"].InnerText);
                                if (node["s_Phone_no"] != null)
                                    pi.s_Phone_no = (node["s_Phone_no"].InnerText);
                                if (node["s_MCR_No"] != null)
                                    pi.s_MCR_No = (node["s_MCR_No"].InnerText);
                                if (node["Dept_Name"] != null)
                                    pi.s_DeptName = (node["Dept_Name"].InnerText);
                                if (node["s_PI_Name"] != null)
                                    pi.s_PIName = (node["s_PI_Name"].InnerText);
                                if (node["NOTDELETED_GDetail"] != null)
                                    pi.NOTDELETED_GDetail = Convert.ToInt32(node["NOTDELETED_GDetail"].InnerText);
                                piList.Add(pi);
                            }
                        }

                    }

                    string PJmasterList = (dr.IsNull("PROJECT_DATA")) == true ? "" : Convert.ToString(dr["PROJECT_DATA"]);
                    if (PJmasterList != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(PJmasterList)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/PROJECT_DATA");

                            foreach (XmlNode node in xmlNodeList)
                            {
                                Project_Master pmMaster = new Project_Master();
                                if (node["ProjmID"] != null)
                                    pmMaster.i_ID = Convert.ToInt32(node["ProjmID"].InnerText);
                                if (node["s_Project_Title"] != null)
                                    pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                                if (node["s_Display_Project_ID"] != null)
                                    pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                                if (node["s_Short_Title"] != null)
                                    pmMaster.s_Short_Title = Convert.ToString(node["s_Short_Title"].InnerText);
                                if (node["Project_Category_Name"] != null)
                                    pmMaster.Project_Category_Name = Convert.ToString(node["Project_Category_Name"].InnerText);
                                if (node["s_Project_Alias1"] != null)
                                    pmMaster.s_Project_Alias1 = Convert.ToString(node["s_Project_Alias1"].InnerText);
                                if (node["s_Project_Alias2"] != null)
                                    pmMaster.s_Project_Alias2 = Convert.ToString(node["s_Project_Alias2"].InnerText);
                                if (node["s_IRB_No"] != null)
                                    pmMaster.s_IRB_No = Convert.ToString(node["s_IRB_No"].InnerText);
                                if (node["b_Ischild"] != null)
                                    pmMaster.b_Ischild = Convert.ToBoolean(node["b_Ischild"].InnerText);
                                if (node["i_Parent_ProjectID"] != null)
                                    pmMaster.i_Parent_ProjectID = Convert.ToInt32(node["i_Parent_ProjectID"].InnerText);

                                pjmasterlist.Add(pmMaster);
                            }
                        }

                    }
                    string parentProjectdata = (dr.IsNull("Parent_Project_Data")) == true ? "" : Convert.ToString(dr["Parent_Project_Data"]);
                    if (parentProjectdata != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(parentProjectdata)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/Parent_Project_Data");
                            foreach (XmlNode node in xmlNodeList)
                            {
                                if (node["s_Project_Title"] != null)
                                    parentProject.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                                if (node["s_Display_Project_ID"] != null)
                                    parentProject.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                                if (node["GrantApplied"] != null)
                                    parentProject.GrantApplied = Convert.ToBoolean(node["GrantApplied"].InnerText);
                                if (node["Total_Amount"] != null)
                                    parentProject.Total_Amount = Convert.ToDecimal(node["Total_Amount"].InnerText);
                                if (node["i_DurationID"] != null)
                                    parentProject.i_DurationID = Convert.ToInt32(node["i_DurationID"].InnerText);
                                if (node["Remaining_Amount"] != null)
                                    parentProject.Remaining_Amount = Convert.ToDecimal(node["Remaining_Amount"].InnerText);
                                if (node["s_Duration"] != null)
                                    parentProject.s_Duration = Convert.ToString(node["s_Duration"].InnerText);

                                //*************  Added by Atul on Behalf of swati and shrutika 08-09-2015
                                if (node["s_Application_ID"] != null)
                                {
                                    parentProject.s_Application_ID = Convert.ToString(node["s_Application_ID"].InnerText);
                                }
                                if (node["dt_ApplicationDate"] != null)
                                {
                                    parentProject.dt_ApplicationDate = Convert.ToString(node["dt_ApplicationDate"].InnerText);
                                }
                                if (node["i_AwardOrganization"] != null)
                                {
                                    parentProject.i_AwardOrganization = Convert.ToInt32(node["i_AwardOrganization"].InnerText);
                                }
                                if (node["i_AwardCountryID"] != null)
                                {
                                    parentProject.i_AwardCountryID = Convert.ToInt32(node["i_AwardCountryID"].InnerText);
                                }
                                if (node["AwardCountry"] != null)
                                {
                                    parentProject.AwardCountry = Convert.ToString(node["AwardCountry"].InnerText);
                                }
                                if (node["s_Grant_Name"] != null)
                                {
                                    parentProject.s_Grant_Name = Convert.ToString(node["s_Grant_Name"].InnerText);
                                }
                                if (node["dt_AwardDate"] != null)
                                {
                                    parentProject.dt_AwardDate = Convert.ToString(node["dt_AwardDate"].InnerText);
                                }
                                if (node["i_Outcome"] != null)
                                {
                                    parentProject.i_Outcome = Convert.ToInt32(node["i_Outcome"].InnerText);
                                }
                                if (node["s_Reviewers_Comments"] != null)
                                {
                                    parentProject.s_Reviewers_Comments = Convert.ToString(node["s_Reviewers_Comments"].InnerXml);
                                }
                                if (node["i_Grant_Sub_SubType_ID"] != null)
                                {
                                    parentProject.i_Grant_Sub_SubType_ID = Convert.ToInt32(node["i_Grant_Sub_SubType_ID"].InnerText);
                                }
                                if (node["i_Grant_Sub_Sub_SubType_ID"] != null)
                                {
                                    parentProject.i_Grant_Sub_Sub_SubType_ID = Convert.ToInt32(node["i_Grant_Sub_Sub_SubType_ID"].InnerText);
                                }
                                //*************  END by Atul on Behalf of swati and shrutika
                            }
                        }
                    }
                    grantMasDet = new GrantMasterDetails()
                    {
                        parentProject = parentProject,
                        Pilisst = piList,
                        project = pjmasterlist[0]
                    };


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return grantMasDet;
        }
    }
}
