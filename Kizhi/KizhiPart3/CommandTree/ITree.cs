namespace Kizhi.Interpretator.CommandTree
{
    public interface ITree
    {
        public void GenerateTree();
        
        public Node GetCommandNode(string[] args);
    }
}