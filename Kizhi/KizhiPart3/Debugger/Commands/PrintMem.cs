using System.IO;
using System.Linq.Expressions;
using Kizhi.Consts;
using Kizhi.Interpretator.Commands;
using Kizhi.Interpretator.State;
using Kizhi.ResultPattern;

namespace Kizhi.Debugger.Commands
{
    public class PrintMem: ICommand
    {
        private readonly State _interpState;
        private readonly TextWriter _writer;

        public PrintMem(State state, TextWriter writer)
        {
            _interpState = state;
            _writer = writer;
        }

        public Result Execute(string[] args)
        {
            foreach (var (key, value) in _interpState.Memory.GetAllVariables())
                _writer.WriteLine($"{key} {value} {_interpState.Memory.GetLastChange(key)}");    
                
            return new Result(CommandResult.Success);
        }
    }
}