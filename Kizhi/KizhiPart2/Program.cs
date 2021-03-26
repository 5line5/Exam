using System;
using System.Collections.Generic;
using System.Diagnostics;
using KizhiPart2.Interpretator;

namespace KizhiPart2
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new List<string>
            {
                "set code",
                "def test\n" +
                "    print b\n" +
                "    call test\n" +
                "call test\r\n",
                "end set code",
                "run"
            };
            
            var interpreter = new Interpreter(Console.Out);
            program.ForEach(interpreter.ExecuteLine);        
        }
    }
}