namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;

    using Scene2d.Commands;

    public class ReflectFiguresCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"\w+\s+(vertically|horizontally)\s+([\w\d-_]+)\s*(#|$)");

        private bool isUpright;

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
                this.name = match.Groups[2].ToString();
                this.isUpright = match.Groups[1].ToString() != "vertically";
            }
            else
            {
                throw new Exception("Неправильный формат ввода данных");
            }
        }

        public ICommand GetCommand()
        {
            return new ReflectFiguresCommand(this.name, this.isUpright);
        }
    }
}
