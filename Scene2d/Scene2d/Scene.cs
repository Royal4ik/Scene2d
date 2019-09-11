namespace Scene2d
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Scene2d.Figures;

    public class Scene
    {
        private readonly Dictionary<string, IFigure> figures =
            new Dictionary<string, IFigure>();

        private readonly Dictionary<string, ICompositeFigure> compositeFigures =
            new Dictionary<string, ICompositeFigure>();

        public void AddFigure(string name, IFigure figure)
        {
            if (!this.compositeFigures.ContainsKey(name) && !this.figures.ContainsKey(name))
            {
                this.figures.Add(name, figure);
            }
            else
            {
                throw new Exception("Данное имя уже существует");
            }
        }

        public void CreateCompositeFigure(string name, IEnumerable<string> childFigures)
        {
            if (!this.compositeFigures.ContainsKey(name) && !this.figures.ContainsKey(name))
            {
                var figureDictionary = new Dictionary<string, IFigure>();
                foreach (var figureName in childFigures)
                {
                    if (this.figures.ContainsKey(figureName))
                    {
                        figureDictionary.Add(figureName, this.figures[figureName]);
                    }
                    else
                    {
                        throw new Exception($"Данного имени {figureName} не существует в рамках сцены");
                    }
                }

                this.compositeFigures.Add(name, new CompositeFigure(figureDictionary));
                }
            else
            {
                throw new Exception("Данное имя композиции уже существует");
            }
        }

        public void DeleteFigure(string name)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                foreach (var childFigures in this.compositeFigures[name].ChildFigures)
                {
                    this.figures.Remove(childFigures.Key);
                }
                
                this.compositeFigures.Remove(name);
            }
            else if (this.figures.ContainsKey(name))
            {
                this.figures.Remove(name);
            }
            else if (name == "scene")
            {
                this.figures.Clear();
                this.compositeFigures.Clear();
            }
            else
            {
                throw new Exception("Данного имени не существует");
            }
        }

        public double CalulateArea(string name)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                Console.WriteLine(this.compositeFigures[name].CalulateArea());
                return this.compositeFigures[name].CalulateArea();
            }

            if (this.figures.ContainsKey(name))
            {
                Console.WriteLine(this.figures[name].CalulateArea());
                return this.figures[name].CalulateArea();
            }

            if (name == "scene")
            {
                Console.WriteLine(this.figures.Values.Sum(figure => figure.CalulateArea()));
                return this.figures.Values.Sum(figure => figure.CalulateArea());
            }

            Console.WriteLine();

            throw new Exception("Данного имени не существует");
        }

        public Rectangle CalculateCircumscribingRectangle(string name)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                Console.WriteLine("({0}, {1}) ({2}, {3})", this.compositeFigures[name].CalculateCircumscribingRectangle().Vertex1.X, this.compositeFigures[name].CalculateCircumscribingRectangle().Vertex1.Y,
                    this.compositeFigures[name].CalculateCircumscribingRectangle().Vertex2.X, this.compositeFigures[name].CalculateCircumscribingRectangle().Vertex2.Y);
                return this.compositeFigures[name].CalculateCircumscribingRectangle();
            }

            if (this.figures.ContainsKey(name))
            {
                Console.WriteLine("({0}, {1}) ({2}, {3})", this.figures[name].CalculateCircumscribingRectangle().Vertex1.X, this.figures[name].CalculateCircumscribingRectangle().Vertex1.Y,
                    this.figures[name].CalculateCircumscribingRectangle().Vertex2.X, this.figures[name].CalculateCircumscribingRectangle().Vertex2.Y);
                return this.figures[name].CalculateCircumscribingRectangle();
            }

            if (name != "scene")
            {

                throw new Exception("Данного имени не существует");
            }

            var minx = double.MaxValue;
            var miny = double.MaxValue;
            var maxy = double.MinValue;
            var maxx = double.MinValue;
            foreach (var figure in this.figures.Values)
            {
                maxx = Math.Max(
                    Math.Max(figure.CalculateCircumscribingRectangle().Vertex1.X, maxx),
                    figure.CalculateCircumscribingRectangle().Vertex2.X);
                minx = Math.Min(
                    Math.Min(figure.CalculateCircumscribingRectangle().Vertex1.X, minx),
                    figure.CalculateCircumscribingRectangle().Vertex2.X);
                maxy = Math.Max(
                    Math.Max(figure.CalculateCircumscribingRectangle().Vertex1.Y, maxy),
                    figure.CalculateCircumscribingRectangle().Vertex2.Y);
                miny = Math.Min(
                    Math.Min(figure.CalculateCircumscribingRectangle().Vertex1.Y, miny),
                    figure.CalculateCircumscribingRectangle().Vertex2.Y);
            }

            var minPoint = new Point { X = minx, Y = maxy };
            var maxPoint = new Point { X = maxx, Y = miny };
            Console.WriteLine("({0}, {1}) ({2}, {3})", minPoint.X, minPoint.Y, maxPoint.X, maxPoint.Y);
            return new Rectangle { Vertex1 = minPoint, Vertex2 = maxPoint };
        }

        public void Move(string name, Point vector)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                this.compositeFigures[name].Move(vector);
            }
            else if (this.figures.ContainsKey(name))
            {
                this.figures[name].Move(vector);
            }
            else if (name == "scene")
            {
                foreach (var figure in this.figures.Values)
                {
                    figure.Move(vector);
                }
            }
            else
            {
                throw new Exception("Данного имени не существует");
            }
        }

        public void Rotate(string name, double angle)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                this.compositeFigures[name].Rotate(angle);
            }
            else if (this.figures.ContainsKey(name))
            {
                this.figures[name].Rotate(angle);
            }
            else if (name == "scene")
            {
                foreach (var figure in this.figures.Values)
                {
                    figure.Rotate(angle);
                }
            }
            else
            {
                throw new Exception("Данного имени не существует");
            }
        }

        public void Reflect(string name, bool isUpright)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                this.compositeFigures[name].Reflect(isUpright);
            }
            else if (this.figures.ContainsKey(name))
            {
                this.figures[name].Reflect(isUpright);
            }
            else if (name == "scene")
            {
                foreach (var figure in this.figures.Values)
                {
                    figure.Reflect(isUpright);
                }
            }
            else
            {
                throw new Exception("Данного имени не существует");
            }
        }

        public void Copy(string name, string copyName)
        {
            if (this.compositeFigures.ContainsKey(name))
            {
                var copyFiguresClone = (ICompositeFigure)this.compositeFigures[name].Clone();
                this.compositeFigures.Add(copyName, copyFiguresClone);
                foreach (var childFigures in copyFiguresClone.ChildFigures)
                {
                    this.figures.Add(childFigures.Key, childFigures.Value );
                }        
            }

            else if (this.figures.ContainsKey(name))
            {
                this.figures.Add(copyName, (IFigure)this.figures[name].Clone());
            }

            else if (name == "scene")
            {
                foreach (var figure in this.figures.Keys)
                {
                    this.figures.Add(figure + "_copy", (IFigure)this.figures[figure].Clone());
                }
            }

            else
            {
                throw new Exception("Данного имени не существует");
            }
        }
    }
}
