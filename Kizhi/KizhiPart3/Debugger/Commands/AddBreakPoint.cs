using System.Collections.Generic;
using Kizhi.Consts;
using Kizhi.Interpretator.Commands;
using Kizhi.ResultPattern;

namespace Kizhi.Debugger.Commands
{
    public class AddBreakPoint : ICommand
    {
        private readonly List<int> _breaks;

        public AddBreakPoint(List<int> breaks) => _breaks = breaks;
        
        public Result Execute(string[] args)
        {
            var line = int.Parse(args[(int) Components.Value]);
            _breaks.Add(line);
            
            return new Result(CommandResult.Success);
        }
    }
}