using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public sealed class EndCommand : ICommand
    {
        private State.State _state;

        public EndCommand(State.State state) => _state = state;

        public Result Execute(string[] args)
        {
            _state.ProgramState = States.WaitingForRunning;
            
            return new Result(CommandResult.Success);
        }
    }    
}