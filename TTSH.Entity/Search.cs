using System;
using System.Runtime.Serialization;

namespace TTSH.Entity
{
    [DataContract]
    public class Search
    {
        [DataMember]
        public long i_ID { get; set; }
        [DataMember]
        public long i_Project_ID { get; set; }
        [DataMember]
        public string s_Display_Project_ID { get; set; }
        [DataMember]
        public string s_Project_Title { get; set; }
        [DataMember]
        public string s_Project_Category { get; set; }
        [DataMember]
        public string Feasibility_Status_Name { get; set; }
        [DataMember]
        public string s_IRB_No { get; set; }
        [DataMember]
        public string Project_Type { get; set; }
        [DataMember]
        public string PI_Names { get; set; }
        [DataMember]
        public string sContractApplicationstatus { get; set; }
        [DataMember]
        public string sDepartment { get; set; }
        [DataMember]
        public long iRecordExists { get; set; }
        [DataMember]
        public string Ethics_ID { get; set; }
        [DataMember]
        public string Project_Status { get; set; }
        [DataMember]
        public string i_Feasibility_ID { get; set; }



        [DataMember]
        public string Project_Category_Name { get; set; }
        [DataMember]
        public string Project_Category { get; set; }
        [DataMember]
        public string PI_Name { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ContAppStatus { get; set; }
        [DataMember]
        public string CTC_status { get; set; }
        [DataMember]
        public int CTCCount { get; set; }
        [DataMember]
        public string Contracts { get; set; }
        [DataMember]
        public string Contract_Status { get; set; }
        [DataMember]
        public string s_SubmissionStatus { get; set; }
        [DataMember]
        public string s_OutComeStatus { get; set; }

        //Selected Project
        [DataMember]
        public string Study_Status { get; set; }
        [DataMember]
        public string cordinatorstatus { get; set; }
        [DataMember]
        public string IsCoordinator { get; set; }
        [DataMember]
        public string DetailsExist { get; set; }
        //Selected Project

        //Grant Detail
        [DataMember]
        public int GM_ID { get; set; }
        [DataMember]
        public int GD_ID { get; set; }
        [DataMember]
        public string GrantDetailStatus { get; set; }
        //Grant Detail
        [DataMember]
        public string Created_By { get; set; }


        [DataMember]
        public string S_ProjectStatus { get; set; }

        [DataMember]
        public string EthicsStatus { get; set; }
        [DataMember]
        public string Project_Status_Disable { get; set; }
        [DataMember]
        public bool isGrantDetailsApplied
        { get; set; }

        [DataMember]
        public bool GrantDetails_Applied { get; set; }
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

        //Senior CSCS
        [DataMember]
        public string GrantName { get; set; }
        [DataMember]
        public string AwardOrg { get; set; }
        [DataMember]
        public string s_Grant_No { get; set; }
        [DataMember]
        public DateTime? GrantExpDate { get; set; }
        [DataMember]
        public DateTime? StartDate { get; set; }
    }
}
