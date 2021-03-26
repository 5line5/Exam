using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public sealed class RemCommand : ICommand
    {
        private readonly State.State _state;
        
        public RemCommand(State.State state) => _state = state;
        
        public Result Execute(string[] args)
        {
            var variable = args[(int) Components.Variable];
            
            if (!_state.Memory.IsVariableInMemory(variable)) 
                return new Result(Errors.VariableNotFound);
            
            _state.Memory.DeleteVariable(variable);
            return new Result(CommandResult.Success);
        }
    }
}