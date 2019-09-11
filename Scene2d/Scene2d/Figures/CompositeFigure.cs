namespace Scene2d.Figures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompositeFigure : ICompositeFigure
    {
        public CompositeFigure(Dictionary<string, IFigure> childFigures)
        {
            this.ChildFigures = childFigures;
        }

        public Dictionary<string, IFigure> ChildFigures { get; }


        public object Clone()
        {
            return new CompositeFigure(this.ChildFigures.ToDictionary(figures => figures.Key + "_copy", figures => (IFigure)figures.Value.Clone()));
        }

        public double CalulateArea()
        {
            return this.ChildFigures.Values.Sum(figures => figures.CalulateArea());
        }

        public Rectangle CalculateCircumscribingRectangle()
        {
            var minx = double.MaxValue;
            var miny = double.MaxValue;
            var maxy = double.MinValue;
            var maxx = double.MinValue;
            foreach (var figures in this.ChildFigures.Values)
            {
                maxx = Math.Max(
                        Math.Max(figures.CalculateCircumscribingRectangle().Vertex1.X, maxx),
                        figures.CalculateCircumscribingRectangle().Vertex2.X);
                minx = Math.Min(
                        Math.Min(figures.CalculateCircumscribingRectangle().Vertex1.X, minx), 
                        figures.CalculateCircumscribingRectangle().Vertex2.X);
                maxy = Math.Max(
                        Math.Max(figures.CalculateCircumscribingRectangle().Vertex1.Y, maxy),
                        figures.CalculateCircumscribingRectangle().Vertex2.Y);
                miny = Math.Min(
                        Math.Min(figures.CalculateCircumscribingRectangle().Vertex1.Y, miny),
                        figures.CalculateCircumscribingRectangle().Vertex2.Y);
            }

            var minPoint = new Point { X = minx, Y = maxy };
            var maxPoint = new Point { X = maxx, Y = miny };
            return new Rectangle { Vertex1 = minPoint, Vertex2 = maxPoint };
        }

        public void Move(Point vector)
        {
            foreach (var figures in this.ChildFigures.Values)
            {
                figures.Move(vector);
            }         
        }

        public void Rotate(double angle)
        {
            foreach (var figures in this.ChildFigures.Values)
            {
                figures.Rotate(angle);
            }
        }

        public void Reflect(bool isUpright)
        {
            foreach (var figures in this.ChildFigures.Values)
            {
                figures.Reflect(isUpright);
            }
        }
    }
}
