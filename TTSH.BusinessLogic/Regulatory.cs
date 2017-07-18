using System;
using System.Collections.Generic;
using System.Data;
using TTSH.DataAccess;
using TTSH.Entity;
using System.Xml;
using System.IO;

namespace TTSH.BusinessLogic
{
    public class Regulatory
    {
        public static List<Regulatory_Master> FillGridRegulatoryMain()
        {
            List<Regulatory_Master> Rm = new List<Regulatory_Master>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spRegulatory_MasterDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    Rm.Add(new Regulatory_Master
                    {
                        i_ID = (dr["RegId"] != DBNull.Value) ? Convert.ToInt32(dr["RegId"]) : 0,
                        i_Project_ID = (dr["i_Project_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_Project_ID"]) : 0,
                        s_Display_Project_ID = (dr["s_Display_Project_ID"] != DBNull.Value) ? Convert.ToString(dr["s_Display_Project_ID"]) : "",
                        s_Project_Title = (dr["s_Project_Title"] != DBNull.Value) ? Convert.ToString(dr["s_Project_Title"]) : "",
                        Project_Category = (dr["Project_Category"] != DBNull.Value) ? Convert.ToString(dr["Project_Category"]) : "",
                        s_IRB_No = (dr["s_IRB_No"] != DBNull.Value) ? Convert.ToString(dr["s_IRB_No"]) : "",
                        Project_Type = (dr["Project_Type"] != DBNull.Value) ? Convert.ToString(dr["Project_Type"]) : "",
                        PI_NAME = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
                        CTC_Status = (dr["CTC_status"] != DBNull.Value) ? Convert.ToString(dr["CTC_status"]) : "",
                        CTCCount = (dr["CTCCount"] != DBNull.Value) ? Convert.ToInt32(dr["CTCCount"]) : 0,
                        Project_Status_Disable = (dr["Project_Status"] != DBNull.Value) ? Convert.ToString(dr["Project_Status"]) : "",
                    });
                }
            }
            catch (Exception)
            {


            }

            return Rm;
        }


        public static RegulatoryNewProjectEntry GetNewProjectEntry(int ID)
        {
            RegulatoryNewProjectEntry pdclist = new RegulatoryNewProjectEntry();
            DataHelper _helper = new DataHelper();
            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            DataTable ProjectsData = new DataTable();
            List<PI_Master> piList = new List<PI_Master>();
            List<Project_Master> pjmasterlist = new List<Project_Master>();

            try
            {

                _helper.InitializedHelper();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "ByProjectId";
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_ID";
                parameter[parameter.Count - 1].Value = ID;
                ProjectsData = _helper.GetData("dbo.[spRegulatory_MasterDML]", parameter);
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

                                pjmasterlist.Add(pmMaster);
                            }
                        }

                    }

                }
                pdclist = new RegulatoryNewProjectEntry()
                {

                    Pilisst = piList,
                    pmlist = pjmasterlist


                };

            }
            catch (Exception)
            {

                throw;
            }
            return pdclist;
        }

        public static List<Regulatory_Master> FillGridRegulatoryDetailsByID(int ID)
        {
            List<Regulatory_Master> RMList = new List<Regulatory_Master>();
            List<Project_Master> pmlist = new List<Project_Master>();
            List<PI_Master> Pilisst = new List<PI_Master>();
            List<RegulatorySixMonthUpdate> RegSixMUpdateList = new List<RegulatorySixMonthUpdate>();
            List<Regulatory_StudyTeam> RegStudyTeamList = new List<Regulatory_StudyTeam>();
            List<Regulatory_Submission_Status> RegSubStatusList = new List<Regulatory_Submission_Status>();
            List<Regulatory_ICF_Details> RegICFDetails = new List<Regulatory_ICF_Details>();
            List<Regulatory_Ammendments_Details> RegAmendDetails = new List<Regulatory_Ammendments_Details>();
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
                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spRegulatory_MasterDML]", parameter);
                if (gridData != null)
                {
                    if (gridData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in gridData.Rows)
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
                                        Pilisst.Add(pi);
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

                                        pmlist.Add(pmMaster);
                                    }
                                }

                            }
                        }




                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RMList;
        }

        public static string Regulatory_Master_DML(Regulatory_Master _Regulatory_Master,
            List<Regulatory_StudyTeam> lstRegulatory_StudyTeam,
            List<Regulatory_ICF_Details> lstRegulatory_ICF_Details,
            List<Regulatory_Submission_Status> lstRegulatory_Submission_Status,
            List<Regulatory_Ammendments_Details> lstRegulatory_Ammendments_Details,
            List<RegulatoryIPManagement> lstRegulatoryIPManagement,
            string mode)
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
                parameter[parameter.Count - 1].Value = _Regulatory_Master.i_ID;
                if (mode.ToString() != "Delete")
                {



                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Sponsor_ID";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Sponsor_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Other_Sponsor";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_Other_Sponsor;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Prism_AppStatus";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.b_Prism_AppStatus;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Prism_AppNo";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_Prism_AppNo;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Prism_AppDate";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.dt_Prism_AppDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_CTC_status_ID";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_CTC_status_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_CTC_Document";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_CTC_Document;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_CTC_EmailDocument";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_CTC_EmailDocument;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_CTC_ApprDate";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.dt_CTC_ApprDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_CTC_No";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_CTC_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_CTC_ExpiryDate";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.dt_CTC_ExpiryDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_NewExt_Appr_Date";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.dt_NewExt_Appr_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_NewExpiry_Date";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.dt_NewExpiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_NewCTCEmailApprDoc";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_NewCTCEmailApprDoc;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_ExtCTCEmailApprDoc";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_ExtCTCEmailApprDoc;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Protocol_No";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_Protocol_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Protocol_Ver_No";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_Protocol_Ver_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Protocol_Date";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.dt_Protocol_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_RecruitedBy_TTSH";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_RecruitedBy_TTSH;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Remarks";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_Remarks;



                    //----------Added by Atul
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.UName;

                    //----------END by Atul


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@DT_LASTUPDATED_DATE";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.DT_LASTUPDATED_DATE;



                    //--------------------Six Month Update--------------------
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Pending_Screen_Outcome";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Pending_Screen_Outcome;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Screen_Failure";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Screen_Failure;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Screened";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Screened;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Randomized";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Randomized;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Withdrawn";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Withdrawn;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Withdrawn_Reason";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_Withdrawn_Reason;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Ongoing_Patient";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Ongoing_Patient;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Completed_No";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_Completed_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_SAE_No";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.i_SAE_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_SAE_Reason";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.s_SAE_Reason;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Internal_Audit";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.b_Internal_Audit;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@RegSIxMId";
                    parameter[parameter.Count - 1].Value = _Regulatory_Master.RegSIxMId;

                    //----------------------------END----------------------------------------------



                    //------------------Sub Tables -------------------------
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Regulatory_StudyTeam";
                    parameter[parameter.Count - 1].Value = lstRegulatory_StudyTeam.ListToDatatable();

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Regulatory_ICF_Details";
                    parameter[parameter.Count - 1].Value = lstRegulatory_ICF_Details.ListToDatatable();

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Regulatory_Submission_Status";
                    parameter[parameter.Count - 1].Value = lstRegulatory_Submission_Status.ListToDatatable().getColumns(5);

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Regulatory_Ammendments_Details";
                    parameter[parameter.Count - 1].Value = lstRegulatory_Ammendments_Details.ListToDatatable().getColumns(2);

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@RegulatoryIPManagement";
                    parameter[parameter.Count - 1].Value = lstRegulatoryIPManagement.ListToDatatable().getColumns(3);

                    //-----------------END--------------------------------------------


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spRegulatory_MasterDML", parameter)))
                {
                    result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }


        public static Regulatory_Master GetRegulatory_MasterDetailsByID(int ID)
        {
            Regulatory_Master _Regulatory_Master = new Regulatory_Master();
            List<Regulatory_StudyTeam> lstRegulatory_StudyTeam = new List<Regulatory_StudyTeam>();
            List<Regulatory_ICF_Details> lstRegulatory_ICF_Details = new List<Regulatory_ICF_Details>();
            List<Regulatory_Submission_Status> lstRegulatory_Submission_Status = new List<Regulatory_Submission_Status>();
            List<Regulatory_Ammendments_Details> lstRegulatory_Ammendments_Details = new List<Regulatory_Ammendments_Details>();
            List<RegulatoryIPManagement> lstRegulatoryIPManagement = new List<RegulatoryIPManagement>();
            List<RegulatorySixMonthUpdate> lstRegulatorySixMonthUpdate = new List<RegulatorySixMonthUpdate>();
            List<PI_Master> piList = new List<PI_Master>();
            List<Project_Master> pjmasterlist = new List<Project_Master>();
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
                    DataTable ProjectsData = new DataTable();
                    ProjectsData = _helper.GetData("dbo.spRegulatory_MasterDML", parameter);
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

                                    pjmasterlist.Add(pmMaster);
                                }
                            }

                        }
                        string ICFstr = (dr.IsNull("ICF")) == true ? "" : Convert.ToString(dr["ICF"]);
                        if (ICFstr != string.Empty)
                        {

                            using (XmlReader reader = XmlReader.Create(new StringReader(ICFstr)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("ICF/ICF_D");

                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Regulatory_ICF_Details RegICF = new Regulatory_ICF_Details();
                                    if (node["s_Version_No"] != null)
                                        RegICF.s_Version_No = Convert.ToString(node["s_Version_No"].InnerText);
                                    if (node["dt_ICF_Date"] != null)
                                        RegICF.dt_ICF_Date = Convert.ToString(node["dt_ICF_Date"].InnerText);
                                    lstRegulatory_ICF_Details.Add(RegICF);
                                }
                            }

                        }
                        string SUBMISSION_STATUSFstr = (dr.IsNull("SUBMISSION_STATUS")) == true ? "" : Convert.ToString(dr["SUBMISSION_STATUS"]);
                        if (SUBMISSION_STATUSFstr != string.Empty)
                        {

                            using (XmlReader reader = XmlReader.Create(new StringReader(SUBMISSION_STATUSFstr)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("SUBMISSION/SUB_STATUS");

                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Regulatory_Submission_Status RegSubStatus = new Regulatory_Submission_Status();
                                    if (node["i_Interval_ID"] != null)
                                        RegSubStatus.i_Interval_ID = Convert.ToInt32(node["i_Interval_ID"].InnerText);
                                    if (node["dt_Submission_Date"] != null)
                                        RegSubStatus.dt_Submission_Date = Convert.ToString(node["dt_Submission_Date"].InnerText);
                                    if (node["s_File_Title"] != null)
                                        RegSubStatus.s_File_Title = Convert.ToString(node["s_File_Title"].InnerText);
                                    if (node["s_Uploaded_File"] != null)
                                        RegSubStatus.s_Uploaded_File = Convert.ToString(node["s_Uploaded_File"].InnerText);
                                    if (node["dt_FileUploaded_Date"] != null)
                                        RegSubStatus.dt_FileUploaded_Date = Convert.ToString(node["dt_FileUploaded_Date"].InnerText);
                                    if (node["UpFileName"] != null)
                                        RegSubStatus.UpFileName = Convert.ToString(node["UpFileName"].InnerText);
                                    if (node["ReportName"] != null)
                                        RegSubStatus.ReportName = Convert.ToString(node["ReportName"].InnerText);
                                    lstRegulatory_Submission_Status.Add(RegSubStatus);
                                }
                            }

                        }
                        string STUDY_TEAMstr = (dr.IsNull("STUDY_TEAM")) == true ? "" : Convert.ToString(dr["STUDY_TEAM"]);
                        if (STUDY_TEAMstr != string.Empty)
                        {

                            using (XmlReader reader = XmlReader.Create(new StringReader(STUDY_TEAMstr)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("STUDY_TEAM/S_TEAM");

                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Regulatory_StudyTeam RegStudyTeam = new Regulatory_StudyTeam();
                                    if (node["s_First_Name"] != null)
                                        RegStudyTeam.s_First_Name = Convert.ToString(node["s_First_Name"].InnerText);
                                    if (node["s_Last_Name"] != null)
                                        RegStudyTeam.s_Last_Name = Convert.ToString(node["s_Last_Name"].InnerText);
                                    if (node["s_Email_ID"] != null)
                                        RegStudyTeam.s_Email_ID = Convert.ToString(node["s_Email_ID"].InnerText);

                                    lstRegulatory_StudyTeam.Add(RegStudyTeam);
                                }
                            }

                        }
                        string AMMENDMENTSstr = (dr.IsNull("AMMENDMENTS")) == true ? "" : Convert.ToString(dr["AMMENDMENTS"]);
                        if (AMMENDMENTSstr != string.Empty)
                        {

                            using (XmlReader reader = XmlReader.Create(new StringReader(AMMENDMENTSstr)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("AMMENDMENTS/AMMENDMENTS_DETAILS");

                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Regulatory_Ammendments_Details RegAmend = new Regulatory_Ammendments_Details();
                                    if (node["s_Uploaded_File"] != null)
                                        RegAmend.s_Uploaded_File = Convert.ToString(node["s_Uploaded_File"].InnerText);
                                    if (node["dt_Submission_Date"] != null)
                                        RegAmend.dt_Submission_Date = Convert.ToString(node["dt_Submission_Date"].InnerText);
                                    if (node["Uploaded_File"] != null)
                                        RegAmend.Uploaded_File = Convert.ToString(node["Uploaded_File"].InnerText);

                                    lstRegulatory_Ammendments_Details.Add(RegAmend);
                                }
                            }

                        }
                        string SIXMONTHDATAstr = (dr.IsNull("SIXMONTHDATA")) == true ? "" : Convert.ToString(dr["SIXMONTHDATA"]);
                        if (SIXMONTHDATAstr != string.Empty)
                        {

                            using (XmlReader reader = XmlReader.Create(new StringReader(SIXMONTHDATAstr)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("DATA/SIXMONTHDATA");

                                foreach (XmlNode node in xmlNodeList)
                                {
                                    RegulatorySixMonthUpdate RegSixM = new RegulatorySixMonthUpdate();
                                    if (node["s_SixmonthName"] != null)
                                        RegSixM.s_SixmonthName = Convert.ToString(node["s_SixmonthName"].InnerText);
                                    if (node["i_Pending_Screen_Outcome"] != null)
                                        RegSixM.i_Pending_Screen_Outcome = Convert.ToInt32(node["i_Pending_Screen_Outcome"].InnerText);
                                    if (node["i_Screen_Failure"] != null)
                                        RegSixM.i_Screen_Failure = Convert.ToInt32(node["i_Screen_Failure"].InnerText);
                                    if (node["i_Screened"] != null)
                                        RegSixM.i_Screened = Convert.ToInt32(node["i_Screened"].InnerText);
                                    if (node["i_Randomized"] != null)
                                        RegSixM.i_Randomized = Convert.ToInt32(node["i_Randomized"].InnerText);
                                    if (node["i_Withdrawn"] != null)
                                        RegSixM.i_Withdrawn = Convert.ToInt32(node["i_Withdrawn"].InnerText);
                                    if (node["s_Withdrawn_Reason"] != null)
                                        RegSixM.s_Withdrawn_Reason = Convert.ToString(node["s_Withdrawn_Reason"].InnerText);
                                    if (node["i_Ongoing_Patient"] != null)
                                        RegSixM.i_Ongoing_Patient = Convert.ToInt32(node["i_Ongoing_Patient"].InnerText);
                                    if (node["i_Completed_No"] != null)
                                        RegSixM.i_Completed_No = Convert.ToInt32(node["i_Completed_No"].InnerText);
                                    if (node["i_SAE_No"] != null)
                                        RegSixM.i_SAE_No = Convert.ToInt32(node["i_SAE_No"].InnerText);
                                    if (node["s_SAE_Reason"] != null)
                                        RegSixM.s_SAE_Reason = Convert.ToString(node["s_SAE_Reason"].InnerText);
                                    if (node["b_Internal_Audit"] != null)
                                        RegSixM.b_Internal_Audit = node["b_Internal_Audit"].InnerText == "0" ? false : true;
                                    if (node["dt_LastUpdated_date"] != null)
                                        RegSixM.dt_LastUpdated_date = Convert.ToString(node["dt_LastUpdated_date"].InnerText);
                                    if (node["NoOfMonths"] != null)
                                    {
                                        RegSixM.NoOfMonths = Convert.ToInt32(node["NoOfMonths"].InnerText);
                                    }
                                    if (node["i_ID"] != null)
                                    {
                                        RegSixM.RegSIxMId = Convert.ToInt32(node["i_ID"].InnerText);
                                    }
                                    if (node["SortDate"] != null)
                                    {
                                        RegSixM.SortDate = Convert.ToDateTime(node["SortDate"].InnerText);
                                    }
                                    if (node["rnum"]!=null)
                                    {
                                        RegSixM.rnum = Convert.ToInt32(node["rnum"].InnerText);
                                    }
                                    lstRegulatorySixMonthUpdate.Add(RegSixM);
                                }
                            }

                        }
                        string IPMGMTstr = (dr.IsNull("IPMGMT")) == true ? "" : Convert.ToString(dr["IPMGMT"]);
                        if (IPMGMTstr != string.Empty)
                        {

                            using (XmlReader reader = XmlReader.Create(new StringReader(IPMGMTstr)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("IP/IP_D");

                                foreach (XmlNode node in xmlNodeList)
                                {
                                    RegulatoryIPManagement RegIP = new RegulatoryIPManagement();
                                    if (node["s_Investigational_Product"] != null)
                                        RegIP.s_Investigational_Product = Convert.ToString(node["s_Investigational_Product"].InnerText);
                                    if (node["s_IPManagement"] != null)
                                        RegIP.s_IPManagement = Convert.ToInt32(node["s_IPManagement"].InnerText);
                                    if (node["s_StorageLocation"] != null)
                                        RegIP.s_StorageLocation = Convert.ToString(node["s_StorageLocation"].InnerText);
                                    if (node["s_IPName"] != null)
                                        RegIP.s_IPName = Convert.ToString(node["s_IPName"].InnerText);

                                    lstRegulatoryIPManagement.Add(RegIP);
                                }
                            }

                        }
                        _Regulatory_Master = new Regulatory_Master()
                        {

                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            i_Sponsor_ID = (dr.IsNull("i_Sponsor_ID") ? 0 : Convert.ToInt32(dr["i_Sponsor_ID"])),
                            s_Other_Sponsor = (dr.IsNull("s_Other_Sponsor") ? string.Empty : Convert.ToString(dr["s_Other_Sponsor"])),
                            b_Prism_AppStatus = (dr.IsNull("b_Prism_AppStatus") ? false : Convert.ToBoolean(dr["b_Prism_AppStatus"])),
                            s_Prism_AppNo = (dr.IsNull("s_Prism_AppNo") ? string.Empty : Convert.ToString(dr["s_Prism_AppNo"])),
                            dt_Prism_AppDate = (dr.IsNull("dt_Prism_AppDate") ? "" : Convert.ToString(dr["dt_Prism_AppDate"])),
                            i_CTC_status_ID = (dr.IsNull("i_CTC_status_ID") ? 0 : Convert.ToInt32(dr["i_CTC_status_ID"])),
                            s_CTC_Document = (dr.IsNull("s_CTC_Document") ? string.Empty : Convert.ToString(dr["s_CTC_Document"])),
                            s_CTC_EmailDocument = (dr.IsNull("s_CTC_EmailDocument") ? string.Empty : Convert.ToString(dr["s_CTC_EmailDocument"])),
                            dt_CTC_ApprDate = (dr.IsNull("dt_CTC_ApprDate") ? "" : Convert.ToString(dr["dt_CTC_ApprDate"])),
                            s_CTC_No = (dr.IsNull("s_CTC_No") ? string.Empty : Convert.ToString(dr["s_CTC_No"])),
                            dt_CTC_ExpiryDate = (dr.IsNull("dt_CTC_ExpiryDate") ? "" : Convert.ToString(dr["dt_CTC_ExpiryDate"])),
                            dt_NewExt_Appr_Date = (dr.IsNull("dt_NewExt_Appr_Date") ? "" : Convert.ToString(dr["dt_NewExt_Appr_Date"])),
                            dt_NewExpiry_Date = (dr.IsNull("dt_NewExpiry_Date") ? "" : Convert.ToString(dr["dt_NewExpiry_Date"])),
                            s_NewCTCEmailApprDoc = (dr.IsNull("s_NewCTCEmailApprDoc") ? "" : Convert.ToString(dr["s_NewCTCEmailApprDoc"])),
                            s_ExtCTCEmailApprDoc = (dr.IsNull("s_ExtCTCEmailApprDoc") ? "" : Convert.ToString(dr["s_ExtCTCEmailApprDoc"])),
                            s_Protocol_No = (dr.IsNull("s_Protocol_No") ? string.Empty : Convert.ToString(dr["s_Protocol_No"])),
                            s_Protocol_Ver_No = (dr.IsNull("s_Protocol_Ver_No") ? string.Empty : Convert.ToString(dr["s_Protocol_Ver_No"])),
                            dt_Protocol_Date = (dr.IsNull("dt_Protocol_Date") ? "" : Convert.ToString(dr["dt_Protocol_Date"])),
                            s_RecruitedBy_TTSH = (dr.IsNull("s_RecruitedBy_TTSH") ? string.Empty : Convert.ToString(dr["s_RecruitedBy_TTSH"])),
                            s_Remarks = (dr.IsNull("s_Remarks") ? string.Empty : Convert.ToString(dr["s_Remarks"])),

                            //s_CreatedBy_ID = (dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"])),
                            //s_ModifyBy_ID = (dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"])),
                            //dt_Created_Date = (dr.IsNull("dt_Created_Date") ? "" : Convert.ToString(dr["dt_Created_Date"])),
                            //dt_Modify_Date = (dr.IsNull("dt_Modify_Date") ? "" : Convert.ToString(dr["dt_Modify_Date"])),
                            //s_CreatedBy_Name = (dr.IsNull("s_CreatedBy_Name") ? string.Empty : Convert.ToString(dr["s_CreatedBy_Name"])),


                            RegSixMUpdateList = lstRegulatorySixMonthUpdate,
                            RegICFDetails = lstRegulatory_ICF_Details,
                            RegAmendDetails = lstRegulatory_Ammendments_Details,
                            RegStudyTeamList = lstRegulatory_StudyTeam,
                            RegSubStatusList = lstRegulatory_Submission_Status,
                            RegIPList = lstRegulatoryIPManagement,
                            pmlist = pjmasterlist,
                            Pilisst = piList

                        };
                    }
                }
                catch (Exception e) { }
                return _Regulatory_Master;
            }
        }



    }
}
