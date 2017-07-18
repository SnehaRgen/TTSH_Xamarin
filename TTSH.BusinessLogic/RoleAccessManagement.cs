using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using TTSH.Entity;
using TTSH.DataAccess;

namespace TTSH.BusinessLogic
{
    /// <summary>
    /// Class For Role Access Management
    /// </summary>
   public sealed class RoleAccessManagement
    {
       #region Declarations
     // TTSH.BusinessLogic.RoleAccessManagement _RoleAccessManagement = new TTSH.BusinessLogic.RoleAccessManagement();
     static  DataSet dsroleAccess;
       #endregion


     
     
        /// <summary>
        /// All the Menus with their child nodes are displayed
        /// </summary>
        /// <returns></returns>
        public static UserMenuRights GetAllMenus(string roleName)
        {
            UserMenuRights userMenuRights;
            DataHelper _helper;
           
            try
            {
                userMenuRights = new UserMenuRights();
                _helper = new DataHelper();
                _helper.InitializedHelper();
                dsroleAccess = new System.Data.DataSet();
       

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@RoleName";
                parameter[parameter.Count - 1].Value = roleName;

                dsroleAccess = _helper.GetDataSet("sp_GetRoleAccess", parameter);
                userMenuRights.parentMenuAccess = new List<ParentMenuAccess>();
                userMenuRights.menuAccessRights = new List<MenuAccessRights>();
                if (dsroleAccess != null)
                {
                    if (dsroleAccess.Tables.Count > 0 && dsroleAccess.Tables[0].Rows.Count > 0)
                    {
                        //Adding Parent Menus
                        if (dsroleAccess.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsroleAccess.Tables[0].Rows.Count; i++)
                            {
                                userMenuRights.parentMenuAccess.Add(new ParentMenuAccess
                               {
                                   MenuId = (dsroleAccess.Tables[0].Rows[i]["MenuId"] != DBNull.Value) ? Convert.ToInt32(dsroleAccess.Tables[0].Rows[i]["MenuId"]) : 0,
                                   MenuName = (dsroleAccess.Tables[0].Rows[i]["MenuName"] != DBNull.Value) ? dsroleAccess.Tables[0].Rows[i]["MenuName"].ToString() : "",
                                   AccessRights = Convert.ToBoolean(dsroleAccess.Tables[1].Rows[i]["AccessRights"])
                               });
                            }
                        }

                        if (dsroleAccess.Tables[1].Rows.Count > 0)
                        {

                            for (int i = 0; i < dsroleAccess.Tables[1].Rows.Count; i++)
                            {
                                userMenuRights.menuAccessRights.Add(new MenuAccessRights
                                {
                                    MenuId = (dsroleAccess.Tables[1].Rows[i]["MenuId"] != DBNull.Value) ? Convert.ToInt32(dsroleAccess.Tables[1].Rows[i]["MenuId"]) : 0,
                                    MenuName = (dsroleAccess.Tables[1].Rows[i]["MenuName"] != DBNull.Value) ? dsroleAccess.Tables[1].Rows[i]["MenuName"].ToString() : "",
                                    Parent = (dsroleAccess.Tables[1].Rows[i]["Parent"] != DBNull.Value) ? Convert.ToInt32(dsroleAccess.Tables[1].Rows[i]["Parent"]) : 0,
                                    Child = (dsroleAccess.Tables[1].Rows[i]["Child"] != DBNull.Value) ? Convert.ToInt32(dsroleAccess.Tables[1].Rows[i]["Parent"]) : 0,
                                    RoleName = (dsroleAccess.Tables[1].Rows[i]["RoleName"]!= DBNull.Value) ? dsroleAccess.Tables[1].Rows[i]["RoleName"].ToString() : "",
                                    RoleId =  (dsroleAccess.Tables[1].Rows[i]["RoleId"] != DBNull.Value) ? Convert.ToInt32(dsroleAccess.Tables[1].Rows[i]["RoleId"]) : 0,
                                    AccessRights =Convert.ToBoolean(dsroleAccess.Tables[1].Rows[i]["AccessRights"])// (dsroleAccess.Tables[1].Rows[i]["AccessRights"] != DBNull.Value) ?Convert.ToBoolean(dsroleAccess.Tables[1].Rows[i]["RoleId"]):false 
                                });
                            }
                        }
                      
                    }
                }

                return userMenuRights;
            }
            catch (Exception Ex)
            {
              
                return null;
            }
        }

        /// <summary>
        /// Binding TreeView 
        /// </summary>
        /// <param name="tvAccess"></param>
        /// <returns></returns>
        public bool BindTreeView(ref TreeView tvAccess, string roleName ="")
        {
              UserMenuRights userMenuRights;
            try
            {
                 userMenuRights = new UserMenuRights();
                userMenuRights =  GetAllMenus(roleName);
                tvAccess.Nodes.Clear();
                foreach (ParentMenuAccess menuMaster in userMenuRights.parentMenuAccess)
                {
                        TreeNode root = new TreeNode();
                        root.Text = menuMaster.MenuName;
                        root.Value = menuMaster.MenuId.ToString();
                        root.ToolTip = "Main";
                        root.SelectAction = TreeNodeSelectAction.None;

                        foreach (MenuAccessRights menuAccessRights in userMenuRights.menuAccessRights)
                        {
                            if (menuMaster.MenuId == menuAccessRights.Parent && menuAccessRights.Parent>0)
                            {
                                TreeNode cn = new TreeNode();
                                cn.Text = menuAccessRights.MenuName;
                                cn.Value = menuAccessRights.MenuId.ToString();
                                cn.SelectAction = TreeNodeSelectAction.None;
                                if(menuAccessRights.AccessRights)
                                    root.Selected = true;
                                root.ChildNodes.Add(cn);
                            }
                        }
                        tvAccess.Nodes.Add(root);
                    }
                    tvAccess.CollapseAll();
                    return true;
                }
               
            
            catch (Exception Ex)
            {
             //   WriteLog(Ex.Message + Environment.NewLine + Ex.Source + Environment.NewLine + Ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Save The User Access
        /// </summary>
        /// <param name="menuxml">Menu For XML</param>
        /// <param name="roleid">Role Id</param>
        /// <param name="createdby">Created By User Id</param>
        /// <returns></returns>
        public static bool SaveAccess(string menuxml, int roleid,int createdby=0)
        {
           DataHelper _helper = new DataHelper();
            try
            {
               

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();
                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@menuXMl";
                parameter[parameter.Count - 1].Value = menuxml.ToString();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@roleid";
                parameter[parameter.Count - 1].Value = roleid;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@createdby";
                parameter[parameter.Count - 1].Value = createdby;

                dsroleAccess = _helper.GetDataSet("sp_SaveMenus", parameter); 
         
                return true;
               
            }
            catch (Exception Ex)
            {
               // WriteLog(Ex.Message + Environment.NewLine + Ex.Source + Environment.NewLine + Ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// Add the access of ChildNodes
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="DT"></param>
        private void GetChildNodes(TreeNode treeNode, DataTable DT)
        {
            foreach (TreeNode n in treeNode.ChildNodes)
            {
                if (n.Checked)
                {
                    DataRow DR = DT.NewRow();
                    DR["MenuId"] = n.Value;
                    DT.Rows.Add(DR);
                }
            }
        }
        /// <summary>
        /// To Check Or Uncheck the Nodes
        /// </summary>
        /// <param name="trNodeCollection"></param>
        /// <param name="isCheck"></param>
        private void CheckUncheckTreeNode(TreeNodeCollection trNodeCollection, bool isCheck)
        {
            foreach (TreeNode trNode in trNodeCollection)
            {
                trNode.Checked = isCheck;
                if (trNode.ChildNodes.Count > 0)
                    CheckUncheckTreeNode(trNode.ChildNodes, isCheck);
            }
        }
        /// <summary>
        /// Gives the Access of Data to the User
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        private DataTable GetTableStructure(int RoleId)
        {
            DataTable DtAccess = new DataTable();

            DataColumn DC = new DataColumn("RoleId");
            DC.DefaultValue = RoleId;
            DtAccess.Columns.Add(DC);

            DC = new DataColumn("MenuId");
            DC.DefaultValue = 0;
            DtAccess.Columns.Add(DC);

            DC = new DataColumn("CreatedDate");
            DC.DefaultValue = DateTime.Now;
            DtAccess.Columns.Add(DC);

            DC = new DataColumn("CreatedBy");
            //DC.DefaultValue = Convert.ToString(HttpContext.Current.Session["UserId"]);
            DtAccess.Columns.Add(DC);

            DC = new DataColumn("ModifyDate");
            DC.DefaultValue = DateTime.Now;
            DtAccess.Columns.Add(DC);

            DC = new DataColumn("ModifiedBy");
          //  DC.DefaultValue = Convert.ToString(HttpContext.Current.Session["UserId"]);
            DtAccess.Columns.Add(DC);

            return DtAccess;
        }

       
    }
}
