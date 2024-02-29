var my_Dict = new StringsDictionary();

var allData_path = "../../../../ConsoleApp4/dictionary.txt";


my_Dict.Add( new KeyValuePair("k1", "v1"));
my_Dict.Add( new KeyValuePair("k2", "v2"));
my_Dict.Add( new KeyValuePair("k3", "v3"));
my_Dict.Add( new KeyValuePair("k4", "v4"));
my_Dict.Print();
var value = my_Dict.Get("k4");
my_Dict.Remove("k1");
Console.WriteLine($"\n{value}\n========================================");
my_Dict.Print();
my_Dict.LoadFactor();
Console.WriteLine(my_Dict.LoadFactor());


using (StreamReader reader = new StreamReader(allData_path))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        var split = line.Split(";");
        var split_key = split[0];
        var line_value = string.Join(";", split, 1,  split.Length-1);
        my_Dict.Add(new KeyValuePair(split_key,line_value));
    }
}
Console.WriteLine("Please, enter a word>>");
var input = Console.ReadLine();
while (input != "End")
{
    var answer = my_Dict.Get(input);
    if (answer == null)
    {
        Console.WriteLine("Sorry, there is not such a word in our system(");
    }
    else
    {
        Console.WriteLine(answer);
    }
    Console.WriteLine("Please, enter next word>>");
    input = Console.ReadLine();
}

Console.WriteLine("Thank you for using our search system !");
public class KeyValuePair
{
    public string Key { get; } 
    public string Value { get; }
    public KeyValuePair(string key, string value)
    {
        Key = key;
        Value = value;
    }
}




public class Node {
    public KeyValuePair Data;
    public Node Next;

    public Node (KeyValuePair data, Node next)
    {
        Data = data;
        Next = next;

    }

}

class LinkedList {
    private Node first = null;

    public void Add( KeyValuePair data)
    
    {
        if (first == null)
        {
            first = new Node(data, null);
            
        }
        else
        { 
            Node nextNode = new Node(data, null); 
            Node curElement = first;

            while (curElement.Next != null)
            {
                curElement = curElement.Next;
            }

            curElement.Next = nextNode;
        }
    }


    public void Remove(string key)
    {
        Node curNode = first;
        Node previousNode = null;
        while (curNode!= null && curNode.Data.Key != key)
        {
            previousNode = curNode;
            curNode = curNode.Next;
        }

        if (curNode != null) 
        {
            if (previousNode == null)
            {
                first = curNode.Next;
            }
            else
            {
                previousNode.Next = curNode.Next;
            }
        }
        
    }

    public int Count()
    {
        var counter = 0;
        var curNode = first;
        while (curNode!=null)
        {
            counter += 1;
            curNode = curNode.Next;
        }

        return counter;
    }
    public Node Get(string key)
    { 
        Node curNode = first;
        while (curNode!= null && curNode.Data.Key != key)
        {
            curNode = curNode.Next;
        }
        return curNode;
    }

   

    public void Print()
    {
        Node curElement = first;
        
        while (curElement != null)
        {
            Console.Write($"|{curElement.Data.Key}:{curElement.Data.Value}| ->");
            curElement = curElement.Next;
        }
        Console.Write("Null");
    }
}




public class StringsDictionary
{
    private const int InitialSize = 10;
    private LinkedList[] _buckets = new LinkedList[InitialSize];

    public StringsDictionary()
    {
        for (var i = 0; i < InitialSize; ++i)
        {
            _buckets[i] = new LinkedList();
        }
    }
    public void Add(KeyValuePair pair)
    
    {   
        var hash = CalculateHash(pair.Key);
        _buckets[hash].Add(pair);
        
    }

    public void Remove(string key)
    {
        var hash = CalculateHash(key);
        _buckets[hash].Remove(key);
    }

    public string Get(string key)
    {
        var hash = CalculateHash(key);
        var value = _buckets[hash].Get(key);
        if (value != null)
        {
            return value.Data.Value;
        }
        else
        {
            return null;
        }

        
    }

    public double LoadFactor()
    {
        double buckets_count = InitialSize;
        double non_empty_buckets = 0;
        foreach (var bucket in _buckets)
        {
            if (bucket.Count() > 0)
            {
                non_empty_buckets += 1;
            }
            
        }

        double load_factor = non_empty_buckets / buckets_count;
        return load_factor;
    }

    private int CalculateHash(string key)
    {
        var hash = 0;   
        foreach (var c in key)
        {
            hash += c;
        }

        return hash % InitialSize;
    }


    public void Print()
    {
        foreach (var b in _buckets)
        {
            b.Print();
            Console.WriteLine("\n========================================");
        }
    }
}