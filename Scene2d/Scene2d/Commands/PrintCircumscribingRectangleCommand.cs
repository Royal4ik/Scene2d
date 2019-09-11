namespace Scene2d.Commands
{
    public class PrintCircumscribingRectangleCommand : ICommand
    {
        private readonly string name;

        public PrintCircumscribingRectangleCommand(string name)
        {
            this.name = name;
        }

        public void Apply(Scene scene)
        {
            scene.CalculateCircumscribingRectangle(this.name);
        }
    }
}
