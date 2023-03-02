Here is one way to 

> add constantline in my chartcontrol showing average of workingday.

This code uses a different approach for instantiating the `ConstantLine`. To keep the example to a minimum, the series is created ad hoc for testing purposes only. The answer should still work with your bound data table.
***

[![screenshot][1]][1]

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

  [1]: https://i.stack.imgur.com/hhaBu.png