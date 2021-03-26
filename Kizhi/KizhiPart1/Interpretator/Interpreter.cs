using System.Collections.Generic;
using System.IO;
using KizhiPart1.Commands;
using KizhiPart1.CommandTree;

namespace KizhiPart1.Interpretator
{
    public class Interpreter: IInterpreter
    {
        private readonly TextWriter _writer;
        private readonly Memory.Memory _memory = new Memory.Memory();
        private Dictionary<string, ICommand> _handlers;
        private ITree<string> _commandTree = new CommandTree.CommandTree(Consts.Rules.RulesForInterpretator);

        public Interpreter(TextWriter writer)
        {
            _writer = writer;
            _commandTree.GenerateTree();
            
            _handlers = new Dictionary<string, ICommand> 
            {
                {Consts.KeyWords.Set, new SetCommand(_memory)},
                {Consts.KeyWords.Sub, new SubCommand(_memory)},
                {Consts.KeyWords.Print, new PrintCommand(_memory, writer)},
                {Consts.KeyWords.Rem, new RemCommand(_memory)},
            };
        }

        public void ExecuteLine(string command)
        {
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
    }
}