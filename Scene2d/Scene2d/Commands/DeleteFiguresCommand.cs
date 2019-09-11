namespace Scene2d.Commands
{
    public class DeleteFiguresCommand : ICommand
    {
        private readonly string name;

        public DeleteFiguresCommand(string name)
        {
            this.name = name;
        }

        public void Apply(Scene scene)
        {
            scene.DeleteFigure(this.name);
        }
    }
}
