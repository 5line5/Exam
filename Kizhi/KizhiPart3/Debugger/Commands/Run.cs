using System;
using Kizhi.Consts;
using Kizhi.Interpretator;
using Kizhi.Interpretator.Commands;
using Kizhi.ResultPattern;

namespace Kizhi.Debugger.Commands
{
    public class Run: ICommand
    {
        private readonly Interpreter _interpreter;
        private readonly Action _starter;
        
        public Run(Action starter, Interpreter interpreter)
        {
            _starter = starter;
            _interpreter = interpreter;
        }
        
        public Result Execute(string[] args)
        {
            if (_interpreter.State.InstructionPointer == _interpreter.State.Memory.InstructionsCount)
            {
                _interpreter.State.Memory.ClearMemory();
                _interpreter.State.InstructionPointer = -1;
            }
            
            if (_interpreter.State.InstructionPointer == -1)
                _interpreter.ChangePointer();
            _starter();

            return new Result(CommandResult.Success);
        }
    }
}