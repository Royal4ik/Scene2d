namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Scene2d.Commands;

    public class GroupFiguresCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"\w+((\s*[\w\d-_]+\,)*\s*(?:[\w\d-_]+))\s+as\s+([\w\d-_]+)\s*(?:#|\s*$)");

        private readonly List<string> nameFigures = new List<string>();

        private string groupName;

        public bool IsCommandReady
        {
            get
            {
                return (this.groupName != null) && (this.nameFigures != null);
            }
        }        
        
        public void AppendLine(string line)
        {
            if (RecognizeRegex.IsMatch(line))
            {
                var match = RecognizeRegex.Match(line);
                var anyNameFigures = match.Groups[1].ToString().Trim().Split(' ', ',');
                for (var i = 0; i < anyNameFigures.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.nameFigures.Add(anyNameFigures[i].Trim());
                    }                   
                }

                this.groupName = match.Groups[3].ToString();                
            }
            else
            {
                throw new Exception("Неправильный формат ввода данных");
            }
        }

        public ICommand GetCommand()
        {
            return new GroupFiguresCommand(this.groupName, this.nameFigures);
        }
    }
}
