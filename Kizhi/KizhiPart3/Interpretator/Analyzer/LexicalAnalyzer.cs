using System;
using System.Collections.Generic;
using System.Linq;

namespace Kizhi.Interpretator.Analyzer
{
    public class LexicalAnalyzer: ILexicalAnalyzer
    {
        public Dictionary<string, (int start, int end)> FindFunctions(string program)
        {
            var res = new Dictionary<string, (int start, int end)>();
            var commandsList = program.Split('\n');
            
            for (var line = 0; line < commandsList.Length; line++)
            {
                if (!commandsList[line].StartsWith("def")) continue;
                
                var body = commandsList
                    .Skip(line + 1)
                    .TakeWhile(element => element.StartsWith(" "))
                    .ToList();
                var functionName = commandsList[line].Split()[1];
                res[functionName] = (line + 1, line + body.Count);
            }
            
            return res;
        }

        public List<string> GetCommandList(string program) 
            => program
                .Split(new[] { '\n' }, StringSplitOptions.None)
                .Select(element => element.Trim())
                .Where(element => element != string.Empty)
                .ToList();
    }
}