using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TTSH.Entity
{


    //----------TableName :-Dummy
    [DataContract]
    public class Dummy
    {
        [DataMember]
        public int id
        {
            get;
            set;
        }
        [DataMember]
        public string Name
        {
            get;
            set;
        }
        [DataMember]
        public string Lname
        {
            get;
            set;
        }
        [DataMember]
        public string Designation
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Category_Master
    [DataContract]
    public class Project_Category_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Type_Master
    [DataContract]
    public class Project_Type_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-Audit
    [DataContract]
    public class Audit
    {
        [DataMember]
        public string ID
        {
            get;
            set;
        }
        [DataMember]
        public string Audit_Action
        {
            get;
            set;
        }
        [DataMember]
        public string Columns
        {
            get;
            set;
        }
        [DataMember]
        public string OldValue
        {
            get;
            set;
        }
        [DataMember]
        public string NewValue
        {
            get;
            set;
        }
        [DataMember]
        public string UserId
        {
            get;
            set;
        }
        [DataMember]
        public string OnDatetime
        {
            get;
            set;
        }
        [DataMember]
        public string TableName
        {
            get;
            set;
        }
        public string PrimaryKeyValue
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Subtype_Master
    [DataContract]
    public class Project_Subtype_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int Project_Type_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblFeasibility_Status_Master
    [DataContract]
    public class Feasibility_Status_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblDrugLocation_Master
    [DataContract]
    public class DrugLocation_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblNotificationMode_Master
    [DataContract]
    public class NotificationMode_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblIRB_Type_Master
    [DataContract]
    public class IRB_Type_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContract_Collobrator_Master
    [DataContract]
    public class Contract_Collobrator_Master
    {
        [DataMember]
        public string i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Email1
        {
            get;
            set;
        }
        [DataMember]
        public string s_Email2
        {
            get;
            set;
        }
        [DataMember]
        public string s_PhoNo
        { get; set; }
        [DataMember]
        public string s_Institution
        {
            get;
            set;
        }
        [DataMember]
        public int i_Country_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public string Country_Name { get; set; }
        [DataMember]
        public string s_initialId { get; set; }
        [DataMember]
        public string s_date { get; set; }
        [DataMember]
        public int I_Collaborator_Id { get; set; }

        [DataMember]
        public string dt_Contract_AssignDate { get; set; }
        [DataMember]
        public string i_ReviewedBy_ID { get; set; }
        [DataMember]
        public string S_ReviewedByName { get; set; }
    }
    [DataContract]
    public class Project_Coordinator_Details
    {
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string i_Coordinator_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Coordinator_name
        {
            get;
            set;
        }
    }

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblStudy_Status_Master
    [DataContract]
    public class Study_Status_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContract_Clauses_Master
    [DataContract]
    public class Contract_Clauses_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblIRB_Status_Master
    [DataContract]
    public class IRB_Status_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblAward_Org_Master
    [DataContract]
    public class Award_Org_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContract_Status_Master
    [DataContract]
    public class Contract_Status_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContract_Category_Master
    [DataContract]
    public class Contract_Category_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblPI_Master
    [DataContract]
    public class PI_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_DeptName
        {
            get;
            set;
        }
        [DataMember]
        public string s_PIName
        {
            get;
            set;
        }
        [DataMember]
        public int i_Dept_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Firstname
        {
            get;
            set;
        }
        [DataMember]
        public string s_Lastname
        {
            get;
            set;
        }
        [DataMember]
        public string s_Email
        {
            get;
            set;
        }
        [DataMember]
        public string s_Phone_no
        {
            get;
            set;
        }
        [DataMember]
        public string s_MCR_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;

        }
        [DataMember]
        public int i_Project_ID { get; set; }
        [DataMember]
        public int NOTDELETED_GDetail { get; set; }

    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblCurrency_Master
    [DataContract]
    public class Currency_Master
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-TestUser
    [DataContract]
    public class TestUser
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string Name
        {
            get;
            set;
        }
        [DataMember]
        public string Surname
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblCTC_Status_Master
    [DataContract]
    public class CTC_Status_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-TestUserContact
    [DataContract]
    public class TestUserContact
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string Phone
        {
            get;
            set;
        }
        [DataMember]
        public string Address
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-TestUserContactMapping
    [DataContract]
    public class TestUserContactMapping
    {
        [DataMember]
        public int Contact_ID
        {
            get;
            set;
        }
        [DataMember]
        public int User_ID
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblDrug_Master
    [DataContract]
    public class Drug_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Expiry_date
        {
            get;
            set;
        }
        [DataMember]
        public string Dose
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Master
    [DataContract]
    public class Project_Master
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
        public string s_Short_Title
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_Category_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_Type_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_Subtype_ID
        {
            get;
            set;
        }
        [DataMember]
        public bool b_Collaboration_Involved
        {
            get;
            set;
        }
        [DataMember]
        public bool b_StartBy_TTSH
        {
            get;
            set;
        }
        [DataMember]
        public bool b_Funding_req
        {
            get;
            set;
        }
        [DataMember]
        public bool b_Ischild
        {
            get;
            set;
        }
        [DataMember]
        public int i_Parent_ProjectID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Alias1
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Alias2
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Desc
        {
            get;
            set;
        }
        [DataMember]
        public int b_IsFeasible
        {
            get;
            set;
        }
        [DataMember]
        public bool b_Isselected_project
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
        public DateTime? dt_IRB_Approve_Enddate
        {
            get;
            set;
        }


        [DataMember]
        public string s_Research_IO
        {
            get;
            set;
        }
        [DataMember]
        public string s_Research_IP
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public List<PI_Master> DEPT_PI { get; set; }
        [DataMember]
        public List<Project_Coordinator_Details> COORDINATOR { get; set; }
        [DataMember]
        public string Project_Category_Name { get; set; }
        [DataMember]
        public string Project_Type { get; set; }
        [DataMember]
        public string PI_NAME { get; set; }
        [DataMember]
        public string Project_StartDate { get; set; }
        [DataMember]
        public List<DataOwner> DataOwnerList { get; set; }
        [DataMember]
        public string s_Coinvestigator
        {
            get;
            set;
        }

        [DataMember]
        public string s_Ethics_DataOwner { get; set; }

        [DataMember]
        public string s_Feasibility_DataOwner { get; set; }

        [DataMember]
        public string s_Regulatory_DataOwner { get; set; }

        [DataMember]
        public string s_Selected_DataOwner { get; set; }

        [DataMember]
        public string s_Grant_DataOwner { get; set; }

        [DataMember]
        public string s_Contract_DataOwner { get; set; }
        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int i_ProjectStatus { get; set; }
        [DataMember]
        public string Dt_ProjectEndDate { get; set; }
        [DataMember]
        public bool b_EthicsNeeded { get; set; }

        [DataMember]
        public string S_ProjectStatus { get; set; }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblCountry_Master
    [DataContract]
    public class Country_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public int i_CountryCode
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContract_Master
    [DataContract]
    public class Contract_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }

        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Contract_ReqDate
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Contract_AssignDate
        {
            get;
            set;
        }
        [DataMember]
        public string i_ReviewedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string S_ReviewedByName
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
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
        public string Project_Category_Name { get; set; }
        [DataMember]
        public string Project_Type { get; set; }
        [DataMember]
        public string PI_NAME { get; set; }
        [DataMember]
        public string s_IRB_No
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
        public int I_Collaborator_Id { get; set; }
        [DataMember]
        public string ContAppStatus
        {
            get;
            set;
        }
        [DataMember]
        public List<PI_Master> pmlist { get; set; }
        [DataMember]
        public List<Contract_Collobrator_Master> ccdlist { get; set; }
        [DataMember]
        public List<Project_Master> pjctmList { get; set; }
        [DataMember]
        public string s_Short_Title
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Alias1
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Alias2
        {
            get;
            set;
        }
        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }
        [DataMember]
        public string Project_Status_Disable { get; set; }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblOther_PI_Master
    [DataContract]
    public class Other_PI_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Firstname
        {
            get;
            set;
        }
        [DataMember]
        public string s_Lastname
        {
            get;
            set;
        }
        [DataMember]
        public string s_Email
        {
            get;
            set;
        }
        [DataMember]
        public string s_Phone_no
        {
            get;
            set;
        }
        [DataMember]
        public string s_MCR_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_DeptName
        {
            get;
            set;
        }
        [DataMember]
        public string s_Organization_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public bool IsDeleted
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }
    }

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRoleMaster
    [DataContract]
    public class RoleMaster
    {
        [DataMember]
        public double RoleId
        {
            get;
            set;
        }
        [DataMember]
        public double GroupId
        {
            get;
            set;
        }
        [DataMember]
        public string RoleName
        {
            get;
            set;
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get;
            set;
        }
        [DataMember]
        public double CreatedBy
        {
            get;
            set;
        }
        [DataMember]
        public DateTime ModifyDate
        {
            get;
            set;
        }
        [DataMember]
        public double ModifiedBy
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblDept_Master
    [DataContract]
    public class Dept_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContractDetails_MultipleContractFile
    [DataContract]
    public class ContractDetails_MultipleContractFile
    {
        [DataMember]
        public int i_ContractDetailsID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ContractFile
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblGrant_Type_Master
    [DataContract]
    public class Grant_Type_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Dept_PI
    [DataContract]
    public class Project_Dept_PI
    {
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_PI_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }

    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Coordinator_Details

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblFeasibility_CRO_Details
    [DataContract]
    public class Feasibility_CRO_Details
    {
        [DataMember]
        public int i_Feasibility_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_CRO_ID
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSponsor_Master
    [DataContract]
    public class Sponsor_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblFactors_Master
    [DataContract]
    public class Factors_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblFeasibility_Details
    [DataContract]
    public class Feasibility_Details
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? s_Email_Send_Date
        {
            get;
            set;
        }
        [DataMember]
        public int i_Feasibility_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Feasibility_Title
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Confidential_Agreement
        {
            get;
            set;
        }
        [DataMember]
        public string s_Confidential_Agreement_File
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Survey_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Survey_Comments
        {
            get;
            set;
        }
        [DataMember]
        public string s_Questionnaire_File
        {
            get;
            set;
        }
        [DataMember]
        public string s_Protocol_No
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Protocol_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Prototcol_Doc_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_Prototcol_File
        {
            get;
            set;
        }
        [DataMember]
        public string s_Protocol_Comments
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Site_Visit_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Coinvestigator
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Interest
        {
            get;
            set;
        }
        [DataMember]
        public string s_Interest_Comments
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Feasibility_Outcome
        {
            get;
            set;
        }
        [DataMember]
        public string s_IM_Invitation
        {
            get;
            set;
        }
        [DataMember]
        public bool? s_In_File
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }

        [DataMember]
        public string s_ModifyBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Feasibility_Start_Date
        {
            get;
            set;
        }

        [DataMember]
        public string PROJECT_DATA
        {
            get;
            set;
        }

        [DataMember]
        public List<PI_Master> DEPT_PI
        {
            get;
            set;
        }

        [DataMember]
        public string SPONSOR
        {
            get;
            set;
        }

        [DataMember]
        public string CRA
        {
            get;
            set;
        }

        [DataMember]
        public string SAE
        {
            get;
            set;
        }
        [DataMember]
        public string s_Checklist_File
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Alias1
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Alias2
        {
            get;
            set;
        }
        [DataMember]
        public string s_Short_Title
        {
            get;
            set;
        }

        [DataMember]
        public List<Project_PI> Project_PIs
        {
            get;
            set;
        }
        [DataMember]
        public string UserCId
        {
            get;
            set;
        }
        [DataMember]
        public string Username
        {
            get;
            set;
        }



    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRoleAccess
    [DataContract]
    public class RoleAccess
    {
        [DataMember]
        public double RoleAccessId
        {
            get;
            set;
        }
        [DataMember]
        public double RoleId
        {
            get;
            set;
        }
        [DataMember]
        public double MenuId
        {
            get;
            set;
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get;
            set;
        }
        [DataMember]
        public double CreatedBy
        {
            get;
            set;
        }
        [DataMember]
        public DateTime ModifyDate
        {
            get;
            set;
        }
        [DataMember]
        public double ModifiedBy
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-StateM
    [DataContract]
    public class StateM
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string State_Name
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-sysdiagrams
    [DataContract]
    public class sysdiagrams
    {
        [DataMember]
        public string name
        {
            get;
            set;
        }
        [DataMember]
        public int principal_id
        {
            get;
            set;
        }
        [DataMember]
        public int diagram_id
        {
            get;
            set;
        }
        [DataMember]
        public int version
        {
            get;
            set;
        }
        [DataMember]
        public byte definition
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-CityM
    [DataContract]
    public class CityM
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public int State_ID
        {
            get;
            set;
        }
        [DataMember]
        public string City_Name
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    [DataContract]
    public class SelectedProject_BU_Details
    {
        [DataMember]
        public int i_Selected_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Cordinator_Id
        {
            get;
            set;
        }
        [DataMember]
        public string s_Blinded_UnBlinded
        {
            get;
            set;
        }
        [DataMember]
        public string s_Cordinator_name
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Selected_SAE_Details
    {
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        
        [DataMember]
        public int i_SAE_ID
        {
            get;
            set;
        }

        [DataMember]
        public int Sel_SAEID
        {
            get;
            set;
        }

    }


    [DataContract]
    public class Selected_CRA_Details
    {


        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_CRO_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_CRA_ID
        {
            get;
            set;
        }

    }

    //----------TableName :-tblSelected_Project_Details
    [DataContract]
    public class Selected_Project_StudyBudgetFile
    {
        [DataMember]
        public int i_Selected_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Budget_Document_File
        {
            get;
            set;
        }
        [DataMember]
        public string s_Budget_Comments
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Selected_Project_Details
    {
        [DataMember]
        public List<SelectedProject_BU_Details> BU_Details
        {
            get;
            set;
        }

        [DataMember]
        public List<Project_PI> Project_PIs
        {
            get;
            set;
        }

        [DataMember]
        public List<PI_Master> DEPT_PI
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Data
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Alias1
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Alias2
        {
            get;
            set;
        }
        [DataMember]
        public string s_Short_Title
        {
            get;
            set;
        }

        [DataMember]
        public List<Selected_Project_StudyBudgetFile> StudyBudgetFile
        {
            get;
            set;
        }


        [DataMember]
        public List<Selected_SAE_Details> SAE_Details
        {
            get;
            set;
        }

        [DataMember]
        public List<Selected_CRA_Details> CRA_Details
        {
            get;
            set;
        }

        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_Id
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Selected_Start_Date
        {
            get;
            set;
        }

        [DataMember]
        public string monthNames
        {
            get;
            set;
        }

        [DataMember]
        public string selectedYear
        {
            get;
            set;
        }

        [DataMember]
        public bool b_IsTeam_Needed
        {
            get;
            set;
        }
        [DataMember]
        public string s_Blinded_Coordinator
        {
            get;
            set;
        }
        [DataMember]
        public string s_Unblinded_Coordinator
        {
            get;
            set;
        }
        [DataMember]
        public string s_Blinded_Cordinator_name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Unblinded_Cordinator_name
        {
            get;
            set;
        }
        [DataMember]
        public int i_SAE_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_CRO_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Study_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_Type_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Clinic1
        {
            get;
            set;
        }
        [DataMember]
        public string s_Clinic2
        {
            get;
            set;
        }
        [DataMember]
        public string s_Research_Days
        {
            get;
            set;
        }
        [DataMember]
        public string s_Followup_Duratrion
        {
            get;
            set;
        }
        [DataMember]
        public string s_Backup_Blinded
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Recruit_Start_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Recruit_End_Date
        {
            get;
            set;
        }
        [DataMember]
        public int i_TTSH_Target
        {
            get;
            set;
        }
        [DataMember]
        public int i_Screen_No
        {
            get;
            set;
        }
        [DataMember]
        public int i_Screen_Failure
        {
            get;
            set;
        }
        [DataMember]
        public int i_Randomized
        {
            get;
            set;
        }
        [DataMember]
        public int i_Completed
        {
            get;
            set;
        }
        [DataMember]
        public int i_Withdrawl
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
        public DateTime? dt_Expiry_date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_CTC_Expiry_date
        {
            get;
            set;
        }
        [DataMember]
        public bool b_CTM_Status
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_CTM_Expiry_date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Drug_Name
        {
            get;
            set;
        }

        [DataMember]
        public bool b_Drug_Status
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Drug_Expiry_date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Drug_Dose
        {
            get;
            set;
        }
        [DataMember]
        public int i_Drug_Location_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Extended_Month_Blinded
        {
            get;
            set;
        }
        [DataMember]
        public int i_CupBoardno_Blinded
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Extended_Month_UnBlinded
        {
            get;
            set;
        }
        [DataMember]
        public int i_CupBoardno_UnBlinded
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Awaiting_Archiving
        {
            get;
            set;
        }
        [DataMember]
        public string s_Reason
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Archiving_Enddate
        {
            get;
            set;
        }
        [DataMember]
        public string s_Offsite_Company
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_IsApproveProject
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public string BLINDED_UNBLINDED_XML
        {
            get;
            set;
        }
        [DataMember]
        public string SAE_XML
        {
            get;
            set;
        }
        [DataMember]
        public string CRA_XML
        {
            get;
            set;
        }
        [DataMember]
        public string STUDY_BUDGET_FILE_XML
        {
            get;
            set;
        }
        [DataMember]
        public string UserCId
        {
            get;
            set;
        }
        [DataMember]
        public string Username
        {
            get;
            set;
        }
        [DataMember]
        public string Co_Ordinator_Type
        {
            get;
            set;
        }


        [DataMember]
        public DateTime? dt_EntryForMonthBlinded
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_EntryForMonthUnBlinded
        {
            get;
            set;
        }

        [DataMember]
        public string s_LastUpdated_By_Blinded
        {
            get;
            set;
        }

        [DataMember]
        public string s_LastUpdated_By_UnBlinded
        {
            get;
            set;
        }
        [DataMember]
        public string s_LastUpdated_By_SelectedTeamUser
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_LastUpdated_By_SelectedTeamUser
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_LastUpdated_By_Blinded
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_LastUpdated_By_UnBlinded
        {
            get;
            set;
        }

        [DataMember]
        public string s_Agreement_Number
        {
            get;
            set;
        }
        [DataMember]
        public string s_AgreementFile
        {
            get;
            set;
        }




        [DataMember]
        public string s_Amount
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? dt_Date_Sent_for_Archiving
        {
            get;
            set;
        }

        [DataMember]
        public int? i_Number_of_Boxes
        {
            get;
            set;
        }

        [DataMember]
        public bool isSelectedTeamUser
        {
            get;
            set;
        }


    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSelected_CRA_Details
    //[DataContract]
    //public class Selected_CRA_Details
    //{
    //    [DataMember]
    //    public int i_Selected_ID
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public int i_CRO_ID
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public int i_CRA_ID
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public string s_description
    //    {
    //        get;
    //        set;
    //    }
    //}
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblFeasibility_Sponsor_Details
    [DataContract]
    public class Feasibility_Sponsor_Details
    {
        [DataMember]
        public int i_Feasibility_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sponsor_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_description
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-Report
    [DataContract]
    public class Report
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string ColumnName
        {
            get;
            set;
        }
        [DataMember]
        public string ColumnDisplayName
        {
            get;
            set;
        }
        [DataMember]
        public int ColumnSequence
        {
            get;
            set;
        }
        [DataMember]
        public int ReportID
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSelectedCollborators_Details
    [DataContract]
    public class SelectedCollborators_Details
    {

        [DataMember]
        public int i_Contract_Details_Id
        {
            get;
            set;
        }
        [DataMember]
        public string i_Collobrator_ID
        {
            get;
            set;
        }
        [DataMember]
        public string i_Collobrator_ID_Unq
        {
            get;
            set;
        }

        [DataMember]
        public string ReviewdById { get; set; }
        [DataMember]
        public string ReviewdByName { get; set; }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-dept
    [DataContract]
    public class dept
    {
        [DataMember]
        public string F1
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRegulatory_Submission_Interval_master
    [DataContract]
    public class Regulatory_Submission_Interval_master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-TestReport
    [DataContract]
    public class TestReport
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_IRB_Approve_Enddate
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sub_targeted
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblEthics_Details
    [DataContract]
    public class Ethics_Details
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_IRB_Type_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_IRB_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_IRB_Approve_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Comments
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
        public string s_IRB_File
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_IRB_Approve_Enddate
        {
            get;
            set;
        }
        [DataMember]
        public string s_Remarks
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Project_Status_date
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sub_Targeted_TTSH
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sub_Recruited_TTSH
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sub_targeted
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sub_Recruited
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_IsRenewal
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_NewStudy_End_date
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_IsClinical_Trial_Insurance
        {
            get;
            set;
        }
        [DataMember]
        public string s_Insurance_Period
        {
            get;
            set;
        }
        [DataMember]
        public string s_Insurance_file
        {
            get;
            set;
        }
        [DataMember]
        public bool b_CRIO_culled
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_CRIO_culled_date
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_IsChildBearing
        {
            get;
            set;
        }
        [DataMember]
        public string CO_Investigator
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }

        [DataMember]
        public List<Project_PI> Project_PIs
        {
            get;
            set;
        }

        [DataMember]
        public string Project_Data
        {
            get;
            set;
        }
        //[DataMember]
        //public string Dept_PI_Names
        //{
        //    get;
        //    set;
        //}

        [DataMember]
        public int i_Project_Category_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Project_Alias1
        {
            get;
            set;
        }

        [DataMember]
        public string s_Project_Alias2
        {
            get;
            set;
        }
        [DataMember]
        public string s_Short_Title
        {
            get;
            set;
        }

        [DataMember]
        public List<PI_Master> Dept_PI
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? dt_Ethics_Start_Date
        {
            get;
            set;
        }

        [DataMember]
        public int i_EthicsReview_ID
        {
            get;
            set;
        }

        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }

        [DataMember]
        public string EthicsStatus { get; set; }

        [DataMember]
        public int i_Name_Of_IRB_ID
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Any_Lapses
        {
            get;
            set;
        }
        [DataMember]
        public string Project_Status_Disable { get; set; }


    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-ReportMapping
    [DataContract]
    public class ReportMapping
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string ReportName
        {
            get;
            set;
        }
        [DataMember]
        public string TableName
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblContract_Collaborator_Details
    [DataContract]
    public class Contract_Collaborator_Details
    {
        [DataMember]
        public int i_Contract_Master_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Contract_Collaborator_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_InitialContract_ID
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Contract_Request_Date
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Contract_AssignDate
        { get; set; }

        [DataMember]
        public string i_ReviewedBy_ID { get; set; }

        [DataMember]
        public string S_ReviewedByName { get; set; }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSelected_Project_StudyBudgetFile
    //[DataContract]
    //public class Selected_Project_StudyBudgetFile
    //{
    //    [DataMember]
    //    public int i_Selected_Project_Details_ID
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public string s_Budget_Document_File
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public string s_Budget_Comments
    //    {
    //        get;
    //        set;
    //    }
    //}
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSelected_Clause_Details
    [DataContract]
    public class Selected_Clause_Details
    {
        [DataMember]
        public int i_Contract_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Contract_Clause_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Status
        {
            get;
            set;
        }
        [DataMember]
        public string s_Comments
        {
            get;
            set;
        }
        [DataMember]
        public string s_Proposed_Changes
        {
            get;
            set;
        }
        [DataMember]
        public string Clause_Name
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRegulatory_Master
    [DataContract]
    public class Regulatory_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Sponsor_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Other_Sponsor
        {
            get;
            set;
        }
        [DataMember]
        public bool b_Prism_AppStatus
        {
            get;
            set;
        }
        [DataMember]
        public string s_Prism_AppNo
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Prism_AppDate
        {
            get;
            set;
        }
        [DataMember]
        public int i_CTC_status_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_CTC_Document
        {
            get;
            set;
        }
        [DataMember]
        public string s_CTC_EmailDocument
        {
            get;
            set;
        }
        [DataMember]
        public string dt_CTC_ApprDate
        {
            get;
            set;
        }
        [DataMember]
        public string s_CTC_No
        {
            get;
            set;
        }
        [DataMember]
        public string dt_CTC_ExpiryDate
        {
            get;
            set;
        }
        [DataMember]
        public string dt_NewExt_Appr_Date
        {
            get;
            set;
        }
        [DataMember]
        public string dt_NewExpiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Protocol_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_Protocol_Ver_No
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Protocol_Date
        {
            get;
            set;
        }
        [DataMember]
        public int i_Pending_Screen_Outcome
        {
            get;
            set;
        }
        [DataMember]
        public int i_Screen_Failure
        {
            get;
            set;
        }
        [DataMember]
        public int i_Screened
        {
            get;
            set;
        }
        [DataMember]
        public int i_Randomized
        {
            get;
            set;
        }
        [DataMember]
        public int i_Withdrawn
        {
            get;
            set;
        }
        [DataMember]
        public string s_Withdrawn_Reason
        {
            get;
            set;
        }
        [DataMember]
        public int i_Ongoing_Patient
        {
            get;
            set;
        }
        [DataMember]
        public int i_Completed_No
        {
            get;
            set;
        }
        [DataMember]
        public int i_SAE_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_SAE_Reason
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Internal_Audit
        {
            get;
            set;
        }
        [DataMember]
        public string s_Rpt_File_Title
        {
            get;
            set;
        }
        [DataMember]
        public string s_Investigation_product
        {
            get;
            set;
        }
        [DataMember]
        public string s_IpManagement
        {
            get;
            set;
        }
        [DataMember]
        public string s_IP_Storage
        {
            get;
            set;
        }
        [DataMember]
        public string s_RecruitedBy_TTSH
        {
            get;
            set;
        }
        [DataMember]
        public string s_Remarks
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_Name { get; set; }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public string DT_LASTUPDATED_DATE
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public string Project_Category { get; set; }
        [DataMember]
        public string Project_Type { get; set; }
        [DataMember]
        public string PI_NAME { get; set; }
        [DataMember]
        public string s_IRB_No { get; set; }
        [DataMember]
        public string Contract_Status { get; set; }
        [DataMember]
        public string s_Display_Project_ID { get; set; }
        [DataMember]
        public string s_Project_Title { get; set; }
        [DataMember]
        public string CTC_Status { get; set; }
        [DataMember]
        public int CTCCount { get; set; }
        [DataMember]
        public string s_NewCTCEmailApprDoc { get; set; }
        [DataMember]
        public string s_ExtCTCEmailApprDoc { get; set; }
        [DataMember]
        public List<Project_Master> pmlist { get; set; }
        [DataMember]
        public List<PI_Master> Pilisst { get; set; }
        [DataMember]
        public int RegSIxMId { get; set; }
        [DataMember]
        public List<RegulatorySixMonthUpdate> RegSixMUpdateList { get; set; }
        [DataMember]
        public List<Regulatory_StudyTeam> RegStudyTeamList { get; set; }
        [DataMember]
        public List<Regulatory_Submission_Status> RegSubStatusList { get; set; }
        [DataMember]
        public List<Regulatory_ICF_Details> RegICFDetails { get; set; }
        [DataMember]
        public List<Regulatory_Ammendments_Details> RegAmendDetails { get; set; }
        [DataMember]
        public List<RegulatoryIPManagement> RegIPList { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }
        [DataMember]
        public int rnum { get; set; }
        [DataMember]
        public string Project_Status_Disable { get; set; }

    }
    [DataContract]
    public class RegulatoryIPManagement
    {
        [DataMember]
        public int i_Regulatory_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Investigational_Product
        {
            get;
            set;
        }
        [DataMember]
        public int s_IPManagement
        {
            get;
            set;
        }
        [DataMember]
        public string s_StorageLocation
        {
            get;
            set;
        }
        [DataMember]
        public string s_IPName
        {
            get;
            set;
        }

    }

    [DataContract]
    public class RegulatoryNewProjectEntry
    {
        [DataMember]
        public List<Project_Master> pmlist { get; set; }
        [DataMember]
        public List<PI_Master> Pilisst { get; set; }

    }
    [DataContract]
    public class RegulatorySixMonthUpdate
    {
        [DataMember]
        public int i_Regulatory_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_SixmonthName
        {
            get;
            set;
        }
        [DataMember]
        public int i_Pending_Screen_Outcome
        {
            get;
            set;
        }
        [DataMember]
        public int i_Screen_Failure
        {
            get;
            set;
        }
        [DataMember]
        public int i_Screened
        {
            get;
            set;
        }
        [DataMember]
        public int i_Randomized
        {
            get;
            set;
        }
        [DataMember]
        public int i_Withdrawn
        {
            get;
            set;
        }
        [DataMember]
        public string s_Withdrawn_Reason
        {
            get;
            set;
        }
        [DataMember]
        public int i_Ongoing_Patient
        {
            get;
            set;
        }
        [DataMember]
        public int i_Completed_No
        {
            get;
            set;
        }
        [DataMember]
        public int i_SAE_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_SAE_Reason
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Internal_Audit
        {
            get;
            set;
        }
        [DataMember]
        public string dt_LastUpdated_date
        {
            get;
            set;
        }
        [DataMember]
        public int NoOfMonths { get; set; }
        [DataMember]
        public int RegSIxMId { set; get; }
        [DataMember]
        public DateTime SortDate { get; set; }
        [DataMember]
        public int rnum { get; set; }
    }

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblProject_Status_Master
    [DataContract]
    public class Project_Status_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblGrant_SubType1_Master
    [DataContract]
    public class Grant_SubType1_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int s_GrantType_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSelectedProject_BU_Details
    //[DataContract]
    //public class SelectedProject_BU_Details
    //{
    //    [DataMember]
    //    public int i_Selected_Project_ID
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public string s_Blinded_UnBlinded
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public int i_Cordinator_Id
    //    {
    //        get;
    //        set;
    //    }
    //    [DataMember]
    //    public string s_Cordinator_name
    //    {
    //        get;
    //        set;
    //    }
    //}
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblCRO_Master
    [DataContract]
    public class CRO_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
//----------TableName :-tblSAE_Master
    [DataContract]
    public class newSAE_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public string newSae_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    
    //----------TableName :-tblSAE_Master

    [DataContract]
    public class SAE_Master
    {
        [DataMember]
        public int i_SAE_ID
        {
            get;
            set;
        }
        
        [DataMember]
        public DateTime dt_SAEDateandTime
        {
            get;
            set;
        }
        [DataMember]
        public Boolean b_SAE_Status
        {
            get;
            set;
        }
        [DataMember]
        public string i_Patient_Studyno
        {
            get;
            set;
        }
        [DataMember]
        public int i_Notification_Mode
        {
            get;
            set;
        }
        [DataMember]
        public Boolean b_IsReadmission
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Readmission_date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Discharge_date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Knowledge_date
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------    
    //----------TableName :-tblCRA_Master
    [DataContract]
    public class CRA_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_CRO_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Email
        {
            get;
            set;
        }
        [DataMember]
        public string s_phone_no
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblFeasibility_Dept_PI
    [DataContract]
    public class Feasibility_Dept_PI
    {
        [DataMember]
        public int i_Feasibility_Id
        {
            get;
            set;
        }
        [DataMember]
        public int i_Dept_Id
        {
            get;
            set;
        }
        [DataMember]
        public int i_PI_Id
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRegulatory_ICF_Details
    [DataContract]
    public class Regulatory_ICF_Details
    {
        [DataMember]
        public int i_Regulatory_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Version_No
        {
            get;
            set;
        }
        [DataMember]
        public string dt_ICF_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRegulatory_Ammendments_Details
    [DataContract]
    public class Regulatory_Ammendments_Details
    {
        [DataMember]
        public int i_Regulatory_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Uploaded_File
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Submission_Date
        {
            get;
            set;
        }
        [DataMember]
        public string Uploaded_File { get; set; }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRegulatory_StudyTeam
    [DataContract]
    public class Regulatory_StudyTeam
    {
        [DataMember]
        public int i_Regulatory_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_First_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Last_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Email_ID
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblGrant_SubType2_Master
    [DataContract]
    public class Grant_SubType2_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int s_GrantSubType1_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblRegulatory_Submission_Status
    [DataContract]
    public class Regulatory_Submission_Status
    {
        [DataMember]
        public int i_Regulatory_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Interval_ID
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Submission_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_File_Title
        {
            get;
            set;
        }
        [DataMember]
        public string s_Uploaded_File
        {
            get;
            set;
        }
        [DataMember]
        public string dt_FileUploaded_Date
        {
            get;
            set;
        }
        [DataMember]
        public string UpFileName
        {
            get;
            set;
        }
        [DataMember]
        public string ReportName
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSenior_CSCS_Details
    [DataContract]
    public class Senior_CSCS_Details
    {
        [DataMember]
        public int Test
        {
            get;
            set;
        }

        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Award_org_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Dept_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_PI_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Grant_No
        {
            get;
            set;
        }
        [DataMember]
        public string s_Reaserch_IO
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_AwardLetter_Date
        {
            get;
            set;
        }
        [DataMember]
        public string dt_AwardLetter_File
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_StartDate
        {
            get;
            set;
        }
        [DataMember]
        public double? d_Protected_time
        {
            get;
            set;
        }
        [DataMember]
        public string s_Grant_Duration
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_ExpiryDate
        {
            get;
            set;
        }
        [DataMember]
        public bool b_IsGrant_Extented
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_NewExpiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Approval_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_GrantExtended_period
        {
            get;
            set;
        }
        [DataMember]

        public string s_Budget_Details_String
        {
            get;
            set;
        }

        [DataMember]
        public double i_Grant_Amount
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public List<Senior_CSCS_Budget_Allocation_Details> budgetDetails
        {
            get;
            set;
        }

        [DataMember]
        public string budgetDetails_XML
        {
            get;
            set;
        }

        [DataMember]
        public List<Project_PI> Dept_PI
        {
            get;
            set;
        }

        [DataMember]
        public int i_Selected_PI_ID
        {
            get;
            set;
        }

        [DataMember]
        public string Dept_PI_XML
        {
            get;
            set;
        }

        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }

        [DataMember]
        public int i_GrantName { get; set; }
        [DataMember]
        public string s_AwardLetter_File { get; set; }
        [DataMember]
        public DateTime? dt_Grant_Expiry_Date { get; set; }
        [DataMember]
        public DateTime? dt_NewGrantExpiry_Date { get; set; }


        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }

        [DataMember]
        public string AwardOrg { get; set; }
        [DataMember]
        public string GrantName { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string PI_Name { get; set; }
        [DataMember]
        public string GrantExpDate { get; set; }
        [DataMember]
        public string Status { get; set; }
    }



    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-StateM_Audit
    [DataContract]
    public class StateM_Audit
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public string State_Name
        {
            get;
            set;
        }
        [DataMember]
        public string AuditDataState
        {
            get;
            set;
        }
        [DataMember]
        public string AuditDMLAction
        {
            get;
            set;
        }
        [DataMember]
        public string AuditUser
        {
            get;
            set;
        }
        [DataMember]
        public DateTime AuditDateTime
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSeniorCSCS_Dept_PI_Details
    [DataContract]
    public class SeniorCSCS_Dept_PI_Details
    {
        [DataMember]
        public int i_SeniorCSCS_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_PI_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Dept_ID
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblSenior_CSCS_Budget_Allocation_Details
    [DataContract]
    public class Senior_CSCS_Budget_Allocation_Details
    {
        [DataMember]
        public int i_Senior_CSCS_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Yearly_Quaterly
        {
            get;
            set;
        }
        [DataMember]
        public string s_Years
        {
            get;
            set;
        }
        [DataMember]
        public double i_Q1
        {
            get;
            set;
        }
        [DataMember]
        public double i_Q2
        {
            get;
            set;
        }
        [DataMember]
        public double i_Q3
        {
            get;
            set;
        }
        [DataMember]
        public double i_Q4
        {
            get;
            set;
        }
        [DataMember]
        public string s_Factors
        {
            get;
            set;
        }
        [DataMember]
        public string i_Currency_ID
        {
            get;
            set;
        }

        [DataMember]
        public double i_Budget_Allocation
        {
            get;
            set;
        }
        [DataMember]
        public double i_Budget_Utilized
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-CityM_Audit
    [DataContract]
    public class CityM_Audit
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }
        [DataMember]
        public int State_ID
        {
            get;
            set;
        }
        [DataMember]
        public string City_Name
        {
            get;
            set;
        }
        [DataMember]
        public string AuditDataState
        {
            get;
            set;
        }
        [DataMember]
        public string AuditDMLAction
        {
            get;
            set;
        }
        [DataMember]
        public string AuditUser
        {
            get;
            set;
        }
        [DataMember]
        public DateTime AuditDateTime
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblGrant_SubType3_Master
    [DataContract]
    public class Grant_SubType3_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int s_GrantSubType2_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Description
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblMenuMaster
    [DataContract]
    public class MenuMaster
    {
        [DataMember]
        public double MenuId
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
        public double CreatedBy
        {
            get;
            set;
        }
        [DataMember]
        public DateTime CreatedDate
        {
            get;
            set;
        }
        [DataMember]
        public double ModifyBy
        {
            get;
            set;
        }
        [DataMember]
        public DateTime ModifyDate
        {
            get;
            set;
        }
        [DataMember]
        public double DisplayOrder
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblGrant_Master
    [DataContract]
    public class Grant_Master
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Application_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_GrantType_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Grant_SubType_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Grant_Sub_SubType_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Grant_Sub_Sub_SubType_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_SubmissionStatus
        {
            get;
            set;
        }
        [DataMember]
        public string s_Old_Application_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Currency_ID
        {
            get;
            set;
        }
        [DataMember]
        public double i_Amount_Requested
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Closing_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Duration
        {
            get;
            set;
        }
        [DataMember]
        public string s_Mentor
        {
            get;
            set;
        }
        [DataMember]
        public double i_FTE
        {
            get;
            set;
        }
        [DataMember]
        public int i_Outcome
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Outcome_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Reviewers_Comments
        {
            get;
            set;
        }
        [DataMember]
        public string s_Created_By
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Modified_By
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modified_Date
        {
            get;
            set;
        }
        [DataMember]
        public bool IsDeleted
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_ApplicationDate
        {
            get;
            set;
        }
        [DataMember]
        public int i_AwardOrganization
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_AwardDate
        {
            get;
            set;
        }
        [DataMember]
        public int i_AwardCountryID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Grant_Name
        {
            get;
            set;
        }
        [DataMember]
        public string CountryName
        {
            get;
            set;
        }
        [DataMember]
        public bool GrantDetails_Applied
        {
            get;
            set;
        }
        [DataMember]
        public Decimal Total_ChildAmount
        {
            get;
            set;
        }
        [DataMember]
        public int i_Child_DurationID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Child_Duration
        {
            get;
            set;
        }
        [DataMember]
        public string Dt_AwardDate { get; set; }
        [DataMember]
        public string GRANT_TYPE { get; set; }
        [DataMember]
        public string GRANT_SUB_TYPE1 { get; set; }
        [DataMember]
        public string GRANT_SUB_TYPE2 { get; set; }
        [DataMember]
        public string GRANT_SUB_TYPE3 { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string AWARD_ORGANIZATION { get; set; }
        [DataMember]
        public bool YearQuaterStatus { get; set; }
    }

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    [DataContract]
    public class ContractList
    {
        [DataMember]
        public string s_ContractId { get; set; }
        [DataMember]
        public string s_ContractCategory { get; set; }
        [DataMember]
        public string s_ContractStatus { get; set; }
        [DataMember]
        public string s_Country { get; set; }
        [DataMember]
        public string s_Contract_Name { get; set; }
        [DataMember]
        public int i_ID { get; set; }
        [DataMember]
        public string dt_Expiry_Date { get; set; }
        [DataMember]
        public string dt_NewExpiry_Date { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ReviwedByid { get; set; }
    }
    //----------TableName :-tblContract_Details
    [DataContract]
    public class Contract_Details
    {

        [DataMember]
        public int test
        {
            get;
            set;
        }

        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Contract_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Contract_Display_Id
        {
            get;
            set;
        }
        [DataMember]
        public int i_Contract_Category_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Contract_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public string dt_LastUpdated_Date
        {
            get;
            set;
        }
        [DataMember]
        public int i_Govt_Lawcountry
        {
            get;
            set;
        }
        [DataMember]
        public string s_Country { get; set; }
        [DataMember]
        public string s_Clauses_File
        {
            get;
            set;
        }
        [DataMember]
        public string s_UploadedContract_File
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Effective_Date
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Finalization_Date
        {
            get;
            set;
        }
        [DataMember]
        public string dt_LastSigned_Date
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Expiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public bool b_Amendments
        {
            get;
            set;
        }
        [DataMember]
        public string dt_NewExpiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_AmendmenstContract_File
        {
            get;
            set;
        }
        [DataMember]
        public int i_Currency_ID
        {
            get;
            set;
        }
        [DataMember]
        public double i_Hospital_Cost
        {
            get;
            set;
        }
        [DataMember]
        public double i_Investigator_fees
        {
            get;
            set;

        }

        [DataMember]
        public double i_Inkind_Contribution
        {
            get;
            set;
        }

        [DataMember]
        public double i_Coordinator_fess
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public string Contracts
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
        public string Project_Category_Name { get; set; }
        [DataMember]
        public string Project_Type { get; set; }
        [DataMember]
        public string PI_NAME { get; set; }
        [DataMember]
        public string s_IRB_No
        {
            get;
            set;
        }
        [DataMember]
        public string Contract_Status { get; set; }
        [DataMember]
        public string dt_Contract_StatusDate { get; set; }
        [DataMember]
        public List<SelectedCollborators_Details> lstSelCollab { get; set; }
        [DataMember]
        public List<Selected_Clause_Details> lstSelClause { get; set; }
        [DataMember]
        public List<ContractDetails_MultipleContractFile> lstmultiple { get; set; }
        [DataMember]
        public List<PI_Master> pmlist { get; set; }
        [DataMember]
        public List<Contract_Collobrator_Master> ccdlist { get; set; }
        [DataMember]
        public List<Project_Master> pjctmList { get; set; }
        [DataMember]
        public List<ContractList> contlist { get; set; }
        [DataMember]
        public List<Contract_Status_Date> listcsd { get; set; }
        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Project_Status_Disable { get; set; }
    }
    [DataContract]
    public class ProjectDataforContractUsers
    {
        [DataMember]
        public List<Project_Master> pmlist { get; set; }
        [DataMember]
        public List<PI_Master> Pilisst { get; set; }
        [DataMember]
        public List<Contract_Collobrator_Master> ccmlist { get; set; }
    }


    [DataContract]
    public class Contract_Status_Date
    {


        [DataMember]
        public int i_Contract_Status_ID
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Status_Date
        {
            get;
            set;
        }
    }

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblGrant_Details
    [DataContract]
    public class Grant_Details
    {
        [DataMember]
        public int i_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_GrantMaster_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_Award_Letter_File
        {
            get;
            set;
        }
        [DataMember]
        public string i_Grant_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Award_Letter_Date
        {
            get;
            set;
        }
        [DataMember]
        public string s_Research_IO
        {
            get;
            set;
        }
        [DataMember]
        public int i_Currency_ID
        {
            get;
            set;
        }
        [DataMember]
        public double i_Donation_Amt
        {
            get;
            set;
        }
        [DataMember]
        public string s_Donation_Body
        {
            get;
            set;
        }
        [DataMember]
        public string dt_Grant_Expiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Grant_Extended
        {
            get;
            set;
        }
        [DataMember]
        public string dt_New_Grant_Expiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public double i_Indirects
        {
            get;
            set;
        }
        [DataMember]
        public double i_Indirects_Amt_Utilized
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Mentor
        {
            get;
            set;
        }
        [DataMember]
        public string s_Mentor_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Mentor_Institute
        {
            get;
            set;
        }
        [DataMember]
        public string s_Mentor_Dept
        {
            get;
            set;
        }
        [DataMember]
        public string s_Tech_PI_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_Tech_PI_Institution
        {
            get;
            set;
        }
        [DataMember]
        public string s_Tech_PI_Dept
        {
            get;
            set;
        }
        [DataMember]
        public string s_Point_of_Submission
        {
            get;
            set;
        }
        [DataMember]
        public double i_FTE
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_ID
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Created_Date
        {
            get;
            set;
        }
        [DataMember]
        public DateTime? dt_Modify_Date
        {
            get;
            set;
        }
        [DataMember]
        public int i_GrantStatus_ID
        {
            get;
            set;
        }
        [DataMember]
        public bool IsDeleted
        {
            get;
            set;
        }
        [DataMember]
        public string s_ModifyBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_CreatedBy_Name
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_IsVariation_Needed
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
        public string Project_Category_Name
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
        public string PI_Name
        {
            get;
            set;
        }
        [DataMember]
        public string s_SubmissionStatus
        {
            get;
            set;
        }
        [DataMember]
        public string s_OutcomeStatus
        {
            get;
            set;
        }
        [DataMember]
        public string GrantDetailStatus
        {
            get;
            set;
        }
        [DataMember]
        public int GM_ID { get; set; }
        [DataMember]
        public int GD_ID { get; set; }

        [DataMember]
        public string UName { get; set; }
        [DataMember]
        public string UID { get; set; }

        [DataMember]
        public List<Project_Master> PMList { get; set; }
        [DataMember]
        public List<PI_Master> PIList { get; set; }
        [DataMember]
        public List<Grant_Master> GMList { get; set; }
        [DataMember]
        public List<Project_Master> CHildProjectList { get; set; }
        [DataMember]
        public List<PI_Master> CHILDPIList { get; set; }
        [DataMember]
        public List<GrantExtensionDetails> GEXTList { get; set; }
        [DataMember]
        public int i_GrantExtended_period { get; set; }
        [DataMember]
        public bool YearQuaterStatus { get; set; }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Project_Status_Disable { get; set; }
    }

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    //----------TableName :-tblEthics_Dept_PI
    [DataContract]
    public class Ethics_Dept_PI
    {
        [DataMember]
        public int i_Ethics_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Dept_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_PI_ID
        {
            get;
            set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 

    //----------------------------------------------END oF Class------------------------------------------------------------ 
    public class ParentMenuAccess
    {

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
        public bool AccessRights
        {
            get;
            set;
        }

    }
    [DataContract]
    public class MenuAccessRights
    {
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
        public string RoleName
        {
            get;
            set;
        }
        [DataMember]
        public int RoleId
        {
            get;
            set;

        }
        [DataMember]
        public bool AccessRights
        {
            get;
            set;
        }


    }

    public class UserMenuRights
    {
        public List<ParentMenuAccess> parentMenuAccess;
        public List<MenuAccessRights> menuAccessRights;
    }
    //--End Of Menu Rights


    [DataContract]
    public class RptSelectedProject
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ProjectTitle { get; set; }
        [DataMember]
        public string DisplayProjectID { get; set; }

    }
    //---Class Regarding Document Category
    [DataContract]
    public class tbldocumentcategory
    {
        [DataMember]
        public int DocumentCategoryId { get; set; }
        [DataMember]
        public string DocumentCategory { get; set; }
        [DataMember]
        public bool IsProjectRelated { get; set; }
        [DataMember]
        public string s_CreatedBy_ID { get; set; }
        [DataMember]
        public DateTime dt_Created_Date { get; set; }
        [DataMember]
        public string s_ModifyBy_ID { get; set; }
        [DataMember]
        public DateTime dt_Modify_Date { get; set; }
    }
    //-End Here

    //--CLass regarding DOcument Management System
    [DataContract]
    public class DMS_DocumentManagementSystem
    {

        [DataMember]
        public int i_DMSId { get; set; }
        [DataMember]
        public string DocTitle { get; set; }
        [DataMember]
        public string DocDescription { get; set; }

        [DataMember]
        public int DocType { get; set; }

        [DataMember]
        public int i_Project_ID { get; set; }

        [DataMember]
        public string s_DMS_FileName { get; set; }

        [DataMember]
        public string s_CreatedBy_ID { get; set; }
        [DataMember]
        public DateTime dt_Created_Date { get; set; }
        [DataMember]
        public string s_ModifyBy_ID { get; set; }
        [DataMember]
        public DateTime dt_Modify_Date { get; set; }
        [DataMember]
        public bool isDeleted { get; set; }

    }

    //CLass Regarding List Of Document Management
    [DataContract]
    public class DocumentManagementSystemFile
    {

        [DataMember]
        public int i_DMSId { get; set; }
        [DataMember]
        public string DocTitle { get; set; }
        [DataMember]
        public string DocDescription { get; set; }

        [DataMember]
        public string DocCategory { get; set; }


        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string ProjectTile { get; set; }

        [DataMember]
        public string Project_ID { get; set; }
    }
    [DataContract]
    public class DocumentManagementSystem
    {
        [DataMember]
        public List<Project_Master> project;
        [DataMember]
        public List<DocumentManagementSystemFile> documentManagementSYstemFile;
    }
    [DataContract]
    public class DataOwner
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public int Project_Id { get; set; }
        [DataMember]
        public string Ftype { get; set; }
    }
    [DataContract]
    public class GrantApplication
    {
        [DataMember]
        public int i_Project_ID { get; set; }
        [DataMember]
        public string s_Project_Title { get; set; }
        [DataMember]
        public string s_Display_Project_ID { get; set; }
        [DataMember]
        public string s_IRB_No { get; set; }
        [DataMember]
        public string PI_NAME { get; set; }
        [DataMember]
        public int i_ID { get; set; }
        [DataMember]
        public string s_SubmissionStatus { get; set; }
        [DataMember]
        public string s_OutcomeStatus { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public bool GrantDetails_Applied
        {
            get;
            set;
        }
        [DataMember]
        public string ParentProject { get; set; }
        [DataMember]
        public string ChildParentProject { get; set; }
        [DataMember]
        public string parentProjectCount { get; set; }
        [DataMember]
        public string IsChildorParent { get; set; }

        [DataMember]
        public string Prog { get; set; }
        [DataMember]
        public string Mutli { get; set; }
        [DataMember]
        public string Project_Status_Disable { get; set; }
    }
    [DataContract]
    public class GrantParentProjectData
    {
        [DataMember]
        public String s_Display_Project_ID;
        [DataMember]
        public string s_Project_Title;
        [DataMember]
        public bool GrantApplied;
        [DataMember]
        public decimal Total_Amount;
        [DataMember]
        public int i_DurationID;
        [DataMember]
        public decimal Remaining_Amount;
        [DataMember]
        public string s_Duration;

        //------------ 08-09-2015
        [DataMember]
        public string s_Short_Title;
        [DataMember]
        public int i_ID;
        [DataMember]
        public string s_Application_ID { get; set; }
        [DataMember]
        public string dt_ApplicationDate { get; set; }
        [DataMember]
        public int i_AwardOrganization { get; set; }
        [DataMember]
        public int i_AwardCountryID { get; set; }
        [DataMember]
        public string AwardCountry { get; set; }
        [DataMember]
        public string s_Grant_Name { get; set; }
        [DataMember]
        public string dt_AwardDate { get; set; }
        [DataMember]
        public int i_Outcome { get; set; }
        [DataMember]
        public string s_Reviewers_Comments { get; set; }
        [DataMember]
        public int i_Grant_Sub_SubType_ID { get; set; }
        [DataMember]
        public int i_Grant_Sub_Sub_SubType_ID { get; set; }
    }

    [DataContract]
    public class GrantMasterDetails
    {
        [DataMember]
        public Grant_Master grant_Master;
        [DataMember]
        public Project_Master project { get; set; }
        [DataMember]
        public List<PI_Master> Pilisst { get; set; }
        [DataMember]
        public GrantParentProjectData parentProject;
    }

    [DataContract]
    public class GrantNewProjectEntry
    {
        [DataMember]
        public List<Project_Master> PMList { get; set; }
        [DataMember]
        public List<PI_Master> PIList { get; set; }
        [DataMember]
        public List<Grant_Master> GMList { get; set; }
        [DataMember]
        public List<Project_Master> CHildProjectList { get; set; }
        [DataMember]
        public List<PI_Master> CHILDpilist = new List<PI_Master>();
    }

    [DataContract]
    public class Other_Dept_PI
    {
        [DataMember]
        public int i_Project_ID { get; set; }
        [DataMember]
        public int i_OtherPI_ID { get; set; }
    }
    [DataContract]
    public class Grant_Budget_Allocation_Details
    {
        [DataMember]
        public int i_Grant_Details_ID { get; set; }
        [DataMember]
        public int i_Project_ID { get; set; }
        [DataMember]
        public string s_Years { get; set; }
        [DataMember]
        public string s_Factors { get; set; }
        [DataMember]
        public int i_TTSH_PI_ID { get; set; }
        [DataMember]
        public int i_Other_PI_ID { get; set; }
        [DataMember]
        public double i_Budget_Allocation { get; set; }
        [DataMember]
        public string s_Yearly_Quaterly { get; set; }
        [DataMember]
        public double i_Budget_Utilized { get; set; }
        [DataMember]
        public double Q1 { get; set; }
        [DataMember]
        public double Q2 { get; set; }
        [DataMember]
        public double Q3 { get; set; }
        [DataMember]
        public double Q4 { set; get; }

    }
    [DataContract]
    public class GrantExtensionDetails
    {
        [DataMember]
        public int i_Grant_Detail_ID
        {
            get;
            set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get;
            set;
        }
        [DataMember]
        public bool? b_Grant_Extended
        {
            get;
            set;
        }
        [DataMember]
        public string dt_New_Grant_Expiry_Date
        {
            get;
            set;
        }
        [DataMember]
        public int i_GrantExtended_period
        {
            get;
            set;
        }
        [DataMember]
        public string s_Budget_Details_String
        {
            get;
            set;
        }
    }

    //----------TableName :-tblSelected_Project_Multiple_SAE_Details
    [DataContract]
    public class Selected_Project_Multiple_SAE_Details
    {
        [DataMember]
        public bool? b_SAE_Status
        {
            get; set;
        }
        [DataMember]
        public string i_Patient_Studyno
        {
            get; set;
        }
        [DataMember]
        public int? i_Notification_Mode
        {
            get; set;
        }
        [DataMember]
        public bool? b_IsReadmission
        {
            get; set;
        }
        [DataMember]
        public DateTime? dt_Readmission_date
        {
            get; set;
        }
        [DataMember]
        public DateTime? dt_Discharge_date
        {
            get; set;
        }
        [DataMember]
        public DateTime? dt_Knowledge_date
        {
            get; set;
        }
        [DataMember]
        public int i_Selected_Project_ID
        {
            get; set;
        }
        [DataMember]
        public int i_Project_ID
        {
            get; set;
        }
        [DataMember]
        public DateTime? dt_EntryDate_Time
        {
            get; set;
        }
    }
    //----------------------------------------------END oF Class------------------------------------------------------------ 
}
