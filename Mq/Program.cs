using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.IO;

enum WinError { ERROR_PATH_NOT_FOUND = 3 };

namespace Mq
{
    internal class Mq
    {
        static void Create(string q) { MessageQueue.Create(q); }
        static void Delete(string q) { MessageQueue.Delete(q); }
        static bool Exists(string q) { return MessageQueue.Exists(q); }
        static void GetPrivateQueues(string machine)
        {
            MessageQueue[] qs = MessageQueue.GetPrivateQueuesByMachine(machine);
            for (long i = 0; i < qs.LongLength; ++i)
            {
                //PrintHead(Console.Out);
                //PrintQ(qs[i]);
                qs[i].Dispose();
            }
        }

        private static void PrintQ(MessageQueue q,TextWriter txt)
        {
            //txt.WriteLine(String.Format("", );
        }
        static void Main(string[] args)
        {
            string command = args[0];
            switch (command)
            {
                case "Create":
                    Create(args[1]);
                    break;
                case "Delete":
                    Delete(args[1]);
                    break;
                case "GetPrivateQueues":
                    GetPrivateQueues(args[1]);
                    break;
                case "Construct":
                    new MessageQueue(args[1]);
                    break;
                case "Exists":
                    if (Exists(args[1]))
                    {
                        Console.Out.WriteLine(string.Format("Exsists {0}", args[1]));
                    }
                    else
                    {
                        Console.Out.WriteLine(string.Format("Not_Exsists {0}", args[1]));
                        Environment.Exit((int)WinError.ERROR_PATH_NOT_FOUND);
                    }
                    break;
            }

        }
    }
}
