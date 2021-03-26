using System;

namespace Refactoring
{
    public class Program
    {
        public static void Main()
        {
            var rm = new RemoteController();
            while (true)
            {
                var command = Console.ReadLine();
                Console.WriteLine(rm.Call(command));
            }
        }
    }
}