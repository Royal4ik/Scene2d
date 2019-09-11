namespace Scene2d
{
    public struct Point
    {
        public Point(double newX, double newY)
        {
            this.X = newX;
            this.Y = newY;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public static Point operator +(Point first, Point second)
        {
            var result = new Point { X = first.X + second.X, Y = first.Y + second.Y };
            return result;
        }

        public static Point operator -(Point first, Point second)
        {
            var result = new Point { X = first.X - second.X, Y = first.Y - second.Y };
            return result;
        }
    }
}
