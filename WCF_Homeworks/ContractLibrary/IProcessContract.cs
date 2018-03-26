using System.ServiceModel;

namespace ContractLibrary
{
    [ServiceContract]
    public interface IProcessContract
    {
        [OperationContract]
        string ShowModulesInfo(string nameProc);

        [OperationContract]
        string ShowThreadsInfo(string nameProc);

        [OperationContract]
        string CloseProcess(string nameProc);

        [OperationContract]
        string StartProcess(string nameProc);

        [OperationContract]
        string GetProcessByPID(int pid);

        [OperationContract]
        string GetAllProcess();
    }
}
