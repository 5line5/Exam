using System;
using System.Collections.Generic;
using System.Linq;
using KizhiPart1.Consts;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.CommandTree
{
    internal class CommandTree: ITree<string>
    {
        private readonly INode<string> _root = new Node<string>(KeyWords.NotFixed, KeyWords.NotFixed);
        private readonly List<string> _rules;

        public CommandTree(List<string> rules) => _rules = rules;

        public void GenerateTree() => _rules.ForEach(rule => _root.AddChild(GenerateBranch(rule)));

        private INode<string> GenerateBranch(string rule)
        {
            var splitedRule = rule.Split(':');
            var command = splitedRule[0];
            var branch = splitedRule[1].Split(new[] { "=>" }, StringSplitOptions.None);
            var localRoot = new Node<string>(command, branch[0]);
            var root = localRoot;
            
            branch.Skip(1).ToList().ForEach(name =>
            {
                var newLocalRoot = new Node<string>(command, name);
                localRoot.AddChild(newLocalRoot);
                localRoot = newLocalRoot;
            });
            
            return root;
        }

        public Result<INode<string>> GetCommandNode(IEnumerable<string> instruction)
        {
            var currentNode = _root;
            var getChildResult = Result<INode<string>>.Fail(Errors.UnknownCommand);
            
            foreach (var component in instruction)
            {
                getChildResult = currentNode.GetChildNode(component);
                if (!getChildResult.IsSuccess)
                    return getChildResult;
                currentNode = getChildResult.Value;
            }

            return getChildResult;
        }
    }
}