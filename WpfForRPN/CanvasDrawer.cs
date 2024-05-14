﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using RPN.Logic;
using System.Windows.Documents;

namespace WpfForRPN
{
    static class PointExtensions
    {
        public static Point ToMathCoordinates(this Point point, Canvas canvas, float zoom)
        {
            return new Point((point.X - canvas.ActualWidth / 2)/zoom,(canvas.ActualHeight/2 - point.Y)/zoom);
        }

        public static Point ToUiCoordinates(this Point point, Canvas canvas, float zoom)
        {
            return new Point((point.X*zoom + canvas.ActualWidth / 2), (canvas.ActualHeight / 2 - point.Y * zoom));
        }
    }
    
    class CanvasDrawer
    {
        private Canvas _canvas;
        private double _axisThickness = 1;
        private Brush _deafaultStroke = Brushes.Black;
        private int _scaleLenght = 5;

        Point xAxisStart, xAxisEnd, yAxisStart, yAxisEnd;
        private readonly float _xStart;
        private readonly float _xEnd;
        private readonly float _step;
        private readonly float _zoom;

        public CanvasDrawer(Canvas canvas, float xStart, float xEnd, float step, float zoom)
        {
            _canvas = canvas;
            xAxisStart = new Point((int)_canvas.ActualWidth/2, 0);
            xAxisEnd = new Point((int) _canvas.ActualWidth/2, (int)_canvas.ActualHeight);

            yAxisStart = new Point(0, (int)_canvas.ActualHeight / 2);
            yAxisEnd = new Point((int)_canvas.ActualWidth, (int)_canvas.ActualHeight/2);

            _xStart = xStart;
            _xEnd = xEnd;
            _step = step;
            _zoom = zoom;
        }

        public List<Point> CalculatePoints(string expression)
        {
            var points = new List<Point>();
            for (double x = _xStart; x <= _xEnd; x = x+_step)
            {
                double y = RpnCalculator.PerformСalculation(expression, x);
                var point = new Point(x, y);
                points.Add(PointExtensions.ToUiCoordinates(point, _canvas, _zoom));
            }

            return points;
        }

        public static void DrawLines(Canvas canvas, List<Point> points)
        {
            for (int i = 0; i < points.Count-1; i++)
            {
                DrawLine(canvas, points[i], points[i + 1], Brushes.Green);
            }
        }

        public static void DrawLine(Canvas canvas, Point startPoint, Point endPoint, Brush color)
        {
            Line line = new Line
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Stroke = color,
                StrokeThickness = 1
            };
            canvas.Children.Add(line);
        }
        public static void DrawLine(Canvas canvas, Point startPoint, Point endPoint)
        {
            Line line = new Line
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            canvas.Children.Add(line);
        }

        public void DrawAxis()
        {
            DrawLineWithArrow(_canvas, xAxisEnd, xAxisStart);
            DrawLineWithArrow(_canvas, yAxisStart, yAxisEnd);
        }

        private void DrawLineWithArrow(Canvas canvas, Point startPoint, Point endPoint)
        {
            DrawLine(canvas, startPoint, endPoint);
            double arrowLength = 10;
            double arrowAngle = Math.PI / 6; // 30 degrees

            // Calculate the angle of the line
            double angle = Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);

            // Calculate the arrow points
            Point arrowPoint1 = new Point(
                endPoint.X - arrowLength * Math.Cos(angle - arrowAngle),
                endPoint.Y - arrowLength * Math.Sin(angle - arrowAngle)
            );

            Point arrowPoint2 = new Point(
                endPoint.X - arrowLength * Math.Cos(angle + arrowAngle),
                endPoint.Y - arrowLength * Math.Sin(angle + arrowAngle)
            );

            DrawLine(canvas, endPoint, arrowPoint1);
            DrawLine(canvas, endPoint, arrowPoint2);
        }
    }
}
