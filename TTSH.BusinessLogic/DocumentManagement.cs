using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using TTSH.Entity;
using TTSH.DataAccess;
using System.Data;
using System.ComponentModel;

namespace TTSH.BusinessLogic
{
    public sealed class DocumentManagement
    {
        #region Declarations
        // TTSH.BusinessLogic.RoleAccessManagement _RoleAccessManagement = new TTSH.BusinessLogic.RoleAccessManagement();
        static DataSet dsdocument;
        #endregion

        /// <summary>
        /// Used To Get The Document Details Related To 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static DocumentManagementSystem GetDocumentWithProject(int projectId)
        {
            DocumentManagementSystem docManSys;
            DataHelper _helper;

            try
            {
                docManSys = new DocumentManagementSystem();
                _helper = new DataHelper();
                _helper.InitializedHelper();
                dsdocument = new System.Data.DataSet();


                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@project_Id";
                parameter[parameter.Count - 1].Value = projectId;

                dsdocument = _helper.GetDataSet("sp_GetProjetDetailsWIthDocument", parameter);
                docManSys.project = new List<Project_Master>();
                docManSys.documentManagementSYstemFile = new List<DocumentManagementSystemFile>();
                if (dsdocument != null)
                {
                    if (dsdocument.Tables.Count > 0 && dsdocument.Tables[0].Rows.Count > 0)
                    {
                        //Adding Parent Menus
                        if (dsdocument.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsdocument.Tables[0].Rows.Count; i++)
                            {
                                docManSys.project.Add(new Project_Master
                                {
                                    s_Display_Project_ID = (dsdocument.Tables[0].Rows[i]["s_Display_Project_ID"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["s_Display_Project_ID"].ToString() : "",
                                    s_Project_Title = (dsdocument.Tables[0].Rows[i]["s_Project_Title"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["s_Project_Title"].ToString() : "",
                                    s_Short_Title = (dsdocument.Tables[0].Rows[i]["s_Short_Title"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["s_Short_Title"].ToString() : "",
                                    s_Project_Alias1 = (dsdocument.Tables[0].Rows[i]["s_Project_Alias1"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["s_Project_Alias1"].ToString() : "",
                                    s_Project_Alias2 = (dsdocument.Tables[0].Rows[i]["s_Project_Alias2"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["s_Project_Alias2"].ToString() : "",
                                    s_IRB_No = (dsdocument.Tables[0].Rows[i]["s_IRB_No"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["s_IRB_No"].ToString() : ""
                                });
                            }
                        }

                        if (dsdocument.Tables[1].Rows.Count > 0)
                        {

                            for (int i = 0; i < dsdocument.Tables[1].Rows.Count; i++)
                            {
                                docManSys.documentManagementSYstemFile.Add(new DocumentManagementSystemFile
                                {
                                    i_DMSId = (dsdocument.Tables[1].Rows[i]["i_DMSId"] != DBNull.Value) ? Convert.ToInt32(dsdocument.Tables[1].Rows[i]["i_DMSId"]) : 0,
                                    DocTitle = (dsdocument.Tables[1].Rows[i]["DocTitle"] != DBNull.Value) ? dsdocument.Tables[1].Rows[i]["DocTitle"].ToString() : "",
                                    DocDescription = (dsdocument.Tables[1].Rows[i]["DocDescription"] != DBNull.Value) ? dsdocument.Tables[1].Rows[i]["DocDescription"].ToString() : "",
                                    DocCategory = (dsdocument.Tables[1].Rows[i]["DocCategory"] != DBNull.Value) ? dsdocument.Tables[1].Rows[i]["DocCategory"].ToString() : "",
                                    FileName = (dsdocument.Tables[1].Rows[i]["FileName"] != DBNull.Value) ? dsdocument.Tables[1].Rows[i]["FileName"].ToString() : ""
                                });
                            }
                        }

                    }
                }

                return docManSys;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<DocumentManagementSystemFile> GetDocuments(string searchText)
        {

            DataHelper _helper = new DataHelper();
            _helper.InitializedHelper();
            dsdocument = new System.Data.DataSet();

            List<DocumentManagementSystemFile> DocList = new List<DocumentManagementSystemFile>();
            try
            {

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@searchText";
                parameter[parameter.Count - 1].Value = searchText;

                dsdocument = _helper.GetDataSet("sp_GetProjetDetailsWIthDocument", parameter);

                if (dsdocument != null)
                {
                    if (dsdocument.Tables.Count > 0 && dsdocument.Tables[0].Rows.Count > 0)
                    {
                        //Adding Parent Menus
                        if (dsdocument.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < dsdocument.Tables[0].Rows.Count; i++)
                            {
                                DocList.Add(
                                new DocumentManagementSystemFile
                                {
                                    i_DMSId = (dsdocument.Tables[0].Rows[i]["i_DMSId"] != DBNull.Value) ? Convert.ToInt32(dsdocument.Tables[0].Rows[i]["i_DMSId"]) : 0,
                                    DocTitle = (dsdocument.Tables[0].Rows[i]["DocTitle"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["DocTitle"].ToString() : "",
                                    DocDescription = (dsdocument.Tables[0].Rows[i]["DocDescription"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["DocDescription"].ToString() : "",
                                    DocCategory = (dsdocument.Tables[0].Rows[i]["DocCategory"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["DocCategory"].ToString() : "",
                                    FileName = (dsdocument.Tables[0].Rows[i]["FileName"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["FileName"].ToString() : "",
                                    Project_ID = (dsdocument.Tables[0].Rows[i]["Project_ID"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["Project_ID"].ToString() : "",
                                    ProjectTile = (dsdocument.Tables[0].Rows[i]["ProjectTile"] != DBNull.Value) ? dsdocument.Tables[0].Rows[i]["ProjectTile"].ToString() : ""
                                });
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return DocList;
        }

        /// <summary>
        /// Function To Save Data
        /// </summary>
        /// <param name="docManSys"></param>
        /// <returns></returns>
        public static bool SaveDocumentData(List<DMS_DocumentManagementSystem> docManSys)
        {
            DataHelper _helper;
            _helper = new DataHelper();
            _helper.InitializedHelper();
            DataTable tbl_DocumentManagementSystem = new DataTable();
            try
            {
                ////Creating DataTable
                tbl_DocumentManagementSystem.Columns.Add("DocTitle", typeof(string));
                tbl_DocumentManagementSystem.Columns.Add("DocDescription", typeof(string));
                tbl_DocumentManagementSystem.Columns.Add("DocType", typeof(Int32));
                tbl_DocumentManagementSystem.Columns.Add("i_Project_ID", typeof(Int32));
                tbl_DocumentManagementSystem.Columns.Add("s_DMS_FileName", typeof(string));

                

                foreach (DMS_DocumentManagementSystem dms in docManSys)
                {
                    tbl_DocumentManagementSystem.Rows.Add(dms.DocTitle, dms.DocDescription, dms.DocType, dms.i_Project_ID, dms.s_DMS_FileName);
                }

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@docmanlib";
                parameter[parameter.Count - 1].Value = tbl_DocumentManagementSystem;
                //parameter[parameter.Count - 1].DbType = SqlDbType.Structured;


                _helper.GetDataSet("sp_InsertDocMan", parameter);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
      
       

    }
}
