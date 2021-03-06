using System;

namespace CommandLineCalculator
{
    public static class Program
    {
        public static void Main()
        {
            var console = new TextUserConsole(Console.In, Console.Out);
            var storage = new FileStorage("db");
            var interpreter = new StatelessInterpreter();
            var stateFull = new StatefulInterpreter();
            interpreter.Run(console, storage);
        }
    }
}