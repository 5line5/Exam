using KizhiPart2.ResultPattern;

namespace KizhiPart2.Commands
{
    public interface ICommand
    {
        Result<string[]> Execute(string[] args);
    }
}