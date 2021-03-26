using System;
using System.Collections.Generic;
using Kizhi.Interpretator;

namespace Kizhi
{
    static class Program
    {
        static void Main(string[] args)
        {
            var program = new List<string>
            {
                "set code",
                "def test\n" +
                "    rem b\n" +
                "set b 12\n" +
                "call test\n" +
                "print b\r\n",
                "end set code",
                "add break 2",
                "run",
                "print mem",
                "step",
                "print mem",
                "step over",
                "print mem",
                "run"
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
            prog2.ForEach(debugger.ExecuteLine);
        }
    }
}