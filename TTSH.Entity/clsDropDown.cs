using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace TTSH.Entity
{
    [DataContract]
    public class clsDropDown
    {
        [DataMember]
        public string DisplayField { get; set; }

        [DataMember]
        public string ValueField { get; set; }
          
    }
}
