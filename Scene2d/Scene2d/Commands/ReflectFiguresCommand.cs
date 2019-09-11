namespace Scene2d.Commands
{
    public class ReflectFiguresCommand : ICommand
    {
        private readonly string name;

        private readonly bool isUpright;

        public ReflectFiguresCommand(string name, bool isUpright)
        {
            this.isUpright = isUpright;
            this.name = name;
        }

        public void Apply(Scene scene)
        {
            scene.Reflect(this.name, this.isUpright);
        }
    }
}
