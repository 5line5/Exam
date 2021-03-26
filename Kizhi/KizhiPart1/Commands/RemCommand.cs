using KizhiPart1.Consts;
using KizhiPart1.Memory;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.Commands
{
    public class RemCommand : ICommand
    {
        private const int VariableIndex = 1;

        private readonly IMemory _memory;
        public RemCommand(IMemory memory) => _memory = memory;
        
        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var deleteVariableResult = _memory.DeleteVariable(variable);
            
            return !deleteVariableResult.IsSuccess ? Result<string[]>.Fail(deleteVariableResult.Error) : Result<string[]>.Ok(args);
        }
    }
}