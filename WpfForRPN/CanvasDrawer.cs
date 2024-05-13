using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
            DrawLine(_canvas, xAxisStart, xAxisEnd);
            DrawLine(_canvas, yAxisStart, yAxisEnd);
        }
    }
}
