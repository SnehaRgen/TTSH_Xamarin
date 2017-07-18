using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TTSH.Entity
{

    [DataContract]
    public enum Mode
    {
        [EnumMember]
        Insert = 1,
        [EnumMember]
        Update = 2,
        [EnumMember]
        Delete = 3


    }

    [DataContract]
    public class Project_PI
    {
        [DataMember]
        public int i_Project_ID { get; set; }

        [DataMember]
        public int i_PI_ID { get; set; }

    }

    [DataContract]
    class Common_Master
    {
        #region " Form contains "
        // Contract_Category_Master
        //Contract_Clauses_Master
        //Contract_Status_Master
        //Country_Master
        //Currency_Master
        //Award_Org_Master
        //Dept_Master
        //DrugLocation_Master
        //Factor_Master
        //Feasibility_Status_Master
        //IRB_Status_Master
        //IRB_Type_Master
        //NotificationMode_Master
        //OtherPI_Master
        //Project_Category_Master
        //Project_Status_Master
        //Project_Subtype_Master
        //Project_Type_Master
        //Sponsor_Master
        //Grant_Type_Master
        //CTC_Status_Master
        //Study_Status_Master
        //Regulatory_Submission_Interval_master
        #endregion

        #region " initialization "
        private int _i_ID;
        private string _s_Name;
        private string _s_Description;
        private int _i_CreatedBy_ID;
        private int _i_ModifyBy_ID;
        private DateTime? _dt_Created_Date;
        private DateTime? _dt_Modify_Date;
        #endregion

        #region " Constructor "
        public Common_Master()
        {
            _i_ID = 0;
            _s_Name = string.Empty;
            _s_Description = string.Empty;
            _i_CreatedBy_ID = 0;
            _i_ModifyBy_ID = 0;
            _dt_Created_Date = null;
            _dt_Modify_Date = null;

        }
        #endregion

        #region " Properties "
        public int i_ID
        {
            get { return _i_ID; }
            set { _i_ID = value; }
        }
        public string s_Name
        {
            get { return _s_Name; }
            set { _s_Name = value; }
        }
        public string s_Description
        {
            get { return _s_Description; }
            set { _s_Description = value; }
        }
        public int i_CreatedBy_ID
        {
            get { return _i_CreatedBy_ID; }
            set { _i_CreatedBy_ID = value; }
        }
        public int i_ModifyBy_ID
        {
            get { return _i_ModifyBy_ID; }
            set { _i_ModifyBy_ID = value; }
        }
        public DateTime? dt_Created_Date
        {
            get { return _dt_Created_Date; }
            set { _dt_Created_Date = value; }
        }
        public DateTime? dt_Modify_Date
        {
            get { return _dt_Modify_Date; }
            set { _dt_Modify_Date = value; }
        }
        #endregion
    }

    public class Ethics_Grid
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Display_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Title
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Category
        {
            get;
            set;
        }

        [DataMember]
        public string s_IRB_No
        {
            get;
            set;
        }

        [DataMember]
        public string PI_Names
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Type
        {
            get;
            set;
        }

        [DataMember]
        public string Status
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Status
        {
            get;
            set;
        }

        [DataMember]
        public int Ethics_ID
        {
            get;
            set;
        }


        [DataMember]
        public string EthicsStatus { get; set; }

        [DataMember]
        public string Project_Status_Disable { get; set; }

    }

    public class Feasibility_Grid
    {
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }

        [DataMember]
        public int i_Feasibility_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Display_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Title
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Category
        {
            get;
            set;
        }

        [DataMember]
        public string s_IRB_No
        {
            get;
            set;
        }

        [DataMember]
        public string PI_Names
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Type
        {
            get;
            set;
        }

        [DataMember]
        public string Status
        {
            get;
            set;
        }

        [DataMember]
        public int Feasibility_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public string FeasfibilityMode
        {
            get;
            set;
        }
        [DataMember]
        public string Feasibility_Status_Name
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Status_Disable { get; set; }

    }

    public class Selected_Grid
    {
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }

        public int Selected_ID
        {
            get;
            set;
        }


        [DataMember]
        public string s_Display_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Title
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Category
        {
            get;
            set;
        }

        [DataMember]
        public string s_IRB_No
        {
            get;
            set;
        }


        [DataMember]
        public string PI_Names
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Type
        {
            get;
            set;
        }

        [DataMember]
        public string Status
        {
            get;
            set;
        }

        [DataMember]
        public string Study_Status
        {
            get;
            set;
        }
        [DataMember]
        public string cordinatorstatus
        {
            get;
            set;
        }
        [DataMember]
        public string IsCoordinator
        {
            get;
            set;
        }

        [DataMember]
        public string DetailsExist
        {
            get;
            set;
        }
        [DataMember]
        public string Project_Status_Disable { get; set; }


    }

    public class ADUserDetails
    {
        [DataMember]
        public string UserGUID
        {
            get;
            set;
        }
        [DataMember]
        public string DisplayName
        {
            get;
            set;
        }
        [DataMember]
        public string LoginName
        {
            get;
            set;
        }
        [DataMember]
        public int MenuId
        {
            get;
            set;
        }
        [DataMember]
        public string MenuName
        {
            get;
            set;
        }
        [DataMember]
        public int Parent
        {
            get;
            set;
        }
        [DataMember]
        public int Child
        {
            get;
            set;
        }
        [DataMember]
        public string Url
        {
            get;
            set;
        }
        [DataMember]
        public int EmployeeID
        {
            get;
            set;
        }
        [DataMember]
        public string EmployeeEmail
        {
            get;
            set;
        }


    }
}
