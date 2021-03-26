using KizhiPart1.ResultPattern;

namespace KizhiPart1.Memory
{
    public interface IMemory
    {
        void AddVariable(string key, int value);

        Result<int> GetValue(string key);

        Result<int> DeleteVariable(string key);
    }
}