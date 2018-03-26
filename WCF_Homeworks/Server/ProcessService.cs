using ContractLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;

namespace Server
{
    public class ProcessService : IProcessContract
    {
        private const string notRunningProcess = "Данный процесс не запущен";

        public string ShowModulesInfo(string nameProc)
        {
            var response = new StringBuilder();
            if (Process.GetProcessesByName(nameProc).Any())
            {
                var proc = Process.GetProcessesByName(nameProc)[0];
                var modules = proc.Modules;

                foreach (ProcessModule module in modules)
                {
                    response.AppendLine(string.Format("Name: {0}  MemorySize: {1}", module.ModuleName, module.ModuleMemorySize));
                }
                return response.ToString();
            }
            return notRunningProcess;
        }

        public string ShowThreadsInfo(string nameProc)
        {
            if (Process.GetProcessesByName(nameProc).Any())
            {
                var proc = Process.GetProcessesByName(nameProc)[0];
                ProcessThreadCollection threads = proc.Threads;
                return string.Format("Количество потоков для данного процесса = {0}", threads.Count);
            }
            return notRunningProcess;
        }

        public string CloseProcess(string nameProc)
        {
            if (Process.GetProcessesByName(nameProc).Any())
            {
                var proc = Process.GetProcessesByName(nameProc)[0];
                proc.CloseMainWindow();
                proc.Close();
                return string.Format("Процесс с именем {0} остановлен", nameProc);
            }
            return notRunningProcess;
        }

        public string StartProcess(string nameProc)
        {
            string error = string.Empty;
            if (Process.GetProcessesByName(nameProc).Any())
            {
                try
                {
                    Process.Start(nameProc);
                }
                catch (Exception ex)
                {
                    error += ex.Message;
                }
            }
            if (string.IsNullOrEmpty(error))
            {
                return string.Format("Процесс с именем {0} запущен", nameProc);
            }
            else
                return string.Format("Не удалось запустить процесс по причине - {0}", error);
        }

        public string GetProcessByPID(int pid)
        {
            string error = string.Empty;
            string processName = string.Empty;
            try
            {
                var process = Process.GetProcessById(pid);
                processName = process.ProcessName;
            }
            catch (Exception)
            {
                error += "Процесса с таким PID нет в системе";
            }
            if (string.IsNullOrEmpty(error))
            {
                return string.Format("Имя процесса: {0}", processName);
            }
            else
                return "Процесса с таким PID нет в системе";
        }

        public string GetAllProcess()
        {
            var response = new StringBuilder();
            var process = Process.GetProcesses(".").Select(proc => proc).OrderBy(proc => proc.Id);
            int i = 0;
            foreach (var proc in process)
            {
                i++;
                string info = string.Format(@"{0}: PID: {1} Name: {2}", i, proc.Id, proc.ProcessName);
                response.AppendLine(info);
            }
            return response.ToString();
        }
    }
}
