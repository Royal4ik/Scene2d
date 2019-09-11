namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Scene2d.Commands;
    using Scene2d.Figures;

    public class AddPolygonCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegexStart = new Regex(@"add polygon\s+([\w\d-_]+)\s*(#|$)");

        private static readonly Regex RecognizeRegexAdd = new Regex(@"add point\s+\((-?\d+)\,\s+(-?\d+)\)\s*(#|$)");

        private static readonly Regex RecognizeRegexEnd = new Regex(@"end polygon\s*(#|$)");

        private readonly List<Point> polygonPoints = new List<Point>();

        private PolygonFigure polygon;

        private string name;

        public bool IsCommandReady { get; set; }

        public void AppendLine(string line)
        {
            if (RecognizeRegexStart.IsMatch(line))
            {
                var match = RecognizeRegexStart.Match(line);
                this.name = match.Groups[1].ToString();
            }
            else if (RecognizeRegexEnd.IsMatch(line))
            {
                if (this.polygonPoints.Count >= 3)
                {
                    this.polygon = new PolygonFigure(this.polygonPoints);
                    this.IsCommandReady = true;
                }
                else
                {
                    throw new Exception("Количество точек полигона должно быть больше 2");
                }
            }
            else if (RecognizeRegexAdd.IsMatch(line))
            {
                var match = RecognizeRegexAdd.Match(line);
                var newPolygonPoint = new Point(
                    Convert.ToDouble(match.Groups[1].Value),
                    Convert.ToDouble(match.Groups[2].Value));
                var isCoincided = this.polygonPoints.Contains(newPolygonPoint);
                this.polygonPoints.Add(newPolygonPoint);
                
                if (isCoincided || IsIntersection(this.polygonPoints))
                {
                    throw new Exception("Точка полигона совпадает с одной из предыдущих или образует пересечение с одной из предыдущих сторон");
                }                 
            }
            else
            {
                throw new Exception("Неправильный формат ввода данных");
            }
        }

        public ICommand GetCommand()
        {
            return new AddFigureCommand(this.name, this.polygon);
        }

        private static bool IsIntersection(List<Point> polygonPoints)
        {
            const double Epsilon = 0.000001;
            var polygonPointsTemp = new List<Point>();
            polygonPointsTemp.AddRange(polygonPoints);
            polygonPointsTemp.Add(polygonPointsTemp[0]);
            for (var i = 0; i < polygonPointsTemp.Count - 1; i++)
            {
                var beginFirstSegment = polygonPointsTemp[i];
                var endFirstSegment = polygonPointsTemp[i + 1];
                var x1 = beginFirstSegment.X;
                var y1 = beginFirstSegment.Y;
                var x2 = endFirstSegment.X;
                var y2 = endFirstSegment.Y;
                for (var j = i; j < polygonPointsTemp.Count - 1; j++)
                {
                    var startSecondSegment = polygonPointsTemp[j];
                    var endSecondSegment = polygonPointsTemp[j + 1];
                    if (beginFirstSegment.Equals(endSecondSegment) | beginFirstSegment.Equals(startSecondSegment) |
                    endFirstSegment.Equals(endSecondSegment) | endFirstSegment.Equals(startSecondSegment))
                    {
                        continue;
                    }

                    var x3 = startSecondSegment.X;
                    var y3 = startSecondSegment.Y;
                    var x4 = endSecondSegment.X;
                    var y4 = endSecondSegment.Y;

                    var x = x1;
                    var y = y4;
                    bool isIntersectionStraight;
                    if (Math.Abs(x1 - x2) < Epsilon)
                    {
                        x = x1;
                        if (Math.Abs(x3 - x4) < Epsilon)
                        {
                            isIntersectionStraight = false;
                        }
                        else
                        {
                            isIntersectionStraight = true;

                            if (Math.Abs(y3 - y4) < Epsilon)
                            {
                                y = y3;
                            }
                            else
                            {
                                var k = (y4 - y3) / (x4 - x3);
                                y = (k * x) - (((x3 * y4) - (x4 * y3)) / k);
                            }
                        }
                    }
                    else if (Math.Abs(y1 - y2) < Epsilon)
                    {
                        y = y1;
                        if (Math.Abs(y3 - y4) < Epsilon)
                        {
                            isIntersectionStraight = false;
                        }
                        else
                        {
                            isIntersectionStraight = true;

                            if (Math.Abs(x3 - x4) < Epsilon)
                            {
                                x = x3;
                            }
                            else
                            {
                                var k = (y4 - y3) / (x4 - x3);
                                x = (y + (x3 * y4 - x4 * y3) / k) / k;
                            }
                        }
                    }
                    else
                    {
                        isIntersectionStraight = true;

                        var k = (y2 - y1) / (x2 - x1);
                        var b = -(x1 * y2 - x2 * y1) / (x2 - x1);
                        if (Math.Abs(y3 - y4) < Epsilon)
                        {
                            y = y3;
                            x = (y - b) / k;
                        }
                        else
                        {
                            if (Math.Abs(x3 - x4) < Epsilon)
                            {
                                x = x3;
                                y = k * x + b;
                            }
                            else
                            {
                                var k2 = (y4 - y3) / (x4 - x3);
                                var b2 = -(x3 * y4 - x4 * y3) / (x4 - x3);
                                x = (b2 - b) / (k - k2);
                                y = k * x + b;
                            }
                        }
                    }

                    if (!isIntersectionStraight)
                    {
                        continue;
                    }

                    if (!((x1 <= x) & (x <= x2) | (x1 >= x) & (x >= x2))
                    || !((y1 <= y) & (y <= y2) | (y1 >= y) & (y >= y2)))
                    {
                        continue;
                    }

                    if ((x3 <= x) & (x <= x4) && ((y3 <= y) & (y <= y4) | (y3 >= y) & (y >= y4)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
