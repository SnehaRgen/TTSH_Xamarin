using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TTSH.Entity
{


    [DataContract]
    public enum DropDownName
    {

        [EnumMember]
        PI = 1,

        [EnumMember]
        PI_Department = 2,

        [EnumMember]
        Project_Status = 3,

        [EnumMember]
        IRB_Type = 4,

        [EnumMember]
        IRB_Status = 5,

        [EnumMember]
        Project_Category = 6,

        [EnumMember]
        Project_Type = 7,

        [EnumMember]
        Fesibility_Status = 8,

        [EnumMember]
        Drug_Location = 9,

        [EnumMember]
        Project_Sub_Types = 10,

        [EnumMember]
        Mode_of_Notification = 11,

        [EnumMember]
        Ethics_Review = 12,

        [EnumMember]
        Clinical_Research = 13,

        [EnumMember]
        Study_Status = 14,
        [EnumMember]
        PI_Details = 15,

        [EnumMember]
        Country = 16,

        [EnumMember]
        Collaborator = 17,

        [EnumMember]
        FillSubType,
        [EnumMember]
        Project_Name,
        [EnumMember]
        FillClauses,
        [EnumMember]
        FillCollaborators,
        [EnumMember]
        Contract_Category,
        [EnumMember]
        Contract_Status,
        [EnumMember]
        GetAllCollaboratorForProject,
        [EnumMember]
        ProjectFeasibility,

        [EnumMember]
        Coordinators,

        [EnumMember]
        Cupboad_Number,

        [EnumMember]
        LeadSponsor,

        [EnumMember]
        CTCstatus,
        [EnumMember]
        RegularoryStatusReportFor,
        [EnumMember]
        RegulatoryIPStorage,
        [EnumMember]
        AuditModules,
        [EnumMember]
        AuditModulesFields,
        [EnumMember]
        MenuMapping,

        [EnumMember]
        GetYear,
        [EnumMember]
        DocumentCategory,
        [EnumMember]
        DataOwner,
        [EnumMember]
        GrantType,
        [EnumMember]
        GrantSubType1,

        [EnumMember]
        GrantSubType2,

        [EnumMember]
        GrantSubType3,

        [EnumMember]
        GrantSubmissionStatus,
        [EnumMember]
        GrantOutCome,
        [EnumMember]
        GrantAwardingOrganization,
        [EnumMember]
        GrantDuration,

        [EnumMember]
        Selected_Years,

        [EnumMember]
        Period_of_Insurance,

        [EnumMember]
        GrantDetailStatus,
        [EnumMember]
        SeniorSCSCGrantName,
        [EnumMember]
        NameOfIRB,
            
        [EnumMember]
        FormsName,
        [EnumMember]
        TablesName




    }
}
