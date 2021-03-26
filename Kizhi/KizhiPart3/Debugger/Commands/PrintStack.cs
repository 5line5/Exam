using System.IO;
using Kizhi.Consts;
using Kizhi.Interpretator.Commands;
using Kizhi.Interpretator.State;
using Kizhi.ResultPattern;

namespace Kizhi.Debugger.Commands
{
    public class PrintStack: ICommand
    {
        private readonly State _interpState;
        private readonly TextWriter _writer;

        public PrintStack(State state, TextWriter writer)
        {
            _interpState = state;
            _writer = writer;
        }
        
        public Result Execute(string[] args)
        {
            foreach (var (calledFrom, functionName) in _interpState.StackTrace)
                _writer.WriteLine($"{calledFrom} {functionName}");        
            
            return new Result(CommandResult.Success);
        }
    }
}