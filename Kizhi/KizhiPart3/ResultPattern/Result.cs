using Kizhi.Consts;

namespace Kizhi.ResultPattern
{
    public class Result
    {
        public Result(string error)
        {
            Error = error;
            OperationResult = CommandResult.Fail;
        }

        public Result(CommandResult operationResult) => OperationResult = operationResult;

        public string Error { get; }
        private CommandResult OperationResult { get; }
        public bool IsSuccess => OperationResult == CommandResult.Success;
    }
}