using System.Collections.Generic;
using System.IO;
using Kizhi.Consts;
using Kizhi.Debugger.Commands;
using Kizhi.IInterpretator;
using Kizhi.Interpretator;
using Kizhi.Interpretator.Commands;
using Kizhi.Interpretator.CommandTree;

namespace Kizhi.Debugger
{
    public class Debugger: IInterpreter
    {
        private readonly TextWriter _writer;
        private readonly ITree _tree = new Kizhi.CommandTree.CommandTree(Rules.RulesForInterpretator);
        private readonly List<int> _breakPoints = new List<int>();
        private readonly Dictionary<string, ICommand> _handlers;
        private readonly Interpreter _interpreter;

        public Debugger(TextWriter writer)
        {
            _interpreter = new Interpreter(writer);

            _handlers = new Dictionary<string, ICommand>
            {
                { Consts.Commands.AddBreak, new AddBreakPoint(_breakPoints) },
                { Consts.Commands.PrintMem, new PrintMem(_interpreter.State, writer) },
                { Consts.Commands.PrintTrace, new PrintStack(_interpreter.State, writer) },
                { Consts.Commands.Step, new Step(_interpreter) },
                { Consts.Commands.StepOver, new StepOver(_interpreter) },
                { Consts.Commands.Run, new Run(ExecuteProgram, _interpreter) }
            };
            
            _tree.GenerateTree();
            _writer = writer;
        }

        public void ExecuteLine(string command)
        {
            if (_interpreter.State.ProgramState == States.WaitingForInput)
            {
                _interpreter.ExecuteLine(command);
                return;
            }
            
            var args = command.Split();
            var node = _tree.GetCommandNode(args);
            if (!_handlers.ContainsKey(node.Command))
            {
                _interpreter.ExecuteLine(command);
                return;
            }

            var result = _handlers[node.Command].Execute(args);
            
            if (!result.IsSuccess)
                _writer.WriteLine(result.Error);
        }

        public void ExecuteProgram()
        {
            
            while (_interpreter.State.InstructionPointer < _interpreter.State.Memory.InstructionsCount 
                   && (!_breakPoints.Contains(_interpreter.State.InstructionPointer) 
                       || _interpreter.State.ProgramState == States.Stoped))
            { 
                _interpreter.State.ProgramState = States.Running;
                ExecuteLine(_interpreter.State.Memory.GetInstructionByIndex(_interpreter.State.InstructionPointer)); 
                _interpreter.ChangePointer();
            }

            if (_breakPoints.Contains(_interpreter.State.InstructionPointer))
                _interpreter.State.ProgramState = States.Stoped;
        }
    }
}