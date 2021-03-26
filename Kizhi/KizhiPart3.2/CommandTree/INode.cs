using System.Collections.Generic;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.CommandTree
{
    public interface INode<T>
    {
        Result<INode<T>> GetChildNode(string nodeName);

        public void AddChild(INode<T> node);

        public bool ContainsChild(string nodeName);

        public List<INode<T>> GetChildren();
        
        public void RemoveChild(string nodeName);

        public bool IsNodeLeaf();

        public T GetValue();

        public string GetNodeName();

        public int GetDeepness();
    }
}