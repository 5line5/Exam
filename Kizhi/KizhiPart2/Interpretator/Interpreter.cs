﻿using System.Collections.Generic;
using System.IO;
using KizhiPart2.Analyzer;
using KizhiPart2.Commands;
using KizhiPart2.CommandTree;
using KizhiPart2.Consts;
 
namespace KizhiPart2.Interpretator 
{
    public class Interpreter: IInterpreter
    {
        private readonly TextWriter _writer;
        private readonly ILexicalAnalyzer _lexicalAnalyzer = new LexicalAnalyzer();
        private readonly ITree<string> _commandTree = new CommandTree.CommandTree(Rules.RulesForInterpretator);
        private readonly ExecutionContext.ExecutionContext _context = new ExecutionContext.ExecutionContext();
        private readonly Dictionary<string, ICommand> _handlers;

        public Interpreter(TextWriter writer)
        {
            _handlers = new Dictionary<string, ICommand>
            {
                { $"{KeyWords.Set} {KeyWords.Code}", new SetCodeCommand(_context) },
                { $"{KeyWords.End} {KeyWords.Set} {KeyWords.Code}", new EndCommand(_context) },
                { KeyWords.Run, new RunCommand(ExecuteProgram) },
                { KeyWords.Set, new SetCommand(_context) },
                { KeyWords.Rem, new RemCommand(_context) },
                { KeyWords.Sub, new SubCommand(_context) },
                { KeyWords.Print, new PrintCommand(_context, writer) }, 
                { KeyWords.Call, new CallCommand(_context) }
            };
            
            _commandTree.GenerateTree();
            _writer = writer;
        }

        public void ExecuteLine(string command)
        {
            if (_context.InterpreterState == States.WaitingForInput)
            {
                LoadProgram(command);
                _context.SetNewState(States.InputEnded);
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
            _context.SetNewState(States.Running);

            while (true)
            {
                var getInstructionResult = _context.GetInstruction(_context.InstructionPointer);
                
                if (!getInstructionResult.IsSuccess)
                    break;
                
                ExecuteLine(getInstructionResult.Value);
                ChangePointer();
            }
            
            _context.ClearExecutionContext();
        }

        public void ChangePointer()
        {
            var nextInstructionNumber = NextPointerLocation(_context.InstructionPointer);
                
            while (!IsRightStep(nextInstructionNumber))
                nextInstructionNumber = NextPointerLocation(_context.StackTrace.Pop().calledFrom);
                
            _context.SetNewPointer(nextInstructionNumber);
        }
        
        public void LoadProgram(string program)
        {
            _context.SetInstructions(_lexicalAnalyzer.GetCommandList(program));
            _context.SetFunctionsInfo(_lexicalAnalyzer.FindFunctions(program));
            _context.SetEntryPoint(_lexicalAnalyzer.FindEntryPoint(program));
        }
        
        private bool IsRightStep(int nextPointer)
        {
            if (_context.StackTrace.Count == 0)
                return true;
            
            var funcInfo = _context.GetFunctionInfo(_context.StackTrace.Peek().functionName).Value;
            
            return nextPointer <= funcInfo.end;
        }
        
        private int NextPointerLocation(int currentPointer)
        {
            var nextPointer = _context.InstructionsCount;
            
            for (var index = currentPointer + 1; index < _context.InstructionsCount; index++)
            {
                var command = _context.GetInstruction(index).Value;
                
                if (!command.StartsWith(KeyWords.Def))
                {
                    nextPointer = index;
                    break;
                }

                index = _context.GetFunctionInfo(command.Split()[1]).Value.end;
            }
            
            return nextPointer;
        }
    }
}