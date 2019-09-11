namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;

    using Scene2d.Commands;

    public class DeleteFiguresCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"\w+\s+([\w\d-_]+)\s*(#|$)");

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
            }
            else
            {
                throw new Exception("Неправильный формат ввода данных");
            }
        }

        public ICommand GetCommand()
        {
            return new DeleteFiguresCommand(this.name);
        }
    }
}
