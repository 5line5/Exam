using KizhiPart3._2.Interpretator;
using KizhiPart3._2.Interpretator.Commands;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Debugger.Commands
{
    public class StepOver: ICommand
    {
        private readonly Interpreter _interpreter;
        
        public StepOver(Interpreter interpreter) => _interpreter = interpreter;
        
        public Result<string[]> Execute(string[] args)
        {
            var deep = _interpreter.Context.StackTrace.Count;
            var getInstructionResult = _interpreter.Context.GetInstruction(_interpreter.Context.InstructionPointer);

            if (getInstructionResult.IsSuccess)
            {
                _interpreter.ExecuteLine(getInstructionResult.Value);
                _interpreter.ChangePointer();
                getInstructionResult = _interpreter.Context.GetInstruction(_interpreter.Context.InstructionPointer);
            }

            while (deep < _interpreter.Context.StackTrace.Count && getInstructionResult.IsSuccess)
            {
                _interpreter.ExecuteLine(getInstructionResult.Value);
                _interpreter.ChangePointer();
                getInstructionResult =  _interpreter.Context.GetInstruction(_interpreter.Context.InstructionPointer);
            }
            
            return getInstructionResult.IsSuccess 
                ? Result<string[]>.Ok(args) 
                : Result<string[]>.Fail(getInstructionResult.Error);
        }
    }
}