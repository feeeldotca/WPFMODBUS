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
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Meter), new PropertyMetadata(0.0));

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
            throw new NotImplementedException();
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

            for(int i =0; i< maxValue-minValue +24; i++)
            {
                if (i % 12 != 0) continue;
                double angle = step * i - 45;
                Line lineScale = new Line();
                lineScale.Stroke = Brushes.White;
                lineScale.StrokeThickness = 1;
                lineScale.Opacity = 0.3;

                lineScale.X1 = radius - (radius - 15) * Math.Cos(angle * Math.PI / 180);
                lineScale.Y1 = radius - (radius - 15) * Math.Sin(angle * Math.PI / 180);
                lineScale.X2 = radius - (radius - 15) * Math.Cos(angle * Math.PI / 180);
                lineScale.Y2= radius - (radius - 15) * Math.Sin(angle * Math.PI / 180);

            }

        }

    }
}
