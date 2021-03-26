using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CommandLineCalculator
{
    internal class State
    {
        public long X { get; set; } = 420L;
        public int CurrentInput { get; set; } = 0;
        public int CurrentOutput { get; set; } = 0;
        public List<string> Inputs { get; } = new List<string>();
        public List<string> Outputs { get; } = new List<string>();

        public void ClearState()
        {
            Outputs.Clear();
            Inputs.Clear();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{X};");
            stringBuilder.Append($"{string.Join("|", Inputs)};");
            stringBuilder.Append($"{string.Join("|", Outputs)}");
            
            return stringBuilder.ToString();
        }
    }

    public sealed class StatefulInterpreter : Interpreter
    { 
        private readonly State _state = new State();
        private static CultureInfo Culture => CultureInfo.InvariantCulture;
        private Storage _storage;

        public override void Run(UserConsole userConsole, Storage storage)
        {
            _storage = storage; 
            RepairState();
            while (true)
            {
                var input = ReadLine(userConsole);
                
                switch (input.Trim())
                {
                    case "exit":
                        _storage.Write( new byte[0]);
                        return;
                    case "add":
                        Add(userConsole);
                        break;
                    case "median":
                        Median(userConsole);
                        break;
                    case "help":
                        Help(userConsole);
                        break;
                    case "rand":
                        Random(userConsole);
                        break;
                    default:
                        WriteLine(userConsole, "Такой команды нет, используйте help для списка команд");
                        break;
                }
                
                _state.ClearState();
            }
        }

        private void RepairState()
        {
            var history = Encoding.Default.GetString(_storage.Read());
            
            if (history == string.Empty)
                return;

            var splited = history.Split(';').Where(e => e!=string.Empty).ToList();
            _state.X = long.Parse(splited[0]);    
            
            if (splited.Count > 1)
                splited[1].Split('|').Where(e => e != string.Empty).ToList().ForEach(_state.Inputs.Add);
            
            if (splited.Count > 2)
                splited[2].Split('|').Where(e => e != string.Empty).ToList().ForEach(_state.Outputs.Add);
        }

        private void SaveState() => _storage.Write(Encoding.Default.GetBytes(_state.ToString()));
        
        private void WriteLine(UserConsole console, string toWrite)
        {
            if (_state.CurrentOutput < _state.Outputs.Count)
            {
                _state.CurrentOutput++;
                return;
            }

            console.WriteLine(toWrite);
            _state.Outputs.Add(toWrite);
            _state.CurrentOutput++;
            SaveState();
        }

        private string ReadLine(UserConsole console)
        {
            if (_state.CurrentInput < _state.Inputs.Count)
                return _state.Inputs[_state.CurrentInput++];
            

            var value = console.ReadLine();
            _state.Inputs.Add(value);
            SaveState();
            _state.CurrentInput++;

            return value;
        }

        private void Random(UserConsole console)
        {
            const int a = 16807;
            const int m = 2147483647;
    
            var count = ReadNumber(console);
            _state.CurrentOutput = _state.Outputs.Count;
            var startIndex = _state.Outputs.Count;
            
            for (var i = startIndex; i < count; i++)
            {    
                console.WriteLine(_state.X.ToString(Culture));
                _state.Outputs.Add(_state.X.ToString(Culture));
                _state.X = a * _state.X % m;
                SaveState();
            }
        }

        private void Add(UserConsole console)
        {
            var a = ReadNumber(console);
            var b = ReadNumber(console);
            WriteLine(console, (a + b).ToString(Culture));
        }
    
        private void Median(UserConsole console)
        {
            var count = ReadNumber(console);
            var numbers = new List<int>();
            for (var i = 0; i < count; i++)
                numbers.Add( ReadNumber(console));

            var result = CalculateMedian(numbers);
            WriteLine(console, result.ToString(Culture));
        }

        private double CalculateMedian(List<int> numbers)
        {
            numbers.Sort();
            var count = numbers.Count;
            if (count == 0)
                return 0;

            if (count % 2 == 1)
                return numbers[count / 2];

            return (numbers[count / 2 - 1] + numbers[count / 2]) / 2.0;
        }

        private void Help(UserConsole console)
        {
            const string exitMessage = "Чтобы выйти из режима помощи введите end";
            const string commands = "Доступные команды: add, median, rand";
            const string helpDescription = "Укажите команду, для которой хотите посмотреть помощь";
            
            WriteLine(console, helpDescription);
            WriteLine(console, commands);
            WriteLine(console, exitMessage);
            while (true)
            {
                var command = ReadLine(console);
                switch (command.Trim())
                {
                    case "end":
                        return;
                    case "add":
                        WriteLine(console, "Вычисляет сумму двух чисел");
                        WriteLine(console, exitMessage);
                        break;
                    case "median":
                        WriteLine(console, "Вычисляет медиану списка чисел");
                        WriteLine(console, exitMessage);
                        break;
                    case "rand":
                        WriteLine(console, "Генерирует список случайных чисел");
                        WriteLine(console, exitMessage);
                        break;
                    default:
                        WriteLine(console, "Такой команды нет");
                        WriteLine(console, commands);
                        WriteLine(console, exitMessage);
                        break;
                }
            }
        }

        private int ReadNumber(UserConsole console)
        {
            return int.Parse(ReadLine(console).Trim(), Culture);
        }
    }
}