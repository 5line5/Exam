using System.Collections.Generic;
using System.IO;
using KizhiPart3._2.CommandTree;
using KizhiPart3._2.Consts;
using KizhiPart3._2.Debugger.Commands;
using KizhiPart3._2.Intepreter;
using KizhiPart3._2.Interpretator;
using KizhiPart3._2.Interpretator.Commands;

namespace KizhiPart3._2.Debugger
{
    public class Debugger: IInterpreter
    {
        private readonly ITree<string> _commandTree = new KizhiPart3._2.CommandTree.CommandTree(Rules.RulesForDebugger);
        private readonly Dictionary<string, ICommand> _handlers;
        private readonly Interpreter _interpreter;
        private readonly List<int> _breakPoints = new List<int>();

        public Debugger(TextWriter writer) 
        {
            _interpreter = new Interpreter(writer);
            _commandTree.GenerateTree();

            _handlers = new Dictionary<string, ICommand>
            {
                {$"{KeyWords.Add} {KeyWords.Break}", new AddBreakPoint(_breakPoints)},
                {$"{KeyWords.Print} {KeyWords.Mem}", new PrintMem(_interpreter, writer)},
                {$"{KeyWords.Print} {KeyWords.Trace}", new PrintStack(_interpreter, writer)},
                {$"{KeyWords.Step}", new Step(_interpreter)},
                {$"{KeyWords.Step} {KeyWords.Over}", new StepOver(_interpreter)},
                {$"{KeyWords.Run}", new Run(ExecuteProgram, _interpreter)},
            };
        }

        public void ExecuteLine(string command)
        {
            var tokens = command.Split();
            var getCommandResult = _commandTree.GetCommandNode(tokens);

            if (!getCommandResult.IsSuccess)
            {
                _interpreter.ExecuteLine(command);
                return;
            }

            var executionResult = _handlers[getCommandResult.Value.GetValue()].Execute(tokens);

            if (!executionResult.IsSuccess)
                _interpreter.Context.ClearExecutionContext();
        }

        public void ExecuteProgram()
        {
            do
            {
                var getInstructionResult = _interpreter.Context.GetInstruction(_interpreter.Context.InstructionPointer);

                if (!getInstructionResult.IsSuccess)
                {
                    _interpreter.Context.ClearExecutionContext();
                    break;
                }

                _interpreter.ExecuteLine(getInstructionResult.Value);
                ChangePointer();
            } while (!_breakPoints.Contains(_interpreter.Context.InstructionPointer));
        }

        public void ChangePointer() => _interpreter.ChangePointer();

        public void LoadProgram(string program) => _interpreter.LoadProgram(program);
    }
}