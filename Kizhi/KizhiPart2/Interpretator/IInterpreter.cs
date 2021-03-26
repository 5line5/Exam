namespace KizhiPart2.Interpretator
{
    public interface IInterpreter
    {
        void ExecuteLine(string command);
        
        void ExecuteProgram();

        void ChangePointer();

        void LoadProgram(string program);
    }
}