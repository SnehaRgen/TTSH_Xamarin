using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSH.DataAccess;
using TTSH.Entity;

namespace TTSH.BusinessLogic
{
    
    public class ReportsBL
    {
        #region ProjectCategory
        public static List<RptProjectCategory> ListRptProjectCategory()
        {
            List<RptProjectCategory> lstItems = new List<RptProjectCategory>();

            try
            {
                DataTable dtProjectCategory = new DataTable();

                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                dtProjectCategory = _helper.GetData("dbo.spRptGetProjectCategory", parameter);
                if (dtProjectCategory != null && dtProjectCategory.Rows.Count > 0)
                {
                    foreach (DataRow row in dtProjectCategory.Rows)
                    {
                        lstItems.Add(new RptProjectCategory() 
                        {
                            CategoryId = (row.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(row["i_ID"]),
                            CategoryName = (row.IsNull("s_Name")) == true ? "" : Convert.ToString(row["s_Name"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstItems;
        }
        #endregion

        #region ProjectType
        public static List<RptProjectType> ListRptProjectType()
        {
            List<RptProjectType> lstItems = new List<RptProjectType>();

            try
            {
                DataTable DtItems = new DataTable();

                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                DtItems = _helper.GetData("dbo.spRptGetProjectType", parameter);
                if (DtItems != null && DtItems.Rows.Count > 0)
                {
                    foreach (DataRow row in DtItems.Rows)
                    {
                        lstItems.Add(new RptProjectType()
                        {
                            TypeId = (row.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(row["i_ID"]),
                            TypeName = (row.IsNull("s_Name")) == true ? "" : Convert.ToString(row["s_Name"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstItems;
        }
        #endregion

        #region Department
        
        public static List<RptDepartment> ListRptDepartment()
        {
            List<RptDepartment> lstItems = new List<RptDepartment>();

            try
            {
                DataTable DtItems = new DataTable();

                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                DtItems = _helper.GetData("dbo.spRptGetDepartment", parameter);
                if (DtItems != null && DtItems.Rows.Count > 0)
                {
                    foreach (DataRow row in DtItems.Rows)
                    {
                        lstItems.Add(new RptDepartment()
                        {
                            DepartmentId = (row.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(row["i_ID"]),
                            DepartmentName = (row.IsNull("s_Name")) == true ? "" : Convert.ToString(row["s_Name"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstItems;
        }
        #endregion

        #region PIName
        
        public static List<RptPIName> ListRptPIName()
        {
            List<RptPIName> lstItems = new List<RptPIName>();

            try
            {
                DataTable DtItems = new DataTable();

                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                DtItems = _helper.GetData("dbo.spRptGetPIName", parameter);
                if (DtItems != null && DtItems.Rows.Count > 0)
                {
                    foreach (DataRow row in DtItems.Rows)
                    {
                        lstItems.Add(new RptPIName()
                        {
                            PIId = (row.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(row["i_ID"]),
                            PIName = (row.IsNull("s_Name")) == true ? "" : Convert.ToString(row["s_Name"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstItems;
        }

        public static List<RptPIName> ListRptPINameByDepartment(String DepartmentId)
        {
            List<RptPIName> lstItems = new List<RptPIName>();

            try
            {
                DataTable DtItems = new DataTable();

                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@Department";
                parameter[parameter.Count - 1].Value = DepartmentId;

                DtItems = _helper.GetData("dbo.spRptGetPIBYDEPT", parameter);
                if (DtItems != null && DtItems.Rows.Count > 0)
                {
                    foreach (DataRow row in DtItems.Rows)
                    {
                        lstItems.Add(new RptPIName()
                        {
                            PIId = (row.IsNull("i_ID")) == true ? 0 : Convert.ToInt64(row["i_ID"]),
                            PIName = (row.IsNull("PIName")) == true ? "" : Convert.ToString(row["PIName"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstItems;
        }

        #endregion
    }
}
