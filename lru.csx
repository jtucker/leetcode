/*

Least recently used cache
- Organizes items in order of use, allwing quick access to an item that hasn't been accessed in the longest time.

space: O(n) 
get item: O(1)
access: O(1)

*/


public class LRUCache
{

    private readonly LinkedList<(int, int)> list;
    private readonly Dictionary<int, LinkedListNode<(int, int)>> hashMap;
    private readonly int capacity;

    public LRUCache(int capacity)
    {
        list = new LinkedList<(int, int)>();
        hashMap = new Dictionary<int, LinkedListNode<(int, int)>>();
        this.capacity = capacity;
    }

    private LinkedListNode<(int, int)> MarkRecentlyUsed(LinkedListNode<(int, int)> node)
    {
        list.Remove(node);
        var newNode = list.AddFirst((node.Value.Item1, node.Value.Item2));
        hashMap[node.Value.Item1] = newNode;
        return newNode;
    }

    public int Get(int key) =>
        hashMap.ContainsKey(key) ? MarkRecentlyUsed(hashMap[key]).Value.Item2 : -1;

    public void Put(int key, int value)
    {
        if (hashMap.ContainsKey(key))
        {
            hashMap[key].Value = (key, value);
            var _ = MarkRecentlyUsed(hashMap[key]);
            return;
        }

        if (hashMap.Count == capacity)
        {
            var lastNode = list.Last;
            var evictedKey = lastNode.Value.Item1;
            list.Remove(lastNode);
            hashMap.Remove(evictedKey);
        }

        var newNode = list.AddFirst((key, value));
        hashMap.Add(key, newNode);
    }
}

public class LRUCache2
{
    private readonly LinkedList<KeyValuePair<int, int>> list;
    private readonly Dictionary<int, LinkedListNode<KeyValuePair<int, int>>> hashMap;
    private readonly int capacity;

    public LRUCache2(int capacity)
    {
        list = new LinkedList<KeyValuePair<int, int>>();
        hashMap = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>();
        this.capacity = capacity;
    }

    private LinkedListNode<KeyValuePair<int, int>> MarkRecentlyUsed(LinkedListNode<KeyValuePair<int, int>> node)
    {
        list.Remove(node);
        list.AddFirst(node);
        return node;
    }

    public int Get(int key) =>
        !hashMap.TryGetValue(key, out var node) ? -1 : MarkRecentlyUsed(node).Value.Value;

    public void Put(int key, int value)
    {
        if (hashMap.TryGetValue(key, out var node))
        {
            node.Value = new KeyValuePair<int, int>(key, value);
            var _ = MarkRecentlyUsed(hashMap[key]);
        }
        else
        {
            var newNode = list.AddFirst(new KeyValuePair<int, int>(key, value));
            hashMap.Add(key, newNode);

            if (hashMap.Count > capacity)
            {
                var lastNode = list.Last;
                var evictedKey = lastNode.Value.Key;
                list.Remove(lastNode);
                hashMap.Remove(evictedKey);
            }
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        foreach (var node in list)
        {
            builder.Append(node.Key).Append('=').Append(node.Value).Append(list.Count > 1 ? "," : "");
        }
        return builder.ToString();
    }
}

var cache = new LRUCache(2);
cache.Put(2, 1);
cache.Put(1, 1);
Console.WriteLine($"Cache currently: {cache}");
cache.Get(2);
Console.WriteLine($"Cache currently: {cache}");
cache.Put(4, 1);
Console.WriteLine($"Cache currently: {cache}");
Console.WriteLine($"cache.Get(1): {cache.Get(1)}");
Console.WriteLine($"cache.Get(2): {cache.Get(2)}");
