namespace Scene2d.Figures
{
    using System.Collections.Generic;

    public interface ICompositeFigure : IFigure
    {
        Dictionary<string, IFigure> ChildFigures { get; }
    }
}
