using System;
using System.Messaging;
using System.IO;
using System.Reflection;

// https://docs.microsoft.com/ja-jp/windows/win32/debug/system-error-codes#system-error-codes
enum WinError { ERROR_PATH_NOT_FOUND = 3, ERROR_BAD_ARGUMENTS = 160 };

namespace Mq
{
    internal static class Mq
    {
        static string Name => Assembly.GetAssembly(typeof(Mq)).GetName().Name;
        static void Create(string q) { MessageQueue.Create(q); }
        static void Delete(string q) { MessageQueue.Delete(q); }
        static bool Exists(string q) { return MessageQueue.Exists(q); }
        static MessageQueue Construct(string q) { return new MessageQueue(q); }

        static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine(string.Format("Usage: {0} [Exists|Construct|Create|Delete] {{qName}}", Name));
                return (int)WinError.ERROR_BAD_ARGUMENTS;
            }
            string command = args[0];
            string qName = args[1];
            switch (command)
            {
                case "Create":
                    Create(qName);
                    return 0;
                case "Delete":
                    Delete(qName);
                    return 0;
                case "Construct":
                    Construct(qName);
                    return 0;
                case "Exists":
                    if (Exists(qName))
                    {
                        Console.Out.WriteLine(string.Format("Exsists {0}", qName));
                        return 0;
                    }
                    else
                    {
                        Console.Out.WriteLine(string.Format("Not_Exsists {0}", qName));
                        return (int)WinError.ERROR_PATH_NOT_FOUND;
                    }
                default:
                    Console.Error.WriteLine(string.Format("{0} invalid command.", command));
                    return (int)WinError.ERROR_BAD_ARGUMENTS;
            }
        }
    }
}
