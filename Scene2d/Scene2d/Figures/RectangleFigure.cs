namespace Scene2d.Figures
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class RectangleFigure : IFigure
    {
        private Rectangle rectangle;

        public RectangleFigure(Point p1, Point p2)
        {
            this.rectangle.Vertex1 = p1;
            this.rectangle.Vertex2 = p2;
        }

        public object Clone()
        {
            return new RectangleFigure(this.rectangle.Vertex1, this.rectangle.Vertex2);
        }

        public double CalulateArea()
        {
            return Math.Abs(this.rectangle.Vertex1.X - this.rectangle.Vertex2.X)
                    * Math.Abs(this.rectangle.Vertex1.Y - this.rectangle.Vertex2.Y);
        }

        public Rectangle CalculateCircumscribingRectangle()
        {
            var circumscribingRectangle = new Rectangle
                                              {
                                                  Vertex1 =
                                                      new Point(
                                                      Math.Min(
                                                          this.rectangle.Vertex1.X,
                                                          this.rectangle.Vertex2.X),
                                                      Math.Max(this.rectangle.Vertex1.Y, this.rectangle.Vertex2.Y)),
                                                  Vertex2 =
                                                      new Point(
                                                      Math.Max(
                                                          this.rectangle.Vertex1.X,
                                                          this.rectangle.Vertex2.X),
                                                      Math.Min(this.rectangle.Vertex1.Y, this.rectangle.Vertex2.Y))
                                              };
            return circumscribingRectangle;
        }

        public void Move(Point vector)
        {
            this.rectangle.Vertex1 = this.rectangle.Vertex1 + vector;
            this.rectangle.Vertex2 = this.rectangle.Vertex2 + vector;
        }

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1407:ArithmeticExpressionsMustDeclarePrecedence", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public void Rotate(double angle)
        {
            const double Eps = 0.00001;
            if (Math.Abs(angle % 90) < Eps)
            {
                var center = new Point
                 {
                     X =
                         Math.Min(this.rectangle.Vertex1.X, this.rectangle.Vertex2.X)
                         + (Math.Abs(this.rectangle.Vertex1.X - this.rectangle.Vertex2.X) / 2),
                     Y =
                         Math.Min(this.rectangle.Vertex1.Y, this.rectangle.Vertex2.Y)
                         + (Math.Abs(this.rectangle.Vertex1.Y - this.rectangle.Vertex2.Y) / 2)
                 };
                angle = angle / 180 * Math.PI;
                var x1Rotate = center.X + (this.rectangle.Vertex1.X - center.X) * Math.Cos(angle) 
                                - (this.rectangle.Vertex1.Y - center.Y) * Math.Sin(angle);
                var y1Rotate = center.Y + (this.rectangle.Vertex1.X - center.X) * Math.Sin(angle)
                                + (this.rectangle.Vertex1.Y - center.Y) * Math.Cos(angle);

                var x2Rotate = center.X + (this.rectangle.Vertex2.X - center.X) * Math.Cos(angle)
                                - (this.rectangle.Vertex2.Y - center.Y) * Math.Sin(angle);
                var y2Rotate = center.Y + (this.rectangle.Vertex2.X - center.X) * Math.Sin(angle)
                                + (this.rectangle.Vertex2.Y - center.Y) * Math.Cos(angle);
                var p1Rotate = new Point { X = x1Rotate, Y = y1Rotate };
                var p2Rotate = new Point { X = x2Rotate, Y = y2Rotate };
                this.rectangle.Vertex1 = p1Rotate;
                this.rectangle.Vertex2 = p2Rotate;
            }
            else
            {
                throw new Exception("Количество градусов должно быть кратно 90");
            }
        }

        public void Reflect(bool isUpright)
        {
        }
    }
}
