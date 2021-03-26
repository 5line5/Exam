using KizhiPart1.ResultPattern;

namespace KizhiPart1.Commands
{
    public interface ICommand
    {
        Result<string[]> Execute(string[] args);
    }
}