using KizhiPart1.Consts;
using KizhiPart1.Memory;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.Commands
{
    public class SubCommand : ICommand
    {
        private const int VariableIndex = 1;
        private const int ValueIndex = 2;
        
        private readonly IMemory _memory;
        
        public SubCommand(IMemory memory) => _memory = memory;

        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var value = int.Parse(args[ValueIndex]);
            var getValueResult = _memory.GetValue(variable);

            if (!getValueResult.IsSuccess)
                return Result<string[]>.Fail(Errors.VariableNotFound);
            
            if (getValueResult.Value < value)
                return Result<string[]>.Fail(Errors.InvalidSubValue);
            
            _memory.AddVariable(variable, getValueResult.Value - value);
            
            return Result<string[]>.Ok(args);
        }
    }
}