using System.Collections.Generic;

namespace KizhiPart3._2.Interpretator.Analyzer
{
    public interface ILexicalAnalyzer
    { 
        Dictionary<string, (int start, int end)> FindFunctions(string program);
        
        List<string> GetCommandList(string program);

        int FindEntryPoint(string program);
    }
}