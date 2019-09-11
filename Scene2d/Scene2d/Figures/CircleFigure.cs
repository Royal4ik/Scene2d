namespace Scene2d.Figures
{
    using System;

    public class CircleFigure : IFigure
    {
        private Circle circle;

        public CircleFigure(Point center, double radius)
        {
            this.circle.Center = center;
            this.circle.Radius = radius;
        }

        public object Clone()
        {    
            return new CircleFigure(this.circle.Center, this.circle.Radius);
        }

        public double CalulateArea()
        {
            return Math.PI * this.circle.Radius * this.circle.Radius;
        }

        public Rectangle CalculateCircumscribingRectangle()
        {
            var cirscumscribing = new Rectangle();
            var p1 = new Point(this.circle.Center.X - this.circle.Radius, this.circle.Center.Y + this.circle.Radius);
            var p2 = new Point(this.circle.Center.X + this.circle.Radius, this.circle.Center.Y - this.circle.Radius);
            cirscumscribing.Vertex1 = p1;
            cirscumscribing.Vertex2 = p2;
            return cirscumscribing;
        }

        public void Move(Point vector)
        {
            this.circle.Center = this.circle.Center + vector;
        }

        public void Rotate(double angle)
        {
        }

        public void Reflect(bool isUpright)
        {
        }
    }
}
