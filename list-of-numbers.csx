var listOfNumbers = new[] {
    "2", "0089", "-0.1", "+3.14", "4.", "-.9", "2e10", "-90E3", "3e+7", "+6e-1", "53.5e93", "-123.456e789",
    "abc", "1a", "1e", "e3", "99e2.5", "--6", "-+3", "95a54e53", "0", "e", ".", ".1", "44e016912630333"
};

foreach (var s in listOfNumbers)
{
    var upperString = s.ToUpper();
    var hasEuler = upperString.Split('E').Select((i, idx) => (i, idx)).ToArray();
    if (upperString.Contains('E') && hasEuler.Length != 2)
    {
        continue;
    }

    var result = hasEuler
        .Aggregate(true, (acc, itm) =>
            acc && (itm.idx == 0
                    ? (double.TryParse(itm.i, out _) || int.TryParse(itm.i, out _))
                    : (int.TryParse(itm.i, out _) || long.TryParse(itm.i, out _))));

    Console.WriteLine($"{s}: {result}");
}