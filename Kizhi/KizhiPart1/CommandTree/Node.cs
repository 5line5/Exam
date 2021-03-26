using System;
using System.Collections.Generic;
using KizhiPart1.Consts;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.CommandTree
{
    public class Node<T>: INode<T>
    {
        private readonly T _value;
        private readonly string _nodeName;
        private readonly Dictionary<string, INode<T>> _childNodes = new Dictionary<string, INode<T>>();

        public Node(T value, string name)
        {
            _value = value;
            _nodeName = name;
        }

        public Result<INode<T>> GetChildNode(string nodeName)
        {
            if (ContainsChild(nodeName))
                return Result<INode<T>>.Ok(_childNodes[nodeName]); 
            
            if(ContainsChild(KeyWords.NotFixed))
                return Result<INode<T>>.Ok(_childNodes[KeyWords.NotFixed]); 
            
            return Result<INode<T>>.Fail(Errors.UnknownCommand);
        }

        public void AddChild(INode<T> node) => _childNodes[node.GetNodeName()] = node;
        
        private bool ContainsChild(string nodeName) => _childNodes.ContainsKey(nodeName);
        
        public T GetValue() => _value;

        public string GetNodeName() => _nodeName;
    }
}