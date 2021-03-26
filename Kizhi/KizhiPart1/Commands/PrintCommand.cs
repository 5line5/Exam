using System.IO;
using KizhiPart1.Consts;
using KizhiPart1.Memory;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.Commands
{
    public class PrintCommand : ICommand
    {
        private const int VariableIndex = 1;
        
        private readonly TextWriter _writer;
        private readonly IMemory _memory;
        
        public PrintCommand(IMemory memory, TextWriter writer)
        {
            _memory = memory;
            _writer = writer;
        }

        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var getValueResult = _memory.GetValue(variable);
            
            if (!getValueResult.IsSuccess) 
                return Result<string[]>.Fail(getValueResult.Error);
            
            _writer.WriteLine(getValueResult.Value);
            
            return Result<string[]>.Ok(args);
        }
    }
}