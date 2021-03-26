using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public sealed class SubCommand : ICommand
    {
        private readonly State.State _state;
        
        public SubCommand(State.State state) => _state = state;

        public Result Execute(string[] args)
        {
            var variable = args[(int) Components.Variable];
            var value = int.Parse(args[(int) Components.Value]);
            
            if (!_state.Memory.IsVariableInMemory(variable)) 
                return new Result(Errors.VariableNotFound);
            
            _state.Memory.SetChange(variable, _state.InstructionPointer);
            _state.Memory.AddVariable(variable, _state.Memory.GetVariableValue(variable) - value);
            
            return new Result(CommandResult.Success);
        }
    }
}