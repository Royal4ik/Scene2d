namespace Scene2d.Commands
{
    using System.Collections.Generic;

    public class GroupFiguresCommand : ICommand
    {
        private readonly string groupName;

        private readonly List<string> nameFigures;

        public GroupFiguresCommand(string groupName, List<string> nameFigures)
        {
            this.groupName = groupName;
            this.nameFigures = nameFigures;
        }

        public void Apply(Scene scene)
        {
            scene.CreateCompositeFigure(this.groupName, this.nameFigures);
        }
    }
}
