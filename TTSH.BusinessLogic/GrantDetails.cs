using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using TTSH.DataAccess;
using TTSH.Entity;
namespace TTSH.BusinessLogic
{
    public class GrantDetails
    {
        #region " Fill Main Grid "
        public static List<Grant_Details> FillGrantDetailGrid()
        {
            List<Grant_Details> Gd = new List<Grant_Details>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spGrant_DetailsDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    Gd.Add(new Grant_Details
                    {
                        GD_ID = (dr["GD_ID"] != DBNull.Value) ? Convert.ToInt32(dr["GD_ID"]) : 0,
                        GM_ID = (dr["GM_ID"] != DBNull.Value) ? Convert.ToInt32(dr["GM_ID"]) : 0,
                        i_Project_ID = (dr["i_Project_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_Project_ID"]) : 0,
                        s_Display_Project_ID = (dr["s_Display_Project_ID"] != DBNull.Value) ? Convert.ToString(dr["s_Display_Project_ID"]) : "",
                        s_Project_Title = (dr["s_Project_Title"] != DBNull.Value) ? Convert.ToString(dr["s_Project_Title"]) : "",
                        Project_Category_Name = (dr["Project_Category"] != DBNull.Value) ? Convert.ToString(dr["Project_Category"]) : "",
                        s_IRB_No = (dr["s_IRB_No"] != DBNull.Value) ? Convert.ToString(dr["s_IRB_No"]) : "",
                        PI_Name = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
                        GrantDetailStatus = (dr["GrantDetailStatus"] != DBNull.Value) ? Convert.ToString(dr["GrantDetailStatus"]) : "",
                        Project_Status_Disable = (dr["Project_Status"] != DBNull.Value) ? Convert.ToString(dr["Project_Status"]) : "",

                    });
                }
            }
            catch (Exception)
            {


            }

            return Gd;
        }
        #endregion

        #region " New Project Entry "
        public static GrantNewProjectEntry FillGrantDetailNewProject(int ID)
        {
            GrantNewProjectEntry GNP = new GrantNewProjectEntry();
            DataHelper _helper = new DataHelper();
            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            DataTable ProjectsData = new DataTable();
            List<PI_Master> piList = new List<PI_Master>();
            List<Project_Master> pjList = new List<Project_Master>();
            List<Grant_Master> gmList = new List<Grant_Master>();
            List<Project_Master> childpmlist = new List<Project_Master>();
            List<PI_Master> childpilist = new List<PI_Master>();
            try
            {
                _helper.InitializedHelper();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "ByProjectId";
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                parameter[parameter.Count - 1].Value = ID;
                ProjectsData = _helper.GetData("dbo.[spGrant_DetailsDML]", parameter);

                foreach (DataRow dr in ProjectsData.Rows)
                {
                    #region " Dept Master "
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
                    #endregion

                    #region "Child Dept Master "
                    string ChildDept = (dr.IsNull("CHILD_PI_DETAILS")) == true ? "" : Convert.ToString(dr["CHILD_PI_DETAILS"]);
                    if (ChildDept != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(ChildDept)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
                            foreach (XmlNode node in xmlNodeList)
                            {
                                PI_Master pi = new PI_Master();

                                if (node["i_ID"] != null)
                                    pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);

                                if (node["s_PI_Name"] != null)
                                    pi.s_PIName = (node["s_PI_Name"].InnerText);

                                if (node["i_Project_ID"] != null)
                                {
                                    pi.i_Project_ID = Convert.ToInt32(node["i_Project_ID"].InnerText);
                                }
                                childpilist.Add(pi);
                            }
                        }

                    }
                    #endregion

                    #region " Project Master "
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
                                if (node["s_Research_IO"] != null)
                                    pmMaster.s_Research_IO = Convert.ToString(node["s_Research_IO"].InnerText);
                                pjList.Add(pmMaster);
                            }
                        }

                    }
                    #endregion

                    #region " Child Project "
                    string ChildProjList = (dr.IsNull("CHILD_PROJECTGRID_DATA")) == true ? "" : Convert.ToString(dr["CHILD_PROJECTGRID_DATA"]);
                    if (ChildProjList != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(ChildProjList)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("CHILD_PROJECT_DATA/CHILD_PROJECT");

                            foreach (XmlNode node in xmlNodeList)
                            {
                                Project_Master pmMaster = new Project_Master();
                                if (node["i_Project_ID"] != null)
                                    pmMaster.S_ProjectStatus = Convert.ToString(node["i_Project_ID"].InnerText);
                                if (node["s_Project_Title"] != null)
                                    pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                                if (node["s_Display_Project_ID"] != null)
                                    pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                                if (node["PI_NAME"] != null)
                                    pmMaster.PI_NAME = Convert.ToString(node["PI_NAME"].InnerText);
                                childpmlist.Add(pmMaster);
                            }
                        }

                    }
                    #endregion

                    #region " Grant Master "
                    string GrantMList = (dr.IsNull("GRANT_MASTER_DATA")) == true ? "" : Convert.ToString(dr["GRANT_MASTER_DATA"]);
                    if (GrantMList != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(GrantMList)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("GRANT_MASTER_DATA/GRANT_MASTER");

                            foreach (XmlNode node in xmlNodeList)
                            {
                                Grant_Master GMaster = new Grant_Master();
                                if (node["i_ID"] != null)
                                    GMaster.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
                                if (node["GRANT_TYPE"] != null)
                                    GMaster.GRANT_TYPE = Convert.ToString(node["GRANT_TYPE"].InnerText);
                                if (node["GRANT_SUB_TYPE1"] != null)
                                    GMaster.GRANT_SUB_TYPE1 = Convert.ToString(node["GRANT_SUB_TYPE1"].InnerText);
                                if (node["GRANT_SUB_TYPE2"] != null)
                                    GMaster.GRANT_SUB_TYPE2 = Convert.ToString(node["GRANT_SUB_TYPE2"].InnerText);
                                if (node["GRANT_SUB_TYPE3"] != null)
                                    GMaster.GRANT_SUB_TYPE3 = Convert.ToString(node["GRANT_SUB_TYPE3"].InnerText);
                                if (node["dt_AwardDate"] != null)
                                    GMaster.Dt_AwardDate = Convert.ToString(node["dt_AwardDate"].InnerText);
                                if (node["DURATION"] != null)
                                    GMaster.s_Duration = Convert.ToString(node["DURATION"].InnerText);
                                if (node["AWARD_ORGANIZATION"] != null)
                                {
                                    GMaster.AWARD_ORGANIZATION = Convert.ToString(node["AWARD_ORGANIZATION"].InnerXml);
                                }
                                if (node["YearQuaterStatus"] != null)
                                {
                                    GMaster.YearQuaterStatus = (node["YearQuaterStatus"].InnerText == "1" ? true : false);
                                }
                                gmList.Add(GMaster);
                            }
                        }

                    }
                    #endregion

                }
                GNP = new GrantNewProjectEntry
                {
                    PIList = piList,
                    PMList = pjList,
                    CHildProjectList = childpmlist,
                    GMList = gmList,
                    CHILDpilist = childpilist
                };
            }
            catch (Exception ex)
            {
                return GNP = null;
            }
            return GNP;
        }
        #endregion

        #region " Edit Grid Details By Id "
        public static Grant_Details GetGrantDetailsById(int ID,int ProjID)
        {
            Grant_Details GDList = new Grant_Details();
            List<Project_Master> pjlist = new List<Project_Master>();
            List<Project_Master> childpmlist = new List<Project_Master>();
            List<PI_Master> pilist = new List<PI_Master>();
            List<PI_Master> childpilist = new List<PI_Master>();
            List<Grant_Master> gmList = new List<Grant_Master>();
            List<GrantExtensionDetails> GExtList = new List<GrantExtensionDetails>();
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
                parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                parameter[parameter.Count - 1].Value = ProjID;
                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spGrant_DetailsDML]", parameter);
                if (gridData != null)
                {
                    if (gridData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in gridData.Rows)
                        {
                            #region " Dept Master "
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
                                        pilist.Add(pi);
                                    }
                                }

                            }
                            #endregion

                            #region "Child Dept Master "
                            string ChildDept = (dr.IsNull("CHILD_PI_DETAILS")) == true ? "" : Convert.ToString(dr["CHILD_PI_DETAILS"]);
                            if (ChildDept != string.Empty)
                            {

                                using (XmlReader reader = XmlReader.Create(new StringReader(ChildDept)))
                                {
                                    XmlDocument xml = new XmlDocument();
                                    xml.Load(reader);
                                    XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
                                    foreach (XmlNode node in xmlNodeList)
                                    {
                                        PI_Master pi = new PI_Master();

                                        if (node["i_ID"] != null)
                                            pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);

                                        if (node["s_PI_Name"] != null)
                                            pi.s_PIName = (node["s_PI_Name"].InnerText);

                                        if (node["i_Project_ID"] != null)
                                        {
                                            pi.i_Project_ID = Convert.ToInt32(node["i_Project_ID"].InnerText);
                                        }
                                        childpilist.Add(pi);
                                    }
                                }

                            }
                            #endregion

                            #region " Project Master "
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
                                        if (node["s_Research_IO"] != null)
                                            pmMaster.s_Research_IO = Convert.ToString(node["s_Research_IO"].InnerText);
                                        pjlist.Add(pmMaster);
                                    }
                                }

                            }
                            #endregion

                            #region " Child Project "
                            string ChildProjList = (dr.IsNull("CHILD_PROJECTGRID_DATA")) == true ? "" : Convert.ToString(dr["CHILD_PROJECTGRID_DATA"]);
                            if (ChildProjList != string.Empty)
                            {

                                using (XmlReader reader = XmlReader.Create(new StringReader(ChildProjList)))
                                {
                                    XmlDocument xml = new XmlDocument();
                                    xml.Load(reader);
                                    XmlNodeList xmlNodeList = xml.SelectNodes("CHILD_PROJECT_DATA/CHILD_PROJECT");

                                    foreach (XmlNode node in xmlNodeList)
                                    {
                                        Project_Master pmMaster = new Project_Master();
                                        if (node["i_Project_ID"] != null)
                                            pmMaster.S_ProjectStatus = Convert.ToString(node["i_Project_ID"].InnerText);
                                        if (node["s_Project_Title"] != null)
                                            pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                                        if (node["s_Display_Project_ID"] != null)
                                            pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                                        if (node["PI_NAME"] != null)
                                            pmMaster.PI_NAME = Convert.ToString(node["PI_NAME"].InnerText);
                                        childpmlist.Add(pmMaster);
                                    }
                                }

                            }
                            #endregion

                            #region " Grant Master "
                            string GrantMList = (dr.IsNull("GRANT_MASTER_DATA")) == true ? "" : Convert.ToString(dr["GRANT_MASTER_DATA"]);
                            if (GrantMList != string.Empty)
                            {

                                using (XmlReader reader = XmlReader.Create(new StringReader(GrantMList)))
                                {
                                    XmlDocument xml = new XmlDocument();
                                    xml.Load(reader);
                                    XmlNodeList xmlNodeList = xml.SelectNodes("GRANT_MASTER_DATA/GRANT_MASTER");

                                    foreach (XmlNode node in xmlNodeList)
                                    {
                                        Grant_Master GMaster = new Grant_Master();
                                        if (node["i_ID"] != null)
                                            GMaster.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
                                        if (node["GRANT_TYPE"] != null)
                                            GMaster.GRANT_TYPE = Convert.ToString(node["GRANT_TYPE"].InnerText);
                                        if (node["GRANT_SUB_TYPE1"] != null)
                                            GMaster.GRANT_SUB_TYPE1 = Convert.ToString(node["GRANT_SUB_TYPE1"].InnerText);
                                        if (node["GRANT_SUB_TYPE2"] != null)
                                            GMaster.GRANT_SUB_TYPE2 = Convert.ToString(node["GRANT_SUB_TYPE2"].InnerText);
                                        if (node["GRANT_SUB_TYPE3"] != null)
                                            GMaster.GRANT_SUB_TYPE3 = Convert.ToString(node["GRANT_SUB_TYPE3"].InnerText);
                                        if (node["dt_AwardDate"] != null)
                                            GMaster.Dt_AwardDate = Convert.ToString(node["dt_AwardDate"].InnerText);
                                        if (node["DURATION"] != null)
                                            GMaster.s_Duration = Convert.ToString(node["DURATION"].InnerText);
                                        if (node["AWARD_ORGANIZATION"] != null)
                                        {
                                            GMaster.AWARD_ORGANIZATION = Convert.ToString(node["AWARD_ORGANIZATION"].InnerXml);
                                        }
                                        gmList.Add(GMaster);
                                    }
                                }

                            }
                            #endregion

                            #region " Extesnion Details "
                            string ExtList = (dr.IsNull("EXTENSION_DETAILS")) == true ? "" : Convert.ToString(dr["EXTENSION_DETAILS"]);
                            if (ExtList != string.Empty)
                            {

                                using (XmlReader reader = XmlReader.Create(new StringReader(ExtList)))
                                {
                                    XmlDocument xml = new XmlDocument();
                                    xml.Load(reader);
                                    XmlNodeList xmlNodeList = xml.SelectNodes("EXTENSION_DETAILS/EXT");

                                    foreach (XmlNode node in xmlNodeList)
                                    {
                                        GrantExtensionDetails ExtMaster = new GrantExtensionDetails();
                                        if (node["i_Project_ID"] != null)
                                            ExtMaster.i_Project_ID = Convert.ToInt32(node["i_Project_ID"].InnerText);
                                        if (node["b_Grant_Extended"] != null)
                                            ExtMaster.b_Grant_Extended = (node["b_Grant_Extended"].InnerText) == "1" ? true : false;
                                        if (node["dt_New_Grant_Expiry_Date"] != null)
                                            ExtMaster.dt_New_Grant_Expiry_Date = Convert.ToString(node["dt_New_Grant_Expiry_Date"].InnerText);
                                        if (node["i_GrantExtended_period"] != null)
                                            ExtMaster.i_GrantExtended_period = Convert.ToInt32(node["i_GrantExtended_period"].InnerText);
                                        if (node["s_Budget_Details_String"] != null)
                                            ExtMaster.s_Budget_Details_String = Convert.ToString(node["s_Budget_Details_String"].InnerText);
                                        GExtList.Add(ExtMaster);
                                    }
                                }

                            }
                            #endregion

                            #region " Grant Details "
                            GDList = new Grant_Details()
                            {
                                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                                i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                                i_GrantMaster_ID = (dr.IsNull("i_GrantMaster_ID") ? 0 : Convert.ToInt32(dr["i_GrantMaster_ID"])),
                                s_Award_Letter_File = (dr.IsNull("s_Award_Letter_File") ? "" : Convert.ToString(dr["s_Award_Letter_File"])),
                                i_Grant_ID = (dr.IsNull("i_Grant_ID") ? "" : Convert.ToString(dr["i_Grant_ID"])),
                                s_Research_IO = (dr.IsNull("s_Research_IO") ? "" : Convert.ToString(dr["s_Research_IO"])),
                                i_Donation_Amt = (dr.IsNull("i_Donation_Amt") ? 0 : Convert.ToDouble(dr["i_Donation_Amt"])),
                                s_Donation_Body = (dr.IsNull("s_Donation_Body") ? "" : Convert.ToString(dr["s_Donation_Body"])),
                                dt_Grant_Expiry_Date = (dr.IsNull("dt_Grant_Expiry_Date") ? "" : Convert.ToString(dr["dt_Grant_Expiry_Date"])),
                                //  b_Grant_Extended = (dr.IsNull("b_Grant_Extended") ? null : (bool?)Convert.ToBoolean(dr["dt_Grant_Expiry_Date"])),
                                //  dt_New_Grant_Expiry_Date = (dr.IsNull("dt_New_Grant_Expiry_Date") ? "" : Convert.ToString(dr["dt_New_Grant_Expiry_Date"])),
                                i_Indirects = (dr.IsNull("i_Indirects") ? 0 : Convert.ToDouble(dr["i_Indirects"])),
                                i_Indirects_Amt_Utilized = (dr.IsNull("i_Indirects_Amt_Utilized") ? 0 : Convert.ToDouble(dr["i_Indirects_Amt_Utilized"])),
                                b_Mentor = (dr.IsNull("b_Mentor") ? null : (bool?)Convert.ToBoolean(dr["b_Mentor"])),
                                s_Mentor_Name = (dr.IsNull("s_Mentor_Name") ? "" : Convert.ToString(dr["s_Mentor_Name"])),
                                s_Mentor_Institute = (dr.IsNull("s_Mentor_Institute") ? "" : Convert.ToString(dr["s_Mentor_Institute"])),
                                s_Mentor_Dept = (dr.IsNull("s_Mentor_Dept") ? "" : Convert.ToString(dr["s_Mentor_Dept"])),
                                s_Tech_PI_Name = (dr.IsNull("s_Tech_PI_Name") ? "" : Convert.ToString(dr["s_Tech_PI_Name"])),
                                s_Tech_PI_Institution = (dr.IsNull("s_Tech_PI_Institution") ? "" : Convert.ToString(dr["s_Tech_PI_Institution"])),
                                s_Tech_PI_Dept = (dr.IsNull("s_Tech_PI_Dept") ? "" : Convert.ToString(dr["s_Tech_PI_Dept"])),
                                s_Point_of_Submission = (dr.IsNull("s_Point_of_Submission") ? "" : Convert.ToString(dr["s_Point_of_Submission"])),
                                i_FTE = (dr.IsNull("i_FTE") ? 0 : Convert.ToInt32(dr["i_FTE"])),
                                i_GrantStatus_ID = (dr.IsNull("i_GrantStatus_ID") ? 0 : Convert.ToInt32(dr["i_GrantStatus_ID"])),
                                b_IsVariation_Needed = (dr.IsNull("b_IsVariation_Needed") ? null : (bool?)Convert.ToBoolean(dr["b_IsVariation_Needed"])),
                                YearQuaterStatus = (bool)dr["YearQuaterStatus"],
                                PIList = pilist,
                                PMList = pjlist,
                                CHILDPIList = childpilist,
                                CHildProjectList = childpmlist,
                                GMList = gmList,
                                GEXTList = GExtList
                            };
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GDList = null;
            }
            return GDList;
        }
        #endregion

        #region " DML Method "
        public static string Grant_Details_DML(Grant_Details _Grant_Details, List<Other_Dept_PI> lstoth, List<Grant_Budget_Allocation_Details> lstGBAD, List<GrantExtensionDetails> lstGExt, string mode)
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
                parameter[parameter.Count - 1].Value = _Grant_Details.i_ID;
                if (mode.ToString() != "Delete")
                {



                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_GrantMaster_ID";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_GrantMaster_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Award_Letter_File";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Award_Letter_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Grant_ID";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_Grant_ID;

                    // parameter.Add(_helper.CreateDbParameter());
                    // parameter[parameter.Count - 1].ParameterName = "@dt_Award_Letter_Date";
                    // parameter[parameter.Count - 1].Value = _Grant_Details.dt_Award_Letter_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Research_IO";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Research_IO;

                    //parameter.Add(_helper.CreateDbParameter());
                    //parameter[parameter.Count - 1].ParameterName = "@i_Currency_ID";
                    //parameter[parameter.Count - 1].Value = _Grant_Details.i_Currency_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Donation_Amt";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_Donation_Amt;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Donation_Body";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Donation_Body;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Grant_Expiry_Date";
                    parameter[parameter.Count - 1].Value = _Grant_Details.dt_Grant_Expiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Grant_Extended";
                    parameter[parameter.Count - 1].Value = _Grant_Details.b_Grant_Extended;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_New_Grant_Expiry_Date";
                    parameter[parameter.Count - 1].Value = _Grant_Details.dt_New_Grant_Expiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_GrantExtended_period";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_GrantExtended_period;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Indirects";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_Indirects;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Indirects_Amt_Utilized";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_Indirects_Amt_Utilized;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Mentor";
                    parameter[parameter.Count - 1].Value = _Grant_Details.b_Mentor;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Mentor_Name";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Mentor_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Mentor_Institute";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Mentor_Institute;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Mentor_Dept";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Mentor_Dept;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Tech_PI_Name";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Tech_PI_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Tech_PI_Institution";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Tech_PI_Institution;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Tech_PI_Dept";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Tech_PI_Dept;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Point_of_Submission";
                    parameter[parameter.Count - 1].Value = _Grant_Details.s_Point_of_Submission;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_FTE";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_FTE;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_GrantStatus_ID";
                    parameter[parameter.Count - 1].Value = _Grant_Details.i_GrantStatus_ID;


                    //----------------- User Name and User Id
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Grant_Details.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserName";
                    parameter[parameter.Count - 1].Value = _Grant_Details.UName;
                    //------------------- END--------------------------------------

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsVariation_Needed";
                    parameter[parameter.Count - 1].Value = _Grant_Details.b_IsVariation_Needed;


                    // parameter.Add(_helper.CreateDbParameter());
                    // parameter[parameter.Count - 1].ParameterName = "@Other_Dept_PI";
                    // parameter[parameter.Count - 1].Value = lstoth.ListToDatatable();

                    DataTable dtGrant_Budget_Allocation_Details = new DataTable();
                    dtGrant_Budget_Allocation_Details.Columns.AddRange(new DataColumn[] 
                    {
                        new DataColumn("i_Grant_Details_ID"),
         new DataColumn("i_Project_ID"),
         new DataColumn("s_Years"),
         new DataColumn("s_Factors"),
         new DataColumn("i_TTSH_PI_ID"),
         new DataColumn("i_Other_PI_ID"),
         new DataColumn("i_Budget_Allocation"),
         new DataColumn("s_Yearly_Quaterly"),
         new DataColumn("i_Budget_Utilized"),
        new DataColumn("Q1"),
        new DataColumn("Q2"),
        new DataColumn("Q3"), new DataColumn("Q4"),
                    });

                    foreach (Grant_Budget_Allocation_Details item in lstGBAD)
                    {
                        dtGrant_Budget_Allocation_Details.Rows.Add(item.i_Grant_Details_ID, item.i_Project_ID, item.s_Years, item.s_Factors, item.i_TTSH_PI_ID, item.i_Other_PI_ID, item.i_Budget_Allocation
                            , item.s_Yearly_Quaterly, item.i_Budget_Utilized, item.Q1,
                                                                            item.Q2,
                               item.Q3,
                               item.Q4);





                    }

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Grant_Budget_Allocation_Details";
                    parameter[parameter.Count - 1].Value = dtGrant_Budget_Allocation_Details;



                    DataTable dtGrant_Extension_Details = new DataTable();
                    dtGrant_Extension_Details.Columns.AddRange(new DataColumn[] 
                    {
     new DataColumn("i_Grant_Details_ID"),new DataColumn("i_Project_ID"),new DataColumn("b_Grant_Extended"),new DataColumn("dt_New_Grant_Expiry_Date"),new DataColumn("i_GrantExtended_period"),
    new DataColumn("s_Budget_Details_String")
                    });

                    foreach (GrantExtensionDetails item in lstGExt)
                    {
                        dtGrant_Extension_Details.Rows.Add(item.i_Grant_Detail_ID, item.i_Project_ID, item.b_Grant_Extended, item.dt_New_Grant_Expiry_Date, item.i_GrantExtended_period, item.s_Budget_Details_String);
                    }


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Grant_Extension_Details";
                    parameter[parameter.Count - 1].Value = dtGrant_Extension_Details;

                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spGrant_DetailsDML", parameter)))
                {
                    result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString().Replace("'", "");
            }
            return result;
        }
        //--================================================END OF Properties===============================================================================
        public static string Other_PI_Master_DML(Other_PI_Master _Other_PI_Master, string mode)
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
                parameter[parameter.Count - 1].Value = _Other_PI_Master.i_ID;
                if (mode.ToString() != "Delete")
                {



                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Firstname";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_Firstname;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Lastname";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_Lastname;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Email";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_Email;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Phone_no";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_Phone_no;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_MCR_No";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_MCR_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_DeptName";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_DeptName;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Organization_Name";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.s_Organization_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCID";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Other_PI_Master.UName;



                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spOther_PI_MasterDML", parameter)))
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
        #endregion
    }
}
