using System.Collections.Generic;
using KizhiPart2.ResultPattern;

namespace KizhiPart2.Memory
{
    public interface IMemory
    {
        void AddVariable(string key, int value);

        Result<int> GetValue(string key);

        public Result<int> DeleteVariable(string key);

        void ClearMemory();
    }
}