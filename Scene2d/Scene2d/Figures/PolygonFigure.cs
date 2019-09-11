namespace Scene2d.Figures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PolygonFigure : IFigure
    {
        private readonly List<Point> polygon;


        public PolygonFigure(List<Point> polygon)
        {
            this.polygon = polygon;
        }

        public object Clone()
        {
            return new PolygonFigure(this.polygon.ToList());
        }

        public double CalulateArea()
        {
            var area = 0.0;

            for (var i = 1; i < this.polygon.Count; i++)
            {
                area += this.polygon[i - 1].X * this.polygon[i].Y - this.polygon[i - 1].Y * this.polygon[i].X;
            }

            area += this.polygon[this.polygon.Count - 1].X * this.polygon[0].Y
                    - this.polygon[this.polygon.Count - 1].Y * this.polygon[0].X;

            return Math.Abs(area / 2);
        }

        public Rectangle CalculateCircumscribingRectangle()
        {
            var minMax = FindExtremePoint(this.polygon);

            return new Rectangle { Vertex1 = minMax[0], Vertex2 = minMax[1] };
        }

        public void Move(Point vector)
        {
            for (var i = 0; i < this.polygon.Count; i++)
            {
                this.polygon[i] += vector;
            }
        }
        
        public void Rotate(double angle)
        {
            var angleInRadians = angle / 180 * Math.PI;
            var centerX = 0.0;
            var centerY = 0.0;
            foreach (var point in this.polygon)
            {
                centerX += point.X;
                centerY += point.Y;
            }

            var center = new Point(centerX / this.polygon.Count, centerY / this.polygon.Count);
            for (var i = 0; i < this.polygon.Count; i++)
            {
                var pointRelativeToCenter = this.polygon[i] - center;
                var newPolygonPoint = new Point
                                          {
                                              X =
                                                  pointRelativeToCenter.X * Math.Cos(angleInRadians)
                                                  - pointRelativeToCenter.Y * Math.Sin(angleInRadians),
                                              Y =
                                                  pointRelativeToCenter.X * Math.Sin(angleInRadians)
                                                  + pointRelativeToCenter.Y * Math.Cos(angleInRadians)
                                          };
                this.polygon[i] = newPolygonPoint + center;
            }
        }

        public void Reflect(bool isUpright)
        {
            var centerX = 0.0;
            var centerY = 0.0;
            foreach (var point in this.polygon)
            {
                centerX += point.X;
                centerY += point.Y;
            }

            var center = new Point(centerX / this.polygon.Count, centerY / this.polygon.Count);
            if (isUpright)
            {
                for (var i = 0; i < this.polygon.Count; i++)
                {
                    this.polygon[i] = new Point((2 * center.X) - this.polygon[i].X, this.polygon[i].Y);
                }
            }
            else
            {
                for (var i = 0; i < this.polygon.Count; i++)
                {
                    this.polygon[i] = new Point(this.polygon[i].X, (2 * center.Y) - this.polygon[i].Y);
                }
            }
        }

        private static Point[] FindExtremePoint(List<Point> polygon)
        {
            var minx = double.MaxValue;
            var miny = double.MaxValue;
            var maxy = double.MinValue;
            var maxx = double.MinValue;

            foreach (var point in polygon)
            {
                minx = Math.Min(minx, point.X);
                maxx = Math.Max(maxx, point.X);
                miny = Math.Min(miny, point.Y);
                maxy = Math.Max(maxy, point.Y);
            }

            return new[] { new Point { X = minx, Y = maxy }, new Point { X = maxx, Y = miny } };
        }
    }
}
