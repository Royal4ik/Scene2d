namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class AddCircleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"\w+\s\w+\s+([\w\d-_]+)\s+\((-?\d+)\,\s+(-?\d+)\)\sradius\s(-?\d+|-?\d+\.\d+)\s*(#|\s*$)");

        private string name;

        private CircleFigure circle;

        public bool IsCommandReady
        {
            get
            {
                return (this.circle != null) && (this.name != null);
            }
        }

        public void AppendLine(string line)
        {
            if (RecognizeRegex.IsMatch(line))
            {
                var match = RecognizeRegex.Match(line);
                var x1 = Convert.ToDouble(match.Groups[2].Value);
                var y1 = Convert.ToDouble(match.Groups[3].Value);
                var radius = Convert.ToDouble(match.Groups[4].Value);
                if (radius > 0)
                {
                    var centerP = new Point { X = x1, Y = y1 };
                    this.circle = new CircleFigure(centerP, radius);
                    this.name = match.Groups[1].Value;
                }
                else
                {
                    throw new Exception("Радиус окружности меньше либо равен нуля");
                }
            }
            else
            {
                throw new Exception("Неправильный формат ввода данных");
            }
        }

        public ICommand GetCommand()
        {
            return new AddFigureCommand(this.name, this.circle);
        }
    }
}
