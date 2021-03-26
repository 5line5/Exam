using System.Collections.Generic;
using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Memory
{
    public class Memory: IMemory
    {
        private readonly Dictionary<string, (int value, int lastChange)> _variables 
            = new Dictionary<string, (int value, int lastChange)>();

        private bool IsVariableInMemory(string key) => _variables.ContainsKey(key);

        public void AddVariable(string key, int value, int lastChange) => _variables[key] = (value, lastChange);
        
        public Result<int> GetValue(string key)
            => IsVariableInMemory(key) 
                ? Result<int>.Ok(_variables[key].value) 
                : Result<int>.Fail(Errors.VariableNotFound);

        public Result<(int value, int lastChange)> GetVariableInfo(string key)
            => IsVariableInMemory(key) 
                ? Result<(int value, int lastChange)>.Ok(_variables[key]) 
                : Result<(int value, int lastChange)>.Fail(Errors.VariableNotFound);

        public Result<(int value, int lastChange)> DeleteVariable(string key)
        {
            if (!IsVariableInMemory(key))
                return Result<(int value, int lastChange)>.Fail(Errors.VariableNotFound);

            var removedValue = _variables[key];
            _variables.Remove(key);

            return Result<(int value, int lastChange)>.Ok(removedValue);
        }

        public Dictionary<string, (int value, int lastChange)> GetAllVariables() => _variables;

        public void ClearMemory() => _variables.Clear();
    }
}