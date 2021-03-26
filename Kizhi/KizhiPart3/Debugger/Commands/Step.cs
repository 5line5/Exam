using Kizhi.Consts;
using Kizhi.IInterpretator;
using Kizhi.Interpretator;
using Kizhi.Interpretator.Commands;
using Kizhi.ResultPattern;

namespace Kizhi.Debugger.Commands
{
    public class Step: ICommand
    {
        private readonly Interpreter _interpreter;

        public Step(Interpreter interpreter) => _interpreter = interpreter;
        
        public Result Execute(string[] args)
        {
            _interpreter.ExecuteLine(_interpreter.State.Memory.GetInstructionByIndex(_interpreter.State.InstructionPointer));
            _interpreter.ChangePointer();
            
            if (_interpreter.State.InstructionPointer == _interpreter.State.Memory.InstructionsCount)
                _interpreter.State.Memory.ClearMemory();        
            
            return new Result(CommandResult.Success);
        }
    }
}