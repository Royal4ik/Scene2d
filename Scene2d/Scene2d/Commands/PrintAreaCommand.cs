namespace Scene2d.Commands
{
    public class PrintAreaCommand : ICommand
    {
        private readonly string name;

        public PrintAreaCommand(string name)
        {
            this.name = name;
        }

        public void Apply(Scene scene)
        {
            scene.CalulateArea(this.name);
        }
    }
}
