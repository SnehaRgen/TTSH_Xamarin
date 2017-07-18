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
using TTSHWCFLayer;
using TTSHWCFLayer.Excel;
using System.IO;
using System.ServiceModel.Web;
using TTSH.BusinessLogic.Authentication;
using System.Xml;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace TTSHWCFLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TTSHWCFService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TTSHWCFService.svc or TTSHWCFService.svc.cs at the Solution Explorer and start debugging.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class TTSHWCFService : ITTSHWCFService
    {

        #region Project Detail Methods

        public Project_Master GetProject_MasterDetailsByID(int ID)
        {
            return ProjectMaster.GetProject_MasterDetailsByID(ID);
            //Project_Master _Project_Master = new Project_Master();
            //{
            //    try
            //    {
            //        DataHelper _helper = new DataHelper();
            //        _helper.InitializedHelper();
            //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //        parameter[parameter.Count - 1].Value = "select";
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //        parameter[parameter.Count - 1].Value = ID;
            //        DataTable ProjectsData = new DataTable();
            //        List<PI_Master> piList = new List<PI_Master>();
            //        List<Project_Coordinator_Details> CoOrdList = new List<Project_Coordinator_Details>();
            //        ProjectsData = _helper.GetData("spProjectMasterDML", parameter);
            //        foreach (DataRow dr in ProjectsData.Rows)
            //        {
            //            string xmlTestManager = (dr.IsNull("DEPT_PI")) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
            //            if (xmlTestManager != string.Empty)
            //            {
            //                //--PI_M.i_Dept_ID,PI_M.i_ID,PI_M.s_Email,PI_M.s_Phone_no,PI_M.s_MCR_No

            //                using (XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");


            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        PI_Master pi = new PI_Master();
            //                        if (node["i_Dept_ID"] != null)
            //                            pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
            //                        if (node["i_ID"] != null)
            //                            pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //                        if (node["s_Email"] != null)
            //                            pi.s_Email = (node["s_Email"].InnerText);
            //                        if (node["s_Phone_no"] != null)
            //                            pi.s_Phone_no = (node["s_Phone_no"].InnerText);
            //                        if (node["s_MCR_No"] != null)
            //                            pi.s_MCR_No = (node["s_MCR_No"].InnerText);
            //                        if (node["Dept_Name"] != null)
            //                            pi.s_DeptName = (node["Dept_Name"].InnerText);
            //                        if (node["s_PI_Name"] != null)
            //                            pi.s_PIName = (node["s_PI_Name"].InnerText);
            //                        piList.Add(pi);
            //                    }
            //                }

            //            }

            //            string CoList = (dr.IsNull("COORDINATOR")) == true ? "" : Convert.ToString(dr["COORDINATOR"]);
            //            if (CoList != string.Empty)
            //            {
            //                //-- PCD.s_Coordinator_name,pcd.i_Coordinator_ID

            //                using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("COORDINATOR/COORDINATOR_D");


            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Project_Coordinator_Details pcd = new Project_Coordinator_Details();
            //                        if (node["s_Coordinator_name"] != null)
            //                            pcd.s_Coordinator_name = (node["s_Coordinator_name"].InnerText);
            //                        if (node["i_Coordinator_ID"] != null)
            //                            pcd.i_Coordinator_ID = Convert.ToInt32(node["i_Coordinator_ID"].InnerText);
            //                        CoOrdList.Add(pcd);
            //                    }
            //                }

            //            }
            //            _Project_Master = new Project_Master()
            //            {
            //                COORDINATOR = CoOrdList,
            //                DEPT_PI = piList,
            //                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID") ? string.Empty : Convert.ToString(dr["s_Display_Project_ID"])),
            //                s_Project_Title = (dr.IsNull("s_Project_Title") ? string.Empty : Convert.ToString(dr["s_Project_Title"])),
            //                s_Short_Title = (dr.IsNull("s_Short_Title") ? string.Empty : Convert.ToString(dr["s_Short_Title"])),
            //                i_Project_Category_ID = (dr.IsNull("i_Project_Category_ID") ? 0 : Convert.ToInt32(dr["i_Project_Category_ID"])),
            //                i_Project_Type_ID = (dr.IsNull("i_Project_Type_ID") ? 0 : Convert.ToInt32(dr["i_Project_Type_ID"])),
            //                i_Project_Subtype_ID = (dr.IsNull("i_Project_Subtype_ID") ? 0 : Convert.ToInt32(dr["i_Project_Subtype_ID"])),
            //                b_Collaboration_Involved = (dr.IsNull("b_Collaboration_Involved") ? false : Convert.ToBoolean(dr["b_Collaboration_Involved"])),
            //                b_StartBy_TTSH = (dr.IsNull("b_StartBy_TTSH") ? false : Convert.ToBoolean(dr["b_StartBy_TTSH"])),
            //                b_Funding_req = (dr.IsNull("b_Funding_req") ? false : Convert.ToBoolean(dr["b_Funding_req"])),
            //                b_Ischild = (dr.IsNull("b_Ischild") ? false : Convert.ToBoolean(dr["b_Ischild"])),
            //                i_Parent_ProjectID = (dr.IsNull("i_Parent_ProjectID") ? 0 : Convert.ToInt32(dr["i_Parent_ProjectID"])),
            //                s_Project_Alias1 = (dr.IsNull("s_Project_Alias1") ? string.Empty : Convert.ToString(dr["s_Project_Alias1"])),
            //                s_Project_Alias2 = (dr.IsNull("s_Project_Alias2") ? string.Empty : Convert.ToString(dr["s_Project_Alias2"])),
            //                s_Project_Desc = (dr.IsNull("s_Project_Desc") ? string.Empty : Convert.ToString(dr["s_Project_Desc"])),
            //                Project_Category_Name = (dr.IsNull("Project_Category_Name") ? string.Empty : Convert.ToString(dr["Project_Category_Name"])),
            //                b_IsFeasible = (dr.IsNull("b_IsFeasible") ? 0 : Convert.ToInt32(dr["b_IsFeasible"])),
            //                b_Isselected_project = (dr.IsNull("b_Isselected_project") ? false : Convert.ToBoolean(dr["b_Isselected_project"])),
            //                s_IRB_No = (dr.IsNull("s_IRB_No") ? string.Empty : Convert.ToString(dr["s_IRB_No"])),
            //                s_Research_IO = (dr.IsNull("s_Research_IO") ? string.Empty : Convert.ToString(dr["s_Research_IO"])),
            //                s_Research_IP = (dr.IsNull("s_Research_IP") ? string.Empty : Convert.ToString(dr["s_Research_IP"])),
            //                Project_StartDate = (dr.IsNull("dt_ProjectStartDate") ? string.Empty : Convert.ToString(dr["dt_ProjectStartDate"])),
            //                s_Coinvestigator = (dr.IsNull("s_Coinvestigator") ? string.Empty : Convert.ToString(dr["s_Coinvestigator"]))
            //                //s_CreatedBy_ID = ( dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"]) ),
            //                //s_ModifyBy_ID = ( dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"]) ),
            //                //dt_Created_Date = ( dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"]) ),
            //                //dt_Modify_Date = ( dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"]) )
            //            };


            //        }
            //    }
            //    catch (Exception e) { }
            //    return _Project_Master;
            //}
        }
        public string GetPI_MasterDetailsByID(int ID)
        {
            return ProjectMaster.GetPI_MasterDetailsByID(ID);
            //List<object> lst = new List<object>();
            //{
            //    try
            //    {
            //        DataHelper _helper = new DataHelper();
            //        _helper.InitializedHelper();
            //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //        parameter[parameter.Count - 1].Value = "select";
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //        parameter[parameter.Count - 1].Value = ID;
            //        DataTable ProjectsData = new DataTable();
            //        ProjectsData = _helper.GetData("dbo.spPI_MasterDML", parameter);
            //        foreach (DataRow dr in ProjectsData.Rows)
            //        {
            //            PI_Master _PI_Master = new PI_Master();


            //            _PI_Master.i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"]));
            //            _PI_Master.i_Dept_ID = (dr.IsNull("i_Dept_ID") ? 0 : Convert.ToInt32(dr["i_Dept_ID"]));
            //            _PI_Master.s_Firstname = (dr.IsNull("s_Firstname") ? string.Empty : Convert.ToString(dr["s_Firstname"]));
            //            _PI_Master.s_Lastname = (dr.IsNull("s_Lastname") ? string.Empty : Convert.ToString(dr["s_Lastname"]));
            //            _PI_Master.s_Email = (dr.IsNull("s_Email") ? string.Empty : Convert.ToString(dr["s_Email"]));
            //            _PI_Master.s_Phone_no = (dr.IsNull("s_Phone_no") ? string.Empty : Convert.ToString(dr["s_Phone_no"]));
            //            _PI_Master.s_MCR_No = (dr.IsNull("s_MCR_No") ? string.Empty : Convert.ToString(dr["s_MCR_No"]));
            //            _PI_Master.s_DeptName = (dr.IsNull("Dept_Name") ? string.Empty : Convert.ToString(dr["Dept_Name"]));
            //            _PI_Master.s_PIName = (dr.IsNull("s_Firstname") ? string.Empty : Convert.ToString(dr["s_Firstname"])) + "  " + (dr.IsNull("s_Lastname") ? string.Empty : Convert.ToString(dr["s_Lastname"]));
            //            //s_Description = (  dr.IsNull("s_Description") ? string.Empty : Convert.ToString(dr["s_Description"]) ),
            //            //s_CreatedBy_ID = ( dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"]) ),
            //            //s_ModifyBy_ID = ( dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"]) ),
            //            //dt_Created_Date = ( dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"]) ),
            //            //dt_Modify_Date = ( dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"]) )
            //            lst.Add(_PI_Master);
            //        }
            //    }
            //    catch (Exception e) { }
            //    return (new JavaScriptSerializer().Serialize(lst));
            //}
        }
        public List<Project_Master> FillGrid_Project_Master()
        {
            return ProjectMaster.FillGrid_Project_Master();
            //List<Project_Master> pm = new List<Project_Master>();

            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = "select";

            //    DataTable gridData = new DataTable();
            //    gridData = _helper.GetData("[dbo].[spProjectMasterDML]", parameter);

            //    foreach (DataRow dr in gridData.Rows)
            //    {
            //        pm.Add(new Project_Master
            //        {
            //            i_ID = Convert.ToInt32(dr["i_ID"]),
            //            s_Display_Project_ID = Convert.ToString(dr["s_Display_Project_ID"]),
            //            s_Project_Title = Convert.ToString(dr["s_Project_Title"]),
            //            Project_Category_Name = Convert.ToString(dr["Project_Category_Name"]),
            //            s_IRB_No = Convert.ToString(dr["s_IRB_No"]),
            //            Project_Type = Convert.ToString(dr["Project_Type"]),
            //            PI_NAME = Convert.ToString(dr["PI_NAME"])


            //        });
            //    }
            //}
            //catch (Exception)
            //{


            //}

            //return pm;

        }

        //--==========================================END of Class========================================================================



        public string Project_Master(Project_Master _Project_Master, List<Project_Dept_PI> pdi, List<Project_Coordinator_Details> pcd, string mode)
        {
            return ProjectMaster.Project_Master(_Project_Master, pdi, pcd, mode);
            //string result = "";
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();

            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = mode.ToString();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = _Project_Master.i_ID;




            //    if (mode.ToString() != "Delete")
            //    {


            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Display_Project_ID";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Display_Project_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Project_Title";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Title;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Short_Title";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Short_Title;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Project_Category_ID";
            //        parameter[parameter.Count - 1].Value = _Project_Master.i_Project_Category_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Project_Type_ID";
            //        parameter[parameter.Count - 1].Value = _Project_Master.i_Project_Type_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Project_Subtype_ID";
            //        parameter[parameter.Count - 1].Value = _Project_Master.i_Project_Subtype_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_Collaboration_Involved";
            //        parameter[parameter.Count - 1].Value = _Project_Master.b_Collaboration_Involved;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_StartBy_TTSH";
            //        parameter[parameter.Count - 1].Value = _Project_Master.b_StartBy_TTSH;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_Funding_req";
            //        parameter[parameter.Count - 1].Value = _Project_Master.b_Funding_req;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_Ischild";
            //        parameter[parameter.Count - 1].Value = _Project_Master.b_Ischild;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Parent_ProjectID";
            //        parameter[parameter.Count - 1].Value = _Project_Master.i_Parent_ProjectID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias1";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Alias1;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias2";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Alias2;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Project_Desc";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Desc;


            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_IsFeasible";
            //        parameter[parameter.Count - 1].Value = _Project_Master.b_IsFeasible;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_Isselected_project";
            //        parameter[parameter.Count - 1].Value = _Project_Master.b_Isselected_project;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_IRB_No";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_IRB_No;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Research_IO";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Research_IO;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Research_IP";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_Research_IP;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@UserCID ";
            //        parameter[parameter.Count - 1].Value = _Project_Master.s_CreatedBy_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_ProjectStartDate ";
            //        parameter[parameter.Count - 1].Value = _Project_Master.Project_StartDate;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
            //        parameter[parameter.Count - 1].Value = pdi.ListToDatatable().getColumns(1);

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@Project_Coordinator_Details";
            //        parameter[parameter.Count - 1].Value = pcd.ListToDatatable();


            //    }
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
            //    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
            //    parameter[parameter.Count - 1].Size = 500;
            //    if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
            //    {
            //        result = "Success" + " | " + parameter[parameter.Count - 1].Value.ToString(); ;
            //    }
            //    else
            //    {
            //        result = parameter[parameter.Count - 1].Value.ToString();
            //    }
            //}
            //catch (Exception ex) { result = ex.Message; }
            //return result;
        }

        public string PI_Master(PI_Master _PI_Master, string mode)
        {
            return ProjectMaster.PI_Master(_PI_Master, mode);
            //string result = "";
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = mode.ToString();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = _PI_Master.i_ID;
            //    if (mode.ToString() != "Delete")
            //    {


            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Dept_ID";
            //        parameter[parameter.Count - 1].Value = _PI_Master.i_Dept_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Firstname";
            //        parameter[parameter.Count - 1].Value = _PI_Master.s_Firstname;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Lastname";
            //        parameter[parameter.Count - 1].Value = _PI_Master.s_Lastname;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Email";
            //        parameter[parameter.Count - 1].Value = _PI_Master.s_Email;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Phone_no";
            //        parameter[parameter.Count - 1].Value = _PI_Master.s_Phone_no;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_MCR_No";
            //        parameter[parameter.Count - 1].Value = _PI_Master.s_MCR_No;


            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@UserCID";
            //        parameter[parameter.Count - 1].Value = _PI_Master.s_CreatedBy_ID;




            //    }
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
            //    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
            //    parameter[parameter.Count - 1].Size = 500;
            //    if (Convert.ToBoolean(_helper.DMLOperation("dbo.spPI_MasterDML", parameter)))
            //    {
            //        result = "Success" + " | " + parameter[parameter.Count - 1].Value.ToString();
            //    }
            //    else
            //    {
            //        result = parameter[parameter.Count - 1].Value.ToString();
            //    }
            //}
            //catch (Exception ex) { }
            //return result;
        }
        #endregion

        #region Common Methods
        public List<clsDropDown> GetDropDownData(DropDownName dropDownName, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {

            List<clsDropDown> listDDL = new List<clsDropDown>();

            try
            {
                DataHelper _helper = new DataHelper();

                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@MODULENAME";
                parameter[parameter.Count - 1].Value = dropDownName.ToString();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@A";
                parameter[parameter.Count - 1].Value = param1;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@B";
                parameter[parameter.Count - 1].Value = param2;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@C";
                parameter[parameter.Count - 1].Value = param3;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@D";
                parameter[parameter.Count - 1].Value = param4;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@E";
                parameter[parameter.Count - 1].Value = param5;

                DataTable DropDown = new DataTable();

                DropDown = _helper.GetData("dbo.SP_POPDDL", parameter);
                if (DropDown != null)
                {
                    foreach (DataRow dr in DropDown.Rows)
                    {

                        string strHex = "";

                        if (dropDownName.ToString() == "Coordinators")
                        {
                            byte[] binaryData = dr.ItemArray[1] as byte[];

                            String g = String.Empty;

                            foreach (var item in binaryData)
                            {
                                g += String.Format("{0:X2}", item);
                            }

                            strHex = ConvertOctetStringToGuid(g).ToString();

                        }
                        else
                        {
                            strHex = (dr.IsNull(1) ? "" : dr[1].ToString());
                        }

                        listDDL.Add(new clsDropDown()
                        {

                            DisplayField = (dr.IsNull(0) ? "" : dr[0].ToString()),
                            ValueField = strHex

                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return listDDL;


        }
        public string[] GetText(string Prefix, int count, string ContextKey)
        {
            List<string> lst = new List<string>();
            DataHelper _helper = new DataHelper();

            _helper.InitializedHelper();

            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();


            DataTable dt = new DataTable(); ;
            string[] isval = ContextKey.Split('~');
            string _modulename = (isval.Length > 0) ? isval[0] : "";
            string _SpName = (isval.Length > 1) ? isval[1] : "";
            string _condition = (isval.Length > 2) ? isval[2] : "";

            parameter.Add(_helper.CreateDbParameter());
            parameter[parameter.Count - 1].ParameterName = "@MODULENAME";
            parameter[parameter.Count - 1].Value = _modulename;

            parameter.Add(_helper.CreateDbParameter());
            parameter[parameter.Count - 1].ParameterName = "@A";
            parameter[parameter.Count - 1].Value = Prefix;

            parameter.Add(_helper.CreateDbParameter());
            parameter[parameter.Count - 1].ParameterName = "@B";
            parameter[parameter.Count - 1].Value = count.ToString();

            parameter.Add(_helper.CreateDbParameter());
            parameter[parameter.Count - 1].ParameterName = "@C";
            parameter[parameter.Count - 1].Value = _condition;


            dt = _helper.GetData(_SpName, parameter);
            if (dt != null)
            {
                if (dt.Columns.Count > 1)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lst.Add(string.Format("{0}^{1}", item[0].ToString(), item[1].ToString()));
                    }
                }
            }

            return lst.ToArray();
        }
        public string GetValidate(string _ModuleName, string _A, string _B, string _C, string _D)
        {
            string RetValues = "";
            DataHelper _helper = new DataHelper();
            _helper.InitializedHelper();
            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            DataTable dt = new DataTable(); ;
            try
            {


                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@MODULENAME";
                parameter[parameter.Count - 1].Value = _ModuleName;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@A";
                parameter[parameter.Count - 1].Value = _A;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@B";
                parameter[parameter.Count - 1].Value = _B;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@C";
                parameter[parameter.Count - 1].Value = _C;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@D";
                parameter[parameter.Count - 1].Value = _D;

                dt = _helper.GetData("dbo.CheckValidate", parameter);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        RetValues = Convert.ToString(ModCommon.iffBlank(dt.Rows[0][0].ToString(), ""));
                    }
                }
            }
            catch (Exception ex)
            {

                RetValues = "#Error";
            }
            return RetValues;
        }

        public string[] GetValues(string _ModuleName, string _A, string _B, string _C, string _D)
        {
            List<string> obj = new List<string>();
            DataHelper _helper = new DataHelper();
            _helper.InitializedHelper();
            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            DataTable dt = new DataTable(); ;
            try
            {


                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@MODULENAME";
                parameter[parameter.Count - 1].Value = _ModuleName;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@A";
                parameter[parameter.Count - 1].Value = _A;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@B";
                parameter[parameter.Count - 1].Value = _B;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@C";
                parameter[parameter.Count - 1].Value = _C;
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@D";
                parameter[parameter.Count - 1].Value = _D;

                dt = _helper.GetData("dbo.SP_POPDDL", parameter);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(string.Format("{0},{1}", item[0].ToString(), item[1].ToString()));
                        }

                    }
                }
            }
            catch (Exception)
            {


            }
            return obj.ToArray();
        }
        public string[] GetCasCadeDdl(String moduleName,string id)
        {
            List<string> lst = new List<string>();
            DataHelper _helper = new DataHelper();

            _helper.InitializedHelper();

            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();


            DataTable dt = new DataTable(); ;
       

            parameter.Add(_helper.CreateDbParameter());
            parameter[parameter.Count - 1].ParameterName = "@MODULENAME";
            parameter[parameter.Count - 1].Value = moduleName;

            parameter.Add(_helper.CreateDbParameter());
            parameter[parameter.Count - 1].ParameterName = "@A";
            parameter[parameter.Count - 1].Value = id;




            dt = _helper.GetData("SP_CasCadianDropdown", parameter);
            if (dt != null)
            {
                if (dt.Columns.Count > 1)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lst.Add(string.Format("{0}^{1}", item[0].ToString(), item[1].ToString()));
                    }
                }
            }

            return lst.ToArray();
        }
        #endregion

        #region Feasibility Methods

        public Feasibility_Details GetFeasibility_DetailsByID(int ID)
        {
            return Feasibility.GetFeasibility_DetailsByIDBAL(ID);
        }

        public string Feasibility_Details(Feasibility_Details _Feasibility_Details, Mode mode)
        {
            return Feasibility.Feasibility_DetailsBAL(_Feasibility_Details, mode);
        }

        public List<Feasibility_Grid> Feasibility_FillGrid()
        {
            return Feasibility.Feasibility_FillGridBAL();
        }

        public string Sponsor(Sponsor_Master sponsor)
        {
            return Feasibility.SponsorBAL(sponsor);
        }

        public string CRO(CRO_Master CRO)
        {
            return Feasibility.CROBAL(CRO);
        }
        #endregion

        #region Ethics Methods
        public Ethics_Dept_PI GetEthics_Dept_PIDetailsByID(int ID)
        {
            Ethics_Dept_PI _Ethics_Dept_PI = new Ethics_Dept_PI();
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
                        _Ethics_Dept_PI = new Ethics_Dept_PI()
                        {


                            i_Ethics_ID = (dr.IsNull("i_Ethics_ID") ? 0 : Convert.ToInt32(dr["i_Ethics_ID"])),
                            i_Dept_ID = (dr.IsNull("i_Dept_ID") ? 0 : Convert.ToInt32(dr["i_Dept_ID"])),
                            i_PI_ID = (dr.IsNull("i_PI_ID") ? 0 : Convert.ToInt32(dr["i_PI_ID"]))
                        };
                    }
                }
                catch (Exception e) { }
                return _Ethics_Dept_PI;
            }
        }

        public Ethics_Details GetEthics_DetailsByID(int ID)
        {
            return Ethics.GetEthics_DetailsByIDBAL(ID);
        }

        public string Ethics_Dept_PI(Ethics_Dept_PI _Ethics_Dept_PI, Mode mode)
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
                parameter[parameter.Count - 1].Value = _Ethics_Dept_PI.i_Ethics_ID;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Ethics_ID ";
                    parameter[parameter.Count - 1].Value = _Ethics_Dept_PI.i_Ethics_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Dept_ID ";
                    parameter[parameter.Count - 1].Value = _Ethics_Dept_PI.i_Dept_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_PI_ID ";
                    parameter[parameter.Count - 1].Value = _Ethics_Dept_PI.i_PI_ID;


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

        public string Ethics_Details(Ethics_Details _Ethics_Details, Mode mode)
        {
            return TTSH.BusinessLogic.Ethics.Ethics_DetailsBAL(_Ethics_Details, mode);
        }

        public List<Ethics_Grid> Ethics_FillGrid()
        {
            return Ethics.Ethics_FillGridBAL();
        }
        #endregion

        #region Selected Project Methods

        public string SAE(SAE_Master SAE)
        {
            return Selected.SAEBAL(SAE);

        }

        public string newSAE(newSAE_Master newSAE)
        {
            return Selected.newSAEBAL(newSAE);

        }

        public string CRA(CRA_Master CRA)
        {
            return Selected.CRABAL(CRA);

        }

        public string Selected_Project_Details(Selected_Project_Details _Selected_Project_Details, Mode mode)
        {
            return Selected.Selected_Project_DetailsBAL(_Selected_Project_Details, mode);
        }

        public Selected_Project_Details GetSelected_Project_DetailsByID(int ID, string year, string month)
        {
            return Selected.GetSelected_Project_DetailsByIDBAL(ID, year, month);
        }



        #endregion

        # region User Authentication

        public bool AuthenticateADUsers(string ADServer, string userName, string password)
        {
            //LdapAuthentication ldap = new LdapAuthentication();
            //return ldap.Authenticate(ADServer, userName, password);

            return TTSH.BusinessLogic.Authentication.LdapAuthentication.Authenticate(ADServer, userName, password);

        }

        public bool AuthenticateADUsersByName(string userName)
        {
            //LdapAuthentication ldap = new LdapAuthentication();
            //return ldap.Authenticate(userName);
            return TTSH.BusinessLogic.Authentication.LdapAuthentication.Authenticate(userName);
        }

        public String[] GetGroupNames(string userName, string password)
        {
            //LdapAuthentication ldap = new LdapAuthentication();
            //return ldap.GetGroupNames(userName, password);
            return TTSH.BusinessLogic.Authentication.LdapAuthentication.GetGroupNames(userName, password);

        }

        public List<ADUserDetails> GetMenusByGroup(string Group)
        {
            return TTSH.BusinessLogic.Authentication.LdapAuthentication.GetMenusByGroup(Group);
        }

        public TTSH.Entity.ADUserDetails GetUserDetails(string UserName)
        {
            return TTSH.BusinessLogic.Authentication.LdapAuthentication.GetUserDetails(UserName);
        }

        public List<TTSH.Entity.ADUserDetails> GetUserADDetails(string ADServer, string userName, string password)
        {
            return TTSH.BusinessLogic.Authentication.LdapAuthentication.GetUserADDetails(ADServer, userName, password);
        }

        //Nitin Vaswani:24-Jul-2015 Get All Thr Groups 
        public String[] GetGroups()
        {
            return TTSH.BusinessLogic.Authentication.LdapAuthentication.GetGroups();

        }

        #endregion

        #region Regulatory Methods
        public List<Regulatory_Master> FillGridRegulatoryMain()
        {
            return Regulatory.FillGridRegulatoryMain();
        }
        public RegulatoryNewProjectEntry GetNewProjectEntry(int ID)
        {
            return Regulatory.GetNewProjectEntry(ID);
        }
        public List<Regulatory_Master> FillGridRegulatoryDetailsByID(int ID)
        {
            return Regulatory.FillGridRegulatoryDetailsByID(ID);
        }
        public string Regulatory_Master_DML(Regulatory_Master _Regulatory_Master,
           List<Regulatory_StudyTeam> lstRegulatory_StudyTeam,
           List<Regulatory_ICF_Details> lstRegulatory_ICF_Details,
           List<Regulatory_Submission_Status> lstRegulatory_Submission_Status,
           List<Regulatory_Ammendments_Details> lstRegulatory_Ammendments_Details,
           List<RegulatoryIPManagement> lstRegulatoryIPManagement,
           string mode)
        {
            return Regulatory.Regulatory_Master_DML(_Regulatory_Master,
                lstRegulatory_StudyTeam,
                lstRegulatory_ICF_Details,
                lstRegulatory_Submission_Status,
                lstRegulatory_Ammendments_Details,
                lstRegulatoryIPManagement, mode);
        }
        public Regulatory_Master GetRegulatory_MasterDetailsByID(int ID)
        {
            return Regulatory.GetRegulatory_MasterDetailsByID(ID);
        }
        #endregion

        #region Contract Methods


        public Contract_Collobrator_Master GetContract_Collobrator_MasterByID(int ID)
        {
            return Contract.GetContract_Collobrator_MasterByID(ID);
            //Contract_Collobrator_Master _Contract_Collobrator_Master = new Contract_Collobrator_Master();

            //{
            //    try
            //    {
            //        DataHelper _helper = new DataHelper();
            //        _helper.InitializedHelper();
            //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //        parameter[parameter.Count - 1].Value = "select";
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //        parameter[parameter.Count - 1].Value = ID;
            //        DataTable ProjectsData = new DataTable();
            //        ProjectsData = _helper.GetData("dbo.spContract_Collobrator_MasterDML", parameter);
            //        foreach (DataRow dr in ProjectsData.Rows)
            //        {
            //            _Contract_Collobrator_Master = new Contract_Collobrator_Master()
            //            {



            //                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                s_Name = (dr.IsNull("s_Name") ? string.Empty : Convert.ToString(dr["s_Name"])),
            //                s_Email1 = (dr.IsNull("s_Email1") ? string.Empty : Convert.ToString(dr["s_Email1"])),
            //                s_Email2 = (dr.IsNull("s_Email2") ? string.Empty : Convert.ToString(dr["s_Email2"])),
            //                s_Institution = (dr.IsNull("s_Institution") ? string.Empty : Convert.ToString(dr["s_Institution"])),
            //                s_PhoNo = (dr.IsNull("s_PhoNo") ? string.Empty : Convert.ToString(dr["s_PhoNo"])),
            //                i_Country_ID = (dr.IsNull("i_Country_ID") ? 0 : Convert.ToInt32(dr["i_Country_ID"]))
            //            };


            //        }
            //    }
            //    catch (Exception ex) { }
            //    return _Contract_Collobrator_Master;
            //}
        }
        public Contract_Details GetContract_DetailsDetailsByID(int ID)
        {

            return Contract.GetContract_DetailsDetailsByID(ID);
            //Contract_Details _Contract_Details = new Contract_Details();
            //{
            //    try
            //    {
            //        DataHelper _helper = new DataHelper();
            //        _helper.InitializedHelper();
            //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //        parameter[parameter.Count - 1].Value = "select";
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //        parameter[parameter.Count - 1].Value = ID;
            //        DataTable ProjectsData = new DataTable();
            //        ProjectsData = _helper.GetData("dbo.spContract_DetailsDML", parameter);

            //        List<PI_Master> piList = new List<PI_Master>();
            //        List<Contract_Collobrator_Master> ccdlist = new List<Contract_Collobrator_Master>();
            //        List<Project_Master> pjmasterlist = new List<TTSH.Entity.Project_Master>();
            //        List<ContractList> ctlist = new List<ContractList>();
            //        List<Contract_Status_Date> lstcsd = new List<Contract_Status_Date>();
            //        foreach (DataRow dr in ProjectsData.Rows)
            //        {
            //            //string xmlTestManager = ( dr.IsNull("DEPT_PI") ) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
            //            //if ( xmlTestManager != string.Empty )
            //            //    {
            //            //    using ( XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)) )
            //            //        {
            //            //        XmlDocument xml = new XmlDocument();
            //            //        xml.Load(reader);
            //            //        XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
            //            //        foreach ( XmlNode node in xmlNodeList )
            //            //            {
            //            //            PI_Master pi = new PI_Master();
            //            //            if ( node["i_Dept_ID"] != null )
            //            //                pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
            //            //            if ( node["i_ID"] != null )
            //            //                pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //            //            if ( node["s_Email"] != null )
            //            //                pi.s_Email = ( node["s_Email"].InnerText );
            //            //            if ( node["s_Phone_no"] != null )
            //            //                pi.s_Phone_no = ( node["s_Phone_no"].InnerText );
            //            //            if ( node["s_MCR_No"] != null )
            //            //                pi.s_MCR_No = ( node["s_MCR_No"].InnerText );
            //            //            if ( node["Dept_Name"] != null )
            //            //                pi.s_DeptName = ( node["Dept_Name"].InnerText );
            //            //            if ( node["s_PI_Name"] != null )
            //            //                pi.s_PIName = ( node["s_PI_Name"].InnerText );
            //            //            piList.Add(pi);
            //            //            }
            //            //        }

            //            //    }
            //            //string CoList = ( dr.IsNull("COLLABORATOR") ) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
            //            //if ( CoList != string.Empty )
            //            //    {
            //            //    using ( XmlReader reader = XmlReader.Create(new StringReader(CoList)) )
            //            //        {
            //            //        XmlDocument xml = new XmlDocument();
            //            //        xml.Load(reader);
            //            //        XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


            //            //        foreach ( XmlNode node in xmlNodeList )
            //            //            {
            //            //            Contract_Collobrator_Master ccd = new Contract_Collobrator_Master();
            //            //            if ( node["i_ID"] != null )
            //            //                ccd.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //            //            if ( node["s_Name"] != null )
            //            //                ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
            //            //            if ( node["s_Email1"] != null )
            //            //                ccd.s_Email1 = Convert.ToString(node["s_Email1"].InnerText);
            //            //            if ( node["s_Email2"] != null )
            //            //                ccd.s_Email2 = Convert.ToString(node["s_Email2"].InnerText);
            //            //            if ( node["s_Institution"] != null )
            //            //                ccd.s_Institution = Convert.ToString(node["s_Institution"].InnerText);
            //            //            if ( node["s_Country_Name"] != null )
            //            //                ccd.Country_Name = Convert.ToString(node["s_Country_Name"].InnerText);
            //            //            if ( node["i_Country_ID"] != null )
            //            //                ccd.i_Country_ID = Convert.ToInt32(node["i_Country_ID"].InnerText);
            //            //            if ( node["dt_Contract_Request_Date"] != null )
            //            //                ccd.s_date = Convert.ToString(node["dt_Contract_Request_Date"].InnerText);
            //            //            if ( node["s_InitialContract_ID"] != null )
            //            //                ccd.s_initialId = Convert.ToString(node["s_InitialContract_ID"].InnerText);
            //            //            if ( node["s_PhoNo"] != null )
            //            //                ccd.s_PhoNo = Convert.ToString(node["s_PhoNo"].InnerText);
            //            //            ccdlist.Add(ccd);
            //            //            }
            //            //        }
            //            //    }
            //            //string PJmasterList = ( dr.IsNull("PROJECT_DATA") ) == true ? "" : Convert.ToString(dr["PROJECT_DATA"]);
            //            //if ( PJmasterList != string.Empty )
            //            //    {

            //            //    using ( XmlReader reader = XmlReader.Create(new StringReader(PJmasterList)) )
            //            //        {
            //            //        XmlDocument xml = new XmlDocument();
            //            //        xml.Load(reader);
            //            //        XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/PROJECT_DATA");

            //            //        foreach ( XmlNode node in xmlNodeList )
            //            //            {
            //            //            Project_Master pmMaster = new Project_Master();
            //            //            if ( node["ProjmID"] != null )
            //            //                pmMaster.i_ID = Convert.ToInt32(node["ProjmID"].InnerText);
            //            //            if ( node["s_Project_Title"] != null )
            //            //                pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
            //            //            if ( node["s_Display_Project_ID"] != null )
            //            //                pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
            //            //            if ( node["s_Short_Title"] != null )
            //            //                pmMaster.s_Short_Title = Convert.ToString(node["s_Short_Title"].InnerText);
            //            //            if ( node["Project_Category_Name"] != null )
            //            //                pmMaster.Project_Category_Name = Convert.ToString(node["Project_Category_Name"].InnerText);
            //            //            if ( node["s_Project_Alias1"] != null )
            //            //                pmMaster.s_Project_Alias1 = Convert.ToString(node["s_Project_Alias1"].InnerText);
            //            //            if ( node["s_Project_Alias2"] != null )
            //            //                pmMaster.s_Project_Alias2 = Convert.ToString(node["s_Project_Alias2"].InnerText);
            //            //            if ( node["s_IRB_No"] != null )
            //            //                pmMaster.s_IRB_No = Convert.ToString(node["s_IRB_No"].InnerText);

            //            //            pjmasterlist.Add(pmMaster);
            //            //            }
            //            //        }

            //            //    }
            //            string StatusDate = (dr.IsNull("Contract_StatusDate")) == true ? "" : Convert.ToString(dr["Contract_StatusDate"]);
            //            if (StatusDate != string.Empty)
            //            {
            //                using (XmlReader reader = XmlReader.Create(new StringReader(StatusDate)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("StatusDateD/StatusDate");


            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Contract_Status_Date csd = new Contract_Status_Date();
            //                        if (node["i_Contract_Status_ID"] != null)
            //                            csd.i_Contract_Status_ID = Convert.ToInt32(node["i_Contract_Status_ID"].InnerText);
            //                        if (node["dt_Status_Date"] != null)
            //                            csd.dt_Status_Date = Convert.ToString(node["dt_Status_Date"].InnerText);

            //                        lstcsd.Add(csd);
            //                    }
            //                }
            //            }
            //            ctlist.Add(new ContractList
            //                {
            //                    i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                    s_Contract_Name = (dr.IsNull("s_Contract_Name") ? string.Empty : Convert.ToString(dr["s_Contract_Name"])),
            //                    s_ContractId = (dr.IsNull("s_ContractId") ? string.Empty : Convert.ToString(dr["s_ContractId"])),
            //                    s_ContractCategory = (dr.IsNull("s_ContractCategory") ? string.Empty : Convert.ToString(dr["s_ContractCategory"])),
            //                    s_ContractStatus = (dr.IsNull("s_ContractStatus") ? string.Empty : Convert.ToString(dr["s_ContractStatus"])),
            //                    dt_Expiry_Date = (dr.IsNull("dt_Expiry_Date") ? "" : Convert.ToString(dr["dt_Expiry_Date"])),
            //                    s_Country = (dr.IsNull("s_Country") ? string.Empty : Convert.ToString(dr["s_Country"])),

            //                });
            //            _Contract_Details = new Contract_Details()
            //            {

            //                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),

            //                listcsd = lstcsd,
            //                contlist = ctlist
            //                //pmlist = piList,
            //                //ccdlist = ccdlist,
            //                //pjctmList = pjmasterlist

            //            };
            //        }
            //    }
            //    catch (Exception e) { }
            //    return _Contract_Details;
            //}
        }
        public Contract_Details GetContractDeta(int ID, int ProjectId = 0)
        {

            return Contract.GetContractDeta(ID, ProjectId);
            //Contract_Details _Contract_Details = new Contract_Details();
            //{
            //    try
            //    {
            //        DataHelper _helper = new DataHelper();
            //        _helper.InitializedHelper();
            //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //        parameter[parameter.Count - 1].Value = "GetContractDetails";
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //        parameter[parameter.Count - 1].Value = ID;
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
            //        parameter[parameter.Count - 1].Value = ProjectId;
            //        DataTable ProjectsData = new DataTable();
            //        ProjectsData = _helper.GetData("dbo.spContract_DetailsDML", parameter);
            //        List<SelectedCollborators_Details> lstSelCollab = new List<SelectedCollborators_Details>();
            //        List<Selected_Clause_Details> lstSelClause = new List<TTSH.Entity.Selected_Clause_Details>();
            //        List<ContractDetails_MultipleContractFile> lstmultiple = new List<ContractDetails_MultipleContractFile>();
            //        List<ContractList> ctlist = new List<ContractList>();
            //        List<Contract_Status_Date> lstcsd = new List<Contract_Status_Date>();
            //        foreach (DataRow dr in ProjectsData.Rows)
            //        {



            //            string XMLclauses = (dr.IsNull("CLAUSES")) == true ? "" : Convert.ToString(dr["CLAUSES"]);
            //            if (XMLclauses != string.Empty)
            //            {
            //                using (XmlReader reader = XmlReader.Create(new StringReader(XMLclauses)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("CLAUSESD/CLAUSES");
            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Selected_Clause_Details selcd = new Selected_Clause_Details();
            //                        if (node["i_Contract_ID"] != null)
            //                            selcd.i_Contract_ID = Convert.ToInt32(node["i_Contract_ID"].InnerText);
            //                        if (node["i_Contract_Clause_ID"] != null)
            //                            selcd.i_Contract_Clause_ID = Convert.ToInt32(node["i_Contract_Clause_ID"].InnerText);
            //                        if (node["s_Status"] != null)
            //                            selcd.s_Status = (node["s_Status"].InnerText);
            //                        if (node["Clause_Name"] != null)
            //                            selcd.Clause_Name = (node["Clause_Name"].InnerText);
            //                        if (node["s_Comments"] != null)
            //                            selcd.s_Comments = (node["s_Comments"].InnerText);
            //                        if (node["s_Proposed_Changes"] != null)
            //                            selcd.s_Proposed_Changes = (node["s_Proposed_Changes"].InnerText);
            //                        lstSelClause.Add(selcd);
            //                    }
            //                }

            //            }
            //            string XMLMultilist = (dr.IsNull("Multiple_Contract_File")) == true ? "" : Convert.ToString(dr["Multiple_Contract_File"]);
            //            if (XMLMultilist != string.Empty)
            //            {
            //                using (XmlReader reader = XmlReader.Create(new StringReader(XMLMultilist)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("MULTIPLE_FILED/Multiple_File");
            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        ContractDetails_MultipleContractFile cdm = new ContractDetails_MultipleContractFile();

            //                        if (node["i_ContractDetailsID"] != null)
            //                            cdm.i_ContractDetailsID = Convert.ToInt32(node["i_ContractDetailsID"].InnerText);
            //                        if (node["s_ContractFile"] != null)
            //                            cdm.s_ContractFile = (node["s_ContractFile"].InnerText);
            //                        lstmultiple.Add(cdm);
            //                    }
            //                }

            //            }
            //            string CoList = (dr.IsNull("COLLABORATOR")) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
            //            if (CoList != string.Empty)
            //            {
            //                using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        SelectedCollborators_Details ccd = new SelectedCollborators_Details();
            //                        if (node["i_ID"] != null)
            //                            ccd.i_Collobrator_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //                        if (node["s_Name"] != null)
            //                            ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);

            //                        lstSelCollab.Add(ccd);
            //                    }
            //                }
            //            }
            //            string StatusDate = (dr.IsNull("Contract_StatusDate")) == true ? "" : Convert.ToString(dr["Contract_StatusDate"]);
            //            if (StatusDate != string.Empty)
            //            {
            //                using (XmlReader reader = XmlReader.Create(new StringReader(StatusDate)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("StatusDateD/StatusDate");


            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Contract_Status_Date csd = new Contract_Status_Date();
            //                        if (node["i_Contract_Status_ID"] != null)
            //                            csd.i_Contract_Status_ID = Convert.ToInt32(node["i_Contract_Status_ID"].InnerText);
            //                        if (node["dt_Status_Date"] != null)
            //                            csd.dt_Status_Date = Convert.ToString(node["dt_Status_Date"].InnerText);

            //                        lstcsd.Add(csd);
            //                    }
            //                }
            //            }
            //            ctlist.Add(new ContractList
            //            {
            //                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                s_Contract_Name = (dr.IsNull("s_Contract_Name") ? string.Empty : Convert.ToString(dr["s_Contract_Name"])),
            //                s_ContractId = (dr.IsNull("s_ContractId") ? string.Empty : Convert.ToString(dr["s_ContractId"])),
            //                s_ContractCategory = (dr.IsNull("s_ContractCategory") ? string.Empty : Convert.ToString(dr["s_ContractCategory"])),
            //                s_ContractStatus = (dr.IsNull("s_ContractStatus") ? string.Empty : Convert.ToString(dr["s_ContractStatus"])),
            //                dt_Expiry_Date = (dr.IsNull("dt_Expiry_Date") ? "" : Convert.ToString(dr["dt_Expiry_Date"])),
            //                s_Country = (dr.IsNull("s_Country") ? string.Empty : Convert.ToString(dr["s_Country"])),

            //            });
            //            _Contract_Details = new Contract_Details()
            //            {

            //                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
            //                s_Contract_Name = (dr.IsNull("s_Contract_Name") ? string.Empty : Convert.ToString(dr["s_Contract_Name"])),
            //                s_Contract_Display_Id = (dr.IsNull("s_Contract_Display_Id") ? string.Empty : Convert.ToString(dr["s_Contract_Display_Id"])),
            //                i_Contract_Category_ID = (dr.IsNull("i_Contract_Category_ID") ? 0 : Convert.ToInt32(dr["i_Contract_Category_ID"])),
            //                i_Contract_Status_ID = (dr.IsNull("i_Contract_Status_ID") ? 0 : Convert.ToInt32(dr["i_Contract_Status_ID"])),
            //                dt_LastUpdated_Date = (dr.IsNull("dt_LastUpdated_Date") ? "" : Convert.ToString(dr["dt_LastUpdated_Date"])),
            //                i_Govt_Lawcountry = (dr.IsNull("i_Govt_Lawcountry") ? 0 : Convert.ToInt32(dr["i_Govt_Lawcountry"])),
            //                s_Clauses_File = (dr.IsNull("s_Clauses_File") ? string.Empty : Convert.ToString(dr["s_Clauses_File"])),
            //                //s_UploadedContract_File = ( dr.IsNull("s_UploadedContract_File") ? string.Empty : Convert.ToString(dr["s_UploadedContract_File"]) ),
            //                dt_Effective_Date = (dr.IsNull("dt_Effective_Date") ? "" : Convert.ToString(dr["dt_Effective_Date"])),
            //                dt_Finalization_Date = (dr.IsNull("dt_Finalization_Date") ? "" : Convert.ToString(dr["dt_Finalization_Date"])),
            //                dt_LastSigned_Date = (dr.IsNull("dt_LastSigned_Date") ? "" : Convert.ToString(dr["dt_LastSigned_Date"])),
            //                dt_Expiry_Date = (dr.IsNull("dt_Expiry_Date") ? "" : Convert.ToString(dr["dt_Expiry_Date"])),
            //                b_Amendments = (dr.IsNull("b_Amendments") ? false : Convert.ToBoolean(dr["b_Amendments"])),
            //                dt_NewExpiry_Date = (dr.IsNull("dt_NewExpiry_Date") ? "" : Convert.ToString(dr["dt_NewExpiry_Date"])),
            //                s_AmendmenstContract_File = (dr.IsNull("s_AmendmenstContract_File") ? string.Empty : Convert.ToString(dr["s_AmendmenstContract_File"])),
            //                s_Country = (dr.IsNull("s_Country") ? string.Empty : Convert.ToString(dr["s_Country"])),
            //                i_Currency_ID = (dr.IsNull("i_Currency_ID") ? 0 : Convert.ToInt32(dr["i_Currency_ID"])),
            //                i_Hospital_Cost = (dr.IsNull("i_Hospital_Cost") ? 0 : Convert.ToInt32(dr["i_Hospital_Cost"])),
            //                i_Investigator_fees = (dr.IsNull("i_Investigator_fees") ? 0 : Convert.ToInt32(dr["i_Investigator_fees"])),
            //                i_Coordinator_fess = (dr.IsNull("i_Coordinator_fess") ? 0 : Convert.ToInt32(dr["i_Coordinator_fess"])),
            //                //dt_Contract_StatusDate = ( dr.IsNull("dt_Status_Date") ? string.Empty : Convert.ToString(dr["dt_Status_Date"]) ),
            //                contlist = ctlist,
            //                lstSelCollab = lstSelCollab,
            //                lstSelClause = lstSelClause,
            //                lstmultiple = lstmultiple,
            //                listcsd = lstcsd
            //            };
            //        }
            //    }
            //    catch (Exception e) { }
            //    return _Contract_Details;
            // }
        }
        public Contract_Master GetContract_MasterDetailsByID(int ID)
        {
            return Contract.GetContract_MasterDetailsByID(ID);
            //Contract_Master _Contract_Master = new Contract_Master();
            //{
            //    try
            //    {
            //        DataHelper _helper = new DataHelper();
            //        _helper.InitializedHelper();
            //        List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //        parameter[parameter.Count - 1].Value = "select";
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //        parameter[parameter.Count - 1].Value = ID;
            //        DataTable ProjectsData = new DataTable();
            //        List<PI_Master> piList = new List<PI_Master>();
            //        List<Contract_Collobrator_Master> ccdlist = new List<Contract_Collobrator_Master>();
            //        List<Project_Master> pjmasterlist = new List<TTSH.Entity.Project_Master>();
            //        ProjectsData = _helper.GetData("dbo.[spContract_MasterDML]", parameter);
            //        foreach (DataRow dr in ProjectsData.Rows)
            //        {

            //            string xmlTestManager = (dr.IsNull("DEPT_PI")) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
            //            if (xmlTestManager != string.Empty)
            //            {
            //                //--PI_M.i_Dept_ID,PI_M.i_ID,PI_M.s_Email,PI_M.s_Phone_no,PI_M.s_MCR_No

            //                using (XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        PI_Master pi = new PI_Master();
            //                        if (node["i_Dept_ID"] != null)
            //                            pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
            //                        if (node["i_ID"] != null)
            //                            pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //                        if (node["s_Email"] != null)
            //                            pi.s_Email = (node["s_Email"].InnerText);
            //                        if (node["s_Phone_no"] != null)
            //                            pi.s_Phone_no = (node["s_Phone_no"].InnerText);
            //                        if (node["s_MCR_No"] != null)
            //                            pi.s_MCR_No = (node["s_MCR_No"].InnerText);
            //                        if (node["Dept_Name"] != null)
            //                            pi.s_DeptName = (node["Dept_Name"].InnerText);
            //                        if (node["s_PI_Name"] != null)
            //                            pi.s_PIName = (node["s_PI_Name"].InnerText);
            //                        piList.Add(pi);
            //                    }
            //                }

            //            }
            //            string CoList = (dr.IsNull("COLLABORATOR")) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
            //            if (CoList != string.Empty)
            //            {
            //                //-- PCD.s_Coordinator_name,pcd.i_Coordinator_ID

            //                using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Contract_Collobrator_Master ccd = new Contract_Collobrator_Master();
            //                        if (node["i_ID"] != null)
            //                            ccd.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //                        if (node["s_Name"] != null)
            //                            ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
            //                        if (node["s_Email1"] != null)
            //                            ccd.s_Email1 = Convert.ToString(node["s_Email1"].InnerText);
            //                        if (node["s_Email2"] != null)
            //                            ccd.s_Email2 = Convert.ToString(node["s_Email2"].InnerText);
            //                        if (node["s_Institution"] != null)
            //                            ccd.s_Institution = Convert.ToString(node["s_Institution"].InnerText);
            //                        if (node["s_Country_Name"] != null)
            //                            ccd.Country_Name = Convert.ToString(node["s_Country_Name"].InnerText);
            //                        if (node["i_Country_ID"] != null)
            //                            ccd.i_Country_ID = Convert.ToInt32(node["i_Country_ID"].InnerText);
            //                        if (node["dt_Contract_Request_Date"] != null)
            //                            ccd.s_date = Convert.ToString(node["dt_Contract_Request_Date"].InnerText);
            //                        if (node["s_InitialContract_ID"] != null)
            //                            ccd.s_initialId = Convert.ToString(node["s_InitialContract_ID"].InnerText);
            //                        if (node["s_PhoNo"] != null)
            //                            ccd.s_PhoNo = Convert.ToString(node["s_PhoNo"].InnerText);
            //                        ccdlist.Add(ccd);
            //                    }
            //                }
            //            }
            //            string PJmasterList = (dr.IsNull("PROJECT_DATA")) == true ? "" : Convert.ToString(dr["PROJECT_DATA"]);
            //            if (PJmasterList != string.Empty)
            //            {

            //                using (XmlReader reader = XmlReader.Create(new StringReader(PJmasterList)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/PROJECT_DATA");

            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Project_Master pmMaster = new Project_Master();
            //                        if (node["ProjmID"] != null)
            //                            pmMaster.i_ID = Convert.ToInt32(node["ProjmID"].InnerText);
            //                        if (node["s_Project_Title"] != null)
            //                            pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
            //                        if (node["s_Display_Project_ID"] != null)
            //                            pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
            //                        if (node["s_Short_Title"] != null)
            //                            pmMaster.s_Short_Title = Convert.ToString(node["s_Short_Title"].InnerText);
            //                        if (node["Project_Category_Name"] != null)
            //                            pmMaster.Project_Category_Name = Convert.ToString(node["Project_Category_Name"].InnerText);
            //                        if (node["s_Project_Alias1"] != null)
            //                            pmMaster.s_Project_Alias1 = Convert.ToString(node["s_Project_Alias1"].InnerText);
            //                        if (node["s_Project_Alias2"] != null)
            //                            pmMaster.s_Project_Alias2 = Convert.ToString(node["s_Project_Alias2"].InnerText);
            //                        if (node["s_IRB_No"] != null)
            //                            pmMaster.s_IRB_No = Convert.ToString(node["s_IRB_No"].InnerText);

            //                        pjmasterlist.Add(pmMaster);
            //                    }
            //                }

            //            }
            //            _Contract_Master = new Contract_Master()
            //            {


            //                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
            //                i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
            //                dt_Contract_ReqDate = (dr.IsNull("dt_Contract_ReqDate") ? "" : Convert.ToString(dr["dt_Contract_ReqDate"])),
            //                dt_Contract_AssignDate = (dr.IsNull("dt_Contract_AssignDate") ? "" : Convert.ToString(dr["dt_Contract_AssignDate"])),
            //                i_ReviewedBy_ID = (dr.IsNull("i_ReviewedBy_ID") ? 0 : Convert.ToInt32(dr["i_ReviewedBy_ID"])),
            //                S_ReviewedByName = (dr.IsNull("S_ReviewedByName") ? string.Empty : Convert.ToString(dr["S_ReviewedByName"])),
            //                pmlist = piList,
            //                ccdlist = ccdlist,
            //                pjctmList = pjmasterlist


            //            };
            //        }
            //    }
            //    catch (Exception e) { }
            //    return _Contract_Master;
            //}
        }
        public ProjectDataforContractUsers FillProjectDataforContractUsers(int ID)
        {
            return Contract.FillProjectDataforContractUsers(ID);
            //ProjectDataforContractUsers pdclist = new ProjectDataforContractUsers();
            //DataHelper _helper = new DataHelper();
            //List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //DataTable ProjectsData = new DataTable();
            //List<PI_Master> piList = new List<PI_Master>();
            //List<Contract_Collobrator_Master> ccdlist = new List<Contract_Collobrator_Master>();
            //List<Project_Master> pjmasterlist = new List<Project_Master>();

            //try
            //{

            //    _helper.InitializedHelper();

            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = "ByProjectId";
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = ID;
            //    ProjectsData = _helper.GetData("dbo.[spContract_DetailsDML]", parameter);
            //    foreach (DataRow dr in ProjectsData.Rows)
            //    {

            //        string xmlTestManager = (dr.IsNull("DEPT_PI")) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
            //        if (xmlTestManager != string.Empty)
            //        {

            //            using (XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)))
            //            {
            //                XmlDocument xml = new XmlDocument();
            //                xml.Load(reader);
            //                XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
            //                foreach (XmlNode node in xmlNodeList)
            //                {
            //                    PI_Master pi = new PI_Master();

            //                    if (node["i_Dept_ID"] != null)
            //                        pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
            //                    if (node["i_ID"] != null)
            //                        pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //                    if (node["s_Email"] != null)
            //                        pi.s_Email = (node["s_Email"].InnerText);
            //                    if (node["s_Phone_no"] != null)
            //                        pi.s_Phone_no = (node["s_Phone_no"].InnerText);
            //                    if (node["s_MCR_No"] != null)
            //                        pi.s_MCR_No = (node["s_MCR_No"].InnerText);
            //                    if (node["Dept_Name"] != null)
            //                        pi.s_DeptName = (node["Dept_Name"].InnerText);
            //                    if (node["s_PI_Name"] != null)
            //                        pi.s_PIName = (node["s_PI_Name"].InnerText);
            //                    piList.Add(pi);
            //                }
            //            }

            //        }
            //        string CoList = (dr.IsNull("COLLABORATOR")) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
            //        if (CoList != string.Empty)
            //        {

            //            using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
            //            {
            //                XmlDocument xml = new XmlDocument();
            //                xml.Load(reader);
            //                XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


            //                foreach (XmlNode node in xmlNodeList)
            //                {
            //                    Contract_Collobrator_Master ccd = new Contract_Collobrator_Master();
            //                    if (node["i_ID"] != null)
            //                        ccd.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
            //                    if (node["s_Name"] != null)
            //                        ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
            //                    if (node["s_Email1"] != null)
            //                        ccd.s_Email1 = Convert.ToString(node["s_Email1"].InnerText);
            //                    if (node["s_Email2"] != null)
            //                        ccd.s_Email2 = Convert.ToString(node["s_Email2"].InnerText);
            //                    if (node["s_Institution"] != null)
            //                        ccd.s_Institution = Convert.ToString(node["s_Institution"].InnerText);
            //                    if (node["s_Country_Name"] != null)
            //                        ccd.Country_Name = Convert.ToString(node["s_Country_Name"].InnerText);
            //                    if (node["i_Country_ID"] != null)
            //                        ccd.i_Country_ID = Convert.ToInt32(node["i_Country_ID"].InnerText);
            //                    if (node["dt_Contract_Request_Date"] != null)
            //                        ccd.s_date = Convert.ToString(node["dt_Contract_Request_Date"].InnerText);
            //                    if (node["s_InitialContract_ID"] != null)
            //                        ccd.s_initialId = Convert.ToString(node["s_InitialContract_ID"].InnerText);
            //                    if (node["s_PhoNo"] != null)
            //                        ccd.s_PhoNo = Convert.ToString(node["s_PhoNo"].InnerText);
            //                    ccdlist.Add(ccd);
            //                }
            //            }

            //            string PJmasterList = (dr.IsNull("PROJECT_DATA")) == true ? "" : Convert.ToString(dr["PROJECT_DATA"]);
            //            if (CoList != string.Empty)
            //            {

            //                using (XmlReader reader = XmlReader.Create(new StringReader(PJmasterList)))
            //                {
            //                    XmlDocument xml = new XmlDocument();
            //                    xml.Load(reader);
            //                    XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/PROJECT_DATA");

            //                    foreach (XmlNode node in xmlNodeList)
            //                    {
            //                        Project_Master pmMaster = new Project_Master();
            //                        if (node["ProjmID"] != null)
            //                            pmMaster.i_ID = Convert.ToInt32(node["ProjmID"].InnerText);
            //                        if (node["s_Project_Title"] != null)
            //                            pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
            //                        if (node["s_Display_Project_ID"] != null)
            //                            pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
            //                        if (node["s_Short_Title"] != null)
            //                            pmMaster.s_Short_Title = Convert.ToString(node["s_Short_Title"].InnerText);
            //                        if (node["Project_Category_Name"] != null)
            //                            pmMaster.Project_Category_Name = Convert.ToString(node["Project_Category_Name"].InnerText);
            //                        if (node["s_Project_Alias1"] != null)
            //                            pmMaster.s_Project_Alias1 = Convert.ToString(node["s_Project_Alias1"].InnerText);
            //                        if (node["s_Project_Alias2"] != null)
            //                            pmMaster.s_Project_Alias2 = Convert.ToString(node["s_Project_Alias2"].InnerText);
            //                        if (node["s_IRB_No"] != null)
            //                            pmMaster.s_IRB_No = Convert.ToString(node["s_IRB_No"].InnerText);

            //                        pjmasterlist.Add(pmMaster);
            //                    }
            //                }

            //            }

            //        }
            //        pdclist = new ProjectDataforContractUsers()
            //        {

            //            Pilisst = piList,
            //            ccmlist = ccdlist,
            //            pmlist = pjmasterlist


            //        };
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //return pdclist;
        }
        public List<Contract_Master> FillGrid_Contract_Master()
        {
            return Contract.FillGrid_Contract_Master();
            //List<Contract_Master> pm = new List<Contract_Master>();

            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = "select";

            //    DataTable gridData = new DataTable();
            //    gridData = _helper.GetData("[dbo].[spContract_MasterDML]", parameter);

            //    foreach (DataRow dr in gridData.Rows)
            //    {
            //        pm.Add(new Contract_Master
            //        {
            //            i_ID = (dr["Contract_ID"] != DBNull.Value) ? Convert.ToInt32(dr["Contract_ID"]) : 0,
            //            i_Project_ID = (dr["i_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_ID"]) : 0,
            //            s_Display_Project_ID = (dr["s_Display_Project_ID"] != DBNull.Value) ? Convert.ToString(dr["s_Display_Project_ID"]) : "",
            //            s_Project_Title = (dr["s_Project_Title"] != DBNull.Value) ? Convert.ToString(dr["s_Project_Title"]) : "",
            //            Project_Category_Name = (dr["Project_Category_Name"] != DBNull.Value) ? Convert.ToString(dr["Project_Category_Name"]) : "",
            //            s_IRB_No = (dr["s_IRB_No"] != DBNull.Value) ? Convert.ToString(dr["s_IRB_No"]) : "",
            //            Project_Type = (dr["Project_Type"] != DBNull.Value) ? Convert.ToString(dr["Project_Type"]) : "",
            //            PI_NAME = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
            //            Status = (dr["Status"] != DBNull.Value) ? Convert.ToString(dr["Status"]) : "",
            //            ContAppStatus = (dr["ContAppStatus"] != DBNull.Value) ? Convert.ToString(dr["ContAppStatus"]) : ""

            //        });
            //    }
            //}
            //catch (Exception)
            //{


            //}

            //return pm;


        }
        public List<Contract_Details> FillGrid_Contract_Details()
        {
            return Contract.FillGrid_Contract_Details();
            //List<Contract_Details> cd = new List<Contract_Details>();

            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = "select";

            //    DataTable gridData = new DataTable();
            //    gridData = _helper.GetData("[dbo].[spContract_DetailsDML]", parameter);

            //    foreach (DataRow dr in gridData.Rows)
            //    {
            //        cd.Add(new Contract_Details
            //        {
            //            //i_ID = ( dr["ContractDetail_ID"] != DBNull.Value ) ? Convert.ToInt32(dr["ContractDetail_ID"]) : 0,
            //            i_Project_ID = (dr["i_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_ID"]) : 0,
            //            s_Display_Project_ID = (dr["s_Display_Project_ID"] != DBNull.Value) ? Convert.ToString(dr["s_Display_Project_ID"]) : "",
            //            s_Project_Title = (dr["s_Project_Title"] != DBNull.Value) ? Convert.ToString(dr["s_Project_Title"]) : "",
            //            Project_Category_Name = (dr["Project_Category_Name"] != DBNull.Value) ? Convert.ToString(dr["Project_Category_Name"]) : "",
            //            s_IRB_No = (dr["s_IRB_No"] != DBNull.Value) ? Convert.ToString(dr["s_IRB_No"]) : "",
            //            Project_Type = (dr["Project_Type"] != DBNull.Value) ? Convert.ToString(dr["Project_Type"]) : "",
            //            PI_NAME = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
            //            Contract_Status = (dr["Contract_Status"] != DBNull.Value) ? Convert.ToString(dr["Contract_Status"]) : "",
            //            Contracts = (dr["Description"] != DBNull.Value) ? Convert.ToString(dr["Description"]) : ""

            //        });
            //    }
            //}
            //catch (Exception)
            //{


            //}

            //return cd;

        }
        public string GetCollobrator_MasterDetailByID(int ID)
        {
            return Contract.GetCollobrator_MasterDetailByID(ID);
            //List<object> lst = new List<object>();
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = "select";
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = ID;
            //    DataTable ProjectsData = new DataTable();
            //    ProjectsData = _helper.GetData("dbo.spContract_Collobrator_MasterDML", parameter);
            //    foreach (DataRow dr in ProjectsData.Rows)
            //    {
            //        Contract_Collobrator_Master cm = new Contract_Collobrator_Master();


            //        cm.i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"]));
            //        cm.s_Name = (dr.IsNull("s_Name") ? string.Empty : Convert.ToString(dr["s_Name"]));
            //        cm.s_Email1 = (dr.IsNull("s_Email1") ? string.Empty : Convert.ToString(dr["s_Email1"]));
            //        cm.s_Email2 = (dr.IsNull("s_Email2") ? string.Empty : Convert.ToString(dr["s_Email2"]));
            //        cm.s_Institution = (dr.IsNull("s_Institution") ? string.Empty : Convert.ToString(dr["s_Institution"]));
            //        cm.s_PhoNo = (dr.IsNull("s_PhoNo") ? string.Empty : Convert.ToString(dr["s_PhoNo"]));
            //        cm.i_Country_ID = (dr.IsNull("i_Country_ID") ? 0 : Convert.ToInt32(dr["i_Country_ID"]));
            //        cm.Country_Name = (dr.IsNull("Country_Name") ? string.Empty : Convert.ToString(dr["Country_Name"]));
            //        lst.Add(cm);

            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //return (new JavaScriptSerializer().Serialize(lst));
        }

        public string Contract_Collobrator_Master_DML(Contract_Collobrator_Master _Contract_Collobrator_Master, string mode)
        {
            return Contract.Contract_Collobrator_Master_DML(_Contract_Collobrator_Master, mode);
            //string result = "";
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = mode.ToString();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.i_ID;
            //    if (mode.ToString() != "Delete")
            //    {



            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Name";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Name;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Email1";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Email1;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Email2";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Email2;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_PhoNo";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_PhoNo;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Institution";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Institution;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Country_ID";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.i_Country_ID;



            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@UserCId";
            //        parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_CreatedBy_ID;



            //    }
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
            //    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
            //    parameter[parameter.Count - 1].Size = 500;
            //    if (Convert.ToBoolean(_helper.DMLOperation("dbo.spContract_Collobrator_MasterDML", parameter)))
            //    {
            //        result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
            //    }
            //    else
            //    {
            //        result = parameter[parameter.Count - 1].Value.ToString();
            //    }
            //}
            //catch (Exception ex) { }
            //return result;
        }
        //--================================================END OF Properties===============================================================================
        public string Contract_Details_DML(Contract_Details _Contract_Details, List<SelectedCollborators_Details> lstSelCollab, List<Selected_Clause_Details> lstSelClause, List<ContractDetails_MultipleContractFile> lstmultiple, string mode)
        {
            return Contract.Contract_Details_DML(_Contract_Details, lstSelCollab, lstSelClause, lstmultiple, mode);
            //string result = "";
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = mode.ToString();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = _Contract_Details.i_ID;
            //    if (mode.ToString() != "Delete")
            //    {


            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Project_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Contract_Name";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.s_Contract_Name;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Contract_Display_Id";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.s_Contract_Display_Id;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Contract_Category_ID";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Contract_Category_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Contract_Status_ID";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Contract_Status_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_LastUpdated_Date";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_LastUpdated_Date;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Govt_Lawcountry";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Govt_Lawcountry;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_Clauses_File";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.s_Clauses_File;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_UploadedContract_File";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.s_UploadedContract_File;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_Effective_Date";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_Effective_Date;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_Finalization_Date";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_Finalization_Date;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_LastSigned_Date";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_LastSigned_Date;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_Expiry_Date";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_Expiry_Date;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@b_Amendments";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.b_Amendments;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_NewExpiry_Date";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_NewExpiry_Date;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_AmendmenstContract_File";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.s_AmendmenstContract_File;


            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Hospital_Cost";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Hospital_Cost;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Investigator_fees";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Investigator_fees;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Coordinator_fess";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.i_Coordinator_fess;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@UserCId";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.s_CreatedBy_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@Statusdate";
            //        parameter[parameter.Count - 1].Value = _Contract_Details.dt_Contract_StatusDate;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@SelectedCollborators_Details";
            //        parameter[parameter.Count - 1].Value = lstSelCollab.ListToDatatable().getColumns(1);
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@Selected_Clause_Details";
            //        parameter[parameter.Count - 1].Value = lstSelClause.ListToDatatable().getColumns(4);
            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@ContractDetails_MultipleContractFile";
            //        parameter[parameter.Count - 1].Value = lstmultiple.ListToDatatable();




            //    }
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
            //    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
            //    parameter[parameter.Count - 1].Size = 500;
            //    if (Convert.ToBoolean(_helper.DMLOperation("dbo.spContract_DetailsDML", parameter)))
            //    {
            //        result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
            //    }
            //    else
            //    {
            //        result = parameter[parameter.Count - 1].Value.ToString();
            //    }
            //}
            //catch (Exception ex) { }
            //return result;
        }
        //--================================================END OF Properties===============================================================================
        public string Contract_Master_DML(Contract_Master _Contract_Master, List<Contract_Collaborator_Details> clist, string mode)
        {
            return Contract.Contract_Master_DML(_Contract_Master, clist, mode);
            //string result = "";
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = mode.ToString();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@i_ID";
            //    parameter[parameter.Count - 1].Value = _Contract_Master.i_ID;
            //    if (mode.ToString() != "Delete")
            //    {



            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
            //        parameter[parameter.Count - 1].Value = _Contract_Master.i_Project_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_Contract_ReqDate";
            //        parameter[parameter.Count - 1].Value = _Contract_Master.dt_Contract_ReqDate;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@dt_Contract_AssignDate";
            //        parameter[parameter.Count - 1].Value = _Contract_Master.dt_Contract_AssignDate;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ReviewedBy_ID";
            //        parameter[parameter.Count - 1].Value = _Contract_Master.i_ReviewedBy_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@S_ReviewedByName";
            //        parameter[parameter.Count - 1].Value = _Contract_Master.S_ReviewedByName;



            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@UserCId";
            //        parameter[parameter.Count - 1].Value = _Contract_Master.s_CreatedBy_ID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@Contract_Collaborator_Details";
            //        parameter[parameter.Count - 1].Value = clist.ListToDatatable();


            //    }
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
            //    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
            //    parameter[parameter.Count - 1].Size = 500;
            //    if (Convert.ToBoolean(_helper.DMLOperation("dbo.spContract_MasterDML", parameter)))
            //    {
            //        result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
            //    }
            //    else
            //    {
            //        result = parameter[parameter.Count - 1].Value.ToString();
            //    }
            //}
            //catch (Exception ex) { }
            //return result;
        }
        //--================================================END OF Properties===============================================================================
        public string ContractDetails_MultipleContractFile_DML(ContractDetails_MultipleContractFile _ContractDetails_MultipleContractFile, string mode)
        {
            return Contract.ContractDetails_MultipleContractFile_DML(_ContractDetails_MultipleContractFile, mode);
            //string result = "";
            //try
            //{
            //    DataHelper _helper = new DataHelper();
            //    _helper.InitializedHelper();
            //    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@StatementType";
            //    parameter[parameter.Count - 1].Value = mode.ToString();

            //    if (mode.ToString() != "Delete")
            //    {

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@i_ContractDetailsID";
            //        parameter[parameter.Count - 1].Value = _ContractDetails_MultipleContractFile.i_ContractDetailsID;

            //        parameter.Add(_helper.CreateDbParameter());
            //        parameter[parameter.Count - 1].ParameterName = "@s_ContractFile";
            //        parameter[parameter.Count - 1].Value = _ContractDetails_MultipleContractFile.s_ContractFile;


            //    }
            //    parameter.Add(_helper.CreateDbParameter());
            //    parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
            //    parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
            //    parameter[parameter.Count - 1].Size = 500;
            //    if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
            //    {
            //        result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
            //    }
            //    else
            //    {
            //        result = parameter[parameter.Count - 1].Value.ToString();
            //    }
            //}
            //catch (Exception ex) { }
            //return result;
        }
        #endregion

        #region Excel Export Methods

        public void ExportToExcel()
        {

            string filename = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "ExportFile\\TTSH" + "_" + Guid.NewGuid() + ".xlsx";


            /*Temp data table*/
            DataTable dtReportData = new DataTable();
            dtReportData.Columns.Add("FirstName", typeof(string));
            dtReportData.Columns.Add("LastName", typeof(string));
            dtReportData.Columns.Add("Age", typeof(int));
            dtReportData.Columns.Add("Email", typeof(string));
            dtReportData.Columns.Add("Contact", typeof(string));
            dtReportData.Columns.Add("Address", typeof(string));

            dtReportData.Rows.Add("Ejaz", "Waquif", 26, "mohd.ejaz@rgensolutions.com", "8446347114", "Nagpur");
            dtReportData.Rows.Add("Atul", "Sirsode", 25, "atul@rgensolutions.com", "844447114", "Nagpur");

            /*Temp data table*/


            TTSHWCFLayer.Excel.ExportExcel objFile = new Excel.ExportExcel();

            List<EntityDynamicExcel> listExcelInfo = new List<EntityDynamicExcel>();

            List<RowData> listRowData = new List<RowData>();

            List<CellData> listData = new List<CellData>();

            CellData objCell = new CellData();


            int columnCount = dtReportData.Columns.Count;

            listData = new List<CellData>();
            RowData objRowPrjId = new RowData();
            objRowPrjId.rowIndex = 1;
            objRowPrjId.rowCellStartIndex = 1;
            objRowPrjId.rowCellEndIndex = columnCount;

            for (int i = 0; i < columnCount; i++)
            {
                objCell = new CellData();
                objCell.cellName = (CellName)i + 1;
                objCell.cellType = CellType.textBlueFill;
                objCell.cellValue = dtReportData.Columns[i].ToString();
                listData.Add(objCell);
            }

            objRowPrjId.listData = listData;
            listRowData.Add(objRowPrjId);
            List<SheetColumns> listSheet1Columns = new List<SheetColumns>();

            if (dtReportData != null && dtReportData.Rows.Count > 0)
            {
                for (int i = 0; i < dtReportData.Rows.Count; i++)
                {
                    int index = i + 1;
                    //sheetName Project-projectId Yellow records
                    listData = new List<CellData>();
                    RowData objRowPrjId1 = new RowData();
                    objRowPrjId1.rowIndex = index + 1;
                    objRowPrjId1.rowCellStartIndex = 1;
                    objRowPrjId1.rowCellEndIndex = columnCount;


                    for (int j = 0; j < columnCount; j++)
                    {
                        objCell = new CellData();
                        objCell.cellName = (CellName)j + 1;
                        objCell.cellType = CellType.textNoFill;
                        objCell.cellValue = dtReportData.Rows[i].ItemArray[j].ToString();
                        listData.Add(objCell);

                        #region To Set Sheet Column Width
                        SheetColumns SheetColumns1 = new SheetColumns();
                        SheetColumns1.columnIndex = j + 1;
                        SheetColumns1.columnWidth = 30;

                        listSheet1Columns.Add(SheetColumns1);
                        #endregion
                    }

                    objRowPrjId1.listData = listData;
                    listRowData.Add(objRowPrjId1);


                }
            }

            #region finalExportObject for Sheet1
            listExcelInfo.Add(new EntityDynamicExcel()
            {
                sheetName = "Project-" + "Details",
                sheetDimensionStart = "A1",
                sheetDimensionEnd = "B5",
                isSheetProtected = false,
                password = "",
                listRowData = listRowData,
                listSheetColumns = listSheet1Columns
            });
            #endregion


            Excel.GenerateDynamicExcel obj = new Excel.GenerateDynamicExcel();
            obj.CreatePackage(filename, listExcelInfo);

            //download file created                    
            //return objFile.DownloadSampleFile(filename);

            System.IO.Stream stream = System.IO.File.OpenRead(filename);

            try
            {
                //filePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "SampleTemplate\\AnalysisTemplate.xlsx";

                //get the orginal name of Excel Template from which it is copied by removing guid appended in copied template file
                int pos = Path.GetFileNameWithoutExtension(filename).LastIndexOf(@"_");
                string FileName = Path.GetFileNameWithoutExtension(filename).Substring(0, pos) + Path.GetExtension(filename);

                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;

                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                response.Headers.Add("Cache-Control", "private");
                response.Headers.Add("X-Download-Options", "noopen");

                response.Headers.Add("Content-Length", stream.Length.ToString());

                //response.Headers.Add("Content-Disposition", "attachment; filename=AnalysisTemplate.xlsx");
                response.Headers.Add("Content-Disposition", string.Format(@"attachment;filename={0}", FileName));

                //response.TransmitFile(FileName);



            }
            catch (Exception ex)
            {

            }

        }

        #endregion

        #region Search
        public Dictionary<string, List<Search>> GetSearchData(string SearchInputValue, string SearchFilterCriteria, string UserID, string UserGroup)
        {
            return SearchBL.GetSearchData(SearchInputValue, SearchFilterCriteria, UserID, UserGroup);
        }
        #endregion

        #region Audit Methods
        public List<Audit> FillGrid_Audit(DateTime FromDate, DateTime ToDate)
        {
            return AuditBL.FillGrid_Audit(FromDate, ToDate);
        }

        #endregion


        #region UserAccessRights
        /// <summary>
        /// Get All The Menus Along With Access RIghts
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public UserMenuRights GetAllMenus(string roleName)
        {
            try
            {
                return RoleAccessManagement.GetAllMenus(roleName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// USed For Saving The Access Rights
        /// </summary>
        /// <param name="menuxml"></param>
        /// <param name="roleid"></param>
        /// <param name="createdby"></param>
        /// <returns></returns>
        public bool SaveAccess(string menuxml, int roleid, int createdby = 0)
        {
            try
            {
                return RoleAccessManagement.SaveAccess(menuxml, roleid, createdby);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion
        public string HelloWorld()
        {
            return "Hello there";
        }

        #region RptSelectedReport

        public List<Selected_Grid> Selected_FillGrid(string userID, bool isSelectedTeamUser)
        {
            return Selected.Selected_FillGridBAL(userID, isSelectedTeamUser);
        }
        
        public List<RptSelectedProject> GetProjectDetails(string Sdate, string Edate)
        {
            return RptSelectedProjectChart.GetProjectDetails(Sdate, Edate);
        }
        #endregion


        #region Reporting Tool 
        public DataSet GetSelectedTables(int ModuleID, string SelectedTablesID)
        {
            return ReportingTool.GetSelectedTables(ModuleID, SelectedTablesID);
        }
        #endregion


        #region Document Managemnet System
        public DocumentManagementSystem GetDocumentWithProject(int projectId)
        {
            try
            {
                return TTSH.BusinessLogic.DocumentManagement.GetDocumentWithProject(projectId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DocumentManagementSystemFile> GetDocuments(string searchText)
        {
            return TTSH.BusinessLogic.DocumentManagement.GetDocuments(searchText);
        }


        public bool SaveDocumentData(List<DMS_DocumentManagementSystem> docManSys)
        {
            return TTSH.BusinessLogic.DocumentManagement.SaveDocumentData(docManSys);
        }
        #endregion

        #region ReportPIByDept
        public List<RptProjectCategory> ListRptProjectCategory()
        {
            return ReportsBL.ListRptProjectCategory();
        }

        public List<RptProjectType> ListRptProjectType()
        {
            return ReportsBL.ListRptProjectType();
        }

        public List<RptDepartment> ListRptDepartment()
        {
            return ReportsBL.ListRptDepartment();
        }

        public List<RptPIName> ListRptPIName()
        {
            return ReportsBL.ListRptPIName();
        }

        public List<RptPIName> ListRptPINameByDepartment(String DepartmentId)
        {
            return ReportsBL.ListRptPINameByDepartment(DepartmentId);
        }

        #endregion

        public static Guid ConvertOctetStringToGuid(String gUID)
        {
            String pattern = @"^(?i)[0-9A-F]{32}";
            Guid gd = new Guid();
            if (Regex.IsMatch(gUID, pattern))
            {
                UInt32 a = Convert.ToUInt32((gUID.Substring(6, 2) +
                    gUID.Substring(4, 2) + gUID.Substring(2, 2) + gUID.Substring(0, 2)), 16);

                UInt16 b = Convert.ToUInt16((gUID.Substring(10, 2) + gUID.Substring(8, 2)), 16);
                UInt16 c = Convert.ToUInt16((gUID.Substring(14, 2) + gUID.Substring(12, 2)), 16);

                Byte d = (Byte)Convert.ToUInt16(gUID.Substring(16, 2), 16);
                Byte e = (Byte)Convert.ToUInt16(gUID.Substring(18, 2), 16);
                Byte f = (Byte)Convert.ToUInt16(gUID.Substring(20, 2), 16);
                Byte g = (Byte)Convert.ToUInt16(gUID.Substring(22, 2), 16);
                Byte h = (Byte)Convert.ToUInt16(gUID.Substring(24, 2), 16);
                Byte i = (Byte)Convert.ToUInt16(gUID.Substring(26, 2), 16);
                Byte j = (Byte)Convert.ToUInt16(gUID.Substring(28, 2), 16);
                Byte k = (Byte)Convert.ToUInt16(gUID.Substring(30, 2), 16);

                gd = new Guid(a, b, c, d, e, f, g, h, i, j, k);
            }

            return gd;
        }


        public string GetUserGUID(string UserName)
        {

            string guid = "";

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@user";
                parameter[parameter.Count - 1].Value = UserName;

                DataTable dt = _helper.GetData("dbo.spGetUserGUID", parameter);

                string strHex = "";

                if (dt.Rows.Count > 0)
                {
                    byte[] binaryData = dt.Rows[0].ItemArray[0] as byte[];
                    strHex = BitConverter.ToString(binaryData);

                    String g = String.Empty;

                    foreach (var item in binaryData)
                    {
                        g += String.Format("{0:X2}", item);
                    }

                    guid = ConvertOctetStringToGuid(g).ToString();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return guid;

        }

        #region DataOwner

        public List<DataOwner_Entity> GetAllDataOwner(string GroupName)
        {
            return DataOwnerBL.GetAllADUsers(GroupName);
        }

        public List<Project_DataOwner> GetProjectsByDO(string ModuleName, string UserGUID)
        {
            return DataOwnerBL.GetProjectsByDO(ModuleName, UserGUID);
        }
        #endregion

        #region "Grant Application"
        public List<GrantApplication> FillGrid_GrantApplication()
        {
            try
            {
                return GranMaster.FillGrid_Grant_Master();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Grant_Application(Grant_Master grant_Master, List<Project_Dept_PI> pdi, string mode)
        {
            try
            {
                return GranMaster.GrantApplication(grant_Master, pdi, mode);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public GrantMasterDetails GetGrantApplicationDetails(int grantId)
        {
            return GranMaster.GetGrantApplicationDetails(grantId);
        }
        public GrantMasterDetails GetNewProjectDetailsForGrant(int ProjectId)
        {
            return GranMaster.GetNewProjectDetails(ProjectId);
        }



        #endregion

        #region " Grant Detail "
        public List<Grant_Details> FillGrantDetailGrid()
        {
            return TTSH.BusinessLogic.GrantDetails.FillGrantDetailGrid();
        }

        public  GrantNewProjectEntry FillGrantDetailNewProject(int ID)
        {
            return TTSH.BusinessLogic.GrantDetails.FillGrantDetailNewProject(ID);
        }
        public Grant_Details GetGrantDetailsById(int ID, int ProjID)
        {
            return TTSH.BusinessLogic.GrantDetails.GetGrantDetailsById(ID,ProjID);
        }

        public string Grant_Details_DML(Grant_Details _Grant_Details, List<Other_Dept_PI> lstoth, List<Grant_Budget_Allocation_Details> lstGBAD,List<GrantExtensionDetails>lstGExt , string mode)
        {
            return GrantDetails.Grant_Details_DML(_Grant_Details, lstoth, lstGBAD,lstGExt, mode);
        }

        public  string Other_PI_Master_DML(Other_PI_Master _Other_PI_Master, string mode)
        {
            return GrantDetails.Other_PI_Master_DML(_Other_PI_Master, mode);
        }
        #endregion

        #region " Grant CSCS "
        public List<Senior_CSCS_Details> FillGrantSeniorCSCSGrid()
        {
            return GrantSeniorCSCS.FillGrantSeniorCSCSGrid();
        }
        public string Senior_CSCS_Details_DML(Senior_CSCS_Details _Senior_CSCS_Details, string mode)
        {
            return GrantSeniorCSCS.Senior_CSCS_Details_DML(_Senior_CSCS_Details, mode);
        }

        public Senior_CSCS_Details GetSenior_CSCS_DetailsByID(int ID)
        {
            return GrantSeniorCSCS.GetSenior_CSCS_DetailsByID(ID);
        }
        #endregion

    }
}
