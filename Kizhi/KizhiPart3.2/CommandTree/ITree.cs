using System.Collections.Generic;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.CommandTree
{
    public interface ITree<T>
    {
        public void GenerateTree();
        
        Result<INode<T>> GetCommandNode(IEnumerable<string> instruction);
    }
}