using System;
using KizhiPart2.Consts;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.Commands
{
    public class SetCommand : ICommand
    {
        private const int VariableIndex = 1;
        private const int ValueIndex = 2;
        
        private readonly ExecutionContext.ExecutionContext _executionContext;
        
        public SetCommand(ExecutionContext.ExecutionContext executionContext) => _executionContext = executionContext;
        
        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var value = Convert.ToInt32(args[ValueIndex]);
            
            if (value < 0)
                return Result<string[]>.Fail(Errors.InvalidSetValue);
            
            _executionContext.Memory.AddVariable(variable, value);
            
            return Result<string[]>.Ok(args);
        }
    }
}