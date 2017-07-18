using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TTSH.Entity
{
    public enum ExcelCellName{A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,X,Y,Z};

    public enum DataType
    {
        String,
        Integer,
        Percentage,
        StringBold,
        IntegerBold,
        PercentBold,
        Date
    }

    [DataContract]
    public class Export
    {
        public int rowIndex { get; set; }
        public List<ExportCellData> listExportRowData { get; set; }
    }

    [DataContract]
     public class ExportCellData
    {
        [DataMember]
        public string dataValue { get; set; }
        [DataMember]
        public DataType dataType {get;set;}
        [DataMember]
        public ExcelCellName cellName{ get; set; }
    }
}
