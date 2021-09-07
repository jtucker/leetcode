var paths = new string[] {
    "/home/", "/../", "/home//foo/", "/a/./b/../../c/"
};

string SecondAttempt(string path)
{
    Console.WriteLine("Using the second attempt");
    var cleanPaths = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
    var pathStack = new Stack<string>();
    foreach (var acc in cleanPaths)
    {
        switch (acc)
        {
            case ".":
                break;
            case "..":
                if (pathStack.Count > 0) pathStack.Pop();
                break;
            default:
                pathStack.Push(acc);
                break;
        }
    }
    return $"/{string.Join("/", pathStack.Reverse().ToArray())}";
}

Func<string, string> functionToExecute =
    Args.Contains("first") ? FirstAttempt : SecondAttempt;

foreach (var path in paths)
{
    Console.WriteLine(functionToExecute(path));
}

string FirstAttempt(string path)
{
    Console.WriteLine("Using the first attempt");
    var cleanPaths = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
        .Aggregate(new string[0], (acc, current) =>
        {
            string[] tempArry;
            switch (current)
            {
                case ".":
                    break;
                case "..":
                    if (acc.Length == 0) return acc;
                    tempArry = new string[acc.Length - 1];
                    for (int i = 0; i < acc.Length - 1; i++)
                        tempArry[i] = acc[i];
                    return tempArry;
                default:
                    tempArry = new string[acc.Length + 1];
                    acc.CopyTo(tempArry, 0);
                    tempArry[^1] = current;
                    return tempArry;
            }
            return acc;
        });

    return $"/{string.Join("/", cleanPaths)}";
}