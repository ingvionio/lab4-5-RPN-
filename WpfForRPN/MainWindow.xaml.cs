using RPN.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPN.Logic;

namespace WpfForRPN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Graph_MouseMove(object sender, MouseEventArgs e)
        {
            Point uiPoint = Mouse.GetPosition(Graph);
            float zoom = float.Parse(tbZoom.Text);
            var mathPoint = Mouse.GetPosition(Graph).ToMathCoordinates(Graph, zoom);
            lblUiCord.Content = $"{uiPoint.X:0.#};{uiPoint.Y:0.#}";
            lblMathCord.Content = $"{mathPoint.X:0.#};{mathPoint.Y:0.#}";
        }

        private void Graph_Loaded(object sender, RoutedEventArgs e)
        {
            //RedrawCanvas();
        }

        private void RedrawCanvas()
        {
            float xStart = float.Parse(tbStart.Text);
            float xEnd = float.Parse(tbEnd.Text);
            float step = float.Parse(tbStep.Text);
            float zoom = float.Parse(tbZoom.Text);

            var canc= new CanvasDrawer(Graph, xStart, xEnd, step, zoom);
            canc.DrawAxis();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            lblOutput.Content = RpnCalculator.PerformСalculation(tbInput.Text, Convert.ToDouble(tbvariableValue.Text));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbInput.Text = tbInput.Text.Trim();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void renew_Click(object sender, RoutedEventArgs e)
        {
            RedrawCanvas();
        }
    }
}
