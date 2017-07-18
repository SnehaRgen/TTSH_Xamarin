using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using TTSH.DataAccess;
using TTSH.Entity;


using System.Data.SqlClient;
using System.Security.Principal;


namespace TTSH.BusinessLogic.Authentication
{
    public class LdapAuthentication
    {
        #region LDAP Authentication Methods
        // Sapna K: LDAP URL and its parameters
        // Usage: LDAP://domainName.TLD/CN=Users,OU=Groups,DC=domainName,DC=TLD
        // Example: LDAP://rgensolutions.com/CN=sapna.kshirsagar,OU=Administrators,DC=rgensolutions,DC=com
        // Example: LDAP://rgensolutions.com/CN=mohd.ejaz,OU=Developers,DC=rgensolutions,DC=com
        // Terminology: LDAP - Lightweight Directory Access Protocol
        // Terminology: TLD - Top Level Domain e.g. com,net,org
        // Terminology: CN - Cononical – or Common Name 
        // Terminology: OU - Organizational Unit
        // Terminology: DC - Domain Controller

        static string DomainName = System.Configuration.ConfigurationManager.AppSettings["DomainName"].ToString();
        static string ADserver = System.Configuration.ConfigurationManager.AppSettings["ADserver"].ToString();

        // Sapna K: Method to check if Directory Exists
        public static bool Exists(string objectPath)
        {
            bool found = false;
            try
            {
                if (DirectoryEntry.Exists("LDAP://" + objectPath))
                {
                    found = true;
                }
            }
            catch (DirectoryServicesCOMException Ex) { }
            return found;
        }

        // Sapna K: Method to Authenticate the user
        public static bool Authenticate(string domain, string userName, string password)
        {
            bool authentic = false;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, userName, password))
                {

                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.SearchRoot = entry;
                    search.Filter = "(SAMAccountName=" + userName + ")";
                    SearchResult result = search.FindOne();
                    if (result != null)
                    {
                       authentic = true; 
                    }
                    
                }
            }
            catch (DirectoryServicesCOMException Ex) { }
            return authentic;
        }

        // Sapna K: Alternative method to Authenticate the User
        public static bool Authenticate(string userName)
        {
            bool authentic = false;
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName ))
                {
                    using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, userName))
                    {
                        authentic = (user != null);
                    }
                }
                
            }
            catch (DirectoryServicesCOMException Ex) 
            { 
                WriteLog(Ex.Message); 
            }
            return authentic;
        }
        
        // Sapna K: Get a list of all of the groups the current user is a member of
        public static string[] GetGroups()
        {
            ArrayList groups = new ArrayList();
            try
            {
                foreach (System.Security.Principal.IdentityReference group in System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
                {
                    groups.Add(group.Translate(typeof(System.Security.Principal.NTAccount)).ToString());
                }
            }
            catch (Exception Ex) { }
            string[] array = groups.ToArray(typeof(string)) as string[];
            return array;
        }

        // Sapna K: Alternative Method to get Groups of current user
        public static string[] GetGroupNames(string userName, string Password)
        {
            List<string> result = new List<string>();
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ADserver, userName, Password))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(pc, userName);
                    using (PrincipalSearchResult<Principal> src = UserPrincipal.FindByIdentity(pc, userName).GetGroups(pc))
                    {
                        src.ToList().ForEach(sr => result.Add(sr.SamAccountName));
                    }
                }
            }
            catch (Exception Ex) { }
            return result.ToArray();
        }

        //Sapna K:Get a list of all Properties of AD users
        public static List<ADUserDetails> GetUserADDetails(string domain, string userName, string password)
        {
            List<ADUserDetails> lstAdUserDetails = new List<ADUserDetails>();
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, userName, password))
                {
                     DirectorySearcher search = new DirectorySearcher(entry);
                     search.Filter = "(SAMAccountName=" + userName + ")";
                     search.PropertiesToLoad.Add("cn");
                     SearchResult result = search.FindOne();
                    
                     if (result == null)
                     {
                         return null;
                     }
                }
            }
            catch (DirectoryServicesCOMException Ex) { }
            return lstAdUserDetails;
        }          

        public static List<ADUserDetails> GetMenusByGroup(string Group)
		{
            DataTable dtData = null;
            DataHelper dtHelper = new DataHelper();
            dtHelper.InitializedHelper();
            List<ADUserDetails> adMenuList = new List<ADUserDetails>();

            try
            {
                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(dtHelper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@RoleName";
                parameter[parameter.Count - 1].Value = Group;

                dtData = dtHelper.GetData("dbo.SpGetMenuItems",parameter);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtData.Rows)
                    {
                        adMenuList.Add(new ADUserDetails {
                            MenuId = (dr.IsNull("MenuId") ? 0 : Convert.ToInt32(dr["MenuId"])),
                            MenuName = (dr.IsNull("MenuName") ? string.Empty : Convert.ToString(dr["MenuName"])),
                            Parent = (dr.IsNull("Parent") ? 0 : Convert.ToInt32(dr["Parent"])),
                            Child = (dr.IsNull("Child") ? 0 : Convert.ToInt32(dr["Child"])),
                            Url = (dr.IsNull("Url") ? string.Empty : Convert.ToString(dr["Url"])),
                        
                        });
                    }
                    
                }
                             
            }
            catch (Exception Ex)
			{
				WriteLog(Ex.Message + Environment.NewLine + Ex.Source + Environment.NewLine + Ex.StackTrace);
              
			}

            return adMenuList;
           
		}

        // Sapna K: Method to get ADUser Profile Details
        public static Entity.ADUserDetails GetUserDetails(string UserName)
        {
            
            TTSH.Entity.ADUserDetails adUserEntity = new Entity.ADUserDetails();
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(context, UserName);
                SecurityIdentifier sid = user.Sid;
                adUserEntity.UserGUID = user.Guid.ToString();
                adUserEntity.DisplayName = user.DisplayName;
                adUserEntity.LoginName = user.Name;
                adUserEntity.EmployeeID = Convert.ToInt32(user.EmployeeId);
                adUserEntity.EmployeeEmail = user.EmailAddress;
            }
            catch(Exception Ex)
            {
                WriteLog(Ex.Message + Environment.NewLine + Ex.Source + Environment.NewLine + Ex.StackTrace);
            }
            return adUserEntity;
        }
      

        #endregion

        #region Public Methods
        public static void WriteLog(string Message)
        {
            try
            {
                // Sapna K: Create a writer and open the Log file
                StreamWriter log;
                if (!File.Exists(HttpContext.Current.Server.MapPath("~/Bin/Log/logfile.txt")))
                {
                    // Sapna K: If file does not exists create new instance of stream writer that writes text to new file
                    log = new StreamWriter(HttpContext.Current.Server.MapPath("~/Bin/Log/logfile.txt"));
                }
                else
                {
                    // Sapna K: If file exists create new instance of stream writer that appends text to existing file
                    log = File.AppendText(HttpContext.Current.Server.MapPath("~/Bin/Log/logfile.txt"));
                }

                // Sapna K: Write to the file:
                log.WriteLine(DateTime.Now);
                log.WriteLine(Message);
                log.WriteLine("*");

                // Sapna K: Close the stream:
                log.Close();
            }
            catch (Exception Ex) { }
        }
        #endregion
    }
}
