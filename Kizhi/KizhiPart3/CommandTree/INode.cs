namespace Kizhi.Interpretator.CommandTree
{
    public interface INode
    {
        public Node GetChildNode(string nodeName);

        public void AddChild(Node node);

        public bool ContainsChild(string nodeName);

        public bool IsNodeLeaf();
    }
}