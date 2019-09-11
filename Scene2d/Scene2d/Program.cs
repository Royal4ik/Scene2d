namespace Scene2d
{
    using System;
    using System.IO;
    using System.Text;

    using Scene2d.CommandBuilders;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            var commandProducer = new CommandProducer();
            var scene = new Scene();
            using (var source = new StreamReader("../../Data/commands.txt", Encoding.UTF8))
            {
                string input;
                var numberLine = 0;
                while ((input = source.ReadLine()) != null)
                {
                    if (input.Trim().Length == 0)
                    {
                        break;                       
                    }

                    numberLine++;

                    try
                    {
                        if (input.Trim()[0] == '#')
                        {
                            continue;
                        }

                        commandProducer.AppendLine(input);

                        if (commandProducer.IsCommandReady)
                        {
                            var command = commandProducer.GetCommand();
                            command.Apply(scene);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка" + "в строке - " + numberLine + ": " + ex.Message);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
