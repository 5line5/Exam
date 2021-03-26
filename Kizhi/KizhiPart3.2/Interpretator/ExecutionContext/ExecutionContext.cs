using System.Collections.Generic;
using KizhiPart3._2.Consts;
using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.ExecutionContext
{
    public class ExecutionContext
    {
        private List<string> _instructions;
        private Dictionary<string, (int start, int end)> _functionsInfo;
        
        public States InterpreterState { get; private set; } = States.Default;

        public Stack<(int calledFrom, string functionName)> StackTrace { get; } 
            = new Stack<(int calledFrom, string functionName)>();

        public Memory.Memory Memory { get; } = new Memory.Memory();

        public int InstructionPointer { get; private set; } = -1;

        private int _entryPoint;

        public int InstructionsCount => _instructions.Count;
        
        public Result<string> GetInstruction(int instructionNumber) 
            => instructionNumber >= InstructionsCount ? Result<string>.Fail(Errors.EndOfTheFile) : 
                                                        Result<string>.Ok(_instructions[instructionNumber]);

        public Result<(int start, int end)> GetFunctionInfo(string functionName)
            => _functionsInfo.TryGetValue(functionName, out var info)
                ? Result<(int start, int end)>.Ok(info)
                : Result<(int start, int end)>.Fail(Errors.FunctionNotFound);

        public void SetInstructions(List<string> instructions) => _instructions = instructions;
        
        public void SetFunctionsInfo(Dictionary<string, (int start, int end)> functionsInfo) 
            => _functionsInfo = functionsInfo;

        public void SetNewPointer(int newPointer) => InstructionPointer = newPointer;
        
        public void SetNewState(States newState) => InterpreterState = newState;

        public void SetEntryPoint(int entryPoint)
        {
            _entryPoint = entryPoint;
            InstructionPointer = entryPoint;
        }
        
        public void ClearExecutionContext()
        {
            InstructionPointer = _entryPoint;
            InterpreterState = States.Default;
            StackTrace.Clear();
            Memory.ClearMemory();
        }
    }
}