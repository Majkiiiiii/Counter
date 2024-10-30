using Microsoft.Maui.Controls.Compatibility;
using System.Diagnostics;

namespace Counter;

public partial class Counter : ContentView
{
    public string Name { get; set; }
    private int counter;
    private int baseValue;
    private Counters parentCounters;

    public int CounterValue
    {
        get => counter;
        set
        {
            counter = value;
            CounterValueLabel.Text = counter.ToString();
        }
    }

    public int BaseValue
    {
        get => baseValue;
        set
        {
            baseValue = value;
        }
    }

    public string Color { get; set; }

    public Counter(string name, int baseValue, string color, Counters parentCounters)
    {
        InitializeComponent();
        Name = name;
        CounterNameLabel.Text = Name;
        this.baseValue = baseValue;
        CounterValue = baseValue;
        this.Color = color;
        this.parentCounters = parentCounters;
        CounterValueLabel.TextColor = Microsoft.Maui.Graphics.Color.FromArgb(color);
    }

    public void OnIncreaseCounter(object sender, EventArgs e)
    {
        CounterValue++;
        parentCounters.SaveCounters();
    }

    public void OnDecreaseCounter(object sender, EventArgs e)
    {
        CounterValue--;
        parentCounters.SaveCounters();
    }

    public void OnResetCounter(object sender, EventArgs e)
    {
        CounterValue = baseValue;
        parentCounters.SaveCounters();
    }

    public void OnDeleteCounter(object sender, EventArgs e)
    {
        parentCounters.RemoveCounter(this);
        Debug.WriteLine("Counter deleted and SaveCounters called");
    }
}
