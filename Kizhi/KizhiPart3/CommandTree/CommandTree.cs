using System;
using System.Collections.Generic;
using System.Linq;
using Kizhi.Consts;
using Kizhi.Interpretator.CommandTree;

namespace Kizhi.CommandTree
{
    internal sealed class CommandTree: ITree
    {
        private Node _root;
        private readonly List<string> _rules;

        public CommandTree(List<string> rules) => _rules = rules;

        public void GenerateTree()
        {
            _root = new Node(Rules.NotFixedVariable, Rules.NotFixedVariable);
            foreach (var rule in _rules)
            {
                var ruleParts = rule.Split(':');
                var command = ruleParts[0];    
                var tokens = ruleParts[1].Split(new []{ "=>" }, StringSplitOptions.None);
                var localRoot = new Node(command, tokens[0]);
                var currentLeaf = localRoot;

                foreach (var token in tokens.Skip(1))
                {
                    var newNode = new Node(command, token);
                    currentLeaf.AddChild(newNode);
                    currentLeaf = newNode;
                }
                
                AddNodeInTree(_root, localRoot);
            }
        }
        
        private void AddNodeInTree(Node root, Node node)
        {
            if(root.ContainsChild(node.NodeName))
                node.GetAllChildes()
                    .ForEach(element => AddNodeInTree(root.GetChildNode(node.NodeName), element));
            else
                root.AddChild(node);
        }
        
        public Node GetCommandNode(string[] args)
        {
            var currentNode = _root;
            
            foreach (var arg in args)
            {
                if (currentNode.IsNodeLeaf())
                    break;

                currentNode = currentNode
                    .GetChildNode(!currentNode.ContainsChild(arg) ? Rules.NotFixedVariable : arg);
            }

            return currentNode;
        }
    }
}