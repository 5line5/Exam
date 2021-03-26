using KizhiPart1.Consts;
using KizhiPart1.Memory;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.Commands
{
    public class SetCommand : ICommand
    {
        private const int VariableIndex = 1;
        private const int ValueIndex = 2;

        private readonly IMemory _memory;
        
        public SetCommand(IMemory memory) => _memory = memory;

        public Result<string[]> Execute(string[] args)
        {    
            var variable = args[VariableIndex];
            var value = int.Parse(args[ValueIndex]);
            
            if (value < 0)
                return Result<string[]>.Fail(Errors.InvalidSetValue);
            
            _memory.AddVariable(variable, value);
            
            return Result<string[]>.Ok(args);
        }
    }
}