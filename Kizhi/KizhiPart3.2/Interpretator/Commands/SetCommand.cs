using System;
using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
{
    public class SetCommand : ICommand
    {
        private const int VariableIndex = 1;
        private const int ValueIndex = 2;
        
        private readonly ExecutionContext.ExecutionContext _context;
        
        public SetCommand(ExecutionContext.ExecutionContext executionContext) => _context = executionContext;
        
        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var value = Convert.ToInt32(args[ValueIndex]);
            
            if (value < 0)
                return Result<string[]>.Fail(Errors.InvalidSetValue);
            
            _context.Memory.AddVariable(variable, value, _context.InstructionPointer);
            
            return Result<string[]>.Ok(args);
        }
    }
}