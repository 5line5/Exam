namespace KizhiPart3._2.Intepreter
{
    public interface IInterpreter
    {
        void ExecuteLine(string command);
        
        void ExecuteProgram();

        void ChangePointer();

        void LoadProgram(string program);
    }
}