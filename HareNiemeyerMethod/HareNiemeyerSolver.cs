namespace HareNiemeyerMethod;

public class HareNiemeyerSolver
{
    private readonly int _quota;
    private readonly int _precisionDivider;
    
    private class IncrementableValue
    {
        public long Value { get; set; }

        public IncrementableValue(long value)
        {
            Value = value;
        }

        public void Increment()
        {
            Value += 1;
        }
    }

    public HareNiemeyerSolver(int quota, int precision)
    {
        _quota = quota;
        _precisionDivider = 1;
        while (precision > 0)
        {
            _precisionDivider *= 10;
            precision--;
        }
    }

    public IEnumerable<(T Item, decimal Value)> Calculate<T>(IEnumerable<T> collection, Func<T, decimal> valueSelector)
    {
        var result = new List<(T, decimal)>();

        var total = collection.Select(valueSelector).Sum();
        var data = collection.Select(x => new
        {
            Item = x,
            PreciseValue = valueSelector(x) / total
        }).Select(x => new
        {
            x.Item,
            x.PreciseValue,
            BaseValue = (int)decimal.Floor(x.PreciseValue * _quota * _precisionDivider),
        }).Select(x => new
        {
            x.Item,
            x.BaseValue,
            x.PreciseValue,
            IncrementableValue = new IncrementableValue(x.BaseValue),
            Reminder = x.PreciseValue * _quota * _precisionDivider - x.BaseValue
        }).ToList();

        var orderedData = data
            .OrderByDescending(x => x.Reminder)
            .ToList();
        
        var missingValues = _quota * _precisionDivider - data.Sum(x => x.BaseValue);
        
        for (var i = missingValues; i > 0; i--)
        {
            orderedData[missingValues-i].IncrementableValue.Increment();
        }

        return data.Select(x => (x.Item, x.IncrementableValue.Value / (decimal)_precisionDivider));
    }
}