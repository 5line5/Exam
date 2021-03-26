using System.Collections.Generic;
using System.Linq;
using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.CommandTree
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

        public void AddChild(INode<T> node) => _childNodes.Add(node.GetNodeName(), node);
        
        public bool ContainsChild(string nodeName) => _childNodes.ContainsKey(nodeName);
        
        public List<INode<T>> GetChildren() => _childNodes.Values.ToList();

        public bool IsNodeLeaf() => _childNodes.Count == 0;

        public T GetValue() => _value;

        public string GetNodeName() => _nodeName;

        public int GetDeepness() => IsNodeLeaf() ? 1 : _childNodes.Values.ToList().Max(elem => elem.GetDeepness() + 1);

        public void RemoveChild(string nodeName) => _childNodes.Remove(nodeName);
    }
}