using System.Collections.Generic;
using Kizhi.Consts;

namespace Kizhi.Interpretator.State
{
    public class State
    {
        public int InstructionPointer { get; set; } = -1;
        public Memory.Memory Memory { get; } = new Memory.Memory();
        public States ProgramState { get; set; } = States.Default;
        public Stack<(int calledFrom, string functionName)> StackTrace { get; } 
            = new Stack<(int calledFrom, string functionName)>();
    }
}