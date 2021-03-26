using System.Collections.Generic;
using System.IO;
using KizhiPart3._2.CommandTree;
using KizhiPart3._2.Consts;
using KizhiPart3._2.Intepreter;
using KizhiPart3._2.Interpretator.Analyzer;
using KizhiPart3._2.Interpretator.Commands;

namespace KizhiPart3._2.Interpretator 
{
    public class Interpreter: IInterpreter
    {
        private readonly TextWriter _writer;
        private readonly ILexicalAnalyzer _lexicalAnalyzer = new LexicalAnalyzer();
        private readonly ITree<string> _commandTree = new CommandTree.CommandTree(Rules.RulesForInterpreter);
        public readonly ExecutionContext.ExecutionContext Context = new ExecutionContext.ExecutionContext();
        private readonly Dictionary<string, ICommand> _handlers;

        public Interpreter(TextWriter writer)
        {
            _handlers = new Dictionary<string, ICommand>
            {
                { $"{KeyWords.Set} {KeyWords.Code}", new SetCodeCommand(Context) },
                { $"{KeyWords.End} {KeyWords.Set} {KeyWords.Code}", new EndCommand(Context) },
                { KeyWords.Run, new RunCommand(ExecuteProgram) },
                { KeyWords.Set, new SetCommand(Context) },
                { KeyWords.Rem, new RemCommand(Context) },
                { KeyWords.Sub, new SubCommand(Context) },
                { KeyWords.Print, new PrintCommand(Context, writer) }, 
                { KeyWords.Call, new CallCommand(Context) }
            };
            
            _commandTree.GenerateTree();
            _writer = writer;
        }

        public void ExecuteLine(string command)
        {
            if (Context.InterpreterState == States.WaitingForInput)
            {
                LoadProgram(command);
                Context.SetNewState(States.InputEnded);
                return;
            }
        
            var splitedCommand = command.Split();
            var getCommandResult = _commandTree.GetCommandNode(splitedCommand);
            
            if (!getCommandResult.IsSuccess)
            {
                _writer.WriteLine(getCommandResult.Error);
                return;
            }
            
            var operationResult = _handlers[getCommandResult.Value.GetValue()].Execute(splitedCommand);
            if (!operationResult.IsSuccess)
                _writer.WriteLine(operationResult.Error);
        } 
        
        public void ExecuteProgram()
        {
            Context.SetNewState(States.Running);

            while (true)
            {
                var getInstructionResult = Context.GetInstruction(Context.InstructionPointer);
                
                if (!getInstructionResult.IsSuccess)
                    break;
                
                ExecuteLine(getInstructionResult.Value);
                ChangePointer();
            }
            
            Context.ClearExecutionContext();
        }

        public void ChangePointer()
        {
            var nextInstructionNumber = NextPointerLocation(Context.InstructionPointer);
                
            while (!IsRightStep(nextInstructionNumber))
                nextInstructionNumber = NextPointerLocation(Context.StackTrace.Pop().calledFrom);
                    
            Context.SetNewPointer(nextInstructionNumber);
        }
        
        public void LoadProgram(string program)
        {
            Context.SetInstructions(_lexicalAnalyzer.GetCommandList(program));
            Context.SetFunctionsInfo(_lexicalAnalyzer.FindFunctions(program));
            Context.SetEntryPoint(_lexicalAnalyzer.FindEntryPoint(program));
        }
        
        private bool IsRightStep(int nextPointer)
        {
            if (Context.StackTrace.Count == 0)
                return true;
            
            var funcInfo = Context.GetFunctionInfo(Context.StackTrace.Peek().functionName).Value;
            
            return nextPointer <= funcInfo.end;
        }
        
        private int NextPointerLocation(int currentPointer)
        {
            var nextPointer = Context.InstructionsCount;
            
            for (var index = currentPointer + 1; index < Context.InstructionsCount; index++)
            {
                var command = Context.GetInstruction(index).Value;
                
                if (!command.StartsWith(KeyWords.Def))
                {
                    nextPointer = index;
                    break;
                }

                index = Context.GetFunctionInfo(command.Split()[1]).Value.end;
            }
            
            return nextPointer;
        }
    }
}