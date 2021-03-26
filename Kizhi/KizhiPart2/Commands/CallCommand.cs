using KizhiPart2.Consts;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.Commands
{
    public class CallCommand: ICommand
    {
        private const int FunctionNameIndex = 1;
        
        private readonly ExecutionContext.ExecutionContext _context;
        
        public CallCommand(ExecutionContext.ExecutionContext executionContext) => _context = executionContext;

        public Result<string[]> Execute(string[] args)
        {
            var functionName = args[FunctionNameIndex];
            var getFunctionInfoResult = _context.GetFunctionInfo(functionName);
            
            if (!getFunctionInfoResult.IsSuccess)
                return Result<string[]>.Fail(Errors.FunctionNotFound);
            
            _context.StackTrace.Push((_context.InstructionPointer, functionName));
            _context.SetNewPointer(getFunctionInfoResult.Value.start);
            
            return Result<string[]>.Ok(args);
        }
    }
}