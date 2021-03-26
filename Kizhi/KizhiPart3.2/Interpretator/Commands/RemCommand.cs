﻿using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
{
    public class RemCommand : ICommand
    {
        private const int VariableIndex = 1;

        private readonly ExecutionContext.ExecutionContext _context;
        
        public RemCommand(ExecutionContext.ExecutionContext executionContext) => _context = executionContext;
        
        public Result<string[]> Execute(string[] args)
        {
            var variable = args[VariableIndex];
            var deleteVariableResult = _context.Memory.DeleteVariable(variable);
            
            return !deleteVariableResult.IsSuccess ? Result<string[]>.Fail(deleteVariableResult.Error) : Result<string[]>.Ok(args);
        }
    }
}