using Microsoft.VisualBasic;
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

namespace feeeldotca.ModbusRTU.Components
{
    /// <summary>
    /// Interaction logic for Meter.xaml
    /// </summary>
    public partial class Meter : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Meter), new PropertyMetadata(0.0, new PropertyChangedCallback(OnValueChanged)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Meter).RefreshValue();
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public Meter()
        {
            InitializeComponent();
            this.SizeChanged += Meter_SizeChanged;
        }

        private void Meter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.RefreshScale();
        }

        double radius = 0.0, step = 0.0;
        private void RefreshScale()
        {
            this.root.Width = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.root.Height = this.root.Width;

            radius = this.root.Height / 2;
            if (double.IsNaN(radius) || radius <= 0) return;

            scale_canvas.Children.Clear();

            double minValue = -40, maxValue = 80;
            step = 270.0 / (maxValue - minValue + 24);
            int interval = 10;
            for(int i =0; i< maxValue-minValue +24; i++)
            {
                if (i % 12 != 0) continue;
                double angle = step * i - 45;
                Line lineScale = new()
                {
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    Opacity = 0.3,

                    X1 = radius - (radius - 15) * Math.Cos(angle * Math.PI / 180),
                    Y1 = radius - (radius - 15) * Math.Sin(angle * Math.PI / 180),
                    X2 = radius - (radius - 15) * Math.Cos(angle * Math.PI / 180),
                    Y2 = radius - (radius - 15) * Math.Sin(angle * Math.PI / 180)
                };

                scale_canvas.Children.Add(lineScale);
                TextBlock txtScale = new TextBlock();
                txtScale.Text = (minValue+interval * i /12).ToString();
                txtScale.Width = 34;
                txtScale.FontSize = 9;
                txtScale.Foreground = Brushes.White;
                txtScale.Opacity = 0.3;
                txtScale.TextAlignment = TextAlignment.Center;

                Canvas.SetLeft(txtScale, radius - (radius - 2) * Math.Cos(angle * Math.PI / 180) - 19);
                Canvas.SetTop(txtScale, radius - (radius - 2) * Math.Sin(angle * Math.PI / 180) - 5);
                scale_canvas.Children.Add(txtScale);
            }
            string path_data_str = $"M{radius * 0.3} {radius}" + $"A{radius * 0.7} {radius * 0.7} 0 1 1 {radius} {radius * 1.7}";
            this.path.Data = Geometry.Parse(path_data_str);
        }

        private void RefreshValue()
        {
            
            double newAngle = (this.Value - -40) * 270 / 120;
            double x = radius - (radius - 21) * Math.Cos(newAngle * Math.PI / 180);
            double y = radius - (radius - 21) * Math.Sin(newAngle * Math.PI / 180);

            int flag = newAngle <= 180 ? 0 : 1;
            string path_data_str = $"M{radius * 0.3} {radius}" + $"A{radius * 0.7} {radius * 0.7} 0 {flag} 1 {x} {y}";
            this.path_value.Data = Geometry.Parse(path_data_str);
        }
    }
}
