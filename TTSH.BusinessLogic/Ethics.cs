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
    public class Ethics
    {

        #region Ethics
        public static string Ethics_DetailsBAL(Ethics_Details _Ethics_Details, Mode mode)
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
                parameter[parameter.Count - 1].Value = _Ethics_Details.i_ID;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias1";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Project_Alias1;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias2";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Project_Alias2;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Short_Title";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Short_Title;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Coinvestigator";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.CO_Investigator;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Category_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Project_Category_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_IRB_Type_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_IRB_Type_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_IRB_Status_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_IRB_Status_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_IRB_Approve_Date";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.dt_IRB_Approve_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Comments";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Comments;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_IRB_No";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_IRB_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_IRB_File";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_IRB_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_IRB_Approve_Enddate";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.dt_IRB_Approve_Enddate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Remarks";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Remarks;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Status_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Project_Status_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Project_Status_date";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.dt_Project_Status_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Sub_Targeted_TTSH";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Sub_Targeted_TTSH;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Sub_targeted";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Sub_targeted;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Sub_Recruited";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Sub_Recruited;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Sub_Recruited_TTSH";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Sub_Recruited_TTSH;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsRenewal";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.b_IsRenewal;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_NewStudy_End_date";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.dt_NewStudy_End_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsClinical_Trial_Insurance";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.b_IsClinical_Trial_Insurance;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Insurance_Period";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Insurance_Period;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Insurance_file";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.s_Insurance_file;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_CRIO_culled";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.b_CRIO_culled;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_CRIO_culled_date";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.dt_CRIO_culled_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsChildBearing";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.b_IsChildBearing;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@s_CreatedBy_ID";
                    //parameter[parameter.Count - 1].Value = _Ethics_Details.s_CreatedBy_ID;//_Ethics_Details.s_CreatedBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.UName;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@dt_Created_Date";
                    //parameter[parameter.Count - 1].Value = _Ethics_Details.dt_Created_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Ethics_Start_Date";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.dt_Ethics_Start_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_EthicsReview_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_EthicsReview_ID;

                    //Any lapses
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Any_Lapses";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.b_Any_Lapses;

                    //Name of IRB
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Name_Of_IRB_ID";
                    parameter[parameter.Count - 1].Value = _Ethics_Details.i_Name_Of_IRB_ID;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("i_Project_ID");
                    dt.Columns.Add("i_PI_ID");


                    foreach (Project_PI pi in _Ethics_Details.Project_PIs)
                    {
                        dt.Rows.Add(pi.i_Project_ID, pi.i_PI_ID);
                    }

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
                    parameter[parameter.Count - 1].Value = dt;


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;

                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spEthicsDetailDML", parameter)))
                {
                    result = "Success" + " | " + parameter[parameter.Count - 1].Value.ToString(); ;
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public static Ethics_Details GetEthics_DetailsByIDBAL(int ID)
        {
            Ethics_Details _Ethics_Details = new Ethics_Details();
            {
                try
                {
                    DataHelper _helper = new DataHelper();
                    _helper.InitializedHelper();
                    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@StatementType";
                    parameter[parameter.Count - 1].Value = "Select";
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ID";
                    parameter[parameter.Count - 1].Value = ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                    parameter[parameter.Count - 1].Size = 500;

                    DataTable ProjectsData = new DataTable();

                    ProjectsData = _helper.GetData("dbo.spEthicsDetailDML", parameter);
                    foreach (DataRow dr in ProjectsData.Rows)
                    {

                        //Parse the xml and fill the PI details
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

                        _Ethics_Details = new Ethics_Details()
                        {


                            //i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            i_IRB_Type_ID = (dr.IsNull("i_IRB_Type_ID") ? 0 : Convert.ToInt32(dr["i_IRB_Type_ID"])),
                            i_IRB_Status_ID = (dr.IsNull("i_IRB_Status_ID") ? 0 : Convert.ToInt32(dr["i_IRB_Status_ID"])),
                            dt_IRB_Approve_Date = (dr.IsNull("dt_IRB_Approve_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_IRB_Approve_Date"])),
                            s_Comments = (dr.IsNull("s_Comments") ? string.Empty : Convert.ToString(dr["s_Comments"])),
                            s_IRB_No = (dr.IsNull("s_IRB_No") ? string.Empty : Convert.ToString(dr["s_IRB_No"])),
                            s_IRB_File = (dr.IsNull("s_IRB_File") ? string.Empty : Convert.ToString(dr["s_IRB_File"])),
                            dt_IRB_Approve_Enddate = (dr.IsNull("dt_IRB_Approve_Enddate") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_IRB_Approve_Enddate"])),
                            s_Remarks = (dr.IsNull("s_Remarks") ? string.Empty : Convert.ToString(dr["s_Remarks"])),
                            i_Project_Status_ID = (dr.IsNull("i_Project_Status_ID") ? 0 : Convert.ToInt32(dr["i_Project_Status_ID"])),
                            dt_Project_Status_date = (dr.IsNull("dt_Project_Status_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Project_Status_date"])),
                            i_Sub_Targeted_TTSH = (dr.IsNull("i_Sub_Targeted_TTSH") ? 0 : Convert.ToInt32(dr["i_Sub_Targeted_TTSH"])),
                            i_Sub_targeted = (dr.IsNull("i_Sub_targeted") ? 0 : Convert.ToInt32(dr["i_Sub_targeted"])),
                            i_Sub_Recruited = (dr.IsNull("i_Sub_Recruited") ? 0 : Convert.ToInt32(dr["i_Sub_Recruited"])),
                            b_IsRenewal = (dr.IsNull("b_IsRenewal") ? null : (bool?)Convert.ToBoolean(dr["b_IsRenewal"])),
                            dt_NewStudy_End_date = (dr.IsNull("dt_NewStudy_End_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_NewStudy_End_date"])),
                            b_IsClinical_Trial_Insurance = (dr.IsNull("b_IsClinical_Trial_Insurance") ? null : (bool?)Convert.ToBoolean(dr["b_IsClinical_Trial_Insurance"])),
                            s_Insurance_Period = (dr.IsNull("s_Insurance_Period") ? "" : Convert.ToString(dr["s_Insurance_Period"])),
                            s_Insurance_file = (dr.IsNull("s_Insurance_file") ? string.Empty : Convert.ToString(dr["s_Insurance_file"])),
                            b_CRIO_culled = (dr.IsNull("b_CRIO_culled") ? false : Convert.ToBoolean(dr["b_CRIO_culled"])),
                            dt_CRIO_culled_date = (dr.IsNull("dt_CRIO_culled_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_CRIO_culled_date"])),
                            b_IsChildBearing = (dr.IsNull("b_IsChildBearing") ? null : (bool?)Convert.ToBoolean(dr["b_IsChildBearing"])),
                            //s_CreatedBy_ID = (dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"])),
                            //s_ModifyBy_ID = (dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"])),
                            //dt_Created_Date = (dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"])),
                            //dt_Modify_Date = (dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"])),
                            Project_Data = (dr.IsNull("Project_Data") ? string.Empty : Convert.ToString(dr["Project_Data"])),
                            //Dept_PI_Names = (dr.IsNull("Dept_PI") ? string.Empty : Convert.ToString(dr["Dept_PI"])),
                            CO_Investigator = (dr.IsNull("CO_Investigator") ? string.Empty : Convert.ToString(dr["CO_Investigator"])),
                            i_Sub_Recruited_TTSH = (dr.IsNull("i_Sub_Recruited_TTSH") ? 0 : Convert.ToInt32(dr["i_Sub_Recruited_TTSH"])),
                            Dept_PI = piList,
                            dt_Ethics_Start_Date = (dr.IsNull("dt_Ethics_Start_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Ethics_Start_Date"])),
                            i_EthicsReview_ID = (dr.IsNull("i_EthicsReview_ID") ? 0 : Convert.ToInt32(dr["i_EthicsReview_ID"])),
                            i_Name_Of_IRB_ID = (dr.IsNull("i_Name_Of_IRB_ID") ? 0 : Convert.ToInt32(dr["i_Name_Of_IRB_ID"])),
                            b_Any_Lapses = (dr.IsNull("b_Any_Lapses") ? null : (bool?)Convert.ToBoolean(dr["b_Any_Lapses"]))

                        };
                    }
                }
                catch (Exception e) { }

            }

            return _Ethics_Details;

        }

        public static List<Ethics_Grid> Ethics_FillGridBAL()
        {
            List<Ethics_Grid> gridlist = new List<Ethics_Grid>();

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
                gridData = _helper.GetData("[dbo].[spEthicsDetailDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    gridlist.Add(new Ethics_Grid
                    {
                        i_ID = Convert.ToInt32(dr["i_ID"]),
                        PI_Names = Convert.ToString(dr["PI_Name"]),
                        s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID") ? "" : Convert.ToString(dr["s_Display_Project_ID"])),
                        s_IRB_No = Convert.ToString(dr["s_IRB_No"]),
                        s_Project_Category = Convert.ToString(dr["Project_Category_Name"]),
                        s_Project_Title = Convert.ToString(dr["s_Project_Title"]),
                        Project_Type = Convert.ToString(dr["Project_Type"]),
                        Ethics_ID = (dr.IsNull("Ethics_ID") ? 0 : Convert.ToInt32(dr["Ethics_ID"])),
                        Status = Convert.ToString(dr["Status"]),
                        Project_Status = (dr.IsNull("Project_Status") ? "New" : Convert.ToString(dr["Project_Status"])),
                        EthicsStatus = Convert.ToString(dr["EthicsStatus"]),
                        Project_Status_Disable = Convert.ToString(dr["Project_Status_Ico"])
                    });
                }


            }
            catch (Exception)
            {

                throw;
            }

            return gridlist;

        }
        #endregion


    }
}
