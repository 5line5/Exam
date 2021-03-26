using System.IO;
using KizhiPart3._2.Interpretator;
using KizhiPart3._2.Interpretator.Commands;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Debugger.Commands
{
    public class PrintStack: ICommand
    {
        private readonly Interpreter _interpreter;
        private readonly TextWriter _writer;

        public PrintStack(Interpreter interpreter, TextWriter writer)
        {
            _interpreter = interpreter;
            _writer = writer;
        }
        
        public Result<string[]> Execute(string[] args)
        {
            if(_interpreter.Context.StackTrace.Count == 0)
                _writer.WriteLine();
            
            foreach (var (calledFrom, functionName) in _interpreter.Context.StackTrace)
                _writer.WriteLine($"{calledFrom} {functionName}");        
            
            return Result<string[]>.Ok(args);
        }
    }
}