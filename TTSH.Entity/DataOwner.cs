using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace TTSH.Entity
{
    [DataContract]
    public class DataOwner_Entity
    {
         [DataMember]
        public string GroupName { get; set; }
         [DataMember]
        public string MemberName { get; set; }
         [DataMember]
        public string GUID { get; set; }
         [DataMember]
         public string EmailId { get; set; }
         //[DataMember]
         //public byte[] ArrByteArray { get; set; }
    }


    [DataContract]
    public class Project_DataOwner
    {
        [DataMember]
        public int i_ID { get; set; }

        [DataMember]
        public int i_Project_ID { get; set; }

        [DataMember]
        public string s_DisplayProject_ID { get; set; }

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
        public bool isDeleted { get; set; }

    }

}
