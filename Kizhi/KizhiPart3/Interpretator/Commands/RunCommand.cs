using System;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public sealed class RunCommand : ICommand
    {
        private Action _starter;
        private State.State _state;

        public RunCommand(Action starter, State.State state)
        {
            _starter = starter;
            _state = state;
        }
        
        public Result Execute(string[] args)
        {
            _state.ProgramState = States.Running;
            _starter();
            _state.Memory.ClearMemory();
            _state.ProgramState = States.Default;
            
            return new Result(CommandResult.Success);
        }
    }
}