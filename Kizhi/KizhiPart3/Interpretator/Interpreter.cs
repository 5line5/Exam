using System.Collections.Generic;
using System.IO;
using Kizhi.Consts;
using Kizhi.IInterpretator;
using Kizhi.Interpretator.Analyzer;
using Kizhi.Interpretator.Commands;
using Kizhi.Interpretator.CommandTree;

namespace Kizhi.Interpretator
{
    public class Interpreter: IInterpreter
    {
        private readonly TextWriter _writer;
        private readonly ILexicalAnalyzer _lexicalAnalyzer = new LexicalAnalyzer();
        private readonly ITree _tree = new Kizhi.CommandTree.CommandTree(Rules.RulesForInterpretator);
        public State.State State { get; } = new State.State();
        private readonly Dictionary<string, ICommand> _handlers;
        
        public Interpreter(TextWriter writer)
        {
            _handlers = new Dictionary<string, ICommand>
            {
                { Consts.Commands.SetCode, new SetCodeCommand(State) },
                { Consts.Commands.EndSetCode, new EndCommand(State) },
                { Consts.Commands.Run, new RunCommand(ExecuteProgram, State) },
                { Consts.Commands.Set, new SetCommand(State) },
                { Consts.Commands.Rem, new RemCommand(State) },
                { Consts.Commands.Sub, new SubCommand(State) },
                { Consts.Commands.Print, new PrintCommand(State, writer) }, 
                { Consts.Commands.Call, new CallCommand(State) }
            };
            
            _tree.GenerateTree();
            _writer = writer;
        }

        public void ExecuteLine(string command)
        {
            if (State.ProgramState == States.WaitingForInput)
            {
                LoadProgram(command);
                State.ProgramState = States.InputEnded;
                return;
            }
            
            var args = command.Split();
            var node = _tree.GetCommandNode(args);
            var result = _handlers[node.Command].Execute(args);
            
            if(!result.IsSuccess)
                _writer.WriteLine(result.Error);
        } 
        
        public void ExecuteProgram()
        {
            ChangePointer();
            while (State.InstructionPointer < State.Memory.InstructionsCount)
            {
                ExecuteLine(State.Memory.GetInstructionByIndex(State.InstructionPointer));
                ChangePointer();
            }
        }
        
        private void LoadProgram(string program)
        {
            State.Memory.SetInstructions(_lexicalAnalyzer.GetCommandList(program));
            State.Memory.SetFunctions(_lexicalAnalyzer.FindFunctions(program));
        }
        
        private bool IsRightStep(int nextPointer)
        {
            if (State.StackTrace.Count == 0) 
                return true;
            var funcInfo = State.Memory.GetFunctionIndexesByName(State.StackTrace.Peek().functionName);
            return nextPointer <= funcInfo.end;
        }

        public void ChangePointer()
        {
            var nextInstructionNumber = NextPointerLocation(State.InstructionPointer);
                
            while (!IsRightStep(nextInstructionNumber))
                nextInstructionNumber = NextPointerLocation(State.StackTrace.Pop().calledFrom);
                
            State.InstructionPointer = nextInstructionNumber;
        }
        
        private int NextPointerLocation(int currentPointer)
        {
            var nextPointer = State.Memory.InstructionsCount;
            
            for (var index = currentPointer + 1; index < State.Memory.InstructionsCount; index++)
            {
                var command = State.Memory.GetInstructionByIndex(index);
                
                if (!command.StartsWith("def"))
                {
                    nextPointer = index;
                    break;
                }

                index = State.Memory.GetFunctionIndexesByName(command.Split()[1]).end;
            }
            
            return nextPointer;
        }
    }
}