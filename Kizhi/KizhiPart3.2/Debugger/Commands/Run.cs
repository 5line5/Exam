using System;
using KizhiPart3._2.Consts;
using KizhiPart3._2.Interpretator;
using KizhiPart3._2.Interpretator.Commands;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Debugger.Commands
{
    public class Run: ICommand
    {
        private readonly Action _starter;
        private readonly Interpreter _interpreter;

        public Run(Action starter, Interpreter interpreter)
        {
            _interpreter = interpreter;
            _starter = starter;
        }

        public Result<string[]> Execute(string[] args)
        {
            _interpreter.Context.SetNewState(States.Running);

            _starter();
            
            return Result<string[]>.Ok(args);
        }
    }
}