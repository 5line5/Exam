using System.Collections.Generic;

namespace Kizhi.Interpretator.Memory
{
    public class Memory
    {
        private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _lastChange = new Dictionary<string, int>();
        private List<string> _instructions = new List<string>();
        private Dictionary<string, (int start, int end)> _functionsIndexes = new Dictionary<string, (int start, int end)>();
        public int InstructionsCount => _instructions.Count;

        public bool IsVariableInMemory(string variable) => _variables.ContainsKey(variable);

        public void AddVariable(string variable, int value) => _variables[variable] = value;

        public int GetVariableValue(string variable) => _variables[variable];

        public void DeleteVariable(string variable) => _variables.Remove(variable);

        public bool IsFunctionInMemory(string functionName) => _functionsIndexes.ContainsKey(functionName);
        
        public (int start, int end) GetFunctionIndexesByName(string functionName) 
            => _functionsIndexes[functionName];
        
        public void SetFunctions(Dictionary<string, (int start, int end)> functions) 
            => _functionsIndexes = functions;

        public void SetInstructions(List<string> instructions) => _instructions = instructions;

        public string GetInstructionByIndex(int index) => _instructions[index];

        public Dictionary<string, int> GetAllVariables() => _variables;

        public void SetChange(string variable, int lastChange) => _lastChange[variable] = lastChange;

        public int GetLastChange(string variable) => _lastChange[variable];
        
        public void ClearMemory()
        {
            _variables.Clear();
            _lastChange.Clear();
        }
    }
}