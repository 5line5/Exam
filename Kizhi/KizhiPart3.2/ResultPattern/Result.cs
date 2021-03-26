using KizhiPart3._2.Consts;

namespace KizhiPart3._2.ResultPattern
{
    public class Result<T>
    {
        public string Error { get; }
        public T Value { get; }
        private CommandResult CommandResult { get; }
        public bool IsSuccess => CommandResult == CommandResult.Success;
        
        private Result(string error)
        {
            Error = error;
            CommandResult = CommandResult.Fail;
        }

        private Result(T value)
        {
            CommandResult = CommandResult.Success;
            Value = value;
        }

        public static Result<T> Fail(string error) => new Result<T>(error);
        
        public static Result<T> Ok(T value) => new Result<T>(value);
    }
}