using System;
using System.Collections.Generic;
using KizhiPart3._2.Interpretator;

namespace KizhiPart3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new List<string>
            {
                "set code",
                "set b 12\n" +
                "def test\n" +
                "    rem b\n" +
                "call test\n" +
                "print b\n",
                "end set code",
                "add break 0",
                "run",
                "run",
            };
            
            var prog2 = new List<string>
            {
                "set code",
                
                "def test\n" +
                "    set a 4\n" +
                "set t 5\n" +
                "call test\n" +
                "sub a 3\n" +
                "call test\n" +
                "print a\r\n",
                
                "end set code",
                "add break 1",
                "run",
                "print trace",
                "run",
                "run"
            };

            var interpreter = new Interpreter(Console.Out);
            var debugger = new Debugger.Debugger(Console.Out); 
            program.ForEach(debugger.ExecuteLine);
        }
    }
}