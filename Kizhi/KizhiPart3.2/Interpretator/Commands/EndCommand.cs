using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
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