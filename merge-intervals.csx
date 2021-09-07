public int[][] Merge(int[][] intervals)
{
    var ordered = intervals.OrderBy(i => i[0]).Select((itm, idx) => (idx, itm));
    var maxValue = int.MinValue;
    var startValue = int.MinValue;
    var seed = new int[][] { };

    var aggregated = ordered.Aggregate(seed, (acc, range) =>
    {
        var item = range.itm;
        var idx = range.idx;
        if (item[0] > maxValue)
        {
            if (idx > 0)
            {
                Array.Resize(ref acc, acc.Length + 1);
                acc[^1] = new[] { startValue, maxValue };
            }
            maxValue = item[1]; // 3 
            startValue = item[0]; // 1
        }
        else if (item[1] >= maxValue)
        {
            maxValue = item[1];
        }
        return acc;
    });

    Array.Resize(ref aggregated, aggregated.Length + 1);
    aggregated[^1] = new int[] { startValue, maxValue };
    return aggregated;
}

var intervals = new int[][] {
    new int []{1, 3},
    new int []{2, 6},
    new int []{8, 10},
    new int []{15, 18},
    //new int[] {1,4},
    //new int[] {4,5},
    // new int[] {1,4},
    // new int[] {0,5},
};

foreach(var interval in Merge(intervals))
{
    Console.WriteLine($"Interval: {interval[0]}:{interval[1]}");
}