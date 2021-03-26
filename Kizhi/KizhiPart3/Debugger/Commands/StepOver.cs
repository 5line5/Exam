using Kizhi.Consts;
using Kizhi.IInterpretator;
using Kizhi.Interpretator;
using Kizhi.Interpretator.Commands;
using Kizhi.ResultPattern;

namespace Kizhi.Debugger.Commands
{
    public class StepOver: ICommand
    {
        private readonly Interpreter _interpreter;

        public StepOver(Interpreter interpreter) => _interpreter = interpreter;
        
        public Result Execute(string[] args)
        {
            var deep = _interpreter.State.StackTrace.Count;
            MakeStep();
            while (deep < _interpreter.State.StackTrace.Count)
                MakeStep();
            
            return new Result(CommandResult.Success);
        }
        
        private void MakeStep()
        {
            _interpreter.ExecuteLine(_interpreter.State.Memory.GetInstructionByIndex(_interpreter.State.InstructionPointer));
            _interpreter.ChangePointer();
            
            if (_interpreter.State.InstructionPointer == _interpreter.State.Memory.InstructionsCount)
                _interpreter.State.Memory.ClearMemory();
        }
    }
}