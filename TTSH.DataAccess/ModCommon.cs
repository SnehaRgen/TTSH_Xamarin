using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Reflection;
using System.Text;
using System.Net.Mail;
using System.Globalization;
using System.Xml;

namespace TTSH.DataAccess
	{
	public static class ModCommon
		{
		#region " Email Class "
		public class Email
			{
			public int _Port { get; set; }
			public string _Host { get; set; }
			}
		#endregion

		#region " Methods "
		public static object iffBlank(object inpval, object outVal)
			{
			if ( Convert.IsDBNull(inpval) )
				{
				return outVal;
				}
			else if ( Convert.ToString(inpval) == string.Empty )
				{
				return outVal;
				}
			else if ( inpval == (object)0 )
				{
				return outVal;
				}
			else if ( inpval == null )
				{
				return outVal;
				}
			else
				{
				outVal = inpval;
				}
			return outVal;
			}
		public static DataTable dtReadOnlyAndAllowDbNull(this DataTable dt)
			{
			int k = dt.Columns.Count - 1;
			for ( int i = 0; i < k; i++ )
				{
				dt.Columns[i].ReadOnly = false; dt.Columns[i].AllowDBNull = true;
				}
			return dt;
			}
        public static DataTable ListToDatatable<T>(this List<T> inputlist)
        {
            DataTable dt = new DataTable();
            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(item.Name, item.PropertyType));
            }
            foreach (T t in inputlist)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo item in typeof(T).GetProperties())
                {
                    dr[item.Name] = item.GetValue(t, null);
                }
                dt.Rows.Add(dr);

            }
            return dt;
        }
        //public static DataTable ListToDatatable<T>(this List<T> inputlist)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        foreach (PropertyInfo item in typeof(T).GetProperties())
        //        {
        //            if (item.Name.ToLower() != "extensiondata")
        //            {
        //                dt.Columns.Add(new DataColumn(item.Name));
        //            }

        //        }
        //        foreach (T t in inputlist)
        //        {
        //            DataRow dr = dt.NewRow();
        //            foreach (PropertyInfo item in typeof(T).GetProperties())
        //            {
        //                if (item.GetMethod.Name.ToLower() != "get_extensiondata")
        //                {
        //                    dr[item.Name] = item.GetValue(t, null);
        //                }

        //            }
        //            dt.Rows.Add(dr);

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return dt;
        //}
		public static DataTable getColumns(this DataTable dts, int colCount)
			{
			DataTable dtx = new DataTable();
			for ( int i = 0; i < dts.Columns.Count; i++ )
				{
				dtx.Columns.Add(dts.Columns[i].ColumnName);
				if ( i == colCount ) break;
				}
			foreach ( DataRow item in dts.Rows )
				{
				dtx.ImportRow(item);
				}
			return dtx;
			}

		public static DataTable ConvertXmlNodeListToDataTable( this XmlNodeList xnl)
			{
			DataTable dt = new DataTable();
			int TempColumn = 0;

			foreach ( XmlNode node in xnl.Item(0).ChildNodes )
				{
				TempColumn++;
				DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));
				if ( dt.Columns.Contains(node.Name) )
					{
					dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
					}
				else
					{
					dt.Columns.Add(dc);
					}
				}

			int ColumnsCount = dt.Columns.Count;
			for ( int i = 0; i < xnl.Count; i++ )
				{
				DataRow dr = dt.NewRow();
				for ( int j = 0; j < ColumnsCount; j++ )
					{
					dr[j] = xnl.Item(i).ChildNodes[j].InnerText;
					}
				dt.Rows.Add(dr);
				}
			return dt;
			}



		public static string DataTableToJsonObj(this DataTable dt)
			{
			DataSet ds = new DataSet();
			ds.Merge(dt);
			StringBuilder JsonString = new StringBuilder();
			if ( ds != null && ds.Tables[0].Rows.Count > 0 )
				{
				JsonString.Append("[");
				int rowct = 0; int colct = 0;
				rowct = ds.Tables[0].Rows.Count;
				for ( int i = 0; i < rowct; i++ )
					{
					JsonString.Append("{");
					colct = ds.Tables[0].Columns.Count;
					for ( int j = 0; j < colct; j++ )
						{
						if ( j < colct - 1 )
							{
							JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
							}
						else if ( j == colct - 1 )
							{
							JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
							}
						}
					if ( i == rowct - 1 )
						{
						JsonString.Append("}");
						}
					else
						{
						JsonString.Append("},");
						}
					}
				JsonString.Append("]");
				return JsonString.ToString();
				}
			else
				{
				return null;
				}
			}
		public static bool SendMail(string _EmailTo, string _Emailfrom, string _Subject, string _body)
			{
			try
				{
				SmtpClient client = new SmtpClient();
				List<Email> _EmailList = GetEmailHostPort(_Emailfrom);

				client.Port = _EmailList[0]._Port;
				client.Host = _EmailList[0]._Host;
				client.EnableSsl = true;
				client.Timeout = ( client.Host.ToLower() == "smtp.office365.com" ) ? 200000 : 10000;

				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.Credentials = new System.Net.NetworkCredential(_Emailfrom, "****password");
				MailMessage _MailMessage = new MailMessage();
				_MailMessage.From = new MailAddress(_Emailfrom);

				/*********** Put below line in loop if you want to have multiple recipients ***********/
				_MailMessage.To.Add(new MailAddress(_EmailTo));
				/**************************************************************************************/
				_MailMessage.Subject = _Subject;
				_MailMessage.Body = _body;
				_MailMessage.IsBodyHtml = true;
				_MailMessage.Priority = MailPriority.High;
				_MailMessage.BodyEncoding = UTF8Encoding.UTF8;
				_MailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
				client.Send(_MailMessage);
				}
			catch { return false; }
			return true;
			}
		public static List<Email> GetEmailHostPort(string _EmailFrom)
			{
			string _HostPort = string.Empty;
			Email em = new Email();
			List<Email> lst = new List<Email>();
			try
				{
				string _domain = _EmailFrom.Substring(_EmailFrom.LastIndexOf('@') + 1).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
				switch ( _domain.Trim().ToLower() )
					{
					case "gmail":
						em._Port = 587;
						em._Host = "smtp.gmail.com";
						break;
					case "mail":
						em._Port = 465; // or 995
						em._Host = "smtp.mail.com";
						break;
					case "yahoo":
						em._Port = 465;// or 995
						em._Host = "smtp.mail.yahoo.com";
						break;
					case "office365":
					case "rgensolutions":
						em._Port = 587;// or 995
						em._Host = "smtp.office365.com";
						break;
					case "outlook":
					case "hotmail":
					case "live":
						em._Port = 587;
						em._Host = "smtp.live.com";
						break;
					default:
						em._Port = 587;
						em._Host = "smtp.gmail.com";
						break;
					}
				}
			catch
				{


				}
			lst.Add(em);
			return lst;

			}
		public static void FillCombo(this DropDownList ddl, DataTable dt, string sTextFeild, string sValueFeild, string sDisFirstPos = "", int sDisFirstPosVal = 0)
			{
			var _with1 = ddl;
			_with1.Items.Clear();
			_with1.DataTextField = sTextFeild;
			_with1.DataValueField = sValueFeild;
			_with1.DataSource = dt;
			_with1.DataBind();
			if ( string.IsNullOrEmpty(sDisFirstPos) )
				{
				ddl.Items.Insert(0, new ListItem("--Select--", System.Convert.ToString(sDisFirstPosVal)));
				}
			else
				{
				if ( sDisFirstPos.Trim().Contains("-") )
					{
					ddl.Items.Insert(0, new ListItem(sDisFirstPos, System.Convert.ToString(sDisFirstPosVal)));
					}
				else
					{
					ddl.Items.Insert(0, new ListItem("--" + System.Convert.ToString(sDisFirstPos) + "--", System.Convert.ToString(sDisFirstPosVal)));
					}
				}
			}

		public static string SetReplace(string strValue)
			{
			return strValue.ToString().Replace("'", "`");
			}
		public static string GetReplace(string strValue)
			{
			return strValue.ToString().Replace("`", "'");
			}
		#endregion

		#region " Conversion Methods "
		public static string ConvertDatetoString(string date, char splittype)
			{
			string[] ArrFDaTe = date.ToString().Split(splittype);
			string frmDaTe = ArrFDaTe[2].ToString();
			if ( ArrFDaTe[1].ToString().Length == 2 )
				frmDaTe = frmDaTe + ArrFDaTe[1].ToString();
			else
				frmDaTe = frmDaTe + "0" + ArrFDaTe[1].ToString();
			if ( ArrFDaTe[0].ToString().Length == 2 )
				frmDaTe = frmDaTe + ArrFDaTe[0].ToString();
			else
				frmDaTe = frmDaTe + "0" + ArrFDaTe[0].ToString();
			return frmDaTe;
			}
		public static DateTime ConvertToDateTime(string Date)
			{
			DateTime dtDate;
			try
				{
				string[] MDate = Date.Split('/');
				string Days = MDate[0];
				string Months = MDate[1];
				string Year = MDate[2];
				if ( Days.Trim().Length == 1 )
					Days = "0" + Days;
				if ( Months.Trim().Length == 1 )
					Months = "0" + Months;
				if ( Year.Trim().Length <= 2 )
					Year = Convert.ToString(( Convert.ToInt32(DateTime.Now.ToString().Substring(0, 1)) * 1000 ) + Convert.ToInt32(Year));
				dtDate = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Months), Convert.ToInt32(Days));

				}
			catch ( Exception ex )
				{
				return ( new DateTime(1900, 01, 01) );
				throw ex;
				}
			return dtDate;
			}
		public static DateTime ConvertSqlDateToDatetime(string date)
			{
			DateTime newdate;
			try
				{
				newdate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
				}
			catch ( Exception ex )
				{
				return ( new DateTime(1900, 01, 01) );
				}
			return newdate;
			}
		#endregion

		#region " SetIndex Methods on GridView "
		public static void SetEntryGridViewPageIndex(this GridView gv, DataTable dt, string filter)
			{
			try
				{
				if ( dt != null )
					{
					if ( dt.Rows.Count > 0 )
						{
						int pageSize = gv.PageSize;
						if ( dt.Rows.Count > pageSize )
							{
							DataRow[] dr = dt.Select(filter);
							DataRow newRow = dt.NewRow();
							newRow.ItemArray = dr[0].ItemArray;
							dt.Rows.Remove(dr[0]);
							dt.Rows.InsertAt(newRow, gv.PageSize * gv.PageIndex);
							}
						}
					}
				}
			catch
				{

				}
			}
		public static void SetGridViewPageIndex(this GridView gv, DataTable dt, string SearchName, string SearchValue)
			{
			try
				{
				if ( dt != null )
					{
					if ( dt.Rows.Count > 0 )
						{
						int pageSize = gv.PageSize;
						if ( dt.Rows.Count > pageSize )
							{
							DataRow[] dr = dt.Select(SearchName + "='" + SearchValue.Replace("'", "`").Trim() + "'");
							DataRow newRow = dt.NewRow();
							newRow.ItemArray = dr[0].ItemArray;
							dt.Rows.Remove(dr[0]);
							dt.Rows.InsertAt(newRow, 0);
							gv.PageIndex = 0;
							}
						}
					}
				}
			catch
				{
				}
			}
		#endregion
		}
	}
