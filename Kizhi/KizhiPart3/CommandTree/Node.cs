using System.Collections.Generic;
using System.Linq;

namespace Kizhi.Interpretator.CommandTree
{
    public class Node: INode
    {
        public string Command { get; }
        public string NodeName { get; }
        private readonly Dictionary<string, Node> _childNodes = new Dictionary<string, Node>();

        public Node(string command, string nodeName)
        {
            Command = command;
            NodeName = nodeName;
        }

        public Node GetChildNode(string nodeName) 
            => IsNodeLeaf() ? this : _childNodes[nodeName];
        

        public void AddChild(Node node) => _childNodes[node.NodeName] = node;

        public bool ContainsChild(string nodeName) 
            => _childNodes.ContainsKey(nodeName);
        
        public bool IsNodeLeaf() => _childNodes.Count == 0;

        public List<Node> GetAllChildes() => _childNodes.Values.ToList();
    }
}