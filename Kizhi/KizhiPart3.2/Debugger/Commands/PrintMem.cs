using System.IO;
using KizhiPart3._2.Interpretator;
using KizhiPart3._2.Interpretator.Commands;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Debugger.Commands
{
    public class PrintMem: ICommand
    {
        private readonly Interpreter _interpreter;
        private readonly TextWriter _writer;
        
        public PrintMem(Interpreter interpreter, TextWriter writer)
        {
            _interpreter = interpreter;
            _writer = writer;
        }
        
        public Result<string[]> Execute(string[] args)
        {
            if (_interpreter.Context.Memory.GetAllVariables().Count == 0)
                _writer.WriteLine();
            
            foreach (var variableInfo in _interpreter.Context.Memory.GetAllVariables())
                _writer.WriteLine($"{variableInfo.Key} {variableInfo.Value.value} {variableInfo.Value.lastChange}");    
            
            return Result<string[]>.Ok(args);
        }
    }
}