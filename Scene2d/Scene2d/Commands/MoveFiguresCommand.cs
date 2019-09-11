namespace Scene2d.Commands
{
    public class MoveFiguresCommand : ICommand
    {
        private readonly string name;

        private readonly Point vector;

        public MoveFiguresCommand(string name, Point vector)
        {
            this.name = name;
            this.vector = vector;
        }

        public void Apply(Scene scene)
        {
            scene.Move(this.name, this.vector);
        }
    }
}
