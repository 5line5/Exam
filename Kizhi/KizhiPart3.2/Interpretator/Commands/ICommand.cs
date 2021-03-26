using KizhiPart3._2.ResultPattern;

namespace KizhiPart3._2.Interpretator.Commands
{
    public interface ICommand
    {
        Result<string[]> Execute(string[] args);
    }
}