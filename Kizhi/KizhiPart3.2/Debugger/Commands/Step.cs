using KizhiPart3._2.Interpretator;
using KizhiPart3._2.Interpretator.Commands;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Debugger.Commands
{
    public class Step: ICommand
    {
        private readonly Interpreter _interpreter;

        public Step(Interpreter interpreter) => _interpreter = interpreter;
        
        public Result<string[]> Execute(string[] args)
        {
            var getInstructionResult = _interpreter.Context.GetInstruction(_interpreter.Context.InstructionPointer);

            if (!getInstructionResult.IsSuccess)
                return Result<string[]>.Fail(getInstructionResult.Error);
            
            _interpreter.ExecuteLine(getInstructionResult.Value);
            _interpreter.ChangePointer();

            return Result<string[]>.Ok(args);
        }
    }
}