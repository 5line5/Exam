using KizhiPart2.Consts;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.Commands
{
    public class EndCommand : ICommand
    {
        private readonly ExecutionContext.ExecutionContext _executionContext;

        public EndCommand(ExecutionContext.ExecutionContext executionContext) => _executionContext = executionContext;

        public Result<string[]> Execute(string[] args)
        {
            _executionContext.SetNewState(States.WaitingForRunning);
            
            return Result<string[]>.Ok(args);
        }
    }    
}