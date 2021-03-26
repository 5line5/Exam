using System;
using System.Collections.Generic;
using System.Linq;
using KizhiPart2.Consts;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.CommandTree
{
    internal sealed class CommandTree: ITree<string>
    {
        private readonly INode<string> _root = new Node<string>(KeyWords.NotFixed, KeyWords.NotFixed);
        private readonly List<string> _rules;

        public CommandTree(List<string> rules) => _rules = rules;

        public void GenerateTree() => _rules.ForEach(rule => AddNodeInTree(_root, GenerateBranch(rule)));

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
        
        private void AddNodeInTree(INode<string> localRoot, INode<string> nodeToAdd)
        {
            if (!localRoot.ContainsChild(nodeToAdd.GetNodeName()))
                localRoot.AddChild(nodeToAdd);
            else
            {
                var currentNode = localRoot.GetChildNode(nodeToAdd.GetNodeName()).Value;

                if (nodeToAdd.IsNodeLeaf())
                {
                    localRoot.RemoveChild(currentNode.GetNodeName());
                    currentNode.GetChildren().ForEach(nodeToAdd.AddChild);
                    localRoot.AddChild(nodeToAdd);
                }
                else
                    nodeToAdd.GetChildren().ForEach(currentNode.AddChild);
            }
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