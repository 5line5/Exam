using System.Collections.Generic;
using System.Linq;
using KizhiPart3._2.Consts;

namespace KizhiPart3._2.Interpretator.Analyzer
{
    public class LexicalAnalyzer: ILexicalAnalyzer
    {
        public Dictionary<string, (int start, int end)> FindFunctions(string program)
        {
            var res = new Dictionary<string, (int start, int end)>();
            var commandsList = program.Split('\n');
            
            for (var line = 0; line < commandsList.Length; line++)
            {
                if (!commandsList[line].StartsWith(KeyWords.Def)) continue;
                
                var body = commandsList
                    .Skip(line + 1)
                    .TakeWhile(element => element.StartsWith(" "))
                    .ToList();
                
                var functionName = commandsList[line].Split()[1];
                res[functionName] = (line, line + body.Count);
            }
            
            return res;
        }

        public List<string> GetCommandList(string program) 
            => program
                .Split('\n')
                .Select(element => element.Trim())
                .Where(element => element != string.Empty)
                .ToList();

        public int FindEntryPoint(string program)
        {
            var instructions = program.Split('\n');

            for (var i = 0; i < instructions.Length; i++)
            {
                if(instructions[i].StartsWith(KeyWords.Def) || instructions[i].StartsWith(" "))
                    continue;

                return i;
            }

            return 0;
        }
    }
}