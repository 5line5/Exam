using System;
using System.Collections.Generic;
using KizhiPart1.Consts;
using KizhiPart1.ResultPattern;

namespace KizhiPart1.Memory
{
    public class Memory: IMemory
    {
        private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();

        private bool IsVariableInMemory(string key) => _variables.ContainsKey(key);

        public void AddVariable(string key, int value) => _variables[key] = value;

        public Result<int> GetValue(string key) 
            => IsVariableInMemory(key) ? Result<int>.Ok(_variables[key]) : Result<int>.Fail(Errors.VariableNotFound);

        public Result<int> DeleteVariable(string key)
        {
            if (!IsVariableInMemory(key))
                return Result<int>.Fail(Errors.VariableNotFound);

            var removedValue = _variables[key];
            _variables.Remove(key);

            return Result<int>.Ok(removedValue);
        }
    }
}