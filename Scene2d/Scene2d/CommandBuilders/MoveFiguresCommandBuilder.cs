namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;

    using Scene2d.Commands;

    public class MoveFiguresCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"\w+\s+([\w\d-_]+)\s+\((-?\d+)\,\s+(-?\d+)\)\s*(#|$)");

        private Point vector;

        private string name;

        public bool IsCommandReady
        {
            get
            {
                return this.name != null;
            }
        }

        public void AppendLine(string line)
        {
            if (RecognizeRegex.IsMatch(line))
            {
                var match = RecognizeRegex.Match(line);
                this.name = match.Groups[1].ToString();
                this.vector.X = Convert.ToDouble(match.Groups[2].Value); 
                this.vector.Y = Convert.ToDouble(match.Groups[3].Value);
            }
            else
            {
                throw new Exception("Неправильный формат ввода данных");
            }
        }

        public ICommand GetCommand()
        {
            return new MoveFiguresCommand(this.name, this.vector);
        }
    }
}

