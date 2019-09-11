namespace Scene2d.Commands
{
    public class RotateFiguresCommand : ICommand
    {
        private readonly string name;

        private readonly double angle;

        public RotateFiguresCommand(string name, double angle)
        {
            this.name = name;
            this.angle = angle;
        }

        public void Apply(Scene scene)
        {
            scene.Rotate(this.name, this.angle);
        }
    }
}
