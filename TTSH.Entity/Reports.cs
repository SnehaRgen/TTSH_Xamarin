using System;
using System.Runtime.Serialization;

namespace TTSH.Entity
{
    [DataContract]
    public class RptProjectCategory
    {
        [DataMember]
        public long CategoryId { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
    }

    [DataContract]
    public class RptProjectType
    {
        [DataMember]
        public long TypeId { get; set; }
        [DataMember]
        public string TypeName { get; set; }
    }

    [DataContract]
    public class RptDepartment
    {
        [DataMember]
        public long DepartmentId { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
    }

    [DataContract]
    public class RptPIName
    {
        [DataMember]
        public long PIId { get; set; }
        [DataMember]
        public string PIName { get; set; }
    }

}
