using System.Collections.Generic;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.CommandTree
{
    public interface ITree<T>
    {
        public void GenerateTree();
        
        Result<INode<T>> GetCommandNode(IEnumerable<string> instruction);
    }
}