using System.IO;
using Kizhi.Consts;
using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public sealed class PrintCommand : ICommand
    {
        private readonly TextWriter _writer;
        private readonly State.State _state;
        
        public PrintCommand(State.State state, TextWriter writer)
        {
            _state = state;
            _writer = writer;
        }

        public Result Execute(string[] args)
        {
            var variable = args[(int) Components.Variable];
            
            if (!_state.Memory.IsVariableInMemory(variable)) 
                return new Result(Errors.VariableNotFound);
            
            _writer.WriteLine(_state.Memory.GetVariableValue(variable));
            return new Result(CommandResult.Success);        }
    }
}