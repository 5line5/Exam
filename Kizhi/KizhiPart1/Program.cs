using System;
using System.Collections.Generic;
using KizhiPart1.Interpretator;

namespace KizhiPart1
{
    static class Program
    {
        static void Main(string[] args)
        {
            var program = new List<string>
            {
                "set a 5",
                "sub a 3",
                "print a",
                "set b 4",
                "print b"
            };
            
            var interpreter = new Interpreter(Console.Out);
            program.ForEach(interpreter.ExecuteLine);
        }
    }
}