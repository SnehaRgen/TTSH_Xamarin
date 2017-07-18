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

namespace TTSH.BusinessLogic
{
    public class Feasibility
    {

        #region Feasibility

        public static Feasibility_Details GetFeasibility_DetailsByIDBAL(int ID)
        {
            Feasibility_Details _Feasibility_Details = new Feasibility_Details();
            {
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
                    parameter[parameter.Count - 1].Value = ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                    parameter[parameter.Count - 1].Size = 500;

                    DataTable ProjectsData = new DataTable();
                    ProjectsData = _helper.GetData("[dbo].[spFeasibilityDetailDML]", parameter);



                    foreach (DataRow dr in ProjectsData.Rows)
                    {

                        List<PI_Master> piList = new List<PI_Master>();

                        string xmlDept_PI = (dr.IsNull("DEPT_PI")) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
                        if (xmlDept_PI != string.Empty)
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(xmlDept_PI)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");

                                xmlNodeList.ConvertXmlNodeListToDataTable();
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

                        _Feasibility_Details = new Feasibility_Details()
                        {


                            //i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            s_Email_Send_Date = (dr.IsNull("s_Email_Send_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["s_Email_Send_Date"])),
                            i_Feasibility_Status_ID = (dr.IsNull("i_Feasibility_Status_ID") ? 0 : Convert.ToInt32(dr["i_Feasibility_Status_ID"])),
                            s_Feasibility_Title = (dr.IsNull("s_Feasibility_Title") ? string.Empty : Convert.ToString(dr["s_Feasibility_Title"])),
                            b_Confidential_Agreement = (dr.IsNull("b_Confidential_Agreement") ? (bool?)null : Convert.ToBoolean(dr["b_Confidential_Agreement"])),
                            s_Confidential_Agreement_File = (dr.IsNull("s_Confidential_Agreement_File") ? string.Empty : Convert.ToString(dr["s_Confidential_Agreement_File"])),
                            dt_Survey_Date = (dr.IsNull("dt_Survey_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Survey_Date"])),
                            s_Survey_Comments = (dr.IsNull("s_Survey_Comments") ? string.Empty : Convert.ToString(dr["s_Survey_Comments"])),
                            s_Questionnaire_File = (dr.IsNull("s_Questionnaire_File") ? string.Empty : Convert.ToString(dr["s_Questionnaire_File"])),
                            s_Protocol_No = (dr.IsNull("s_Protocol_No") ? string.Empty : Convert.ToString(dr["s_Protocol_No"])),
                            dt_Protocol_Date = (dr.IsNull("dt_Protocol_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Protocol_Date"])),
                            s_Prototcol_Doc_No = (dr.IsNull("s_Prototcol_Doc_No") ? string.Empty : Convert.ToString(dr["s_Prototcol_Doc_No"])),
                            s_Prototcol_File = (dr.IsNull("s_Prototcol_File") ? string.Empty : Convert.ToString(dr["s_Prototcol_File"])),
                            dt_Site_Visit_Date = (dr.IsNull("dt_Site_Visit_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Site_Visit_Date"])),
                            s_Coinvestigator = (dr.IsNull("s_Coinvestigator") ? string.Empty : Convert.ToString(dr["s_Coinvestigator"])),
                            b_Interest = (dr.IsNull("b_Interest") ? (bool?)null : Convert.ToBoolean(dr["b_Interest"])),
                            s_Interest_Comments = (dr.IsNull("s_Interest_Comments") ? string.Empty : Convert.ToString(dr["s_Interest_Comments"])),
                            b_Feasibility_Outcome = dr.IsNull("b_Feasibility_Outcome") ? null : (bool?)(bool)dr["b_Feasibility_Outcome"],
                            s_IM_Invitation = (dr.IsNull("s_IM_Invitation") ? string.Empty : Convert.ToString(dr["s_IM_Invitation"])),
                            s_In_File = dr.IsNull("s_In_File") ? null : (bool?)(bool)dr["s_In_File"],
                            //s_CreatedBy_ID = (dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"])),
                            //s_ModifyBy_ID = (dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"])),
                            //dt_Created_Date = (dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"])),
                            dt_Modify_Date = (dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"])),
                            dt_Feasibility_Start_Date = (dr.IsNull("dt_Feasibility_Start_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Feasibility_Start_Date"])),
                            SPONSOR = (dr.IsNull("SPONSOR") ? string.Empty : Convert.ToString(dr["SPONSOR"])),
                            PROJECT_DATA = (dr.IsNull("PROJECT_DATA") ? string.Empty : Convert.ToString(dr["PROJECT_DATA"])),
                            DEPT_PI = piList,
                            //SAE = (dr.IsNull("SAE") ? string.Empty : Convert.ToString(dr["SAE"])),
                            CRA = (dr.IsNull("CRA") ? string.Empty : Convert.ToString(dr["CRA"])),
                            s_Checklist_File = (dr.IsNull("s_Checklist_File") ? string.Empty : Convert.ToString(dr["s_Checklist_File"])),
                            s_ModifyBy_Name = (dr.IsNull("s_ModifyBy_Name") ? string.Empty : Convert.ToString(dr["s_ModifyBy_Name"])),
                            s_Protocol_Comments = (dr.IsNull("s_Protocol_Comments") ? string.Empty : Convert.ToString(dr["s_Protocol_Comments"]))

                        };
                    }
                }
                catch (Exception e) { }
                return _Feasibility_Details;
            }
        }

        public static string Feasibility_DetailsBAL(Feasibility_Details _Feasibility_Details, Mode mode)
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
                parameter[parameter.Count - 1].Value = _Feasibility_Details.i_ID;
                if (mode.ToString() != "Delete")
                {
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias1";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Project_Alias1;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias2";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Project_Alias2;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Short_Title";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Short_Title;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Email_Send_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Email_Send_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Feasibility_Status_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.i_Feasibility_Status_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Feasibility_Title ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Feasibility_Title;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Confidential_Agreement ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.b_Confidential_Agreement;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Confidential_Agreement_File ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Confidential_Agreement_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Survey_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.dt_Survey_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Survey_Comments ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Survey_Comments;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Questionnaire_File ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Questionnaire_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Protocol_No ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Protocol_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Protocol_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.dt_Protocol_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Prototcol_Doc_No ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Prototcol_Doc_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Prototcol_File ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Prototcol_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Protocol_Comments";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Protocol_Comments;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Site_Visit_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.dt_Site_Visit_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Coinvestigator ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Coinvestigator;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Interest ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.b_Interest;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Interest_Comments ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Interest_Comments;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Feasibility_Outcome ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.b_Feasibility_Outcome;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_IM_Invitation ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_IM_Invitation;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_In_File ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_In_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Checklist_File";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_Checklist_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_ModifyBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.s_ModifyBy_Name;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@dt_Created_Date ";
                    //parameter[parameter.Count - 1].Value = _Feasibility_Details.dt_Created_Date;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@dt_Modify_Date ";
                    //parameter[parameter.Count - 1].Value = _Feasibility_Details.dt_Modify_Date;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Feasibility_Start_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Details.dt_Feasibility_Start_Date;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Feasibility_ID");
                    dt.Columns.Add("Sponsor_ID");

                    dt.Rows.Add("", _Feasibility_Details.SPONSOR);

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Feasibility_Sponsor_Details";
                    parameter[parameter.Count - 1].Value = dt;


                    //DataTable dt3 = new DataTable();
                    //dt3.Columns.Add("i_Project_ID");
                    //dt3.Columns.Add("i_SAE_ID");
                    
                    //dt3.Rows.Add("", _Feasibility_Details.SAE, "");


                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@Selected_SAE_Details";
                    //parameter[parameter.Count - 1].Value = dt3;


                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("i_Project_ID");
                    dt1.Columns.Add("i_CRO_ID");
                    dt1.Columns.Add("i_CRA_ID");

                    dt1.Rows.Add("", _Feasibility_Details.CRA, "");


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Selected_CRA_Details";
                    parameter[parameter.Count - 1].Value = dt1;

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("i_Project_ID");
                    dt2.Columns.Add("i_PI_ID");


                    foreach (Project_PI pi in _Feasibility_Details.Project_PIs)
                    {
                        dt2.Rows.Add(pi.i_Project_ID, pi.i_PI_ID);
                    }

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
                    parameter[parameter.Count - 1].Value = dt2;

                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spFeasibilityDetailDML", parameter)))
                {
                    result = "Success";
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public static List<Feasibility_Grid> Feasibility_FillGridBAL()
        {
            List<Feasibility_Grid> gridlist = new List<Feasibility_Grid>();

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
                gridData = _helper.GetData("[dbo].[spFeasibilityDetailDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    gridlist.Add(new Feasibility_Grid
                    {
                        i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                        PI_Names = (dr.IsNull("PI_Name") ? "" : Convert.ToString(dr["PI_Name"])),
                        s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID") ? "" : Convert.ToString(dr["s_Display_Project_ID"])),
                        s_IRB_No = (dr.IsNull("s_IRB_No") ? "" : Convert.ToString(dr["s_IRB_No"])),
                        s_Project_Category = (dr.IsNull("Project_Category_Name") ? "" : Convert.ToString(dr["Project_Category_Name"])),
                        s_Project_Title = (dr.IsNull("s_Project_Title") ? "" : Convert.ToString(dr["s_Project_Title"])),
                        //Project_Type = (dr.IsNull("Project_Type") ? "" : Convert.ToString(dr["Project_Type"])),
                        Feasibility_Status_ID = (dr.IsNull("i_Feasibility_Status_ID") ? 0 : Convert.ToInt32(dr["i_Feasibility_Status_ID"])),
                        Feasibility_Status_Name = (dr.IsNull("Feasibility_Status_Name") ? "New" : Convert.ToString(dr["Feasibility_Status_Name"])),
                        FeasfibilityMode = (dr.IsNull("FeasfibilityMode") ? "" : Convert.ToString(dr["FeasfibilityMode"])),
                        i_Feasibility_ID = (dr.IsNull("i_Feasibility_ID") ? 0 : Convert.ToInt32(dr["i_Feasibility_ID"])),
                        Project_Status_Disable = (dr.IsNull("Project_Status") ? "" : Convert.ToString(dr["Project_Status"])),

                    });
                }


            }
            catch (Exception)
            {

                throw;
            }

            return gridlist;

        }

        public static string Feasibility_CRO_Details(Feasibility_CRO_Details _Feasibility_CRO_Details, Mode mode)
        {
            string result = "";
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "StatementType";
                parameter[parameter.Count - 1].Value = mode.ToString();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = _Feasibility_CRO_Details.i_CRO_ID;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Feasibility_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_CRO_Details.i_Feasibility_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_CRO_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_CRO_Details.i_CRO_ID;


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
                {
                    result = "Success";
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public static string Feasibility_Dept_PI(Feasibility_Dept_PI _Feasibility_Dept_PI, Mode mode)
        {
            string result = "";
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "StatementType";
                parameter[parameter.Count - 1].Value = mode.ToString();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.i_Dept_Id;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Feasibility_Id ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.i_Feasibility_Id;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Dept_Id ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.i_Dept_Id;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_PI_Id ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.i_PI_Id;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Description ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.s_Description;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_CreatedBy_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.s_CreatedBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_ModifyBy_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.s_ModifyBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Created_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.dt_Created_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Modify_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Dept_PI.dt_Modify_Date;


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
                {
                    result = "Success";
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public static string Feasibility_Sponsor_Details(Feasibility_Sponsor_Details _Feasibility_Sponsor_Details, Mode mode)
        {
            string result = "";
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "StatementType";
                parameter[parameter.Count - 1].Value = mode.ToString();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = _Feasibility_Sponsor_Details.i_Feasibility_ID;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Feasibility_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Sponsor_Details.i_Feasibility_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Sponsor_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Sponsor_Details.i_Sponsor_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_description ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Sponsor_Details.s_description;


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
                {
                    result = "Success";
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public static string Feasibility_Status_Master(Feasibility_Status_Master _Feasibility_Status_Master, Mode mode)
        {
            string result = "";
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "StatementType";
                parameter[parameter.Count - 1].Value = mode.ToString();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.i_ID;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.i_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Name ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.s_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Description ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.s_Description;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_CreatedBy_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.s_CreatedBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_ModifyBy_ID ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.s_ModifyBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Created_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.dt_Created_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Modify_Date ";
                    parameter[parameter.Count - 1].Value = _Feasibility_Status_Master.dt_Modify_Date;


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
                {
                    result = "Success";
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public static string SponsorBAL(Sponsor_Master sponsor)
        {

            string result = "";


            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "Insert";

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_Name";
                parameter[parameter.Count - 1].Value = sponsor.s_Name;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserCId";
                parameter[parameter.Count - 1].Value = sponsor.s_CreatedBy_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;

                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spSponsor_MasterDML", parameter)))
                {
                    result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static string CROBAL(CRO_Master CRO)
        {
            string result = "";


            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "Insert";

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_Name";
                parameter[parameter.Count - 1].Value = CRO.s_Name;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserCId";
                parameter[parameter.Count - 1].Value = CRO.s_CreatedBy_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;

                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spCRO_MasterDML", parameter)))
                {
                    result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }





        #endregion
    }
}
