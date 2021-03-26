using System.IO;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
{
    public class PrintCommand : ICommand
    {
        private const int VariableIndex = 1;
        
        private readonly TextWriter _writer;
        private readonly ExecutionContext.ExecutionContext _context;
        
        public PrintCommand(ExecutionContext.ExecutionContext context, TextWriter writer)
        {
            _context = context;
            _writer = writer;
        }

        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var getValueResult = _context.Memory.GetValue(variable);
            
            if (!getValueResult.IsSuccess) 
                return Result<string[]>.Fail(getValueResult.Error);
            
            _writer.WriteLine(getValueResult.Value);
            
            return Result<string[]>.Ok(args);
        }
    }
}