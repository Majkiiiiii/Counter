using System.Text.Json;

namespace Counter;

public partial class Counters : ContentPage
{
    private int counterCount = 0;
    private readonly string FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "counters.json");

    public Counters()
    {
        InitializeComponent();
        LoadCounters();
    }

    public void OnAddCounter(object sender, EventArgs e)
    {
        string name = CounterNameEntry.Text;
        int baseValue = int.TryParse(BaseCounterValueEntry.Text, out int value) ? value : 0;
        string color = CounterColorEntry.Text;

        counterCount++;
        var counter = new Counter(name, baseValue, color, this);
        CountersLayout.Children.Add(counter);
        SaveCounters();
    }

    public void SaveCounters()
    {
        var counters = CountersLayout.Children.OfType<Counter>().Select(c => new CounterData
        {
            Name = c.Name,
            BaseValue = c.BaseValue,
            CurrentValue = c.CounterValue,
            Color = c.Color
        }).ToList();

        var json = JsonSerializer.Serialize(counters);
        File.WriteAllText(FileName, json);
    }

    private void LoadCounters()
    {
        if (File.Exists(FileName))
        {
            var json = File.ReadAllText(FileName);
            var counters = JsonSerializer.Deserialize<List<CounterData>>(json);

            if (counters != null)
            {
                foreach (var counterData in counters)
                {
                    var counter = new Counter(counterData.Name, counterData.BaseValue, counterData.Color, this)
                    {
                        CounterValue = counterData.CurrentValue
                    };
                    CountersLayout.Children.Add(counter);
                }
            }
        }
    }
    public void RemoveCounter(Counter counter)
    {
        CountersLayout.Children.Remove(counter);
        SaveCounters();
    }
}
