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
    public class ProjectMaster
    {
        public static Project_Master GetProject_MasterDetailsByID(int ID)
        {
            Project_Master _Project_Master = new Project_Master();
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
                    List<PI_Master> piList = new List<PI_Master>();
                    List<Project_Coordinator_Details> CoOrdList = new List<Project_Coordinator_Details>();
                    ProjectsData = _helper.GetData("spProjectMasterDML", parameter);
                    foreach (DataRow dr in ProjectsData.Rows)
                    {
                        string xmlTestManager = (dr.IsNull("DEPT_PI")) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
                        if (xmlTestManager != string.Empty)
                        {
                            //--PI_M.i_Dept_ID,PI_M.i_ID,PI_M.s_Email,PI_M.s_Phone_no,PI_M.s_MCR_No

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

                        string CoList = (dr.IsNull("COORDINATOR")) == true ? "" : Convert.ToString(dr["COORDINATOR"]);
                        if (CoList != string.Empty)
                        {
                            //-- PCD.s_Coordinator_name,pcd.i_Coordinator_ID

                            using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("COORDINATOR/COORDINATOR_D");


                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Project_Coordinator_Details pcd = new Project_Coordinator_Details();
                                    if (node["s_Coordinator_name"] != null)
                                        pcd.s_Coordinator_name = (node["s_Coordinator_name"].InnerText);
                                    if (node["i_Coordinator_ID"] != null)
                                        pcd.i_Coordinator_ID = Convert.ToString(node["i_Coordinator_ID"].InnerText);
                                    CoOrdList.Add(pcd);
                                }
                            }

                        }
                        _Project_Master = new Project_Master()
                        {
                            COORDINATOR = CoOrdList,
                            DEPT_PI = piList,
                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID") ? string.Empty : Convert.ToString(dr["s_Display_Project_ID"])),
                            s_Project_Title = (dr.IsNull("s_Project_Title") ? string.Empty : Convert.ToString(dr["s_Project_Title"])),
                            s_Short_Title = (dr.IsNull("s_Short_Title") ? string.Empty : Convert.ToString(dr["s_Short_Title"])),
                            i_Project_Category_ID = (dr.IsNull("i_Project_Category_ID") ? 0 : Convert.ToInt32(dr["i_Project_Category_ID"])),
                            i_Project_Type_ID = (dr.IsNull("i_Project_Type_ID") ? 0 : Convert.ToInt32(dr["i_Project_Type_ID"])),
                            i_Project_Subtype_ID = (dr.IsNull("i_Project_Subtype_ID") ? 0 : Convert.ToInt32(dr["i_Project_Subtype_ID"])),
                            b_Collaboration_Involved = (dr.IsNull("b_Collaboration_Involved") ? false : Convert.ToBoolean(dr["b_Collaboration_Involved"])),
                            b_StartBy_TTSH = (dr.IsNull("b_StartBy_TTSH") ? false : Convert.ToBoolean(dr["b_StartBy_TTSH"])),
                            b_Funding_req = (dr.IsNull("b_Funding_req") ? false : Convert.ToBoolean(dr["b_Funding_req"])),
                            b_Ischild = (dr.IsNull("b_Ischild") ? false : Convert.ToBoolean(dr["b_Ischild"])),
                            i_Parent_ProjectID = (dr.IsNull("i_Parent_ProjectID") ? 0 : Convert.ToInt32(dr["i_Parent_ProjectID"])),
                            s_Project_Alias1 = (dr.IsNull("s_Project_Alias1") ? string.Empty : Convert.ToString(dr["s_Project_Alias1"])),
                            s_Project_Alias2 = (dr.IsNull("s_Project_Alias2") ? string.Empty : Convert.ToString(dr["s_Project_Alias2"])),
                            s_Project_Desc = (dr.IsNull("s_Project_Desc") ? string.Empty : Convert.ToString(dr["s_Project_Desc"])),
                            Project_Category_Name = (dr.IsNull("Project_Category_Name") ? string.Empty : Convert.ToString(dr["Project_Category_Name"])),
                            b_IsFeasible = (dr.IsNull("b_IsFeasible") ? 0 : Convert.ToInt32(dr["b_IsFeasible"])),
                            b_Isselected_project = (dr.IsNull("b_Isselected_project") ? false : Convert.ToBoolean(dr["b_Isselected_project"])),
                            s_IRB_No = (dr.IsNull("s_IRB_No") ? string.Empty : Convert.ToString(dr["s_IRB_No"])),
                            s_Research_IO = (dr.IsNull("s_Research_IO") ? string.Empty : Convert.ToString(dr["s_Research_IO"])),
                            s_Research_IP = (dr.IsNull("s_Research_IP") ? string.Empty : Convert.ToString(dr["s_Research_IP"])),
                            Project_StartDate = (dr.IsNull("dt_ProjectStartDate") ? string.Empty : Convert.ToString(dr["dt_ProjectStartDate"])),
                            s_Coinvestigator = (dr.IsNull("s_Coinvestigator") ? string.Empty : Convert.ToString(dr["s_Coinvestigator"])),
                            s_Contract_DataOwner = (dr.IsNull("DO_Contract") ? string.Empty : Convert.ToString(dr["DO_Contract"])),
                            s_Ethics_DataOwner = (dr.IsNull("DO_Ethics") ? string.Empty : Convert.ToString(dr["DO_Ethics"])),
                            s_Grant_DataOwner = (dr.IsNull("DO_Grant") ? string.Empty : Convert.ToString(dr["DO_Grant"])),
                            s_Feasibility_DataOwner = (dr.IsNull("DO_Feasibility") ? string.Empty : Convert.ToString(dr["DO_Feasibility"])),
                            s_Regulatory_DataOwner = (dr.IsNull("DO_Regulatory") ? string.Empty : Convert.ToString(dr["DO_Regulatory"])),
                            s_Selected_DataOwner = (dr.IsNull("DO_Selected") ? string.Empty : Convert.ToString(dr["DO_Selected"])),
                            dt_IRB_Approve_Enddate = (dr.IsNull("dt_IRB_Approve_Enddate") ? null:(DateTime?) Convert.ToDateTime(dr["dt_IRB_Approve_Enddate"])),


                            i_ProjectStatus = (dr.IsNull("i_ProjectStatusID") ? 0 : Convert.ToInt32(dr["i_ProjectStatusID"])),
                            Dt_ProjectEndDate = (dr.IsNull("dt_ProjectEndDate") ? string.Empty : Convert.ToString(dr["dt_ProjectEndDate"])),
                            b_EthicsNeeded = (dr.IsNull("b_EthicsNeeded") ? false : Convert.ToBoolean(dr["b_EthicsNeeded"])),

                        };


                    }
                }
                catch (Exception e) { }
                return _Project_Master;
            }
        }
        public static string GetPI_MasterDetailsByID(int ID)
        {
            List<object> lst = new List<object>();
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
                    ProjectsData = _helper.GetData("dbo.spPI_MasterDML", parameter);
                    foreach (DataRow dr in ProjectsData.Rows)
                    {
                        PI_Master _PI_Master = new PI_Master();


                        _PI_Master.i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"]));
                        _PI_Master.i_Dept_ID = (dr.IsNull("i_Dept_ID") ? 0 : Convert.ToInt32(dr["i_Dept_ID"]));
                        _PI_Master.s_Firstname = (dr.IsNull("s_Firstname") ? string.Empty : Convert.ToString(dr["s_Firstname"]));
                        _PI_Master.s_Lastname = (dr.IsNull("s_Lastname") ? string.Empty : Convert.ToString(dr["s_Lastname"]));
                        _PI_Master.s_Email = (dr.IsNull("s_Email") ? string.Empty : Convert.ToString(dr["s_Email"]));
                        _PI_Master.s_Phone_no = (dr.IsNull("s_Phone_no") ? string.Empty : Convert.ToString(dr["s_Phone_no"]));
                        _PI_Master.s_MCR_No = (dr.IsNull("s_MCR_No") ? string.Empty : Convert.ToString(dr["s_MCR_No"]));
                        _PI_Master.s_DeptName = (dr.IsNull("Dept_Name") ? string.Empty : Convert.ToString(dr["Dept_Name"]));
                        _PI_Master.s_PIName = (dr.IsNull("s_Firstname") ? string.Empty : Convert.ToString(dr["s_Firstname"])) + "  " + (dr.IsNull("s_Lastname") ? string.Empty : Convert.ToString(dr["s_Lastname"]));
                        //s_Description = (  dr.IsNull("s_Description") ? string.Empty : Convert.ToString(dr["s_Description"]) ),
                        //s_CreatedBy_ID = ( dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"]) ),
                        //s_ModifyBy_ID = ( dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"]) ),
                        //dt_Created_Date = ( dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"]) ),
                        //dt_Modify_Date = ( dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"]) )
                        lst.Add(_PI_Master);
                    }
                }
                catch (Exception e) { }
                return (new JavaScriptSerializer().Serialize(lst));
            }
        }
        public static List<Project_Master> FillGrid_Project_Master()
        {
            List<Project_Master> pm = new List<Project_Master>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spProjectMasterDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    pm.Add(new Project_Master
                    {
                        i_ID = Convert.ToInt32(dr["i_ID"]),
                        s_Display_Project_ID = Convert.ToString(dr["s_Display_Project_ID"]),
                        s_Project_Title = Convert.ToString(dr["s_Project_Title"]),
                        Project_Category_Name = Convert.ToString(dr["Project_Category_Name"]),
                        s_IRB_No = Convert.ToString(dr["s_IRB_No"]),
                        Project_Type = Convert.ToString(dr["Project_Type"]),
                        PI_NAME = Convert.ToString(dr["PI_NAME"]),
                        s_CreatedBy_ID = Convert.ToString(dr["s_CreatedBy_ID"]),
                       S_ProjectStatus=Convert.ToString(dr["S_ProjectStatus"])
                    });
                }
            }
            catch (Exception)
            {


            }

            return pm;

        }

        public static string Project_Master(Project_Master _Project_Master, List<Project_Dept_PI> pdi, List<Project_Coordinator_Details> pcd, string mode)
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
                parameter[parameter.Count - 1].Value = _Project_Master.i_ID;




                if (mode.ToString() != "Delete")
                {


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Display_Project_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Display_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Title";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Title;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Short_Title";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Short_Title;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Category_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.i_Project_Category_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Type_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.i_Project_Type_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_Subtype_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.i_Project_Subtype_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Collaboration_Involved";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_Collaboration_Involved;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_StartBy_TTSH";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_StartBy_TTSH;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Funding_req";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_Funding_req;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Ischild";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_Ischild;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Parent_ProjectID";
                    parameter[parameter.Count - 1].Value = _Project_Master.i_Parent_ProjectID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias1";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Alias1;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias2";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Alias2;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Desc";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Project_Desc;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsFeasible";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_IsFeasible;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Isselected_project";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_Isselected_project;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_IRB_No";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_IRB_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Research_IO";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Research_IO;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Research_IP";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Research_IP;

                    //----------Added by Atul
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCID";
                    parameter[parameter.Count - 1].Value = _Project_Master.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Project_Master.UName;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ProjectStatusId";
                    parameter[parameter.Count - 1].Value = _Project_Master.i_ProjectStatus;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Dt_ProjectEndDate";
                    parameter[parameter.Count - 1].Value = _Project_Master.Dt_ProjectEndDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_EthicsNeeded";
                    parameter[parameter.Count - 1].Value = _Project_Master.b_EthicsNeeded;
                    //----------End by Atul
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_ProjectStartDate ";
                    parameter[parameter.Count - 1].Value = _Project_Master.Project_StartDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Project_Dept_PI";
                    parameter[parameter.Count - 1].Value = pdi.ListToDatatable().getColumns(1);

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Project_Coordinator_Details";
                    parameter[parameter.Count - 1].Value = pcd.ListToDatatable();

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Ethics_DO_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Ethics_DataOwner;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Feasibility_DO_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Feasibility_DataOwner;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Selected_DO_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Selected_DataOwner;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Regulatory_DO_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Regulatory_DataOwner;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Contract_DO_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Contract_DataOwner;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Grant_DO_ID";
                    parameter[parameter.Count - 1].Value = _Project_Master.s_Grant_DataOwner;

                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
                {
                    result = "Success" + " | " + parameter[parameter.Count - 1].Value.ToString(); ;
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

        public static string PI_Master(PI_Master _PI_Master, string mode)
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
                parameter[parameter.Count - 1].Value = _PI_Master.i_ID;
                if (mode.ToString() != "Delete")
                {


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Dept_ID";
                    parameter[parameter.Count - 1].Value = _PI_Master.i_Dept_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Firstname";
                    parameter[parameter.Count - 1].Value = _PI_Master.s_Firstname;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Lastname";
                    parameter[parameter.Count - 1].Value = _PI_Master.s_Lastname;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Email";
                    parameter[parameter.Count - 1].Value = _PI_Master.s_Email;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Phone_no";
                    parameter[parameter.Count - 1].Value = _PI_Master.s_Phone_no;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_MCR_No";
                    parameter[parameter.Count - 1].Value = _PI_Master.s_MCR_No;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCID";
                    parameter[parameter.Count - 1].Value = _PI_Master.s_CreatedBy_ID;




                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spPI_MasterDML", parameter)))
                {
                    result = "Success" + " | " + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) { }
            return result;
        }
    }
}
