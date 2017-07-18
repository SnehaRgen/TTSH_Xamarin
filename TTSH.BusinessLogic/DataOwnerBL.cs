using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTSH.Entity;
using System.Threading.Tasks;
using TTSH.DataAccess;
using System.Data;
using System.Text.RegularExpressions;

namespace TTSH.BusinessLogic
{
    public class DataOwnerBL
    {
        public static List<DataOwner_Entity> GetAllADUsers(string GroupName)
        {
            List<DataOwner_Entity> lstDataOwner = null;
            try 
	        {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();

                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@GroupName";
                parameter[parameter.Count - 1].Value = GroupName;

                DataTable dtOwner = new DataTable();

                dtOwner = _helper.GetData("dbo.spGetAllADUsers", parameter);
                if (dtOwner != null && dtOwner.Rows.Count > 0)
                {
                    lstDataOwner = new List<DataOwner_Entity>();

                    string s = dtOwner.Columns["GUID"].DataType.Name.ToString().ToUpper().Trim();
                    /*the datatype will either be BYTE[] (old version) or GUID (new version Linked Server changes), hence type is checked in GUID date 01/oct/2015*/
                    foreach (DataRow dr in dtOwner.Rows)
                    {
                        lstDataOwner.Add(new DataOwner_Entity() {
                            EmailId = (dr.IsNull("MAIL")) == true ? "" : Convert.ToString(dr["MAIL"]),
                            GroupName = (dr.IsNull("GROUPNAME")) == true ? "" : Convert.ToString(dr["GROUPNAME"]),
                            MemberName = (dr.IsNull("NAME")) == true ? "" : Convert.ToString(dr["NAME"]),
                            GUID = (dr["GUID"].GetType().Name.ToString().ToUpper().Trim()=="GUID")?Convert.ToString(dr["GUID"]).ToUpper():GetGUID((byte[])dr["GUID"]).ToUpper()
                        });
                    }
                }
	        }
	        catch (Exception ex)
	        {
                return null;
	        }
            return lstDataOwner;
        }

        private static string GetGUID(byte[] arrGuid)
        {
            string strHex;
            String GUID = "";
            try
            {
                strHex = BitConverter.ToString(arrGuid);

                String g = String.Empty;

                foreach (var item in arrGuid)
                {
                    g += String.Format("{0:X2}", item);
                }

                GUID = ConvertOctStrToGuid(g).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

            return GUID;
        }

        private static Guid ConvertOctStrToGuid(String gUID)
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

        public static List<Project_DataOwner> GetProjectsByDO(string ModuleName, string UserGUID)
        {
            List<Project_DataOwner> oList = null;
            DataTable dtData = null;

            try
            {
                DataHelper _helper = new DataHelper();
                _helper.InitializedHelper();


                List<System.Data.Common.DbParameter> parameter = new List<System.Data.Common.DbParameter>();

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@ModuleName";
                parameter[parameter.Count - 1].Value = ModuleName;

                parameter.Add(_helper.CreateDbParameter());
                parameter[parameter.Count - 1].ParameterName = "@UserGUID";
                parameter[parameter.Count - 1].Value = UserGUID;

                dtData = _helper.GetData("dbo.spGetProjectsByUserGUID", parameter);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    oList = new List<Project_DataOwner>();

                    foreach (DataRow dr in dtData.Rows)
                    {
                        oList.Add(new Project_DataOwner() {
                            i_Project_ID = (dr.IsNull("i_Project_ID") ? 0 : Convert.ToInt32(dr["i_Project_ID"])),
                            s_DisplayProject_ID = (dr.IsNull("s_Display_Project_ID") ? "" : Convert.ToString(dr["s_Display_Project_ID"]))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return oList;
        }

    }
}
