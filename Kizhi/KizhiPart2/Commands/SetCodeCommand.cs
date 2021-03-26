using KizhiPart2.Consts;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.Commands
{
    public class SetCodeCommand: ICommand
    {
        private readonly ExecutionContext.ExecutionContext _executionContext;

        public SetCodeCommand(ExecutionContext.ExecutionContext executionContext) => _executionContext = executionContext;
        
        public Result<string[]> Execute(string[] args)
        {
            _executionContext.SetNewState(States.WaitingForInput);
            
            return Result<string[]>.Ok(args);
        }
    }
}