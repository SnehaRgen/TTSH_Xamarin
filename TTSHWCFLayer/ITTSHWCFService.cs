using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TTSH.Entity;
using TTSHWCFLayer.Excel;

namespace TTSHWCFLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITTSHWCFService" in both code and config file together.
    [ServiceContract(Namespace = "WcfAjaxServices")]

    public interface ITTSHWCFService
    {

        [OperationContract]
        [WebGet]
        string HelloWorld();

        #region Project Details Methods

        [OperationContract]
        Project_Master GetProject_MasterDetailsByID(int ID);
        [OperationContract]
        string Project_Master(Project_Master _Project_Master, List<Project_Dept_PI> pdi, List<Project_Coordinator_Details> pcd, string mode);
        //[OperationContract]
        //PI_Master GetPI_MasterDetailsByID(int ID);
        [OperationContract]
        string PI_Master(PI_Master _PI_Master, string mode);
        [OperationContract]
        List<Project_Master> FillGrid_Project_Master();
        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string GetPI_MasterDetailsByID(int ID);
        #endregion

        #region Common Methods
        [OperationContract]
        List<clsDropDown> GetDropDownData(DropDownName dropDownName, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "");

        [OperationContract]
        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string GetValidate(string _ModuleName, string _A, string _B, string _C, string _D);

        [OperationContract]
        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string[] GetValues(string _ModuleName, string _A, string _B, string _C, string _D);

        [OperationContract]
        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]

        string[] GetCasCadeDdl(String moduleName, string id);
        #endregion

        #region Feasibility Methods
        [OperationContract]
        Feasibility_Details GetFeasibility_DetailsByID(int ID);

        [OperationContract]
        string Feasibility_Details(Feasibility_Details _Feasibility_Details, Mode mode);

        [OperationContract]
        List<Feasibility_Grid> Feasibility_FillGrid();

        [OperationContract]
        string Sponsor(Sponsor_Master sponsor);

        [OperationContract]
        string CRO(CRO_Master CRO);

        #endregion

        #region Ethics Methods

        [OperationContract]
        Ethics_Details GetEthics_DetailsByID(int ID);

        [OperationContract]
        string Ethics_Details(Ethics_Details _Ethics_Details, Mode mode);

        [OperationContract]
        [WebGet]
        List<Ethics_Grid> Ethics_FillGrid();






        #endregion

        #region Selected Project Methods
        [OperationContract]
        List<Selected_Grid> Selected_FillGrid(string userID, bool isSelectedTeamUser);

        [OperationContract]
        string SAE(SAE_Master SAE);

        [OperationContract]
        string newSAE(newSAE_Master newSAE);

        [OperationContract]
        string CRA(CRA_Master CRA);

        [OperationContract]
        Selected_Project_Details GetSelected_Project_DetailsByID(int ID, string year, string month);

        [OperationContract]
        string Selected_Project_Details(Selected_Project_Details _Selected_Project_Details, Mode mode);


        #endregion

        #region " Contract Methods "

        [OperationContract]
        Contract_Collobrator_Master GetContract_Collobrator_MasterByID(int ID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string GetCollobrator_MasterDetailByID(int ID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string[] GetText(string Prefix, int count, string ContextKey);

        [OperationContract]
        List<Contract_Master> FillGrid_Contract_Master();
        [OperationContract]
        List<Contract_Details> FillGrid_Contract_Details();
        [OperationContract]
        Contract_Details GetContract_DetailsDetailsByID(int ID);
        [OperationContract]
        Contract_Master GetContract_MasterDetailsByID(int ID);


        [OperationContract]
        ProjectDataforContractUsers FillProjectDataforContractUsers(int ID);

        [OperationContract]
        Contract_Details GetContractDeta(int ID, int ProjectId = 0);
        [OperationContract]
        string Contract_Collobrator_Master_DML(Contract_Collobrator_Master _Contract_Collobrator_Master, string mode);
        [OperationContract]
        string Contract_Details_DML(Contract_Details _Contract_Details, List<SelectedCollborators_Details> lstSelCollab, List<Selected_Clause_Details> lstSelClause, List<ContractDetails_MultipleContractFile> lstmultiple, string mode);
        [OperationContract]
        string Contract_Master_DML(Contract_Master _Contract_Master, List<Contract_Collaborator_Details> clist, string mode);
        [OperationContract]
        string ContractDetails_MultipleContractFile_DML(ContractDetails_MultipleContractFile _ContractDetails_MultipleContractFile, string mode);
        #endregion

        #region User Authentication

        [OperationContract]
        bool AuthenticateADUsers(string Adserver, string userName, string password);

        [OperationContract]
        bool AuthenticateADUsersByName(string userName);

        [OperationContract]
        String[] GetGroupNames(string userName, string password);

        [OperationContract]
        List<ADUserDetails> GetMenusByGroup(string Group);

        [OperationContract]
        TTSH.Entity.ADUserDetails GetUserDetails(string UserName);

        [OperationContract]
        List<ADUserDetails> GetUserADDetails(string ADServer, string userName, string password);

        [OperationContract]
        string GetUserGUID(string UserName);


        [OperationContract]
        string[] GetGroups();

        #endregion

        #region Excel Export Methods

        [OperationContract]
        void ExportToExcel();

        #endregion

        #region Search
        [OperationContract]
        Dictionary<string, List<Search>> GetSearchData(string SearchInputValue, string SearchFilterCriteria, string UserID, string UserGroup);
        #endregion

        #region Audit Methods
        [OperationContract]
        List<Audit> FillGrid_Audit(DateTime FromDate, DateTime ToDate);
        #endregion

        #region "Regulatory Module"
        [OperationContract]
        List<Regulatory_Master> FillGridRegulatoryMain();
        [OperationContract]
        RegulatoryNewProjectEntry GetNewProjectEntry(int ID);

        [OperationContract]
        List<Regulatory_Master> FillGridRegulatoryDetailsByID(int ID);

        [OperationContract]
        string Regulatory_Master_DML(Regulatory_Master _Regulatory_Master,
           List<Regulatory_StudyTeam> lstRegulatory_StudyTeam,
           List<Regulatory_ICF_Details> lstRegulatory_ICF_Details,
           List<Regulatory_Submission_Status> lstRegulatory_Submission_Status,
           List<Regulatory_Ammendments_Details> lstRegulatory_Ammendments_Details,
           List<RegulatoryIPManagement> lstRegulatoryIPManagement,
           string mode);

        [OperationContract]
        Regulatory_Master GetRegulatory_MasterDetailsByID(int ID);
        #endregion

        #region "User Access Rights"
        [OperationContract]
        UserMenuRights GetAllMenus(string roleName);

        [OperationContract]
        bool SaveAccess(string menuxml, int roleid, int createdby = 0);
        #endregion

        #region "RptSelectedProject"
        [OperationContract]
        List<RptSelectedProject> GetProjectDetails(string Sdate, string Edate);
        #endregion

        #region "Reporting Tool"
        [OperationContract]
        DataSet GetSelectedTables(int ModuleID, string SelectedTablesID);
        #endregion        

        #region "Document Management System"
        [OperationContract]
        DocumentManagementSystem GetDocumentWithProject(int projectId);

        [OperationContract]
        List<DocumentManagementSystemFile> GetDocuments(string searchText);

        [OperationContract]
        bool SaveDocumentData(List<DMS_DocumentManagementSystem> docManSys);
        #endregion

        #region ReportPIByDept

        [OperationContract]
        List<RptProjectCategory> ListRptProjectCategory();

        [OperationContract]
        List<RptProjectType> ListRptProjectType();

        [OperationContract]
        List<RptDepartment> ListRptDepartment();

        [OperationContract]
        List<RptPIName> ListRptPIName();

        [OperationContract]
        List<RptPIName> ListRptPINameByDepartment(String DepartmentId);

        #endregion

        #region DataOwner
        [OperationContract]
        List<DataOwner_Entity> GetAllDataOwner(string GroupName);

        [OperationContract]
        List<Project_DataOwner> GetProjectsByDO(string ModuleName, string UserGUID);
        #endregion

        #region "Grant Application"
        [OperationContract]
        List<GrantApplication> FillGrid_GrantApplication();
        [OperationContract]
        string Grant_Application(Grant_Master grant_Master, List<Project_Dept_PI> pdi, string mode);
        [OperationContract]
        GrantMasterDetails GetGrantApplicationDetails(int grantId);
        [OperationContract]
        GrantMasterDetails GetNewProjectDetailsForGrant(int ProjectId);
        #endregion

        #region " Grant Detail "
        [OperationContract]
        List<Grant_Details> FillGrantDetailGrid();
        [OperationContract]
        GrantNewProjectEntry FillGrantDetailNewProject(int ID);
        [OperationContract]
        Grant_Details GetGrantDetailsById(int ID, int ProjID);
        [OperationContract]
        string Grant_Details_DML(Grant_Details _Grant_Details, List<Other_Dept_PI> lstoth, List<Grant_Budget_Allocation_Details> lstGBAD, List<GrantExtensionDetails> lstGExt, string mode);
        [OperationContract]
        string Other_PI_Master_DML(Other_PI_Master _Other_PI_Master, string mode);
        #endregion

        #region " Grant Senior CSCS "
        [OperationContract]
        List<Senior_CSCS_Details> FillGrantSeniorCSCSGrid();
        [OperationContract]
        string Senior_CSCS_Details_DML(Senior_CSCS_Details _Senior_CSCS_Details, string mode);

        [OperationContract]
        Senior_CSCS_Details GetSenior_CSCS_DetailsByID(int ID);
        #endregion
    }


}
