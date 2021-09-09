var listOfDates = new[] {
    (45, 30), (30, 0), (30, 7), (24, 8)
};

int TwiceAsOld(int fatherAge, int sonAge)
{
    if (sonAge == 0) return fatherAge;
    return Math.Abs(fatherAge - (sonAge * 2));
}

foreach (var pair in listOfDates)
    Console.WriteLine(TwiceAsOld(pair.Item1, pair.Item2));