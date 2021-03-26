﻿using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
{
    public class SubCommand : ICommand
    {
        private const int VariableIndex = 1;
        private const int ValueIndex = 2;
        
        private readonly ExecutionContext.ExecutionContext _context;
        
        public SubCommand(ExecutionContext.ExecutionContext executionContext) => _context = executionContext;

        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var value = int.Parse(args[ValueIndex]);
            var getValueResult = _context.Memory.GetValue(variable);

            if (!getValueResult.IsSuccess)
                return Result<string[]>.Fail(Errors.VariableNotFound);
            
            if (getValueResult.Value < value)
                return Result<string[]>.Fail(Errors.InvalidSubValue);
            
            _context.Memory.AddVariable(variable, getValueResult.Value - value, _context.InstructionPointer);
            
            return Result<string[]>.Ok(args);
        }
    }
}