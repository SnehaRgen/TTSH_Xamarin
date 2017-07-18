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
    public class Contract
    {
        public static Contract_Collobrator_Master GetContract_Collobrator_MasterByID(int ID)
        {
            Contract_Collobrator_Master _Contract_Collobrator_Master = new Contract_Collobrator_Master();

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
                    ProjectsData = _helper.GetData("dbo.spContract_Collobrator_MasterDML", parameter);
                    foreach (DataRow dr in ProjectsData.Rows)
                    {
                        _Contract_Collobrator_Master = new Contract_Collobrator_Master()
                        {



                            i_ID = (dr.IsNull("i_ID") ? "" : Convert.ToString(dr["i_ID"])),
                            s_Name = (dr.IsNull("s_Name") ? string.Empty : Convert.ToString(dr["s_Name"])),
                            s_Email1 = (dr.IsNull("s_Email1") ? string.Empty : Convert.ToString(dr["s_Email1"])),
                            s_Email2 = (dr.IsNull("s_Email2") ? string.Empty : Convert.ToString(dr["s_Email2"])),
                            s_Institution = (dr.IsNull("s_Institution") ? string.Empty : Convert.ToString(dr["s_Institution"])),
                            s_PhoNo = (dr.IsNull("s_PhoNo") ? string.Empty : Convert.ToString(dr["s_PhoNo"])),
                            i_Country_ID = (dr.IsNull("i_Country_ID") ? 0 : Convert.ToInt32(dr["i_Country_ID"])),
                            dt_Contract_AssignDate = Convert.ToString(dr["dt_Contract_AssignDate"]),
                            i_ReviewedBy_ID = Convert.ToString(dr["i_ReviewedBy_ID"])

                        };


                    }
                }
                catch (Exception ex) { }
                return _Contract_Collobrator_Master;
            }
        }
        public static Contract_Details GetContract_DetailsDetailsByID(int ID)
        {
            Contract_Details _Contract_Details = new Contract_Details();
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
                    ProjectsData = _helper.GetData("dbo.spContract_DetailsDML", parameter);

                    List<PI_Master> piList = new List<PI_Master>();
                    List<Contract_Collobrator_Master> ccdlist = new List<Contract_Collobrator_Master>();
                    List<Project_Master> pjmasterlist = new List<TTSH.Entity.Project_Master>();
                    List<ContractList> ctlist = new List<ContractList>();
                    List<Contract_Status_Date> lstcsd = new List<Contract_Status_Date>();
                    foreach (DataRow dr in ProjectsData.Rows)
                    {
                        //string xmlTestManager = ( dr.IsNull("DEPT_PI") ) == true ? "" : Convert.ToString(dr["DEPT_PI"]);
                        //if ( xmlTestManager != string.Empty )
                        //    {
                        //    using ( XmlReader reader = XmlReader.Create(new StringReader(xmlTestManager)) )
                        //        {
                        //        XmlDocument xml = new XmlDocument();
                        //        xml.Load(reader);
                        //        XmlNodeList xmlNodeList = xml.SelectNodes("DEPT_PI/DEPT");
                        //        foreach ( XmlNode node in xmlNodeList )
                        //            {
                        //            PI_Master pi = new PI_Master();
                        //            if ( node["i_Dept_ID"] != null )
                        //                pi.i_Dept_ID = Convert.ToInt32(node["i_Dept_ID"].InnerText);
                        //            if ( node["i_ID"] != null )
                        //                pi.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
                        //            if ( node["s_Email"] != null )
                        //                pi.s_Email = ( node["s_Email"].InnerText );
                        //            if ( node["s_Phone_no"] != null )
                        //                pi.s_Phone_no = ( node["s_Phone_no"].InnerText );
                        //            if ( node["s_MCR_No"] != null )
                        //                pi.s_MCR_No = ( node["s_MCR_No"].InnerText );
                        //            if ( node["Dept_Name"] != null )
                        //                pi.s_DeptName = ( node["Dept_Name"].InnerText );
                        //            if ( node["s_PI_Name"] != null )
                        //                pi.s_PIName = ( node["s_PI_Name"].InnerText );
                        //            piList.Add(pi);
                        //            }
                        //        }

                        //    }
                        //string CoList = ( dr.IsNull("COLLABORATOR") ) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
                        //if ( CoList != string.Empty )
                        //    {
                        //    using ( XmlReader reader = XmlReader.Create(new StringReader(CoList)) )
                        //        {
                        //        XmlDocument xml = new XmlDocument();
                        //        xml.Load(reader);
                        //        XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


                        //        foreach ( XmlNode node in xmlNodeList )
                        //            {
                        //            Contract_Collobrator_Master ccd = new Contract_Collobrator_Master();
                        //            if ( node["i_ID"] != null )
                        //                ccd.i_ID = Convert.ToInt32(node["i_ID"].InnerText);
                        //            if ( node["s_Name"] != null )
                        //                ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
                        //            if ( node["s_Email1"] != null )
                        //                ccd.s_Email1 = Convert.ToString(node["s_Email1"].InnerText);
                        //            if ( node["s_Email2"] != null )
                        //                ccd.s_Email2 = Convert.ToString(node["s_Email2"].InnerText);
                        //            if ( node["s_Institution"] != null )
                        //                ccd.s_Institution = Convert.ToString(node["s_Institution"].InnerText);
                        //            if ( node["s_Country_Name"] != null )
                        //                ccd.Country_Name = Convert.ToString(node["s_Country_Name"].InnerText);
                        //            if ( node["i_Country_ID"] != null )
                        //                ccd.i_Country_ID = Convert.ToInt32(node["i_Country_ID"].InnerText);
                        //            if ( node["dt_Contract_Request_Date"] != null )
                        //                ccd.s_date = Convert.ToString(node["dt_Contract_Request_Date"].InnerText);
                        //            if ( node["s_InitialContract_ID"] != null )
                        //                ccd.s_initialId = Convert.ToString(node["s_InitialContract_ID"].InnerText);
                        //            if ( node["s_PhoNo"] != null )
                        //                ccd.s_PhoNo = Convert.ToString(node["s_PhoNo"].InnerText);
                        //            ccdlist.Add(ccd);
                        //            }
                        //        }
                        //    }
                        //string PJmasterList = ( dr.IsNull("PROJECT_DATA") ) == true ? "" : Convert.ToString(dr["PROJECT_DATA"]);
                        //if ( PJmasterList != string.Empty )
                        //    {

                        //    using ( XmlReader reader = XmlReader.Create(new StringReader(PJmasterList)) )
                        //        {
                        //        XmlDocument xml = new XmlDocument();
                        //        xml.Load(reader);
                        //        XmlNodeList xmlNodeList = xml.SelectNodes("PROJECT/PROJECT_DATA");

                        //        foreach ( XmlNode node in xmlNodeList )
                        //            {
                        //            Project_Master pmMaster = new Project_Master();
                        //            if ( node["ProjmID"] != null )
                        //                pmMaster.i_ID = Convert.ToInt32(node["ProjmID"].InnerText);
                        //            if ( node["s_Project_Title"] != null )
                        //                pmMaster.s_Project_Title = Convert.ToString(node["s_Project_Title"].InnerText);
                        //            if ( node["s_Display_Project_ID"] != null )
                        //                pmMaster.s_Display_Project_ID = Convert.ToString(node["s_Display_Project_ID"].InnerText);
                        //            if ( node["s_Short_Title"] != null )
                        //                pmMaster.s_Short_Title = Convert.ToString(node["s_Short_Title"].InnerText);
                        //            if ( node["Project_Category_Name"] != null )
                        //                pmMaster.Project_Category_Name = Convert.ToString(node["Project_Category_Name"].InnerText);
                        //            if ( node["s_Project_Alias1"] != null )
                        //                pmMaster.s_Project_Alias1 = Convert.ToString(node["s_Project_Alias1"].InnerText);
                        //            if ( node["s_Project_Alias2"] != null )
                        //                pmMaster.s_Project_Alias2 = Convert.ToString(node["s_Project_Alias2"].InnerText);
                        //            if ( node["s_IRB_No"] != null )
                        //                pmMaster.s_IRB_No = Convert.ToString(node["s_IRB_No"].InnerText);

                        //            pjmasterlist.Add(pmMaster);
                        //            }
                        //        }

                        //    }
                        string StatusDate = (dr.IsNull("Contract_StatusDate")) == true ? "" : Convert.ToString(dr["Contract_StatusDate"]);
                        if (StatusDate != string.Empty)
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(StatusDate)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("StatusDateD/StatusDate");


                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Contract_Status_Date csd = new Contract_Status_Date();
                                    if (node["i_Contract_Status_ID"] != null)
                                        csd.i_Contract_Status_ID = Convert.ToInt32(node["i_Contract_Status_ID"].InnerText);
                                    if (node["dt_Status_Date"] != null)
                                        csd.dt_Status_Date = Convert.ToString(node["dt_Status_Date"].InnerText);

                                    lstcsd.Add(csd);
                                }
                            }
                        }
                        ctlist.Add(new ContractList
                        {
                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            s_Contract_Name = (dr.IsNull("s_Contract_Name") ? string.Empty : Convert.ToString(dr["s_Contract_Name"])),
                            s_ContractId = (dr.IsNull("s_ContractId") ? string.Empty : Convert.ToString(dr["s_ContractId"])),
                            s_ContractCategory = (dr.IsNull("s_ContractCategory") ? string.Empty : Convert.ToString(dr["s_ContractCategory"])),
                            s_ContractStatus = (dr.IsNull("s_ContractStatus") ? string.Empty : Convert.ToString(dr["s_ContractStatus"])),
                            dt_Expiry_Date = (dr.IsNull("dt_Expiry_Date") ? "" : Convert.ToString(dr["dt_Expiry_Date"])),
                            s_Country = (dr.IsNull("s_Country") ? string.Empty : Convert.ToString(dr["s_Country"])),
                            dt_NewExpiry_Date = (dr.IsNull("dt_NewExpiry_Date") ? "" : Convert.ToString(dr["dt_NewExpiry_Date"])),
                            //ReviwedByid = (dr.Table.Columns.Contains("ReviewdById")) ? dr.IsNull("ReviewdById") == true ? "" : Convert.ToString(dr["ReviewdById"]) : ""
                            ReviwedByid = dr.IsNull("ReviwedByid") ? string.Empty : Convert.ToString(dr["ReviwedByid"])

                        });
                        _Contract_Details = new Contract_Details()
                        {

                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),

                            listcsd = lstcsd,
                            contlist = ctlist
                            //pmlist = piList,
                            //ccdlist = ccdlist,
                            //pjctmList = pjmasterlist

                        };
                    }
                }
                catch (Exception e) { }
                return _Contract_Details;
            }
        }
        public static Contract_Details GetContractDeta(int ID, int ProjectId = 0)
        {
            Contract_Details _Contract_Details = new Contract_Details();
            {
                try
                {
                    DataHelper _helper = new DataHelper();
                    _helper.InitializedHelper();
                    List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@StatementType";
                    parameter[parameter.Count - 1].Value = "GetContractDetails";
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ID";
                    parameter[parameter.Count - 1].Value = ID;
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = ProjectId;
                    DataTable ProjectsData = new DataTable();
                    ProjectsData = _helper.GetData("dbo.spContract_DetailsDML", parameter);
                    List<SelectedCollborators_Details> lstSelCollab = new List<SelectedCollborators_Details>();
                    List<Selected_Clause_Details> lstSelClause = new List<TTSH.Entity.Selected_Clause_Details>();
                    List<ContractDetails_MultipleContractFile> lstmultiple = new List<ContractDetails_MultipleContractFile>();
                    List<ContractList> ctlist = new List<ContractList>();
                    List<Contract_Status_Date> lstcsd = new List<Contract_Status_Date>();
                    foreach (DataRow dr in ProjectsData.Rows)
                    {



                        string XMLclauses = (dr.IsNull("CLAUSES")) == true ? "" : Convert.ToString(dr["CLAUSES"]);
                        if (XMLclauses != string.Empty)
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(XMLclauses)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("CLAUSESD/CLAUSES");
                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Selected_Clause_Details selcd = new Selected_Clause_Details();
                                    if (node["i_Contract_ID"] != null)
                                        selcd.i_Contract_ID = Convert.ToInt32(node["i_Contract_ID"].InnerText);
                                    if (node["i_Contract_Clause_ID"] != null)
                                        selcd.i_Contract_Clause_ID = Convert.ToInt32(node["i_Contract_Clause_ID"].InnerText);
                                    if (node["s_Status"] != null)
                                        selcd.s_Status = (node["s_Status"].InnerText);
                                    if (node["Clause_Name"] != null)
                                        selcd.Clause_Name = (node["Clause_Name"].InnerText);
                                    if (node["s_Comments"] != null)
                                        selcd.s_Comments = (node["s_Comments"].InnerText);
                                    if (node["s_Proposed_Changes"] != null)
                                        selcd.s_Proposed_Changes = (node["s_Proposed_Changes"].InnerText);
                                    lstSelClause.Add(selcd);
                                }
                            }

                        }
                        string XMLMultilist = (dr.IsNull("Multiple_Contract_File")) == true ? "" : Convert.ToString(dr["Multiple_Contract_File"]);
                        if (XMLMultilist != string.Empty)
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(XMLMultilist)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("MULTIPLE_FILED/Multiple_File");
                                foreach (XmlNode node in xmlNodeList)
                                {
                                    ContractDetails_MultipleContractFile cdm = new ContractDetails_MultipleContractFile();

                                    if (node["i_ContractDetailsID"] != null)
                                        cdm.i_ContractDetailsID = Convert.ToInt32(node["i_ContractDetailsID"].InnerText);
                                    if (node["s_ContractFile"] != null)
                                        cdm.s_ContractFile = (node["s_ContractFile"].InnerText);
                                    lstmultiple.Add(cdm);
                                }
                            }

                        }
                        string CoList = (dr.IsNull("COLLABORATOR")) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
                        string[] arr;
                        if (CoList != string.Empty)
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


                                foreach (XmlNode node in xmlNodeList)
                                {
                                    SelectedCollborators_Details ccd = new SelectedCollborators_Details();
                                    arr = node["i_ID"].InnerText.Split('|');
                                    if (node["i_ID"] != null)
                                    {
                                        ccd.i_Collobrator_ID = Convert.ToString(node["i_ID"].InnerText);
                                        if (arr.Length > 0)
                                        {
                                            ccd.i_Collobrator_ID_Unq = arr[0];
                                        }
                                    }
                                    if (node["s_Name"] != null)
                                    {
                                        ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
                                    }

                                    lstSelCollab.Add(ccd);
                                    lstSelCollab = lstSelCollab.GroupBy(test => test.i_Collobrator_ID_Unq).Select(grp => grp.First()).ToList();
                                }
                            }
                        }
                        string StatusDate = (dr.IsNull("Contract_StatusDate")) == true ? "" : Convert.ToString(dr["Contract_StatusDate"]);
                        if (StatusDate != string.Empty)
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(StatusDate)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("StatusDateD/StatusDate");


                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Contract_Status_Date csd = new Contract_Status_Date();
                                    if (node["i_Contract_Status_ID"] != null)
                                        csd.i_Contract_Status_ID = Convert.ToInt32(node["i_Contract_Status_ID"].InnerText);
                                    if (node["dt_Status_Date"] != null)
                                        csd.dt_Status_Date = Convert.ToString(node["dt_Status_Date"].InnerText);

                                    lstcsd.Add(csd);
                                }
                            }
                        }
                        ctlist.Add(new ContractList
                        {
                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            s_Contract_Name = (dr.IsNull("s_Contract_Name") ? string.Empty : Convert.ToString(dr["s_Contract_Name"])),
                            s_ContractId = (dr.IsNull("s_ContractId") ? string.Empty : Convert.ToString(dr["s_ContractId"])),
                            s_ContractCategory = (dr.IsNull("s_ContractCategory") ? string.Empty : Convert.ToString(dr["s_ContractCategory"])),
                            s_ContractStatus = (dr.IsNull("s_ContractStatus") ? string.Empty : Convert.ToString(dr["s_ContractStatus"])),
                            dt_Expiry_Date = (dr.IsNull("dt_Expiry_Date") ? "" : Convert.ToString(dr["dt_Expiry_Date"])),
                            s_Country = (dr.IsNull("s_Country") ? string.Empty : Convert.ToString(dr["s_Country"])),

                        });
                        _Contract_Details = new Contract_Details()
                        {

                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            s_Contract_Name = (dr.IsNull("s_Contract_Name") ? string.Empty : Convert.ToString(dr["s_Contract_Name"])),
                            s_Contract_Display_Id = (dr.IsNull("s_Contract_Display_Id") ? string.Empty : Convert.ToString(dr["s_Contract_Display_Id"])),
                            i_Contract_Category_ID = (dr.IsNull("i_Contract_Category_ID") ? 0 : Convert.ToInt32(dr["i_Contract_Category_ID"])),
                            i_Contract_Status_ID = (dr.IsNull("i_Contract_Status_ID") ? 0 : Convert.ToInt32(dr["i_Contract_Status_ID"])),
                            dt_LastUpdated_Date = (dr.IsNull("dt_LastUpdated_Date") ? "" : Convert.ToString(dr["dt_LastUpdated_Date"])),
                            i_Govt_Lawcountry = (dr.IsNull("i_Govt_Lawcountry") ? 0 : Convert.ToInt32(dr["i_Govt_Lawcountry"])),
                            s_Clauses_File = (dr.IsNull("s_Clauses_File") ? string.Empty : Convert.ToString(dr["s_Clauses_File"])),
                            //s_UploadedContract_File = ( dr.IsNull("s_UploadedContract_File") ? string.Empty : Convert.ToString(dr["s_UploadedContract_File"]) ),
                            dt_Effective_Date = (dr.IsNull("dt_Effective_Date") ? "" : Convert.ToString(dr["dt_Effective_Date"])),
                            dt_Finalization_Date = (dr.IsNull("dt_Finalization_Date") ? "" : Convert.ToString(dr["dt_Finalization_Date"])),
                            dt_LastSigned_Date = (dr.IsNull("dt_LastSigned_Date") ? "" : Convert.ToString(dr["dt_LastSigned_Date"])),
                            dt_Expiry_Date = (dr.IsNull("dt_Expiry_Date") ? "" : Convert.ToString(dr["dt_Expiry_Date"])),
                            b_Amendments = (dr.IsNull("b_Amendments") ? false : Convert.ToBoolean(dr["b_Amendments"])),
                            dt_NewExpiry_Date = (dr.IsNull("dt_NewExpiry_Date") ? "" : Convert.ToString(dr["dt_NewExpiry_Date"])),
                            s_AmendmenstContract_File = (dr.IsNull("s_AmendmenstContract_File") ? string.Empty : Convert.ToString(dr["s_AmendmenstContract_File"])),
                            s_Country = (dr.IsNull("s_Country") ? string.Empty : Convert.ToString(dr["s_Country"])),
                            i_Currency_ID = (dr.IsNull("i_Currency_ID") ? 0 : Convert.ToInt32(dr["i_Currency_ID"])),
                            i_Hospital_Cost = (dr.IsNull("i_Hospital_Cost") ? 0 : Convert.ToInt32(dr["i_Hospital_Cost"])),
                            i_Investigator_fees = (dr.IsNull("i_Investigator_fees") ? 0 : Convert.ToInt32(dr["i_Investigator_fees"])),
                            i_Inkind_Contribution = (dr.IsNull("i_Inkind_Contribution") ? 0 : Convert.ToInt32(dr["i_Inkind_Contribution"])),
                            i_Coordinator_fess = (dr.IsNull("i_Coordinator_fess") ? 0 : Convert.ToInt32(dr["i_Coordinator_fess"])),
                            //dt_Contract_StatusDate = ( dr.IsNull("dt_Status_Date") ? string.Empty : Convert.ToString(dr["dt_Status_Date"]) ),
                            contlist = ctlist,
                            lstSelCollab = lstSelCollab,
                            lstSelClause = lstSelClause,
                            lstmultiple = lstmultiple,
                            listcsd = lstcsd
                        };
                    }
                }
                catch (Exception e) { }
                return _Contract_Details;
            }
        }
        public static Contract_Master GetContract_MasterDetailsByID(int ID)
        {
            Contract_Master _Contract_Master = new Contract_Master();
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
                    List<Contract_Collobrator_Master> ccdlist = new List<Contract_Collobrator_Master>();
                    List<Project_Master> pjmasterlist = new List<TTSH.Entity.Project_Master>();
                    ProjectsData = _helper.GetData("dbo.[spContract_MasterDML]", parameter);
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
                                    piList.Add(pi);
                                }
                            }

                        }
                        string CoList = (dr.IsNull("COLLABORATOR")) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
                        if (CoList != string.Empty)
                        {
                            //-- PCD.s_Coordinator_name,pcd.i_Coordinator_ID

                            using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
                            {
                                XmlDocument xml = new XmlDocument();
                                xml.Load(reader);
                                XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


                                foreach (XmlNode node in xmlNodeList)
                                {
                                    Contract_Collobrator_Master ccd = new Contract_Collobrator_Master();
                                    if (node["i_ID"] != null)
                                        ccd.i_ID = Convert.ToString(node["i_ID"].InnerText);
                                    if (node["s_Name"] != null)
                                        ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
                                    if (node["s_Email1"] != null)
                                        ccd.s_Email1 = Convert.ToString(node["s_Email1"].InnerText);
                                    if (node["s_Email2"] != null)
                                        ccd.s_Email2 = Convert.ToString(node["s_Email2"].InnerText);
                                    if (node["s_Institution"] != null)
                                        ccd.s_Institution = Convert.ToString(node["s_Institution"].InnerText);
                                    if (node["s_Country_Name"] != null)
                                        ccd.Country_Name = Convert.ToString(node["s_Country_Name"].InnerText);
                                    if (node["i_Country_ID"] != null)
                                        ccd.i_Country_ID = Convert.ToInt32(node["i_Country_ID"].InnerText);
                                    if (node["dt_Contract_Request_Date"] != null)
                                        ccd.s_date = Convert.ToString(node["dt_Contract_Request_Date"].InnerText);
                                    if (node["s_InitialContract_ID"] != null)
                                        ccd.s_initialId = Convert.ToString(node["s_InitialContract_ID"].InnerText);
                                    if (node["s_PhoNo"] != null)
                                        ccd.s_PhoNo = Convert.ToString(node["s_PhoNo"].InnerText);
                                    if (node["i_Collobrator_ID"] != null)
                                        ccd.I_Collaborator_Id = Convert.ToInt32(node["i_Collobrator_ID"].InnerText);
                                    if (node["dt_Contract_AssignDate"] != null)
                                        ccd.dt_Contract_AssignDate = Convert.ToString(node["dt_Contract_AssignDate"].InnerText);
                                    if (node["i_ReviewedBy_ID"] != null)
                                        ccd.i_ReviewedBy_ID = Convert.ToString(node["i_ReviewedBy_ID"].InnerText);
                                    if (node["S_ReviewedByName"] != null)
                                        ccd.S_ReviewedByName = Convert.ToString(node["S_ReviewedByName"].InnerText);

                                    ccdlist.Add(ccd);
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
                        _Contract_Master = new Contract_Master()
                        {


                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            dt_Contract_ReqDate = (dr.IsNull("dt_Contract_ReqDate") ? "" : Convert.ToString(dr["dt_Contract_ReqDate"])),
                            //dt_Contract_AssignDate = (dr.IsNull("dt_Contract_AssignDate") ? "" : Convert.ToString(dr["dt_Contract_AssignDate"])),
                            //i_ReviewedBy_ID = (dr.IsNull("i_ReviewedBy_ID") ? "" : Convert.ToString(dr["i_ReviewedBy_ID"])),
                            //S_ReviewedByName = (dr.IsNull("S_ReviewedByName") ? string.Empty : Convert.ToString(dr["S_ReviewedByName"])),
                            pmlist = piList,
                            ccdlist = ccdlist,
                            pjctmList = pjmasterlist


                        };
                    }
                }
                catch (Exception e) { }
                return _Contract_Master;
            }
        }
        public static ProjectDataforContractUsers FillProjectDataforContractUsers(int ID)
        {
            ProjectDataforContractUsers pdclist = new ProjectDataforContractUsers();
            DataHelper _helper = new DataHelper();
            List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
            DataTable ProjectsData = new DataTable();
            List<PI_Master> piList = new List<PI_Master>();
            List<Contract_Collobrator_Master> ccdlist = new List<Contract_Collobrator_Master>();
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
                ProjectsData = _helper.GetData("dbo.[spContract_DetailsDML]", parameter);
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
                    string CoList = (dr.IsNull("COLLABORATOR")) == true ? "" : Convert.ToString(dr["COLLABORATOR"]);
                    if (CoList != string.Empty)
                    {

                        using (XmlReader reader = XmlReader.Create(new StringReader(CoList)))
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(reader);
                            XmlNodeList xmlNodeList = xml.SelectNodes("COLLABORATORD/COLLABORATOR");


                            foreach (XmlNode node in xmlNodeList)
                            {
                                Contract_Collobrator_Master ccd = new Contract_Collobrator_Master();
                                if (node["i_ID"] != null)
                                    ccd.i_ID = Convert.ToString(node["i_ID"].InnerText);
                                if (node["s_Name"] != null)
                                    ccd.s_Name = Convert.ToString(node["s_Name"].InnerText);
                                if (node["s_Email1"] != null)
                                    ccd.s_Email1 = Convert.ToString(node["s_Email1"].InnerText);
                                if (node["s_Email2"] != null)
                                    ccd.s_Email2 = Convert.ToString(node["s_Email2"].InnerText);
                                if (node["s_Institution"] != null)
                                    ccd.s_Institution = Convert.ToString(node["s_Institution"].InnerText);
                                if (node["s_Country_Name"] != null)
                                    ccd.Country_Name = Convert.ToString(node["s_Country_Name"].InnerText);
                                if (node["i_Country_ID"] != null)
                                    ccd.i_Country_ID = Convert.ToInt32(node["i_Country_ID"].InnerText);
                                if (node["dt_Contract_Request_Date"] != null)
                                    ccd.s_date = Convert.ToString(node["dt_Contract_Request_Date"].InnerText);
                                if (node["s_InitialContract_ID"] != null)
                                    ccd.s_initialId = Convert.ToString(node["s_InitialContract_ID"].InnerText);
                                if (node["s_PhoNo"] != null)
                                    ccd.s_PhoNo = Convert.ToString(node["s_PhoNo"].InnerText);
                                if (node["i_ReviewedBy_ID"] != null)
                                    ccd.i_ReviewedBy_ID = Convert.ToString(node["i_ReviewedBy_ID"].InnerText);
                                ccdlist.Add(ccd);
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


                    pdclist = new ProjectDataforContractUsers()
                    {

                        Pilisst = piList,
                        ccmlist = ccdlist,
                        pmlist = pjmasterlist


                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return pdclist;
        }
        public static List<Contract_Master> FillGrid_Contract_Master()
        {
            List<Contract_Master> pm = new List<Contract_Master>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spContract_MasterDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    pm.Add(new Contract_Master
                    {
                        i_ID = (dr["Contract_ID"] != DBNull.Value) ? Convert.ToInt32(dr["Contract_ID"]) : 0,
                        i_Project_ID = (dr["i_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_ID"]) : 0,
                        s_Display_Project_ID = (dr["s_Display_Project_ID"] != DBNull.Value) ? Convert.ToString(dr["s_Display_Project_ID"]) : "",
                        s_Project_Title = (dr["s_Project_Title"] != DBNull.Value) ? Convert.ToString(dr["s_Project_Title"]) : "",
                        Project_Category_Name = (dr["Project_Category_Name"] != DBNull.Value) ? Convert.ToString(dr["Project_Category_Name"]) : "",
                        s_IRB_No = (dr["s_IRB_No"] != DBNull.Value) ? Convert.ToString(dr["s_IRB_No"]) : "",
                        Project_Type = (dr["Project_Type"] != DBNull.Value) ? Convert.ToString(dr["Project_Type"]) : "",
                        PI_NAME = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
                        Status = (dr["Status"] != DBNull.Value) ? Convert.ToString(dr["Status"]) : "",
                        ContAppStatus = (dr["ContAppStatus"] != DBNull.Value) ? Convert.ToString(dr["ContAppStatus"]) : "",
                        Project_Status_Disable = (dr["Project_Status"] != DBNull.Value) ? Convert.ToString(dr["Project_Status"]) : "",

                    });
                }
            }
            catch (Exception)
            {


            }

            return pm;


        }
        public static List<Contract_Details> FillGrid_Contract_Details()
        {
            List<Contract_Details> cd = new List<Contract_Details>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spContract_DetailsDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    cd.Add(new Contract_Details
                    {
                        //i_ID = ( dr["ContractDetail_ID"] != DBNull.Value ) ? Convert.ToInt32(dr["ContractDetail_ID"]) : 0,
                        i_Project_ID = (dr["i_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_ID"]) : 0,
                        s_Display_Project_ID = (dr["s_Display_Project_ID"] != DBNull.Value) ? Convert.ToString(dr["s_Display_Project_ID"]) : "",
                        s_Project_Title = (dr["s_Project_Title"] != DBNull.Value) ? Convert.ToString(dr["s_Project_Title"]) : "",
                        Project_Category_Name = (dr["Project_Category_Name"] != DBNull.Value) ? Convert.ToString(dr["Project_Category_Name"]) : "",
                        s_IRB_No = (dr["s_IRB_No"] != DBNull.Value) ? Convert.ToString(dr["s_IRB_No"]) : "",
                        Project_Type = (dr["Project_Type"] != DBNull.Value) ? Convert.ToString(dr["Project_Type"]) : "",
                        PI_NAME = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
                        Contract_Status = (dr["Contract_Status"] != DBNull.Value) ? Convert.ToString(dr["Contract_Status"]) : "",
                        Contracts = (dr["Description"] != DBNull.Value) ? Convert.ToString(dr["Description"]) : "",
                        Status = (dr["status"] != DBNull.Value) ? Convert.ToString(dr["status"]) : "",
                        Project_Status_Disable = (dr["Project_Status"] != DBNull.Value) ? Convert.ToString(dr["Project_Status"]) : "",
                    });
                }
            }
            catch (Exception)
            {


            }

            return cd;

        }
        public static string GetCollobrator_MasterDetailByID(int ID)
        {
            List<object> lst = new List<object>();
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
                ProjectsData = _helper.GetData("dbo.spContract_Collobrator_MasterDML", parameter);
                foreach (DataRow dr in ProjectsData.Rows)
                {
                    Contract_Collobrator_Master cm = new Contract_Collobrator_Master();


                    cm.i_ID = (dr.IsNull("i_ID") ? "" : Convert.ToString(dr["i_ID"]));
                    cm.s_Name = (dr.IsNull("s_Name") ? string.Empty : Convert.ToString(dr["s_Name"]));
                    cm.s_Email1 = (dr.IsNull("s_Email1") ? string.Empty : Convert.ToString(dr["s_Email1"]));
                    cm.s_Email2 = (dr.IsNull("s_Email2") ? string.Empty : Convert.ToString(dr["s_Email2"]));
                    cm.s_Institution = (dr.IsNull("s_Institution") ? string.Empty : Convert.ToString(dr["s_Institution"]));
                    cm.s_PhoNo = (dr.IsNull("s_PhoNo") ? string.Empty : Convert.ToString(dr["s_PhoNo"]));
                    cm.i_Country_ID = (dr.IsNull("i_Country_ID") ? 0 : Convert.ToInt32(dr["i_Country_ID"]));
                    cm.Country_Name = (dr.IsNull("Country_Name") ? string.Empty : Convert.ToString(dr["Country_Name"]));
                    cm.dt_Contract_AssignDate = Convert.ToString(dr["dt_Contract_AssignDate"]);
                    cm.i_ReviewedBy_ID = Convert.ToString(dr["i_ReviewedBy_ID"]);
                    lst.Add(cm);

                }
            }
            catch (Exception)
            {

                throw;
            }
            return (new JavaScriptSerializer().Serialize(lst));
        }

        public static string Contract_Collobrator_Master_DML(Contract_Collobrator_Master _Contract_Collobrator_Master, string mode)
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
                parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.i_ID;
                if (mode.ToString() != "Delete")
                {



                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Name";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Email1";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Email1;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Email2";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Email2;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_PhoNo";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_PhoNo;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Institution";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_Institution;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Country_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.i_Country_ID;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Contract_AssignDate";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.dt_Contract_AssignDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ReviewedBy_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.i_ReviewedBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@S_ReviewedByName";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.S_ReviewedByName;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Contract_Collobrator_Master.s_CreatedBy_ID;



                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spContract_Collobrator_MasterDML", parameter)))
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
        //--== static==============================================END OF Properties===============================================================================
        public static string Contract_Details_DML(Contract_Details _Contract_Details, List<SelectedCollborators_Details> lstSelCollab, List<Selected_Clause_Details> lstSelClause, List<ContractDetails_MultipleContractFile> lstmultiple, string mode)
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
                parameter[parameter.Count - 1].Value = _Contract_Details.i_ID;
                if (mode.ToString() != "Delete")
                {


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Contract_Name";
                    parameter[parameter.Count - 1].Value = _Contract_Details.s_Contract_Name;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Contract_Display_Id";
                    parameter[parameter.Count - 1].Value = _Contract_Details.s_Contract_Display_Id;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Contract_Category_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Contract_Category_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Contract_Status_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Contract_Status_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_LastUpdated_Date";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_LastUpdated_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Govt_Lawcountry";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Govt_Lawcountry;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Clauses_File";
                    parameter[parameter.Count - 1].Value = _Contract_Details.s_Clauses_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_UploadedContract_File";
                    parameter[parameter.Count - 1].Value = _Contract_Details.s_UploadedContract_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Effective_Date";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_Effective_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Finalization_Date";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_Finalization_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_LastSigned_Date";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_LastSigned_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Expiry_Date";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_Expiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_Amendments";
                    parameter[parameter.Count - 1].Value = _Contract_Details.b_Amendments;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_NewExpiry_Date";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_NewExpiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_AmendmenstContract_File";
                    parameter[parameter.Count - 1].Value = _Contract_Details.s_AmendmenstContract_File;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Hospital_Cost";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Hospital_Cost;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Investigator_fees";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Investigator_fees;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Inkind_Contribution";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Inkind_Contribution;


                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Coordinator_fess";
                    parameter[parameter.Count - 1].Value = _Contract_Details.i_Coordinator_fess;

                    //----------Added by Atul
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Contract_Details.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Contract_Details.UName;

                    //----------END by Atul
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Statusdate";
                    parameter[parameter.Count - 1].Value = _Contract_Details.dt_Contract_StatusDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@SelectedCollborators_Details";
                    parameter[parameter.Count - 1].Value = lstSelCollab.ListToDatatable().getColumns(3);
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Selected_Clause_Details";
                    parameter[parameter.Count - 1].Value = lstSelClause.ListToDatatable().getColumns(4);
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@ContractDetails_MultipleContractFile";
                    parameter[parameter.Count - 1].Value = lstmultiple.ListToDatatable();




                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spContract_DetailsDML", parameter)))
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
        //--== static==============================================END OF Properties===============================================================================
        public static string Contract_Master_DML(Contract_Master _Contract_Master, List<Contract_Collaborator_Details> clist, string mode)
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
                parameter[parameter.Count - 1].Value = _Contract_Master.i_ID;
                if (mode.ToString() != "Delete")
                {



                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Project_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Master.i_Project_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Contract_ReqDate";
                    parameter[parameter.Count - 1].Value = _Contract_Master.dt_Contract_ReqDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Contract_AssignDate";
                    parameter[parameter.Count - 1].Value = _Contract_Master.dt_Contract_AssignDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_ReviewedBy_ID";
                    parameter[parameter.Count - 1].Value = _Contract_Master.i_ReviewedBy_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@S_ReviewedByName";
                    parameter[parameter.Count - 1].Value = _Contract_Master.S_ReviewedByName;


                    //----------Added by Atul
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Contract_Master.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Username";
                    parameter[parameter.Count - 1].Value = _Contract_Master.UName;

                    //----------END by Atul

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Contract_Collaborator_Details";
                    parameter[parameter.Count - 1].Value = clist.ListToDatatable();


                    //---------------Update alias1,alias2,dsrbNo,Short Title-----------in project master
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Short_Title";
                    parameter[parameter.Count - 1].Value = _Contract_Master.s_Short_Title;
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias1";
                    parameter[parameter.Count - 1].Value = _Contract_Master.s_Project_Alias1;
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Project_Alias2";
                    parameter[parameter.Count - 1].Value = _Contract_Master.s_Project_Alias2;
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_IRB_No";
                    parameter[parameter.Count - 1].Value = _Contract_Master.s_IRB_No;
                    //------------------END---------------------


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spContract_MasterDML", parameter)))
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
        //--== static==============================================END OF Properties===============================================================================
        public static string ContractDetails_MultipleContractFile_DML(ContractDetails_MultipleContractFile _ContractDetails_MultipleContractFile, string mode)
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
                    parameter[parameter.Count - 1].ParameterName = "@i_ContractDetailsID";
                    parameter[parameter.Count - 1].Value = _ContractDetails_MultipleContractFile.i_ContractDetailsID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_ContractFile";
                    parameter[parameter.Count - 1].Value = _ContractDetails_MultipleContractFile.s_ContractFile;


                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spProjectMasterDML", parameter)))
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
    }
}
