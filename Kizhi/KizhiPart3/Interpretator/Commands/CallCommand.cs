using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;


namespace Kizhi.Interpretator.Commands
{
    public sealed class CallCommand: ICommand
    {
        private readonly State.State _state;
        public CallCommand(State.State state) => _state = state;

        public Result Execute(string[] args)
        {
            var functionName = args[(int) Components.Variable];
            
            if (!_state.Memory.IsFunctionInMemory(functionName))
                return new Result(Errors.FunctionNotFound);
            
            _state.StackTrace.Push((_state.InstructionPointer, functionName)); 
            var start = _state.Memory.GetFunctionIndexesByName(functionName).start; 
            _state.InstructionPointer = start - 1; 
            
            return new Result(CommandResult.Success);
        }
    }
}