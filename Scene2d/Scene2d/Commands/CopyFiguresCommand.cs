
namespace Scene2d.Commands
{
    public class CopyFiguresCommand : ICommand
    {
        private readonly string name;
        private readonly string copyName;

        public CopyFiguresCommand(string name, string copyName)
        {
            this.name = name;
            this.copyName = copyName;
        }

        public void Apply(Scene scene)
        {
            scene.Copy(this.name, this.copyName);
        }
    }
}
