using System.Collections.Generic;

namespace KizhiPart2.Analyzer
{
    public interface ILexicalAnalyzer
    { 
        Dictionary<string, (int start, int end)> FindFunctions(string program);
        
        List<string> GetCommandList(string program);

        int FindEntryPoint(string program);
    }
}