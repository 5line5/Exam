using System.Collections.Generic;

namespace Kizhi.Interpretator.Analyzer
{
    public interface ILexicalAnalyzer
    { 
        Dictionary<string, (int start, int end)> FindFunctions(string program);
        
        List<string> GetCommandList(string program);
    }
}