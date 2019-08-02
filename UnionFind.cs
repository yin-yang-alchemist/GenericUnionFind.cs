using System;
using System.Collections.Generic;
using System.Linq;

namespace UnionFind
{
    public class Tree<T>
    {
        private Dictionary<T, Node<T>> existingNodes;

        public Tree()
        {
            existingNodes = new Dictionary<T, Node<T>>();
        }

        private Node<T> GetNode(T value)
        {
            if (existingNodes.ContainsKey(value))
            {
                return existingNodes[value];
            }
            else
            {
                var node = new Node<T>(value);
                existingNodes[value] = node;
                return node;
            }
        }

        public void Add(T value)
        {
            GetNode(value);
        }

        public void Unite(T value1, T value2)
        {
            Node<T> root1 = FindRoot(GetNode(value1));
            Node<T> root2 = FindRoot(GetNode(value2));

            if (root1.Rank < root2.Rank)
            {
                root1.Parent = root2;
                root2.Rank = Math.Max(root1.Rank + 1, root2.Rank);
            }
            else
            {
                root2.Parent = root1;
                root1.Rank = Math.Max(root2.Rank + 1, root1.Rank);
            }
        }

        private Node<T> FindRoot(Node<T> node)
        {
            if (node.Parent == node)
            {
                return node;
            }
            else
            {
                Node<T> root = FindRoot(node.Parent);
                node.Parent = root;
                return root;
            }
        }

        public IEnumerable<T[]> GetGroups()
        {
            var allNodes = existingNodes.Values;
            var valueGroups = allNodes.GroupBy(node => FindRoot(node).Value, node => node.Value);
            return valueGroups.Select(group => group.ToArray());
        }
    }

    internal class Node<T>
    {
        public int Rank;
        public Node<T> Parent;
        public readonly T Value;

        public Node(T value)
        {
            this.Rank = 0;
            this.Parent = this;
            this.Value = value;
        }
    }
}
