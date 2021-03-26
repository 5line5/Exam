using System.Collections.Generic;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Memory
{
    public interface IMemory
    {
        void AddVariable(string key, int value, int lastChange);
        
        Result<int> GetValue(string key);

        Result<(int value, int lastChange)> GetVariableInfo(string key);

        Result<(int value, int lastChange)> DeleteVariable(string key);

        Dictionary<string, (int value, int lastChange)> GetAllVariables();
        
        void ClearMemory();
    }
}