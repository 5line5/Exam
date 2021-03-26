using System;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
{
    public class RunCommand : ICommand
    {
        private readonly Action _starter;

        public RunCommand(Action starter) => _starter = starter;

        public Result<string[]> Execute(string[] args)
        {
            _starter();
            
            return Result<string[]>.Ok(args);
        }
    }
}