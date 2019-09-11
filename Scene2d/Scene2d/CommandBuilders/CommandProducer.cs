namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Scene2d.Commands;

    public class CommandProducer : ICommandBuilder
    {
        private static readonly Dictionary<Regex, Func<ICommandBuilder>> Commands =
            new Dictionary<Regex, Func<ICommandBuilder>>
            {
                { new Regex("^add rectangle .*"), () => new AddRectangleCommandBuilder() },
                { new Regex("^add circle .*"), () => new AddCircleCommandBuilder() },
                { new Regex("^group .*"), () => new GroupFiguresCommandBuilder() },
                { new Regex("^delete .*"), () => new DeleteFiguresCommandBuilder() },
                { new Regex("^copy .*"), () => new CopyFiguresCommandBuilder() },
                { new Regex("^move .*"), () => new MoveFiguresCommandBuilder() },
                { new Regex("^rotate .*"), () => new RotateFiguresCommandBuilder() },
                { new Regex("^reflect .*"), () => new ReflectFiguresCommandBuilder() },
                { new Regex("^print area for .*"), () => new PrintAreaFiguresCommandBuilder() },
                {
                    new Regex("^print circumscribing rectangle for .*"),
                    () => new PrintCircumscribingRectangleCommandBuilder()
                },
                { new Regex(@"^(add polygon|add point|end polygon)(.*|$)"), () => new AddPolygonCommandBuilder() }
            };

        private ICommandBuilder currentBuilder;

        public bool IsCommandReady
        {
            get
            {
                return this.currentBuilder != null && this.currentBuilder.IsCommandReady;
            }
        }

        public void AppendLine(string line)
        {
            var isException = true;
            var pair = Commands.SingleOrDefault(pair1 => pair1.Key.IsMatch(line));
            if (this.currentBuilder == null)
            {
                if (pair.Key != null)
                {
                    isException = false;
                    this.currentBuilder = pair.Value();
                }
            }
            else
            {
                if ((pair.Key == null) || (pair.Value().GetType() != this.currentBuilder.GetType()))
                {
                    throw new Exception("Отсутствует end polygon");
                }

                isException = false;
            }

            if (isException)
            {
                throw new Exception("Неправильный формат ввода данных");
            }

            this.currentBuilder.AppendLine(line);
        }

        public ICommand GetCommand()
        {
            if (this.currentBuilder == null)
            {
                return null;
            }

            var command = this.currentBuilder.GetCommand();
            this.currentBuilder = null;

            return command;
        }
    }
}
