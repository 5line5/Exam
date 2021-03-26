using System.Collections.Generic;
using KizhiPart3._2.Interpretator.Commands;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Debugger.Commands
{
    public class AddBreakPoint: ICommand
    {
        private const int BreakPointLine = 2;
        
        private readonly List<int> _breakPoints;

        public AddBreakPoint(List<int> breakPoints) => _breakPoints = breakPoints;
        
        public Result<string[]> Execute(string[] args)
        {
            _breakPoints.Add(int.Parse(args[BreakPointLine]));
            
            return Result<string[]>.Ok(args);
        }
    }
}