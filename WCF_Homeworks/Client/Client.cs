using ContractLibrary;
using System;
using System.ServiceModel;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";

            Uri address = new Uri("http://localhost:4000/IContract");

            BasicHttpBinding binding = new BasicHttpBinding();
            
            EndpointAddress endpointAddress = new EndpointAddress(address);
            
            ChannelFactory<IProcessContract> factory = new ChannelFactory<IProcessContract>(binding, endpointAddress);
            
            IProcessContract channel = factory.CreateChannel();
            GiveChoiseToUser(channel);

            Console.ReadLine();
        }

        private static void GiveChoiseToUser(IProcessContract channel)
        {
            while (true)
            {
                ShowMenu();
                string str = Console.ReadLine();
                try
                {
                    switch (str)
                    {
                        case "1":
                            {
                                Console.WriteLine(channel.GetAllProcess()); 
                                break;
                            }
                        case "2":
                            {
                                Console.WriteLine("Введите PID:");
                                int pid = -1;
                                if (int.TryParse(Console.ReadLine(), out pid))
                                    Console.WriteLine(channel.GetProcessByPID(pid));   
                                else                                
                                    Console.WriteLine("Введены некорректные символы");                                
                                break;
                            }
                        case "3":
                            {
                                Console.WriteLine(channel.StartProcess(GetProcessName()));
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine(channel.CloseProcess(GetProcessName()));
                                break;
                            }
                        case "5":
                            {
                                Console.WriteLine(channel.ShowThreadsInfo(GetProcessName()));
                                break;
                            }
                        case "6":
                            {
                                Console.WriteLine(channel.ShowModulesInfo(GetProcessName()));
                                break;
                            }
                        case "7": { Console.WriteLine("Press any key to exit.."); return; }
                        default:
                            Console.WriteLine("bad choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n1.Список всех процессов");
            Console.WriteLine("2.Выбрать процесс по PID(вывести инфо)");
            Console.WriteLine("3.Запустить процесс");
            Console.WriteLine("4.Остановить процесс");
            Console.WriteLine("5.Показать информацию о потоках");
            Console.WriteLine("6.Показать информацию о модулях");
            Console.WriteLine("7.Выход");
        }

        private static string GetProcessName()
        {
            Console.WriteLine("Введите название процесса");
            string nameProc = Console.ReadLine();
            return nameProc;
        }
    }
}
