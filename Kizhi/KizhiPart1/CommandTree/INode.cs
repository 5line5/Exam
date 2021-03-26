using KizhiPart1.ResultPattern;

namespace KizhiPart1.CommandTree
{
    public interface INode<T>
    {
        Result<INode<T>> GetChildNode(string nodeName);

        void AddChild(INode<T> node);
        
        T GetValue();

        string GetNodeName();
    }
}