using System.Collections.Generic;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.CommandTree
{
    public interface ITree<T>
    {
        void GenerateTree();
        
        Result<INode<T>> GetCommandNode(IEnumerable<string> instruction);
    }
}