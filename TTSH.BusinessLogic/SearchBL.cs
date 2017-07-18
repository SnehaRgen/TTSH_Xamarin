using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTSH.Entity;
using System.Threading.Tasks;
using TTSH.DataAccess;
using System.Data;

namespace TTSH.BusinessLogic
{
    public class SearchBL
    {
        public static Dictionary<string, List<Search>> GetSearchData(string SearchInputValue, string SearchFilterCriteria, string UserID, string UserGroup)
        {
            string Message = null;
            string SearchString1 = null, SearchString2 = null, SearchString3 = null;
            string[] ArrSearch = null;
            DataTable dtData = null;
            List<Search> lstSearch = null;
            Dictionary<string, List<Search>> objSearch = new Dictionary<string, List<Search>>();
            try
            {
                if (string.IsNullOrEmpty(SearchInputValue))
                {
                    Message = "Please enter input value";
                }
                if (string.IsNullOrEmpty(SearchFilterCriteria))
                {
                    Message = "Please enter filter criteria";
                }
                if (SearchInputValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 3)
                {
                    Message = "Please do not enter more than 3 input values";
                }

                string[] sArray = { "FEASIBALITY", "ETHICS", "SELECTED", "REGULATORY", "GRANT", "CONTRACT", "ALLPROJECTS", "CONTRACT_MGMT", "Grant_Master", "Grant_SeniorCSCS" };
                if (!sArray.Contains(SearchFilterCriteria))
                {
                    Message = "Please enter proper filter criteria";
                }

                ArrSearch = SearchInputValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                switch (ArrSearch.Count())
                {
                    case 3:
                        SearchString1 = ArrSearch[0];
                        SearchString2 = ArrSearch[1];
                        SearchString3 = ArrSearch[2];
                        break;
                    case 2:
                        SearchString1 = ArrSearch[0];
                        SearchString2 = ArrSearch[1];
                        break;
                    case 1:
                        SearchString1 = ArrSearch[0];
                        break;
                    default:
                        SearchString1 = "";
                        SearchString2 = "";
                        SearchString3 = "";
                        break;
                }



                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_SearchString1";
                parameter[parameter.Count - 1].Value = SearchString1;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_SearchString2";
                parameter[parameter.Count - 1].Value = SearchString2;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_SearchString3";
                parameter[parameter.Count - 1].Value = SearchString3;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserID";
                parameter[parameter.Count - 1].Value = UserID;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserGroup";
                parameter[parameter.Count - 1].Value = UserGroup;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@s_Filter_Criteria";
                parameter[parameter.Count - 1].Value = SearchFilterCriteria;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Size = 500;
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;

                dtData = _helper.GetData("dbo.spSearch_Projects", parameter);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    lstSearch = new List<Search>();

                    foreach (DataRow dr in dtData.Rows)
                    {
                        if (dr.Table.Columns.Contains("s_Grant_No") )
                        {
                            lstSearch.Add(new Search()
                            {

                                i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                                PI_Name = (dr.IsNull("PI_Name") ? "" : Convert.ToString(dr["PI_Name"])),
                                GrantName = (dr.IsNull("GrantName") ? "" : Convert.ToString(dr["GrantName"])),
                                AwardOrg = (dr.IsNull("AwardOrg") ? "" : Convert.ToString(dr["AwardOrg"])),
                                s_Grant_No = (dr.IsNull("s_Grant_No") ? "" : Convert.ToString(dr["s_Grant_No"])),
                                GrantExpDate = (dr.IsNull("GrantExpDate") ? null :(DateTime?) Convert.ToDateTime(dr["GrantExpDate"])),
                                StartDate = (dr.IsNull("StartDate") ? null : (DateTime?)Convert.ToDateTime(dr["StartDate"]))                               
                            });
                        
                        }
                        else if (dr.Table.Columns.Contains("cordinatorstatus") == true)
                        {
                            lstSearch.Add(new Search()
                            {
                                i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                                PI_Names = (dr.IsNull("PI_Name") ? "" : Convert.ToString(dr["PI_Name"])),
                                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID") ? "" : Convert.ToString(dr["s_Display_Project_ID"])),
                                s_IRB_No = (dr.IsNull("s_IRB_No") ? "" : Convert.ToString(dr["s_IRB_No"])),
                                s_Project_Category = (dr.IsNull("Project_Category_Name") ? "" : Convert.ToString(dr["Project_Category_Name"])),
                                s_Project_Title = (dr.IsNull("s_Project_Title") ? "" : Convert.ToString(dr["s_Project_Title"])),
                                Study_Status = (dr.IsNull("Study_Status") ? "New" : Convert.ToString(dr["Study_Status"])),
                                //Selected_ID = (dr.IsNull("Selected_ID") ? 0 : Convert.ToInt32(dr["Selected_ID"])),
                                Status = (dr.IsNull("status") ? "New" : Convert.ToString(dr["status"])),
                                cordinatorstatus = (dr.IsNull("cordinatorstatus") ? "Edit" : Convert.ToString(dr["cordinatorstatus"])),
                                S_ProjectStatus = dr.Table.Columns.Contains("S_ProjectStatus") ? dr.IsNull("S_ProjectStatus") == true ? "" : Convert.ToString(dr["S_ProjectStatus"]) : "",
                                EthicsStatus = dr.Table.Columns.Contains("EthicsStatus") ? dr.IsNull("EthicsStatus") == true ? "" : Convert.ToString(dr["EthicsStatus"]) : "",
                                iRecordExists = dr.Table.Columns.Contains("IsPresent") ? dr.IsNull("IsPresent") == true ? 0 : Convert.ToInt64(dr["IsPresent"]) : 0,/*used in search*/
                                IsCoordinator = dr.Table.Columns.Contains("IsCoordinator") ? dr.IsNull("IsCoordinator") == true ? "" : Convert.ToString(dr["IsCoordinator"]) : "",
                                DetailsExist = dr.Table.Columns.Contains("DetailsExist") ? dr.IsNull("DetailsExist") == true ? "" : Convert.ToString(dr["DetailsExist"]) : "",
                                Project_Status_Disable = dr.Table.Columns.Contains("Project_Status_Ico") ? dr.IsNull("Project_Status_Ico") == true ? "" : Convert.ToString(dr["Project_Status_Ico"]) : ""
                            });
                        }
                        else if (dr.Table.Columns.Contains("s_OutcomeStatus") == true)
                        {
                            lstSearch.Add(new Search()
                            {
                                i_ID = (dr.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(dr["i_ID"]),
                                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID")) == true ? "" : Convert.ToString(dr["s_Display_Project_ID"]),
                                s_Project_Title = (dr.IsNull("s_Project_Title")) == true ? "" : Convert.ToString(dr["s_Project_Title"]),
                                s_IRB_No = (dr.IsNull("s_IRB_No")) == true ? "" : Convert.ToString(dr["s_IRB_No"]),
                                PI_Name = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                s_OutComeStatus = (dr.IsNull("s_OutcomeStatus")) == true ? "" : Convert.ToString(dr["s_OutcomeStatus"]),
                                s_SubmissionStatus = (dr.IsNull("s_SubmissionStatus")) == true ? "" : Convert.ToString(dr["s_SubmissionStatus"]),
                                i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                                GM_ID = dr.Table.Columns.Contains("GM_ID") ? dr.IsNull("GM_ID") == true ? 0 : Convert.ToInt32(dr["GM_ID"]) : 0,
                                GD_ID = dr.Table.Columns.Contains("GD_ID") ? dr.IsNull("GD_ID") == true ? 0 : Convert.ToInt32(dr["GD_ID"]) : 0,
                                GrantDetailStatus = dr.Table.Columns.Contains("GrantDetailStatus") ? dr.IsNull("GrantDetailStatus") == true ? "" : Convert.ToString(dr["GrantDetailStatus"]) : "",
                                Project_Category_Name = dr.Table.Columns.Contains("Project_Category") ? dr.IsNull("Project_Category") == true ? "" : Convert.ToString(dr["Project_Category"]) : "",
                                S_ProjectStatus = dr.Table.Columns.Contains("S_ProjectStatus") ? dr.IsNull("S_ProjectStatus") == true ? "" : Convert.ToString(dr["S_ProjectStatus"]) : "",
                                EthicsStatus = dr.Table.Columns.Contains("EthicsStatus") ? dr.IsNull("EthicsStatus") == true ? "" : Convert.ToString(dr["EthicsStatus"]) : "",
                                isGrantDetailsApplied = dr.Table.Columns.Contains("GrantDetails_Applied") ? dr.IsNull("GrantDetails_Applied") == true ? true : false : false,
                                GrantDetails_Applied = dr.Table.Columns.Contains("GrantDetails_Applied") ? dr.IsNull("GrantDetails_Applied") == true ? true : false : false,

                                ParentProject = dr.Table.Columns.Contains("ParentProject") ? (dr.IsNull("ParentProject")) == true ? "" : Convert.ToString(dr["ParentProject"]) : "",
                                ChildParentProject = dr.Table.Columns.Contains("ChildParentProject") ? (dr.IsNull("ChildParentProject")) == true ? "" : Convert.ToString(dr["ChildParentProject"]) : "",
                                parentProjectCount = dr.Table.Columns.Contains("parentProjectCount") ? (dr.IsNull("parentProjectCount")) == true ? "" : Convert.ToString(dr["parentProjectCount"]) : "",
                                IsChildorParent = dr.Table.Columns.Contains("IsChildorParent") ? (dr.IsNull("IsChildorParent")) == true ? "" : Convert.ToString(dr["IsChildorParent"]) : "",

                                Prog = dr.Table.Columns.Contains("Prog") ? (dr.IsNull("Prog")) == true ? "" : Convert.ToString(dr["Prog"]) : "",
                                Mutli = dr.Table.Columns.Contains("Mutli") ? (dr.IsNull("Mutli")) == true ? "" : Convert.ToString(dr["Mutli"]) : "",
                                iRecordExists = dr.Table.Columns.Contains("IsPresent") ? (dr.IsNull("IsPresent")) == true ? 0 : Convert.ToInt64(dr["IsPresent"]) : 0/*used in search*/,
                                Project_Status_Disable = dr.Table.Columns.Contains("Project_Status_Ico") ? dr.IsNull("Project_Status_Ico") == true ? "" : Convert.ToString(dr["Project_Status_Ico"]) : ""
                            });
                        }

                        else if (dr.Table.Columns.Contains("Contract_Application_status") == false && dr.Table.Columns.Contains("s_Department") == false && dr.Table.Columns.Contains("Feasibality_Status") == false)
                        {
                            lstSearch.Add(new Search()
                            {
                                i_ID = (dr.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(dr["i_ID"]),
                                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID")) == true ? "" : Convert.ToString(dr["s_Display_Project_ID"]),
                                s_Project_Title = (dr.IsNull("s_Project_Title")) == true ? "" : Convert.ToString(dr["s_Project_Title"]),
                                s_Project_Category = (dr.IsNull("Project_Category")) == true ? "" : Convert.ToString(dr["Project_Category"]),
                                Project_Category = (dr.IsNull("Project_Category")) == true ? "" : Convert.ToString(dr["Project_Category"]),
                                Feasibility_Status_Name = null,
                                s_IRB_No = (dr.IsNull("s_IRB_No")) == true ? "" : Convert.ToString(dr["s_IRB_No"]),
                                PI_Names = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                PI_Name = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                sContractApplicationstatus = null,
                                sDepartment = null,
                                iRecordExists = (dr.IsNull("IsPresent")) == true ? 0 : Convert.ToInt64(dr["IsPresent"]),/*used in search*/
                                Project_Status = (dr.IsNull("Project_Status")) == true ? "New" : Convert.ToString(dr["Project_Status"]),
                                Ethics_ID = (dr.IsNull("Ethics_ID")) == true ? "" : Convert.ToString(dr["Ethics_ID"]),
                                CTC_status = dr.Table.Columns.Contains("CTC_status") ? dr.IsNull("CTC_status") == true ? "" : Convert.ToString(dr["CTC_status"]) : "",
                                CTCCount = dr.Table.Columns.Contains("CTCCount") ? dr.IsNull("CTCCount") == true ? 0 : Convert.ToInt32(dr["CTCCount"]) : 0,
                                i_Project_ID = dr.Table.Columns.Contains("i_Project_ID") ? dr.IsNull("i_Project_ID") == true ? 0 : Convert.ToInt32(dr["i_Project_ID"]) : 0,
                                S_ProjectStatus = dr.Table.Columns.Contains("S_ProjectStatus") ? dr.IsNull("S_ProjectStatus") == true ? "" : Convert.ToString(dr["S_ProjectStatus"]) : "",
                                EthicsStatus = dr.Table.Columns.Contains("EthicsStatus") ? dr.IsNull("EthicsStatus") == true ? "" : Convert.ToString(dr["EthicsStatus"]) : "",
                                Project_Status_Disable = dr.Table.Columns.Contains("Project_Status_Ico") ? dr.IsNull("Project_Status_Ico") == true ? "" : Convert.ToString(dr["Project_Status_Ico"]) : ""

                            });
                        }
                        else if (dr.Table.Columns.Contains("Contract_Application_status") == false && dr.Table.Columns.Contains("s_Department") == false && dr.Table.Columns.Contains("Feasibality_Status") == true)
                        {
                            lstSearch.Add(new Search()
                            {
                                i_ID = (dr.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(dr["i_ID"]),
                                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID")) == true ? "" : Convert.ToString(dr["s_Display_Project_ID"]),
                                s_Project_Title = (dr.IsNull("s_Project_Title")) == true ? "" : Convert.ToString(dr["s_Project_Title"]),
                                s_Project_Category = (dr.IsNull("Project_Category")) == true ? "" : Convert.ToString(dr["Project_Category"]),
                                Feasibility_Status_Name = (dr.IsNull("Feasibality_Status")) == true ? "New" : Convert.ToString(dr["Feasibality_Status"]),
                                s_IRB_No = (dr.IsNull("s_IRB_No")) == true ? "" : Convert.ToString(dr["s_IRB_No"]),
                                PI_Names = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                sContractApplicationstatus = null,
                                sDepartment = null,
                                iRecordExists = (dr.IsNull("IsPresent")) == true ? 0 : Convert.ToInt64(dr["IsPresent"]),/*used in search*/
                                i_Feasibility_ID = (dr.IsNull("i_Feasibility_ID")) == true ? "" : Convert.ToString(dr["i_Feasibility_ID"]),
                                i_Project_ID = dr.Table.Columns.Contains("i_Project_ID") ? dr.IsNull("i_Project_ID") == true ? 0 : Convert.ToInt32(dr["i_Project_ID"]) : 0,
                                S_ProjectStatus = dr.Table.Columns.Contains("S_ProjectStatus") ? dr.IsNull("S_ProjectStatus") == true ? "" : Convert.ToString(dr["S_ProjectStatus"]) : "",
                                EthicsStatus = dr.Table.Columns.Contains("EthicsStatus") ? dr.IsNull("EthicsStatus") == true ? "" : Convert.ToString(dr["EthicsStatus"]) : "",
                                Project_Status_Disable = dr.Table.Columns.Contains("Project_Status_Ico") ? dr.IsNull("Project_Status_Ico") == true ? "" : Convert.ToString(dr["Project_Status_Ico"]) : ""
                            });
                        }
                        else if (dr.Table.Columns.Contains("Contract_Application_status") == true)
                        {
                            lstSearch.Add(new Search()
                            {
                                i_ID = (dr.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(dr["i_ID"]),
                                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID")) == true ? "" : Convert.ToString(dr["s_Display_Project_ID"]),
                                s_Project_Title = (dr.IsNull("s_Project_Title")) == true ? "" : Convert.ToString(dr["s_Project_Title"]),
                                Project_Category_Name = (dr.IsNull("Project_Category_Name")) == true ? "" : Convert.ToString(dr["Project_Category_Name"]),
                                Project_Type = (dr.IsNull("Project_Type")) == true ? "" : Convert.ToString(dr["Project_Type"]),
                                Status = dr.Table.Columns.Contains("status") == true ? (dr.IsNull("Status")) == true ? "" : Convert.ToString(dr["Status"]) : "",
                                ContAppStatus = dr.Table.Columns.Contains("ContAppStatus") == true ? (dr.IsNull("ContAppStatus")) == true ? "" : Convert.ToString(dr["ContAppStatus"]) : "",
                                Feasibility_Status_Name = null,
                                s_IRB_No = (dr.IsNull("s_IRB_No")) == true ? "" : Convert.ToString(dr["s_IRB_No"]),
                                PI_Names = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                PI_Name = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                sContractApplicationstatus = (dr.IsNull("Contract_Application_status")) == true ? "" : Convert.ToString(dr["Contract_Application_status"]),
                                sDepartment = null,
                                iRecordExists = (dr.IsNull("IsPresent")) == true ? 0 : Convert.ToInt64(dr["IsPresent"]),/*used in search*/
                                Contracts = dr.Table.Columns.Contains("Description") == true ? (dr.IsNull("Description")) == true ? "" : Convert.ToString(dr["Description"]) : "",
                                Contract_Status = dr.Table.Columns.Contains("Contract_Status") == true ? (dr.IsNull("Contract_Status")) == true ? "" : Convert.ToString(dr["Contract_Status"]) : "",
                                i_Project_ID = dr.Table.Columns.Contains("i_Project_ID") ? dr.IsNull("i_Project_ID") == true ? 0 : Convert.ToInt32(dr["i_Project_ID"]) : 0,
                                Created_By = dr.Table.Columns.Contains("s_CreatedBy_ID") ? dr.IsNull("s_CreatedBy_ID") == true ? "" : Convert.ToString(dr["s_CreatedBy_ID"]) : "",
                                S_ProjectStatus = dr.Table.Columns.Contains("S_ProjectStatus") ? dr.IsNull("S_ProjectStatus") == true ? "" : Convert.ToString(dr["S_ProjectStatus"]) : "",
                                EthicsStatus = dr.Table.Columns.Contains("EthicsStatus") ? dr.IsNull("EthicsStatus") == true ? "" : Convert.ToString(dr["EthicsStatus"]) : "",
                                Project_Status_Disable = dr.Table.Columns.Contains("Project_Status_Ico") ? dr.IsNull("Project_Status_Ico") == true ? "" : Convert.ToString(dr["Project_Status_Ico"]) : ""
                            });
                        }
                        else if (dr.Table.Columns.Contains("s_Department") == true)
                        {
                            lstSearch.Add(new Search()
                            {
                                i_ID = (dr.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(dr["i_ID"]),
                                s_Display_Project_ID = (dr.IsNull("s_Display_Project_ID")) == true ? "" : Convert.ToString(dr["s_Display_Project_ID"]),
                                s_Project_Title = (dr.IsNull("s_Project_Title")) == true ? "" : Convert.ToString(dr["s_Project_Title"]),
                                s_Project_Category = (dr.IsNull("Project_Category")) == true ? "" : Convert.ToString(dr["Project_Category"]),
                                Feasibility_Status_Name = null,
                                Project_Type = (dr.IsNull("Project_Type")) == true ? "" : Convert.ToString(dr["Project_Type"]),
                                s_IRB_No = (dr.IsNull("s_IRB_No")) == true ? "" : Convert.ToString(dr["s_IRB_No"]),
                                PI_Names = (dr.IsNull("PI_NAME")) == true ? "" : Convert.ToString(dr["PI_NAME"]),
                                sContractApplicationstatus = null,
                                sDepartment = (dr.IsNull("s_Department")) == true ? "" : Convert.ToString(dr["s_Department"]),
                                iRecordExists = (dr.IsNull("IsPresent")) == true ? 0 : Convert.ToInt64(dr["IsPresent"]),/*used in search*/
                                i_Project_ID = dr.Table.Columns.Contains("i_Project_ID") ? dr.IsNull("i_Project_ID") == true ? 0 : Convert.ToInt32(dr["i_Project_ID"]) : 0,
                                S_ProjectStatus = dr.Table.Columns.Contains("S_ProjectStatus") ? dr.IsNull("S_ProjectStatus") == true ? "" : Convert.ToString(dr["S_ProjectStatus"]) : "",
                                EthicsStatus = dr.Table.Columns.Contains("EthicsStatus") ? dr.IsNull("EthicsStatus") == true ? "" : Convert.ToString(dr["EthicsStatus"]) : "",
                                Project_Status_Disable = dr.Table.Columns.Contains("Project_Status_Ico") ? dr.IsNull("Project_Status_Ico") == true ? "" : Convert.ToString(dr["Project_Status_Ico"]) : ""

                            });
                        }

                    }
                }
                else
                {
                    int retValPos = parameter.FindIndex(delegate(System.Data.Common.DbParameter pm) { return pm.ParameterName == "@Ret_Parameter"; });
                    Message = parameter[retValPos].Value.ToString();
                }
                if (string.IsNullOrEmpty(Message))
                {
                    Message = "SUCCESS";
                }

                objSearch.Add(Message, lstSearch);
                return objSearch;
                //return lstSearch;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return null;
            }
        }
    }
}
