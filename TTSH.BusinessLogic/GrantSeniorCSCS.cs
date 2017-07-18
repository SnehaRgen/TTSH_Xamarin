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
    public static class GrantSeniorCSCS
    {
        #region " Fill Main Grid "
        public static List<Senior_CSCS_Details> FillGrantSeniorCSCSGrid()
        {
            List<Senior_CSCS_Details> SCd = new List<Senior_CSCS_Details>();

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@StatementType";
                parameter[parameter.Count - 1].Value = "select";

                DataTable gridData = new DataTable();
                gridData = _helper.GetData("[dbo].[spSenior_CSCS_DetailsDML]", parameter);

                foreach (DataRow dr in gridData.Rows)
                {
                    SCd.Add(new Senior_CSCS_Details
                    {
                        i_ID = (dr["i_ID"] != DBNull.Value) ? Convert.ToInt32(dr["i_ID"]) : 0,
                        AwardOrg = (dr["AwardOrg"] != DBNull.Value) ? Convert.ToString(dr["AwardOrg"]) : "",
                        s_Grant_No = (dr["s_Grant_No"] != DBNull.Value) ? Convert.ToString(dr["s_Grant_No"]) : "",
                        GrantName = (dr["GrantName"] != DBNull.Value) ? Convert.ToString(dr["GrantName"]) : "",
                        StartDate = (dr["StartDate"] != DBNull.Value) ? Convert.ToString(dr["StartDate"]) : "",
                        PI_Name = (dr["PI_NAME"] != DBNull.Value) ? Convert.ToString(dr["PI_NAME"]) : "",
                        GrantExpDate = (dr["GrantExpDate"] != DBNull.Value) ? Convert.ToString(dr["GrantExpDate"]) : ""



                    });
                }
            }
            catch (Exception)
            {


            }

            return SCd;
        }
        #endregion

        #region " DMl Method "
        public static string Senior_CSCS_Details_DML(Senior_CSCS_Details _Senior_CSCS_Details, string mode)
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
                parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.i_ID;
                if (mode.ToString() != "Delete")
                {

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Award_org_ID";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.i_Award_org_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Grant_No";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.s_Grant_No;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_GrantName";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.i_GrantName;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Reaserch_IO";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.s_Reaserch_IO;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_AwardLetter_Date";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.dt_AwardLetter_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_AwardLetter_File";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.s_AwardLetter_File;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_StartDate";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.dt_StartDate;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@d_Protected_time";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.d_Protected_time;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Grant_Duration";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.s_Grant_Duration;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Grant_Expiry_Date";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.dt_Grant_Expiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@b_IsGrant_Extented";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.b_IsGrant_Extented;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_NewGrantExpiry_Date";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.dt_NewGrantExpiry_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@dt_Approval_Date";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.dt_Approval_Date;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_GrantExtended_period";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.s_GrantExtended_period;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Grant_Amount";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.i_Grant_Amount;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@i_Selected_PI_ID";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.i_Selected_PI_ID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@s_Budget_Details_String";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.s_Budget_Details_String;
                    

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@UserCId";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.UID;

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@USerName";
                    parameter[parameter.Count - 1].Value = _Senior_CSCS_Details.UName;

                    /*Dept PI table*/
                    DataTable PItable = new DataTable();
                    PItable.Columns.Add("i_SeniorCSCS_ID");
                    PItable.Columns.Add("i_PI_ID");


                    foreach (Project_PI pi in _Senior_CSCS_Details.Dept_PI)
                    {
                        PItable.Rows.Add(pi.i_Project_ID, pi.i_PI_ID);
                    }
                    /*End of Dept PI table*/

                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Senior_Project_Dept_PI";
                    parameter[parameter.Count - 1].Value = PItable;


                    /*Grant detail table*/
                    DataTable budgetTable = new DataTable();
                    budgetTable.Columns.Add("i_Senior_CSCS_ID");
                    budgetTable.Columns.Add("[s_Years]");
                    budgetTable.Columns.Add("s_Factors");
                    budgetTable.Columns.Add("i_Budget_Allocation");
                    budgetTable.Columns.Add("s_Yearly_Quaterly");
                    budgetTable.Columns.Add("i_Budget_Utilized");
                    budgetTable.Columns.Add("Q1");
                    budgetTable.Columns.Add("Q2");
                    budgetTable.Columns.Add("Q3");
                    budgetTable.Columns.Add("Q4");

                    foreach (Senior_CSCS_Budget_Allocation_Details details in _Senior_CSCS_Details.budgetDetails)
                    {

                        budgetTable.Rows.Add(details.i_Senior_CSCS_ID, details.s_Years, details.s_Factors, details.i_Budget_Allocation, details.s_Yearly_Quaterly, details.i_Budget_Utilized, details.i_Q1, details.i_Q2, details.i_Q3, details.i_Q4);
                    }

                    /*End of Grant detail table*/
                    parameter.Add(_helper.CreateDbParameter());
                    parameter[parameter.Count - 1].ParameterName = "@Grant_Senior_CSCS_BudgetData";
                    parameter[parameter.Count - 1].Value = budgetTable;
                  

                }
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Ret_Parameter";
                parameter[parameter.Count - 1].Direction = ParameterDirection.Output;
                parameter[parameter.Count - 1].Size = 500;
                if (Convert.ToBoolean(_helper.DMLOperation("dbo.spSenior_CSCS_DetailsDML", parameter)))
                {
                    result = "Success" + "|" + parameter[parameter.Count - 1].Value.ToString();
                }
                else
                {
                    result = parameter[parameter.Count - 1].Value.ToString();
                }
            }
            catch (Exception ex) {
                result = ex.Message;
            }
            return result;
        }
        //--================================================END OF Properties===============================================================================

        public static Senior_CSCS_Details GetSenior_CSCS_DetailsByID(int ID)
        {
            Senior_CSCS_Details _Senior_CSCS_Details = new Senior_CSCS_Details();
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
                    ProjectsData = _helper.GetData("dbo.spSenior_CSCS_DetailsDML", parameter);
                    foreach (DataRow dr in ProjectsData.Rows)
                    {
                        _Senior_CSCS_Details = new Senior_CSCS_Details()
                        {

                            i_ID = (dr.IsNull("i_ID") ? 0 : Convert.ToInt32(dr["i_ID"])),
                            i_Award_org_ID = (dr.IsNull("i_Award_org_ID") ? 0 : Convert.ToInt32(dr["i_Award_org_ID"])),
                            s_Grant_No = (dr.IsNull("s_Grant_No") ? string.Empty : Convert.ToString(dr["s_Grant_No"])),
                            i_GrantName = (dr.IsNull("i_GrantName") ? 0 : Convert.ToInt32(dr["i_GrantName"])),
                            s_Reaserch_IO = (dr.IsNull("s_Reaserch_IO") ? string.Empty : Convert.ToString(dr["s_Reaserch_IO"])),
                            dt_AwardLetter_Date = (dr.IsNull("dt_AwardLetter_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_AwardLetter_Date"])),
                            s_AwardLetter_File = (dr.IsNull("s_AwardLetter_File") ? string.Empty : Convert.ToString(dr["s_AwardLetter_File"])),
                            dt_StartDate = (dr.IsNull("dt_StartDate") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_StartDate"])),
                            d_Protected_time = (dr.IsNull("d_Protected_time") ? null : (double?)Convert.ToDouble(dr["d_Protected_time"])),
                            s_Grant_Duration = (dr.IsNull("s_Grant_Duration") ? string.Empty : Convert.ToString(dr["s_Grant_Duration"])),
                            dt_Grant_Expiry_Date = (dr.IsNull("dt_Grant_Expiry_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Grant_Expiry_Date"])),
                            b_IsGrant_Extented = (dr.IsNull("b_IsGrant_Extented") ? false : Convert.ToBoolean(dr["b_IsGrant_Extented"])),
                            dt_NewGrantExpiry_Date = (dr.IsNull("dt_NewGrantExpiry_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_NewGrantExpiry_Date"])),
                            dt_Approval_Date = (dr.IsNull("dt_Approval_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Approval_Date"])),
                            s_GrantExtended_period = (dr.IsNull("s_GrantExtended_period") ? string.Empty : Convert.ToString(dr["s_GrantExtended_period"])),
                            i_Grant_Amount = (dr.IsNull("i_Grant_Amount") ? 0 : Convert.ToInt32(dr["i_Grant_Amount"])),
                            //s_CreatedBy_ID = (dr.IsNull("s_CreatedBy_ID") ? string.Empty : Convert.ToString(dr["s_CreatedBy_ID"])),
                            //s_ModifyBy_ID = (dr.IsNull("s_ModifyBy_ID") ? string.Empty : Convert.ToString(dr["s_ModifyBy_ID"])),
                            //dt_Created_Date = (dr.IsNull("dt_Created_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Created_Date"])),
                            //dt_Modify_Date = (dr.IsNull("dt_Modify_Date") ? DateTime.MinValue : Convert.ToDateTime(dr["dt_Modify_Date"])),
                            //IsDeleted = (dr.IsNull("IsDeleted") ? false : Convert.ToBoolean(dr["IsDeleted"])),
                            //s_ModifyBy_Name = (dr.IsNull("s_ModifyBy_Name") ? string.Empty : Convert.ToString(dr["s_ModifyBy_Name"])),
                            //s_CreatedBy_Name = (dr.IsNull("s_CreatedBy_Name") ? string.Empty : Convert.ToString(dr["s_CreatedBy_Name"])),
                            i_Selected_PI_ID = (dr.IsNull("i_Selected_PI_ID") ? 0 : Convert.ToInt32(dr["i_Selected_PI_ID"])),
                            budgetDetails_XML = (dr.IsNull("budgetDetails_XML") ? string.Empty : Convert.ToString(dr["budgetDetails_XML"])),
                            Dept_PI_XML = (dr.IsNull("Dept_PI_XML") ? string.Empty : Convert.ToString(dr["Dept_PI_XML"])),
                            s_Budget_Details_String = (dr.IsNull("s_Budget_Details_String") ? string.Empty : Convert.ToString(dr["s_Budget_Details_String"]))

                        };
                    }
                }
                catch (Exception e) { }
                return _Senior_CSCS_Details;
            }
        }
        #endregion
    }
}
