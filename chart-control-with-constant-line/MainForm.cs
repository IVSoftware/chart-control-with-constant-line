using DevExpress.XtraCharts.Native;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chart_control_with_constant_line
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm() =>InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Specify data members to bind the chart's series template.
            Series series = new Series("series", ViewType.Bar);
            for (int i = 0; i < 25; i++)
            {
                series.Points.AddPoint(
                    argument: i, 
                    value: 1 + (0.25 - _rando.NextDouble()));
            }
            chartControl1.Series.Add(series);

            // Specify the template's series view.
            chartControl1.SeriesTemplate.View = new StackedBarSeriesView();
            ((BarSeriesView)series.View).ColorEach = true;

            // register chart summary function
            // series.QualitativeSummaryOptions.SummaryFunction = "AVERAGE([WorkingDay])";
            // ConstantLine line = new ConstantLine("Average Workingday:", series.QualitativeSummaryOptions.SummaryFunction);

            ConstantLine line = new ConstantLine
            {
                Name= "Average Working Day",
                AxisValue = series.Points.Average(_ => _.NumericalValue),
                Color = Color.Red,
            };

            XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            diagram.AxisY.ConstantLines.Add(line);
        }
        Random _rando = new Random();
    }
}
