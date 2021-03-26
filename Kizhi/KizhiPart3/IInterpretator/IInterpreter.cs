namespace Kizhi.IInterpretator
{
    public interface IInterpreter
    {
        void ExecuteLine(string command);
        
        void ExecuteProgram();
    }
}