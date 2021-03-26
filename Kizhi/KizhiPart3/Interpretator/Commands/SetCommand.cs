using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public sealed class SetCommand : ICommand
    {
        private readonly State.State _state;
        
        public SetCommand(State.State state) => _state = state;
        
        public Result Execute(string[] args)
        {
            var variable = args[(int) Components.Variable];
            var value = Convert.ToInt32(args[(int) Components.Value]);
            _state.Memory.SetChange(variable, _state.InstructionPointer);
            _state.Memory.AddVariable(variable, value);
            
            return new Result(CommandResult.Success);
        }
    }
}