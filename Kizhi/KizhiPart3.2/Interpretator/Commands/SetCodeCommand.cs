using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
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