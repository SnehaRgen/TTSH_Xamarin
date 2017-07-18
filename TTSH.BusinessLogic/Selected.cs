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
    public class Selected
    {
        public static List<Selected_Grid> Selected_FillGridBAL(string userID, bool isSelectedTeamUser)
        {
            List<Selected_Grid> gridlist = new List<Selected_Grid>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserCId";
                parameter[parameter.Count - 1].Value = userID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@isSelectedTeamUser";
                parameter[parameter.Count - 1].Value = isSelectedTeamUser;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spSelectedProjectDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    gridlist.Add(new Selected_Grid
                    {
                        i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                        PI_Names = (dr.IsNull("PI_Name") ? "" : Convert.ToString(dr["PI_Name"])),
                        s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID") ? "" : Convert.ToString(dr["s_Display_Project_ID"])),
                        s_IRB_No = (dr.IsNull("s_IRB_No") ? "" : Convert.ToString(dr["s_IRB_No"])),
                        s_Project_Category = (dr.IsNull("Project_Category_Name") ? "" : Convert.ToString(dr["Project_Category_Name"])),
                        s_Project_Title = (dr.IsNull("s_Project_Title") ? "" : Convert.ToString(dr["s_Project_Title"])),
                        Study_Status = (dr.IsNull("Study_Status") ? "New" : Convert.ToString(dr["Study_Status"])),
                        Selected_ID = (dr.IsNull("Selected_i_ID") ? 0 : Convert.ToInt32(dr["Selected_i_ID"])),
                        Status = (dr.IsNull("status") ? "New" : Convert.ToString(dr["status"])),
                        cordinatorstatus = (dr.IsNull("cordinatorstatus") ? "Edit" : Convert.ToString(dr["cordinatorstatus"])),
                        IsCoordinator = dr.Table.Columns.Contains("isCoordinator") ? (dr.IsNull("isCoordinator")) == true ? "" : Convert.ToString(dr["isCoordinator"]) : "",
                        DetailsExist = dr.Table.Columns.Contains("DetailsExist") ? (dr.IsNull("DetailsExist")) == true ? "" : Convert.ToString(dr["DetailsExist"]) : "",
                        Project_Status_Disable = (dr["Project_Status"] != DBNull.Value) ? Convert.ToString(dr["Project_Status"]) : "",

                    });
                }


            }
            catch (Exception)
            {

                throw;
            }

            return gridlist;

        }

        public static Selected_Project_Details GetSelected_Project_DetailsByIDBAL(int ID, string year, string month)
        {
            Selected_Project_Details _Selected_Project_Details = new Selected_Project_Details();
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
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Id";
                    parameter[parameter.Count - 1].Value = ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@selectedYear";
                    parameter[parameter.Count - 1].Value = year;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@selectedMonth";
                    parameter[parameter.Count - 1].Value = month;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                    parameter[parameter.Count - 1].Size = 500;

                    DataTable ProjectsData = new DataTable();
                    ProjectsData = _helper.GetData("spSelectedProjectDML", parameter);
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
                        //Common.TestLogError2("BeforeSAEXML Variable");
                        _Selected_Project_Details = new Selected_Project_Details()
                        {
                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_Id = (dr.IsNull("i_Project_Id") ? 0 : Convert.ToInt32(dr["i_Project_Id"])),
                            b_IsTeam_Needed = (dr.IsNull("b_IsTeam_Needed") ? false : Convert.ToBoolean(dr["b_IsTeam_Needed"])),
                            s_Blinded_Coordinator = (dr.IsNull("s_Blinded_Coordinator") ? "" : Convert.ToString(dr["s_Blinded_Coordinator"])),
                            s_Unblinded_Coordinator = (dr.IsNull("s_Unblinded_Coordinator") ? "" : Convert.ToString(dr["s_Unblinded_Coordinator"])),
                            s_Blinded_Cordinator_name = (dr.IsNull("s_Blinded_Cordinator_name") ? string.Empty : Convert.ToString(dr["s_Blinded_Cordinator_name"])),
                            s_Unblinded_Cordinator_name = (dr.IsNull("s_Unblinded_Cordinator_name") ? string.Empty : Convert.ToString(dr["s_Unblinded_Cordinator_name"])),
                            //i_CRO_ID = (dr.IsNull("i_CRO_ID") ? 0 : Convert.ToInt32(dr["i_CRO_ID"])),
                            i_Study_Status_ID = (dr.IsNull("i_Study_Status_ID") ? 0 : Convert.ToInt32(dr["i_Study_Status_ID"])),
                            i_Project_Type_ID = (dr.IsNull("i_Project_Type_ID") ? 0 : Convert.ToInt32(dr["i_Project_Type_ID"])),
                            s_Clinic1 = (dr.IsNull("s_Clinic1") ? string.Empty : Convert.ToString(dr["s_Clinic1"])),
                            s_Clinic2 = (dr.IsNull("s_Clinic2") ? string.Empty : Convert.ToString(dr["s_Clinic2"])),
                            s_Research_Days = (dr.IsNull("s_Research_Days") ? "" : Convert.ToString(dr["s_Research_Days"])),
                            s_Followup_Duratrion = (dr.IsNull("s_Followup_Duratrion") ? string.Empty : Convert.ToString(dr["s_Followup_Duratrion"])),
                            s_Backup_Blinded = (dr.IsNull("s_Backup_Blinded") ? string.Empty : Convert.ToString(dr["s_Backup_Blinded"])),
                            dt_Recruit_Start_Date = (dr.IsNull("dt_Recruit_Start_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Recruit_Start_Date"])),
                            dt_Recruit_End_Date = (dr.IsNull("dt_Recruit_End_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Recruit_End_Date"])),
                            i_TTSH_Target = (dr.IsNull("i_TTSH_Target") ? 0 : Convert.ToInt32(dr["i_TTSH_Target"])),
                            i_Screen_No = (dr.IsNull("i_Screen_No") ? 0 : Convert.ToInt32(dr["i_Screen_No"])),
                            i_Screen_Failure = (dr.IsNull("i_Screen_Failure") ? 0 : Convert.ToInt32(dr["i_Screen_Failure"])),
                            i_Randomized = (dr.IsNull("i_Randomized") ? 0 : Convert.ToInt32(dr["i_Randomized"])),
                            i_Completed = (dr.IsNull("i_Completed") ? 0 : Convert.ToInt32(dr["i_Completed"])),
                            i_Withdrawl = (dr.IsNull("i_Withdrawl") ? 0 : Convert.ToInt32(dr["i_Withdrawl"])),
                            s_IRB_No = (dr.IsNull("s_IRB_No") ? string.Empty : Convert.ToString(dr["s_IRB_No"])),
                            //dt_Expiry_date = (dr.IsNull("dt_Expiry_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Expiry_date"])),
                            dt_Expiry_date = (dr.IsNull("IRB_Approve_Enddate") ? DateTime.MinValue : Convert.ToDateTime(dr["IRB_Approve_Enddate"])),
                            
                            dt_CTC_Expiry_date = (dr.IsNull("dt_CTC_Expiry_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_CTC_Expiry_date"])),
                            b_CTM_Status = (dr.IsNull("b_CTM_Status") ? false : Convert.ToBoolean(dr["b_CTM_Status"])),
                            dt_CTM_Expiry_date = (dr.IsNull("dt_CTM_Expiry_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_CTM_Expiry_date"])),
                            s_Drug_Name = (dr.IsNull("s_Drug_Name") ? string.Empty : Convert.ToString(dr["s_Drug_Name"])),
                            b_Drug_Status = (dr.IsNull("b_Drug_Status") ? false : Convert.ToBoolean(dr["b_Drug_Status"])),
                            
                            dt_Drug_Expiry_date = (dr.IsNull("dt_Drug_Expiry_date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Drug_Expiry_date"])),
                            s_Drug_Dose = (dr.IsNull("s_Drug_Dose") ? string.Empty : Convert.ToString(dr["s_Drug_Dose"])),
                            i_Drug_Location_ID = (dr.IsNull("i_Drug_Location_ID") ? 0 : Convert.ToInt32(dr["i_Drug_Location_ID"])),
                            dt_Selected_Start_Date = (dr.IsNull("dt_Selected_Start_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Selected_Start_Date"])),
                            //dt_EntryForMonthUnBlinded = (dr.IsNull("dt_EntryForMonthUnBlinded") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_EntryForMonthUnBlinded"])),
                            dt_EntryForMonthBlinded = (dr.IsNull("dt_EntryForMonthBlinded") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_EntryForMonthBlinded"])),
                            monthNames = (dr.IsNull("monthNames") ? "" : Convert.ToString(dr["monthNames"])),
                            Project_Data = (dr.IsNull("PROJECT_DATA") ? "" : Convert.ToString(dr["PROJECT_DATA"])),
                            DEPT_PI = piList,
                            dt_Modify_Date = (dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"])),
                            s_ModifyBy_Name = (dr.IsNull("s_ModifyBy_Name") ? "" : Convert.ToString(dr["s_ModifyBy_Name"])),
                            BLINDED_UNBLINDED_XML = (dr.IsNull("BLINDED_UNBLINDED") ? "" : Convert.ToString(dr["BLINDED_UNBLINDED"])),
                            SAE_XML = (dr.IsNull("SAE") ? "" : Convert.ToString(dr["SAE"])),
                            CRA_XML = (dr.IsNull("CRA") ? "" : Convert.ToString(dr["CRA"])),
                            STUDY_BUDGET_FILE_XML = (dr.IsNull("STUDY_BUDGET_FILE") ? "" : Convert.ToString(dr["STUDY_BUDGET_FILE"])),
                            Co_Ordinator_Type = (dr.IsNull("Co_Ordinator_Type") ? "" : Convert.ToString(dr["Co_Ordinator_Type"])),
                            s_Reason = (dr.IsNull("s_Reason") ? "" : Convert.ToString(dr["s_Reason"])),
                            s_Offsite_Company = (dr.IsNull("s_Offsite_Company") ? "" : Convert.ToString(dr["s_Offsite_Company"])),
                            dt_LastUpdated_By_Blinded = (dr.IsNull("dt_LastUpdated_By_Blinded") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_LastUpdated_By_Blinded"])),
                            dt_LastUpdated_By_UnBlinded = (dr.IsNull("dt_LastUpdated_By_UnBlinded") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_LastUpdated_By_UnBlinded"])),
                            s_LastUpdated_By_Blinded = (dr.IsNull("s_LastUpdated_By_Blinded") ? "" : Convert.ToString(dr["s_LastUpdated_By_Blinded"])),
                            s_LastUpdated_By_UnBlinded = (dr.IsNull("s_LastUpdated_By_UnBlinded") ? "" : Convert.ToString(dr["s_LastUpdated_By_UnBlinded"])),
                            dt_Archiving_Enddate = (dr.IsNull("dt_Archiving_Enddate") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Archiving_Enddate"])),
                            dt_Extended_Month_Blinded = (dr.IsNull("dt_Extended_Month_Blinded") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Extended_Month_Blinded"])),
                            dt_Extended_Month_UnBlinded = (dr.IsNull("dt_Extended_Month_UnBlinded") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Extended_Month_UnBlinded"])),
                            i_CupBoardno_Blinded = (dr.IsNull("i_CupBoardno_Blinded") ? 0 : Convert.ToInt32(dr["i_CupBoardno_Blinded"])),
                            i_CupBoardno_UnBlinded = (dr.IsNull("i_CupBoardno_UnBlinded") ? 0 : Convert.ToInt32(dr["i_CupBoardno_UnBlinded"])),
                            i_Number_of_Boxes = (dr.IsNull("i_Number_of_Boxes") ? 0 : Convert.ToInt32(dr["i_Number_of_Boxes"])),
                            dt_Date_Sent_for_Archiving = (dr.IsNull("dt_Date_Sent_for_Archiving") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Date_Sent_for_Archiving"])),
                            s_Agreement_Number = (dr.IsNull("s_Agreement_Number") ? "" : Convert.ToString(dr["s_Agreement_Number"])),
                            s_AgreementFile = (dr.IsNull("s_AgreementFile") ? "" : Convert.ToString(dr["s_AgreementFile"])),
                            s_Amount = (dr.IsNull("s_Amount") ? "" : Convert.ToString(dr["s_Amount"])),
                            b_Awaiting_Archiving = (dr.IsNull("b_Awaiting_Archiving") ? null :(bool?) Convert.ToBoolean(dr["b_Awaiting_Archiving"])),
                            b_IsApproveProject = (dr.IsNull("b_IsApproveProject") ? null :(bool?) Convert.ToBoolean(dr["b_IsApproveProject"])),
                            s_LastUpdated_By_SelectedTeamUser = (dr.IsNull("s_LastUpdated_By_SelectedTeamUser") ? "" : Convert.ToString(dr["s_LastUpdated_By_SelectedTeamUser"])),
                            dt_LastUpdated_By_SelectedTeamUser = (dr.IsNull("dt_LastUpdated_By_SelectedTeamUser") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_LastUpdated_By_SelectedTeamUser"]))

                        };
                    }
                }
                catch (Exception e) { }
                return _Selected_Project_Details;
            }
        }

        public static string Selected_Project_DetailsBAL(Selected_Project_Details _Selected_Project_Details, Mode mode)
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

                if (mode.ToString() != "Delete")
                {
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ID";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Id";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Project_Id;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias1";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Project_Alias1;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias2";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Project_Alias2;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Short_Title";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Short_Title;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsTeam_Needed";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.b_IsTeam_Needed;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Blinded_Coordinator";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Blinded_Coordinator;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Unblinded_Coordinator";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Unblinded_Coordinator;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Blinded_Cordinator_name";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Blinded_Cordinator_name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Unblinded_Cordinator_name";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Unblinded_Cordinator_name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_SAE_ID";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_SAE_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_CRO_ID";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_CRO_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Study_Status_ID";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Study_Status_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Type_ID";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Project_Type_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Clinic1";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Clinic1;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Clinic2";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Clinic2;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Research_Days";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Research_Days;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Followup_Duratrion";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Followup_Duratrion;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Backup_Blinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Backup_Blinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Recruit_Start_Date";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Recruit_Start_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Recruit_End_Date";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Recruit_End_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_TTSH_Target";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_TTSH_Target;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Screen_No";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Screen_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Screen_Failure";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Screen_Failure;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Randomized";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Randomized;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Completed";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Completed;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Withdrawl";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Withdrawl;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_IRB_No";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_IRB_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Expiry_date";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Expiry_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_CTC_Expiry_date";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_CTC_Expiry_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_CTM_Status";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.b_CTM_Status;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_CTM_Expiry_date";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_CTM_Expiry_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Drug_Name";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Drug_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Drug_Status";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.b_Drug_Status;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Drug_Expiry_date ";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Drug_Expiry_date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Drug_Dose ";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Drug_Dose;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Selected_Start_Date ";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Selected_Start_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Drug_Location_ID ";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Drug_Location_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Extended_Month_Blinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Extended_Month_Blinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_CupBoardno_Blinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_CupBoardno_Blinded;

                    //------------------------
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Extended_Month_UnBlinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Extended_Month_UnBlinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_CupBoardno_UnBlinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_CupBoardno_UnBlinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Awaiting_Archiving";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.b_Awaiting_Archiving;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Reason";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Reason;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Archiving_Enddate";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Archiving_Enddate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Offsite_Company";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Offsite_Company;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsApproveProject";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.b_IsApproveProject;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.UserCId;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_EntryForMonthBlinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_EntryForMonthBlinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_EntryForMonthUnBlinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_EntryForMonthUnBlinded;

                    //---
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_LastUpdated_By_Blinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_LastUpdated_By_Blinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_LastUpdated_By_UnBlinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_LastUpdated_By_UnBlinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_LastUpdated_By_SelectedTeamUser";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_LastUpdated_By_SelectedTeamUser;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_LastUpdated_By_Blinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_LastUpdated_By_Blinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_LastUpdated_By_UnBlinded";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_LastUpdated_By_UnBlinded;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_LastUpdated_By_SelectedTeamUser";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_LastUpdated_By_SelectedTeamUser;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@isSelectedTeamUser";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.isSelectedTeamUser;

                    //Budget files
                    if (_Selected_Project_Details.StudyBudgetFile != null)
                    {
                        DataTable dtBudget = new DataTable();
                        dtBudget.Columns.Add("i_Selected_Project_ID");
                        dtBudget.Columns.Add("s_Budget_Document_File");
                        dtBudget.Columns.Add("s_Budget_Comments");

                        foreach (Selected_Project_StudyBudgetFile budgetFile in _Selected_Project_Details.StudyBudgetFile)
                        {
                            dtBudget.Rows.Add(budgetFile.i_Selected_Project_ID, budgetFile.s_Budget_Document_File, budgetFile.s_Budget_Comments);
                        }
                        parameter.Add(_helper.CreateDbParameter());
                        parameter[parameter.Count - 1].ParameterName = "@Selected_Project_StudyBudgetFile";
                        parameter[parameter.Count - 1].Value = dtBudget;
                    }
                    //Budget files

                    //Common.TestLogError2("Before Storing SAE_ID value.....in selected project table...");
                    //SAE Details
                    if (_Selected_Project_Details.SAE_Details != null)
                    {
                        DataTable dtSAE = new DataTable();
                        dtSAE.Columns.Add("i_Project_ID");
                        dtSAE.Columns.Add("i_SAE_ID");
                        dtSAE.Columns.Add("Sel_SAEID");

                        foreach (Selected_SAE_Details SAE in _Selected_Project_Details.SAE_Details)
                        {
                            dtSAE.Rows.Add(SAE.i_Project_ID, SAE.i_SAE_ID,SAE.Sel_SAEID);
                        }

                        parameter.Add(_helper.CreateDbParameter());
                        parameter[parameter.Count - 1].ParameterName = "@Selected_SAE_Details";
                        parameter[parameter.Count - 1].Value = dtSAE;
                    }
                    //SAE Details
                   // Common.TestLogError2("After Storing SAE_ID value.....in selected project table...");

                    //CRA Details
                    if (_Selected_Project_Details.CRA_Details != null)
                    {
                        DataTable dtCRA = new DataTable();
                        dtCRA.Columns.Add("i_Project_ID");
                        dtCRA.Columns.Add("i_CRO_ID");
                        dtCRA.Columns.Add("i_CRA_ID");

                        foreach (Selected_CRA_Details CRA in _Selected_Project_Details.CRA_Details)
                        {
                            dtCRA.Rows.Add(CRA.i_Project_ID, CRA.i_CRO_ID, CRA.i_CRA_ID);
                        }

                        parameter.Add(_helper.CreateDbParameter());
                        parameter[parameter.Count - 1].ParameterName = "@Selected_CRA_Details";
                        parameter[parameter.Count - 1].Value = dtCRA;
                    }
                    //CRA Details

                    //Backup coordinators details
                    if (_Selected_Project_Details.BU_Details != null)
                    {
                        DataTable dtBackupCord = new DataTable();
                        dtBackupCord.Columns.Add("i_Selected_Project_ID");
                        dtBackupCord.Columns.Add("s_Blinded_UnBlinded");
                        dtBackupCord.Columns.Add("i_Cordinator_Id");
                        dtBackupCord.Columns.Add("s_Cordinator_name");

                        foreach (SelectedProject_BU_Details BU_Detail in _Selected_Project_Details.BU_Details)
                        {
                            dtBackupCord.Rows.Add(BU_Detail.i_Selected_Project_ID, BU_Detail.s_Blinded_UnBlinded, BU_Detail.s_Cordinator_Id, BU_Detail.s_Cordinator_name);
                        }
                        parameter.Add(_helper.CreateDbParameter());
                        parameter[parameter.Count - 1].ParameterName = "@SelectedProject_BU_Details";
                        parameter[parameter.Count - 1].Value = dtBackupCord;
                    }
                    //Backup coordinators details

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.Username;


                    //-------------------------
                    if (_Selected_Project_Details.Project_PIs != null)
                    {
                        DataTable dt2 = new DataTable();
                        dt2.Columns.Add("i_Project_ID");
                        dt2.Columns.Add("i_PI_ID");


                        foreach (Project_PI pi in _Selected_Project_Details.Project_PIs)
                        {
                            dt2.Rows.Add(pi.i_Project_ID, pi.i_PI_ID);
                        }
                        parameter.Add(_helper.CreateDbParameter());
                        parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
                        parameter[parameter.Count - 1].Value = dt2;
                    }

                    //Additional fiels
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Number_of_Boxes";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.i_Number_of_Boxes;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Date_Sent_for_Archiving";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.dt_Date_Sent_for_Archiving;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Amount";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Amount;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Agreement_Number";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_Agreement_Number;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_AgreementFile";
                    parameter[parameter.Count - 1].Value = _Selected_Project_Details.s_AgreementFile;

                    //End of Additional fiels

                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spSelectedProjectDML", parameter)))
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


        public static string newSAEBAL(newSAE_Master newSAE)
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
                parameter[parameter.Count - 1].ParameterName = "@UserCId";
                parameter[parameter.Count - 1].Value = newSAE.s_CreatedBy_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Sae_Name";
                parameter[parameter.Count - 1].Value = newSAE.newSae_Name;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;

                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spSAE_MasterDML", parameter)))
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
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



        public static string SAEBAL(SAE_Master SAE)
        {
            string result = "";
            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "Update";

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserCId";
                parameter[parameter.Count - 1].Value = SAE.s_ModifyBy_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_SAE_ID";
                parameter[parameter.Count - 1].Value = SAE.i_SAE_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@dt_SAEDateandTime";
                parameter[parameter.Count - 1].Value = SAE.dt_SAEDateandTime;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@b_SAE_Status";
                parameter[parameter.Count - 1].Value = SAE.b_SAE_Status;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_Patient_Studyno";
                parameter[parameter.Count - 1].Value = SAE.i_Patient_Studyno;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_Notification_Mode";
                parameter[parameter.Count - 1].Value = SAE.i_Notification_Mode;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@b_IsReadmission";
                parameter[parameter.Count - 1].Value = SAE.b_IsReadmission;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@dt_Readmission_date";
                parameter[parameter.Count - 1].Value = SAE.dt_Readmission_date;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@dt_Discharge_date";
                parameter[parameter.Count - 1].Value = SAE.dt_Discharge_date;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@dt_Knowledge_date";
                parameter[parameter.Count - 1].Value = SAE.dt_Knowledge_date;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spSAE_MasterDML", parameter)))
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
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




        public static string CRABAL(CRA_Master CRA)
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
                parameter[parameter.Count - 1].Value = CRA.s_Name;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_Email";
                parameter[parameter.Count - 1].Value = CRA.s_Email;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_phone_no";
                parameter[parameter.Count - 1].Value = CRA.s_phone_no;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserCId";
                parameter[parameter.Count - 1].Value = CRA.s_CreatedBy_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_CRO_ID";
                parameter[parameter.Count - 1].Value = CRA.i_CRO_ID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;

                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spCRA_MasterDML", parameter)))
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
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



        //public Selected_Clause_Details GetSelected_Clause_DetailsDetailsByID(int ID)
        //{
        //    Selected_Clause_Details _Selected_Clause_Details = new Selected_Clause_Details();
        //    {
        //        try
        //        {
        //            DataHelper _helper = new DataHelper();
        //            _helper.InitializedHelper();
        //            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "StatementType";
        //            parameter[parameter.Count - 1].Value = "GetProjectByID";
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //            parameter[parameter.Count - 1].Value = ID;
        //            DataTable ProjectsData = new DataTable();
        //            ProjectsData = _helper.GetData("UAT.spProjectSelect", parameter);
        //            foreach (DataRow dr in ProjectsData.Rows)
        //            {
        //                _Selected_Clause_Details = new Selected_Clause_Details()
        //                {


        //                    i_Contract_ID = (dr.IsNull("i_Contract_ID") ? 0 : Convert.ToInt32(dr["i_Contract_ID"])),
        //                    i_Contract_Clause_ID = (dr.IsNull("i_Contract_Clause_ID") ? 0 : Convert.ToInt32(dr["i_Contract_Clause_ID"])),
        //                    s_Status = (dr.IsNull("s_Status") ? string.Empty : Convert.ToString(dr["s_Status"])),
        //                    s_Comments = (dr.IsNull("s_Comments") ? string.Empty : Convert.ToString(dr["s_Comments"]))
        //                };
        //            }
        //        }
        //        catch (Exception e) { }
        //        return _Selected_Clause_Details;
        //    }
        //}
        ////--==========================================END of Class========================================================================
        //public Selected_CRA_Details GetSelected_CRA_DetailsDetailsByID(int ID)
        //{
        //    Selected_CRA_Details _Selected_CRA_Details = new Selected_CRA_Details();
        //    {
        //        try
        //        {
        //            DataHelper _helper = new DataHelper();
        //            _helper.InitializedHelper();
        //            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "StatementType";
        //            parameter[parameter.Count - 1].Value = "GetProjectByID";
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //            parameter[parameter.Count - 1].Value = ID;
        //            DataTable ProjectsData = new DataTable();
        //            ProjectsData = _helper.GetData("UAT.spProjectSelect", parameter);
        //            foreach (DataRow dr in ProjectsData.Rows)
        //            {
        //                _Selected_CRA_Details = new Selected_CRA_Details()
        //                {


        //                    i_Selected_ID = (dr.IsNull("i_Selected_ID") ? 0 : Convert.ToInt32(dr["i_Selected_ID"])),
        //                    i_CRO_ID = (dr.IsNull("i_CRO_ID") ? 0 : Convert.ToInt32(dr["i_CRO_ID"])),
        //                    i_CRA_ID = (dr.IsNull("i_CRA_ID") ? 0 : Convert.ToInt32(dr["i_CRA_ID"])),
        //                    s_description = (dr.IsNull("s_description") ? string.Empty : Convert.ToString(dr["s_description"]))
        //                };
        //            }
        //        }
        //        catch (Exception e) { }
        //        return _Selected_CRA_Details;
        //    }
        //}
        ////--==========================================END of Class========================================================================

        ////--==========================================END of Class========================================================================
        //public Selected_Project_StudyBudgetFile GetSelected_Project_StudyBudgetFileDetailsByID(int ID)
        //{
        //    Selected_Project_StudyBudgetFile _Selected_Project_StudyBudgetFile = new Selected_Project_StudyBudgetFile();
        //    {
        //        try
        //        {
        //            DataHelper _helper = new DataHelper();
        //            _helper.InitializedHelper();
        //            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "StatementType";
        //            parameter[parameter.Count - 1].Value = "GetProjectByID";
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //            parameter[parameter.Count - 1].Value = ID;
        //            DataTable ProjectsData = new DataTable();
        //            ProjectsData = _helper.GetData("UAT.spProjectSelect", parameter);
        //            foreach (DataRow dr in ProjectsData.Rows)
        //            {
        //                _Selected_Project_StudyBudgetFile = new Selected_Project_StudyBudgetFile()
        //                {


        //                    i_Selected_Project_Details_ID = (dr.IsNull("i_Selected_Project_Details_ID") ? 0 : Convert.ToInt32(dr["i_Selected_Project_Details_ID"])),
        //                    s_Budget_Document_File = (dr.IsNull("s_Budget_Document_File") ? string.Empty : Convert.ToString(dr["s_Budget_Document_File"])),
        //                    s_Budget_Comments = (dr.IsNull("s_Budget_Comments") ? string.Empty : Convert.ToString(dr["s_Budget_Comments"]))
        //                };
        //            }
        //        }
        //        catch (Exception e) { }
        //        return _Selected_Project_StudyBudgetFile;
        //    }
        //}
        ////--==========================================END of Class========================================================================
        //public SelectedProject_BU_Details GetSelectedProject_BU_DetailsDetailsByID(int ID)
        //{
        //    SelectedProject_BU_Details _SelectedProject_BU_Details = new SelectedProject_BU_Details();
        //    {
        //        try
        //        {
        //            DataHelper _helper = new DataHelper();
        //            _helper.InitializedHelper();
        //            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "StatementType";
        //            parameter[parameter.Count - 1].Value = "GetProjectByID";
        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //            parameter[parameter.Count - 1].Value = ID;
        //            DataTable ProjectsData = new DataTable();
        //            ProjectsData = _helper.GetData("UAT.spProjectSelect", parameter);
        //            foreach (DataRow dr in ProjectsData.Rows)
        //            {
        //                _SelectedProject_BU_Details = new SelectedProject_BU_Details()
        //                {


        //                    i_Selected_Project_ID = (dr.IsNull("i_Selected_Project_ID") ? 0 : Convert.ToInt32(dr["i_Selected_Project_ID"])),
        //                    s_Blinded_UnBlinded = (dr.IsNull("s_Blinded_UnBlinded") ? string.Empty : Convert.ToString(dr["s_Blinded_UnBlinded"])),
        //                    i_Cordinator_Id = (dr.IsNull("i_Cordinator_Id") ? 0 : Convert.ToInt32(dr["i_Cordinator_Id"])),
        //                    s_Cordinator_name = (dr.IsNull("s_Cordinator_name") ? string.Empty : Convert.ToString(dr["s_Cordinator_name"]))
        //                };
        //            }
        //        }
        //        catch (Exception e) { }
        //        return _SelectedProject_BU_Details;
        //    }
        //}

        ////--==========================================END of Class========================================================================
        //public string Selected_Clause_Details(Selected_Clause_Details _Selected_Clause_Details, Mode mode)
        //{
        //    string result = "";
        //    try
        //    {
        //        DataHelper _helper = new DataHelper();
        //        _helper.InitializedHelper();
        //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "StatementType";
        //        parameter[parameter.Count - 1].Value = mode.ToString();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //        parameter[parameter.Count - 1].Value = _Selected_Clause_Details.i_Contract_ID;
        //        if (mode.ToString() != "Delete")
        //        {

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_Contract_ID ";
        //            parameter[parameter.Count - 1].Value = _Selected_Clause_Details.i_Contract_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_Contract_Clause_ID ";
        //            parameter[parameter.Count - 1].Value = _Selected_Clause_Details.i_Contract_Clause_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_Status ";
        //            parameter[parameter.Count - 1].Value = _Selected_Clause_Details.s_Status;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_Comments ";
        //            parameter[parameter.Count - 1].Value = _Selected_Clause_Details.s_Comments;


        //        }
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
        //        parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
        //        parameter[parameter.Count - 1].Size = 500;
        //        if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
        //        {
        //            result = "Success";
        //        }
        //        else
        //        {
        //            result = parameter[parameter.Count - 1].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return result;
        //}
        ////--================================================END OF Properties===============================================================================
        //public string Selected_CRA_Details(Selected_CRA_Details _Selected_CRA_Details, Mode mode)
        //{
        //    string result = "";
        //    try
        //    {
        //        DataHelper _helper = new DataHelper();
        //        _helper.InitializedHelper();
        //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "StatementType";
        //        parameter[parameter.Count - 1].Value = mode.ToString();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //        parameter[parameter.Count - 1].Value = _Selected_CRA_Details.i_Selected_ID;
        //        if (mode.ToString() != "Delete")
        //        {

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_Selected_ID ";
        //            parameter[parameter.Count - 1].Value = _Selected_CRA_Details.i_Selected_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_CRO_ID ";
        //            parameter[parameter.Count - 1].Value = _Selected_CRA_Details.i_CRO_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_CRA_ID ";
        //            parameter[parameter.Count - 1].Value = _Selected_CRA_Details.i_CRA_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_description ";
        //            parameter[parameter.Count - 1].Value = _Selected_CRA_Details.s_description;


        //        }
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
        //        parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
        //        parameter[parameter.Count - 1].Size = 500;
        //        if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
        //        {
        //            result = "Success";
        //        }
        //        else
        //        {
        //            result = parameter[parameter.Count - 1].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return result;
        //}
        ////--================================================END OF Properties===============================================================================

        ////--================================================END OF Properties===============================================================================
        //public string Selected_Project_StudyBudgetFile(Selected_Project_StudyBudgetFile _Selected_Project_StudyBudgetFile, Mode mode)
        //{
        //    string result = "";
        //    try
        //    {
        //        DataHelper _helper = new DataHelper();
        //        _helper.InitializedHelper();
        //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "StatementType";
        //        parameter[parameter.Count - 1].Value = mode.ToString();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //        parameter[parameter.Count - 1].Value = _Selected_Project_StudyBudgetFile.i_Selected_Project_Details_ID;
        //        if (mode.ToString() != "Delete")
        //        {

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_Selected_Project_Details_ID ";
        //            parameter[parameter.Count - 1].Value = _Selected_Project_StudyBudgetFile.i_Selected_Project_Details_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_Budget_Document_File ";
        //            parameter[parameter.Count - 1].Value = _Selected_Project_StudyBudgetFile.s_Budget_Document_File;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_Budget_Comments ";
        //            parameter[parameter.Count - 1].Value = _Selected_Project_StudyBudgetFile.s_Budget_Comments;


        //        }
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
        //        parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
        //        parameter[parameter.Count - 1].Size = 500;
        //        if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
        //        {
        //            result = "Success";
        //        }
        //        else
        //        {
        //            result = parameter[parameter.Count - 1].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return result;
        //}
        ////--================================================END OF Properties===============================================================================
        //public string SelectedProject_BU_Details(SelectedProject_BU_Details _SelectedProject_BU_Details, Mode mode)
        //{
        //    string result = "";
        //    try
        //    {
        //        DataHelper _helper = new DataHelper();
        //        _helper.InitializedHelper();
        //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "StatementType";
        //        parameter[parameter.Count - 1].Value = mode.ToString();
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
        //        parameter[parameter.Count - 1].Value = _SelectedProject_BU_Details.i_Selected_Project_ID;
        //        if (mode.ToString() != "Delete")
        //        {

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_Selected_Project_ID ";
        //            parameter[parameter.Count - 1].Value = _SelectedProject_BU_Details.i_Selected_Project_ID;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_Blinded_UnBlinded ";
        //            parameter[parameter.Count - 1].Value = _SelectedProject_BU_Details.s_Blinded_UnBlinded;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@i_Cordinator_Id ";
        //            parameter[parameter.Count - 1].Value = _SelectedProject_BU_Details.i_Cordinator_Id;

        //            parameter.Add(_helper.CreateDbParameter());
        //            parameter[parameter.Count - 1].ParameterName = "@s_Cordinator_name ";
        //            parameter[parameter.Count - 1].Value = _SelectedProject_BU_Details.s_Cordinator_name;


        //        }
        //        parameter.Add(_helper.CreateDbParameter());
        //        parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
        //        parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
        //        parameter[parameter.Count - 1].Size = 500;
        //        if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
        //        {
        //            result = "Success";
        //        }
        //        else
        //        {
        //            result = parameter[parameter.Count - 1].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return result;
        //}
        ////--================================================END OF Properties===============================================================================



    }
}
