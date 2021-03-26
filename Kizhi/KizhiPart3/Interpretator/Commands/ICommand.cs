using Kizhi.ResultPattern;

namespace Kizhi.Interpretator.Commands
{
    public interface ICommand
    {
        Result Execute(string[] args);
    }
}