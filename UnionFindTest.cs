using System;
using UnionFind;

public class Program
{
    static void Main(string[] args)
    {
        string[] names = {"John", "George", "Paul", "Mick", "Ringo", "Bill", "Brian", "Charlie", "Keith", "Thomas"};
        (string, string)[] pairs = {
            ("John", "George"),
            ("Paul", "Ringo"),
            ("Ringo", "George"),
            ("Charlie", "Bill"),
            ("Keith", "Mick"),
            ("Brian", "Bill"),
            ("Charlie", "Keith")
        };

        var tree = new Tree<string>();
        foreach (string name in names)
        {
            tree.Add(name);
        }
        foreach (var (name1, name2) in pairs)
        {
            tree.Unite(name1, name2);
        }

        foreach (string[] group in tree.GetGroups())
        {
            Console.WriteLine(String.Join(", ", group));
        }
    }
}
