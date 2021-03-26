using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public class SetCodeCommand: ICommand
    {
        private State.State _state;

        public SetCodeCommand(State.State state) => _state = state;
        
        public Result Execute(string[] args)
        {
            _state.ProgramState = States.WaitingForInput;
            
            return new Result(CommandResult.Success);
        }
    }
}